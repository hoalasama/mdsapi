using mdswebapi.Dtos.Chat;
using mdswebapi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace mdswebapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly UserManager<Customer> _userManager;
        private readonly mdsDbContext _context;

        public ChatController(UserManager<Customer> userManager, mdsDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateChat([FromBody] CreateChatDto createChatDto)
        {
            var sender = await _userManager.FindByIdAsync(createChatDto.SenderId);
            var receiver = await _userManager.FindByIdAsync(createChatDto.ReceiverId);

            if (sender == null || receiver == null)
            {
                return BadRequest(new { Message = "Invalid sender or receiver ID" });
            }

            var chat = new Chat
            {
                SenderId = createChatDto.SenderId,
                ReceiverId = createChatDto.ReceiverId,
                Content = createChatDto.Content,
                Sender = sender,
                Receiver = receiver
            };

            _context.Chats.Add(chat);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Chat created successfully", ChatId = chat.Id });
        }
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetChatsByUserId(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound(new { Message = "User not found" });
            }

            var chats = await _context.Chats
                .Include(c => c.Sender)
                .Include(c => c.Receiver)
                .Where(c => c.SenderId == userId || c.ReceiverId == userId)
                .ToListAsync();

            return Ok(chats);
        }
    }
}
