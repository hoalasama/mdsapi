using mdswebapi.Dtos.Medicine;
using mdswebapi.Dtos.Pharmacy;
using mdswebapi.Interfaces;
using mdswebapi.Mappers;
using mdswebapi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace mdswebapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineController : ControllerBase
    {
        private readonly IMedicineRepo _medicineRepo;
        private readonly IPharmacyRepo _pharmacyRepo;
        private readonly ICategoryRepo _categoryRepo;
        private readonly mdsDbContext _mdsDbContext;
        public MedicineController(IMedicineRepo medicineRepo, IPharmacyRepo pharmacyRepo, ICategoryRepo categoryRepo, mdsDbContext mdsDbContext)
        {
            _medicineRepo = medicineRepo;
            _pharmacyRepo = pharmacyRepo;
            _categoryRepo = categoryRepo;
            _mdsDbContext = mdsDbContext;
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
        [Authorize(Roles = "Admin, Phar")]
        public async Task<IActionResult> Create([FromRoute] string pharId, [FromBody] CreateMedicineDto medicineDto)
        {
            if(!await _pharmacyRepo.PharmacyExists1(pharId))
            {
                return BadRequest("Pharmacy does not exist");
            }
            if(!await _categoryRepo.CategoryExists(medicineDto.CateId))
            {
                return BadRequest("Category does not exist");
            }
            var pharmacyModel = await _pharmacyRepo.GetByIdAsync1(pharId);
            if (pharmacyModel == null)
            {
                return NotFound("Pharmacy not exists");
            }
            var id = pharmacyModel.CustomerId;
            var userId = User.FindFirstValue("nameid");
            var nameId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (!User.IsInRole("Admin") && id != nameId)
            {
                return BadRequest("You do not have permission to create this medicine.");
            }

            var realpharid = pharmacyModel.PharId;
            var medicineModel = medicineDto.ToMedicineFromCreate(realpharid);
            await _medicineRepo.CreateAsync(medicineModel);
            return CreatedAtAction(nameof(GetById), new { id = medicineModel.MedId}, medicineModel.ToMedicineDto());
        }
        [HttpPut]
        [Route("{id}")]
        [Authorize(Roles = "Admin, Phar")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateMedicineRequestDto updateDto)
        {
            if (!await _categoryRepo.CategoryExists(updateDto.CateId))
            {
                return BadRequest("Category does not exist");
            }
            var med = await _medicineRepo.GetByIdAsync(id);
            if (med == null)
            {
                return NotFound("not found medicine");
            }
            var pharId = med.PharId;
            var pharmacyModel = await _pharmacyRepo.GetByIdAsync(pharId);
            if (pharmacyModel == null)
            {
                return NotFound("Pharmacy not exists");   
            }
            var pharuser = pharmacyModel.CustomerId;
            var userId = User.FindFirstValue("nameid");
            var nameId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (!User.IsInRole("Admin") && pharuser != nameId)
            {
                return BadRequest("You do not have permission to update this medicine.");
            }

            var medicine = await _medicineRepo.UpdateAsync(id, updateDto.ToMedicineFromUpdate());
            if (medicine == null)
            {
                return BadRequest("Medicine not found");
            }
            return Ok(medicine.ToMedicineDto1());
        }

        [HttpDelete]
        [Authorize(Roles = "Admin, Phar")]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var med = await _medicineRepo.GetByIdAsync(id);
            if (med == null)
            {
                return NotFound("not found medicine");
            }
            var pharId = med.PharId;
            var pharmacyModel = await _pharmacyRepo.GetByIdAsync(pharId);
            if (pharmacyModel == null)
            {
                return NotFound("Pharmacy not exists");
            }
            var pharuser = pharmacyModel.CustomerId;
            var userId = User.FindFirstValue("nameid");
            var nameId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (!User.IsInRole("Admin") && pharuser != nameId)
            {
                return BadRequest("You do not have permission to delete this medicine.");
            }
            var medicineModel = await _medicineRepo.DeleteAsync(id);
            if (medicineModel == null)
            {
                return NotFound("Medicine does not exists");
            }
            return Ok(medicineModel);
        }
        [HttpGet("search")]
        public async Task<IActionResult> SearchMedicines(string q)
        {
            var medicines = await _mdsDbContext.Medicines
                .Where(m => m.MedName.Contains(q) || m.MedDesc.Contains(q))
                .ToListAsync();

            return Ok(medicines);
        }
    }
}
