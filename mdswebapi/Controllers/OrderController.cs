using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mdswebapi.Models;
using System;
using System.Linq;

namespace mdswebapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly mdsDbContext _context;

        public OrderController(mdsDbContext context)
        {
            _context = context;
        }

        [HttpPost("placeorder/{customerId}")]
        public IActionResult PlaceOrder(string customerId)
        {
            var cart = _context.Carts
                .Include(c => c.CartDetails)
                .ThenInclude(cd => cd.Med)
                .FirstOrDefault(c => c.CustomerId == customerId);

            if (cart == null || !cart.CartDetails.Any())
            {
                return BadRequest("Cart is empty or does not exist.");
            }

            var groupedCartDetails = cart.CartDetails
                .GroupBy(cd => cd.Med.PharId)
                .ToList();

            var orders = new List<Order>();

            foreach (var group in groupedCartDetails)
            {
                var orderItems = new List<OrderItem>();

                foreach (var cartDetail in group)
                {
                    var medicine = cartDetail.Med;
                    if (medicine.MedRemain < cartDetail.Quantity)
                    {
                        return BadRequest($"Not enough stock for medicine {medicine.MedName}. Available: {medicine.MedRemain}, Requested: {cartDetail.Quantity}");
                    }

                    medicine.MedRemain -= cartDetail.Quantity;

                    var orderItem = new OrderItem
                    {
                        MedId = cartDetail.MedId,
                        ItemQuantity = cartDetail.Quantity
                    };
                    orderItems.Add(orderItem);
                }

                var order = new Order
                {
                    CustomerId = customerId,
                    OrderPlacedAt = DateTime.Now,
                    OsId = 1,
                    OrderItems = orderItems
                };

                _context.Orders.Add(order);
                orders.Add(order);
            }

            _context.CartDetails.RemoveRange(cart.CartDetails);
            _context.SaveChanges();

            return Ok(orders);
        }

        [HttpGet("vieworders/{customerId}")]
        public IActionResult ViewOrders(string customerId)
        {
            var orders = _context.Orders
                .Where(o => o.CustomerId == customerId)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Med)
                .Include(o => o.Customer)
                .Include(o => o.Os)
                .ToList();

            if (!orders.Any())
            {
                return NotFound("No orders found for this customer.");
            }

            return Ok(orders);
        }

        [HttpPut("updateshippingstatus/{orderId}/{statusId}")]
        public IActionResult UpdateShippingStatus(int orderId, int statusId)
        {
            var order = _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Med)
                .FirstOrDefault(o => o.OrderId == orderId);

            if (order == null)
            {
                return NotFound("Order not found.");
            }

            order.OsId = statusId;

            if (statusId == 4)
            {
                order.OrderDeliveredAt = DateTime.Now;
            }

            _context.SaveChanges();

            return Ok(order);
        }
        [HttpGet("viewpharmacyorders/{pharmacyId}")]
        public IActionResult ViewPharmacyOrders(string pharmacyId)
        {
            var users = _context.Customers.Find(pharmacyId);
            var pharid = users.PharmacyId;
            var orders = _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Med)
                .ThenInclude(m => m.Phar)
                .Include(o => o.Os)
                .Where(o => o.OrderItems.Any(oi => oi.Med.PharId == pharid))
                .ToList();

            if (!orders.Any())
            {
                return NotFound("No orders found for this pharmacy.");
            }

            var orderList = orders.Select(o => new
            {
                o.OrderId,
                o.Os.OsDesc,
                o.OrderPlacedAt,
                o.OrderDeliveredAt,
                TotalPrice = o.OrderItems.Sum(oi => oi.Med.MedPrice * oi.ItemQuantity),
                Items = o.OrderItems.Select(oi => new
                {
                    oi.Med.MedName,
                    oi.ItemQuantity,
                    oi.Med.MedPrice
                })
            }).ToList();

            return Ok(orderList);
        }
    }
}
