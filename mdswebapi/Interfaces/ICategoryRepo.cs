using mdswebapi.Dtos.Category;
using mdswebapi.Dtos.Pharmacy;
using mdswebapi.Models;

namespace mdswebapi.Interfaces
{
    public interface ICategoryRepo
    {
        Task<List<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(int id);
        Task<Category> CreateAsync(Category categoryModel);
        Task<Category?> UpdateAsync(int id, UpdateCategoryRequestDto categoryDto);
        Task<Category?> DeleteAsync(int id);
        Task<bool> CategoryExists(int? id);
    }
}
