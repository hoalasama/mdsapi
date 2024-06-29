using mdswebapi.Dtos.Review;
using mdswebapi.Interfaces;
using mdswebapi.Mappers;
using mdswebapi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;

namespace mdswebapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewRepo _reviewRepo;
        private readonly mdsDbContext _context;
        public ReviewController(IReviewRepo reviewRepo, mdsDbContext context)
        {
            _reviewRepo = reviewRepo;
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var review = await _reviewRepo.GetAllAsync();
            var reviewdto = review.Select(x => x.ToReviewDto());

            return Ok(reviewdto);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var reviews = await _context.Reviews
                                .Where(x => x.MedId == id)
                                .ToListAsync();
            return Ok(reviews);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateReviewDto reviewDto)
        {
            var reviewModel = reviewDto.ToReviewFromCreate();
            await _reviewRepo.CreateAsync(reviewModel);
            return Ok(reviewModel);
        }
    }
}
