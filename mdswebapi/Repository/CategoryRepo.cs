using mdswebapi.Dtos.Category;
using mdswebapi.Interfaces;
using mdswebapi.Models;
using Microsoft.EntityFrameworkCore;

namespace mdswebapi.Repository
{
    public class CategoryRepo : ICategoryRepo
    {
        private readonly mdsDbContext _context;
        public CategoryRepo(mdsDbContext context)
        {
            _context = context;
        }

        public Task<bool> CategoryExists(int? id)
        {
            return _context.Categories.AnyAsync(s => s.CateId == id);
        }

        public async Task<Category> CreateAsync(Category categoryModel)
        {
            await _context.Categories.AddAsync(categoryModel);
            await _context.SaveChangesAsync();
            return categoryModel;
        }

        public async Task<Category?> DeleteAsync(int id)
        {
            var categoryModel = await _context.Categories.FirstOrDefaultAsync(x => x.CateId == id);
            if (categoryModel == null)
            {
                return null;
            }
            _context.Categories.Remove(categoryModel);
            await _context.SaveChangesAsync();
            return categoryModel;
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync(); 
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task<Category?> UpdateAsync(int id, UpdateCategoryRequestDto categoryDto)
        {
            var existingCategory = await _context.Categories.FirstOrDefaultAsync(x => x.CateId == id);
            if (existingCategory == null)
            {
                return null;
            }
            existingCategory.CateDesc = categoryDto.CateDesc;
            existingCategory.CateName = categoryDto.CateName;

            await _context.SaveChangesAsync();
            return existingCategory;
        }
    }
}
