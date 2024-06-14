using mdswebapi.Dtos.Pharmacy;
using mdswebapi.Interfaces;
using mdswebapi.Mappers;
using mdswebapi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace mdswebapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PharmacyController : ControllerBase
    {
        private readonly mdsDbContext _context;
        private readonly IPharmacyRepo _pharmacyRepo;
        public PharmacyController(mdsDbContext context, IPharmacyRepo pharmacyRepo)
        {
            _pharmacyRepo = pharmacyRepo;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var pharmacy = await _pharmacyRepo.GetAllAsync();
            var pharmacyDto = pharmacy.Select(s => s.ToPharmacyDto());

            return Ok(pharmacyDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            var pharmacy = await _pharmacyRepo.GetByIdAsync(id);

            if (pharmacy == null)
            {
                return NotFound();
            }
            return Ok(pharmacy.ToPharmacyDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePharmacyRequestDto PharmacyDto)
        {
            var pharmacyModel = PharmacyDto.ToPharmacyFromCreateDto();
            await _pharmacyRepo.CreateAsync(pharmacyModel);
            return CreatedAtAction(nameof(GetById), new { id = pharmacyModel.PharId }, pharmacyModel.ToPharmacyDto());
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdatePharmacyRequestDto updateDto)
        {
            var pharmacyModel = await _pharmacyRepo.UpdateAsync(id, updateDto);

            if (pharmacyModel == null)
            {
                return NotFound();
            }

            return Ok(pharmacyModel.ToPharmacyDto());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var pharmacyModel = await _pharmacyRepo.DeleteAsync(id);

            if (pharmacyModel == null)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}
