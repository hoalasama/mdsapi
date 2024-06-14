using mdswebapi.Dtos.Pharmacy;
using mdswebapi.Interfaces;
using mdswebapi.Models;
using Microsoft.EntityFrameworkCore;

namespace mdswebapi.Repository
{
    public class PharmacyRepo : IPharmacyRepo
    {
        private readonly mdsDbContext _context;
        public PharmacyRepo(mdsDbContext context)
        {
            _context = context;
        }

        public async Task<Pharmacy> CreateAsync(Pharmacy pharmacyModel)
        {
            await _context.Pharmacies.AddAsync(pharmacyModel);
            await _context.SaveChangesAsync();
            return pharmacyModel;
        }

        public async Task<Pharmacy?> DeleteAsync(int id)
        {
            var pharmacyModel = await _context.Pharmacies.FirstOrDefaultAsync(x => x.PharId == id);

            if (pharmacyModel == null)
            {
                return null;
            }

            _context.Pharmacies.Remove(pharmacyModel);
            await _context.SaveChangesAsync();
            return pharmacyModel;
        }

        public async Task<List<Pharmacy>> GetAllAsync()
        {
            return await _context.Pharmacies.Include(c => c.Medicines).ToListAsync();
        }

        public async Task<Pharmacy?> GetByIdAsync(int id)
        {
            return await _context.Pharmacies.Include(c => c.Medicines).FirstOrDefaultAsync(i => i.PharId == id);
        }

        public Task<bool> PharmacyExists(int id)
        {
            return _context.Pharmacies.AnyAsync(s => s.PharId == id);
        }

        public async Task<Pharmacy?> UpdateAsync(int id, UpdatePharmacyRequestDto pharmacyDto)
        {
            var existingPharmacy = await _context.Pharmacies.FirstOrDefaultAsync(x => x.PharId == id);
            if (existingPharmacy == null)
            {
                return null;
            }
            existingPharmacy.PharName = pharmacyDto.PharName;
            existingPharmacy.PharPhone = pharmacyDto.PharPhone;
            existingPharmacy.PharEmail = pharmacyDto.PharEmail;
            existingPharmacy.PharAddress = pharmacyDto.PharAddress;

            await _context.SaveChangesAsync();
            return existingPharmacy;
        }
    }
}
