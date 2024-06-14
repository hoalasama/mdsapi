using mdswebapi.Models;

namespace mdswebapi.Interfaces
{
    public interface IMedicineRepo
    {
        Task<List<Medicine>> GetAllAsync();
        Task<Medicine?> GetByIdAsync(int id);
        Task<Medicine?> CreateAsync(Medicine medicineModel);
        Task<Medicine?> UpdateAsync(int id, Medicine medicineModel);
    }
}
