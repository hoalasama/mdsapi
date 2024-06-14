using mdswebapi.Interfaces;
using mdswebapi.Models;
using Microsoft.EntityFrameworkCore;

namespace mdswebapi.Repository
{
    public class MedicineRepo : IMedicineRepo
    {
        private readonly mdsDbContext _context;
        public MedicineRepo(mdsDbContext context)
        {
            _context = context;
        }

        public async Task<Medicine?> CreateAsync(Medicine medicineModel)
        {
            await _context.Medicines.AddAsync(medicineModel);
            await _context.SaveChangesAsync();
            return medicineModel;
        }

        public async Task<List<Medicine>> GetAllAsync()
        {
            return await _context.Medicines.ToListAsync();
        }

        public async Task<Medicine?> GetByIdAsync(int id)
        {
            return await _context.Medicines.FindAsync(id);
        }

        public async Task<Medicine?> UpdateAsync(int id, Medicine medicineModel)
        {
            var existingMedicine = await _context.Medicines.FindAsync(id);
            if (existingMedicine == null)
            {
                return null;
            }

            existingMedicine.MedName = medicineModel.MedName;
            existingMedicine.MedDesc = medicineModel.MedDesc;
            existingMedicine.MedDiscount = medicineModel.MedDiscount;
            existingMedicine.MedRemain = medicineModel.MedRemain;
            existingMedicine.MedPrice = medicineModel.MedPrice;
            existingMedicine.MedPicture = medicineModel.MedPicture;
            existingMedicine.CateId = medicineModel.CateId;

            await _context.SaveChangesAsync();
            return medicineModel;
        }
    }
}
