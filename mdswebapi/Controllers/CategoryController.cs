using mdswebapi.Attributes;
using mdswebapi.Dtos.Category;
using mdswebapi.Interfaces;
using mdswebapi.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace mdswebapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepo _categoryRepo;
        public CategoryController(ICategoryRepo categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var category = await _categoryRepo.GetAllAsync();
            var categoryDto = category.Select(s => s.ToCategoryDto());

            return Ok(categoryDto);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var category = await _categoryRepo.GetByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }
            return Ok(category.ToCategoryDto());
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateCategoryRequestDto categoryDto)
        {
            var categoryModel = categoryDto.CreateCategoryRequestDto();
            await _categoryRepo.CreateAsync(categoryModel);
            return CreatedAtAction(nameof(GetById), new { id = categoryModel.CateId }, categoryModel.ToCategoryDto());
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCategoryRequestDto categoryDto)
        {
            var categoryModel = await _categoryRepo.UpdateAsync(id, categoryDto);
            if (categoryModel == null)
            {
                return NotFound();
            }

            return Ok(categoryModel.ToCategoryDto());
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var categoryModel = await _categoryRepo.DeleteAsync(id);
            if (categoryModel == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
