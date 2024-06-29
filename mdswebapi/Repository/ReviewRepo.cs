using mdswebapi.Interfaces;
using mdswebapi.Models;
using Microsoft.EntityFrameworkCore;

namespace mdswebapi.Repository
{
    public class ReviewRepo : IReviewRepo
    {
        private readonly mdsDbContext _context;
        public ReviewRepo(mdsDbContext context)
        {
            _context = context; 
        }
        public async Task<Review?> CreateAsync(Review reviewModel)
        {
            await _context.Reviews.AddAsync(reviewModel);
            await _context.SaveChangesAsync();
            return reviewModel;
        }

        public Task<Review?> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Review>> GetAllAsync()
        {
            return await _context.Reviews.ToListAsync();
        }

        public async Task<Review?> GetByIdAsync(int id)
        {
            return await _context.Reviews.FindAsync(id);
        }

        public Task<Review?> UpdateAsync(int id, Review reviewModel)
        {
            throw new NotImplementedException();
        }
    }
}
