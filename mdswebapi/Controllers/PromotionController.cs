using mdswebapi.Dtos.Promotion;
using mdswebapi.Interfaces;
using mdswebapi.Mappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace mdswebapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionController : ControllerBase
    {
        private readonly IPromotionRepo _promotionRepo;
        public PromotionController(IPromotionRepo promotionRepo)
        {
            _promotionRepo = promotionRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var promotion = await _promotionRepo.GetAllAsync();
            var promotionDto = promotion.Select(s => s.ToPromotionDto());
            
            return Ok(promotionDto);
        }
        [HttpGet("{id}")]

        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var promotion = await _promotionRepo.GetByIdAsync(id);
            if (promotion == null)
            {
                return NotFound();
            }
            return Ok(promotion.ToPromotionDto());
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePromotionRequestDto promtionDto)
        {
            var promotionModel = promtionDto.CreatePromotionRequestDto();
            await _promotionRepo.CreateAsync(promotionModel);
            return CreatedAtAction(nameof(GetById), new {id = promotionModel.PromoId}, promotionModel.ToPromotionDto());
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdatePromotionRequestDto promotionDto)
        {
            var promotionModel = await _promotionRepo.UpdateAsync(id, promotionDto);
            if (promotionModel == null) { return NotFound(); }
            return Ok(promotionModel.ToPromotionDto());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var promotionModel = await _promotionRepo.DeleteAsync(id);
            if (promotionModel == null)
            {
                return NotFound();
            }
            return NoContent();
        }
        
    }
}
