using mdswebapi.Dtos.Cart;
using mdswebapi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace mdswebapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly mdsDbContext _context;
        public CartController(mdsDbContext context)
        {
            _context = context;
        }
        [HttpPost("add")]
        public async Task<IActionResult> AddToCart([FromBody] AddCartDetailDto dto)
        {
            var cart = await _context.Carts.Include(c => c.CartDetails)
                                           .FirstOrDefaultAsync(c => c.CustomerId == dto.CustomerId);

            if (cart == null)
            {
                cart = new Cart { CustomerId = dto.CustomerId };
                _context.Carts.Add(cart);
            }

            var cartDetail = cart.CartDetails.FirstOrDefault(cd => cd.MedId == dto.MedId);
            if (cartDetail == null)
            {
                cartDetail = new CartDetail
                {
                    MedId = dto.MedId,
                    Quantity = dto.Quantity
                };
                cart.CartDetails.Add(cartDetail);
            }
            else
            {
                cartDetail.Quantity += dto.Quantity;
            }

            await _context.SaveChangesAsync();
            return Ok(cart);
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetCart(string customerId)
        {
            var cart = await _context.Carts.Include(c => c.CartDetails)
                                           .ThenInclude(cd => cd.Med)
                                           .FirstOrDefaultAsync(c => c.CustomerId == customerId);

            if (cart == null)
            {
                return NotFound();
            }

            return Ok(cart);
        }

        [HttpPut("edit")]
        public async Task<IActionResult> EditCartItem([FromBody] EditCartDetailDto dto)
        {
            var cartDetail = await _context.CartDetails.Include(cd => cd.Cart)
                                                       .FirstOrDefaultAsync(cd => cd.CdId == dto.CartDetailId && cd.Cart.CustomerId == dto.CustomerId);

            if (cartDetail == null)
            {
                return NotFound();
            }

            cartDetail.Quantity = dto.Quantity;
            await _context.SaveChangesAsync();

            return Ok(cartDetail);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteCartItem([FromBody] DeleteCartDetailDto dto)
        {
            var cartDetail = await _context.CartDetails.Include(cd => cd.Cart)
                                                       .FirstOrDefaultAsync(cd => cd.CdId == dto.CartDetailId && cd.Cart.CustomerId == dto.CustomerId);

            if (cartDetail == null)
            {
                return NotFound();
            }

            _context.CartDetails.Remove(cartDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
    
    
