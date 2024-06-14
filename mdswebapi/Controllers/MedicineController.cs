using mdswebapi.Dtos.Medicine;
using mdswebapi.Interfaces;
using mdswebapi.Mappers;
using mdswebapi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace mdswebapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineController : ControllerBase
    {
        private readonly IMedicineRepo _medicineRepo;
        private readonly IPharmacyRepo _pharmacyRepo;
        private readonly ICategoryRepo _categoryRepo;
        public MedicineController(IMedicineRepo medicineRepo, IPharmacyRepo pharmacyRepo, ICategoryRepo categoryRepo)
        {
            _medicineRepo = medicineRepo;
            _pharmacyRepo = pharmacyRepo;
            _categoryRepo = categoryRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() 
        {
            var medicine = await _medicineRepo.GetAllAsync();
            var medicineDto = medicine.Select(s => s.ToMedicineDto());

            return Ok(medicineDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var medicine = await _medicineRepo.GetByIdAsync(id);
            if (medicine == null)
            {
                return NotFound();
            }
            return Ok(medicine.ToMedicineDto());
        }

        [HttpPost("{pharId}")]
        public async Task<IActionResult> Create([FromRoute] int pharId, [FromBody] CreateMedicineDto medicineDto)
        {
            if(!await _pharmacyRepo.PharmacyExists(pharId))
            {
                return BadRequest("Pharmacy does not exist");
            }
            if(!await _categoryRepo.CategoryExists(medicineDto.CateId))
            {
                return BadRequest("Category does not exist");
            }
            var medicineModel = medicineDto.ToMedicineFromCreate(pharId);
            await _medicineRepo.CreateAsync(medicineModel);
            return CreatedAtAction(nameof(GetById), new { id = medicineModel.MedId}, medicineModel.ToMedicineDto());
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateMedicineRequestDto updateDto)
        {
            if (!await _categoryRepo.CategoryExists(updateDto.CateId))
            {
                return BadRequest("Category does not exist");
            }
            var medicine = await _medicineRepo.UpdateAsync(id, updateDto.ToMedicineFromUpdate());
            if (medicine == null)
            {
                return BadRequest("Medicine not found");
            }
            return Ok(medicine.ToMedicineDto1());
        }
    }
}
