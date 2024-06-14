using mdswebapi.Dtos.Promotion;
using mdswebapi.Models;

namespace mdswebapi.Interfaces
{
    public interface IPromotionRepo
    {
        Task<List<Promotion>> GetAllAsync();
        Task<Promotion?> GetByIdAsync(int id);
        Task<Promotion> CreateAsync(Promotion promotionModel);
        Task<Promotion?> UpdateAsync(int id, UpdatePromotionRequestDto promotionDto);
        Task<Promotion?> DeleteAsync(int id);
    }
}
