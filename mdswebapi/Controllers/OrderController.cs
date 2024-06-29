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
                .FirstOrDefault(c => c.CustomerId == customerId);

            if (cart == null || !cart.CartDetails.Any())
            {
                return BadRequest("Cart is empty or does not exist.");
            }

            var order = new Order
            {
                CustomerId = customerId,
                OrderPlacedAt = DateTime.Now,
                OsId = 1
            };

            foreach (var cartDetail in cart.CartDetails)
            {
                var orderItem = new OrderItem
                {
                    MedId = cartDetail.MedId,
                    ItemQuantity = cartDetail.Quantity
                };

                order.OrderItems.Add(orderItem);
            }

            _context.Orders.Add(order);

            _context.CartDetails.RemoveRange(cart.CartDetails);
            _context.SaveChanges();

            return Ok(order);
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

            _context.SaveChanges();

            return Ok(order);
        }
    }
}
