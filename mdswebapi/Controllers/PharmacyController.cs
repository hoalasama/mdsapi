using mdswebapi.Dtos.Pharmacy;
using mdswebapi.Interfaces;
using mdswebapi.Mappers;
using mdswebapi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;
using System.Security.Claims;

namespace mdswebapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PharmacyController : ControllerBase
    {
        private readonly mdsDbContext _context;
        private readonly IPharmacyRepo _pharmacyRepo;
        private readonly UserManager<Customer> _userManager;
        public PharmacyController(mdsDbContext context, IPharmacyRepo pharmacyRepo, UserManager<Customer> userManager)
        {
            _pharmacyRepo = pharmacyRepo;
            _context = context;
            _userManager = userManager;
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
        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromRoute] string? id,[FromBody] CreatePharmacyRequestDto PharmacyDto)
        {
            var pharmacyModel = PharmacyDto.ToPharmacyFromCreateDto();
            pharmacyModel.CustomerId = id;
            await _pharmacyRepo.CreateAsync(pharmacyModel);
            await _context.SaveChangesAsync();

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return BadRequest($"Not found user");
            }
            user.PharmacyId = pharmacyModel.PharId;
            pharmacyModel.CustomerId = id;
            await _userManager.UpdateAsync(user);

            return CreatedAtAction(nameof(GetById), new { id = pharmacyModel.PharId }, pharmacyModel.ToPharmacyDto());
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize(Roles = "Admin, Phar")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdatePharmacyRequestDto updateDto)
        {
            var pharmacyModel = await _pharmacyRepo.GetByIdAsync(id);

            if (pharmacyModel == null)
            {
                return NotFound();
            }
            var pharUser = pharmacyModel.CustomerId;

            var userId = User.FindFirstValue("nameid");
            var nameId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (nameId == null)
            {
                return BadRequest($"Not found user");
            }

            if (!User.IsInRole("Admin") && pharUser != nameId)
            {
                return BadRequest("You do not have permission to update this pharmacy.");
            }

            var updatedPharmacy = await _pharmacyRepo.UpdateAsync(id, updateDto);

            return Ok(updatedPharmacy.ToPharmacyDto());
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Admin, Phar")]
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
