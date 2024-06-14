using mdswebapi.Dtos.Promotion;
using mdswebapi.Interfaces;
using mdswebapi.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace mdswebapi.Repository
{
    public class PromotionRepo : IPromotionRepo
    {
        private readonly mdsDbContext _context;
        public PromotionRepo(mdsDbContext context)
        {
            _context = context;
        }

        public async Task<Promotion> CreateAsync(Promotion promotionModel)
        {
            await _context.Promotions.AddAsync(promotionModel);
            await _context.SaveChangesAsync();
            return promotionModel;
        }

        public async Task<Promotion?> DeleteAsync(int id)
        {
            var promotionModel = await _context.Promotions.FirstOrDefaultAsync(x => x.PromoId == id);
            if (promotionModel == null)
            {
                return null;
            }
            _context.Promotions.Remove(promotionModel);
            await _context.SaveChangesAsync();
            return promotionModel;
        }

        public async Task<List<Promotion>> GetAllAsync()
        {
            return await _context.Promotions.ToListAsync();
        }

        public async Task<Promotion?> GetByIdAsync(int id)
        {
            return await _context.Promotions.FindAsync(id);
        }

        public async Task<Promotion?> UpdateAsync(int id, UpdatePromotionRequestDto categoryDto)
        {
            var existingPromotion = await _context.Promotions.FirstOrDefaultAsync(x => x.PromoId == id);
            if (existingPromotion == null)
            {
                return null;
            }
            existingPromotion.PromoName = categoryDto.PromoName;
            existingPromotion.StartDate = categoryDto.StartDate;
            existingPromotion.EndDate = categoryDto.EndDate;
            existingPromotion.DiscountPercent = categoryDto.DiscountPercent;
            await _context.SaveChangesAsync();
            return existingPromotion;
        }
    }
}
