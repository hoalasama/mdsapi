using mdswebapi.Dtos.Review;
using mdswebapi.Interfaces;
using mdswebapi.Mappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace mdswebapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewRepo _reviewRepo;
        public ReviewController(IReviewRepo reviewRepo)
        {
            _reviewRepo = reviewRepo;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var review = await _reviewRepo.GetAllAsync();
            var reviewdto = review.Select(x => x.ToReviewDto());

            return Ok(reviewdto);
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
