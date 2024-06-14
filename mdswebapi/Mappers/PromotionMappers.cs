using mdswebapi.Dtos.Promotion;
using mdswebapi.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace mdswebapi.Mappers
{
    public static class PromotionMappers
    {
        public static PromotionDto ToPromotionDto(this Promotion promotionModel)
        {
            return new PromotionDto
            {
                PromoId = promotionModel.PromoId,
                PromoName = promotionModel.PromoName,
                DiscountPercent = promotionModel.DiscountPercent,
                StartDate = promotionModel.StartDate,
                EndDate = promotionModel.EndDate,
            };
        }
        public static Promotion CreatePromotionRequestDto(this CreatePromotionRequestDto promotionDto)
        {
            return new Promotion
            {
                PromoName = promotionDto.PromoName,
                DiscountPercent = promotionDto.DiscountPercent,
                StartDate = promotionDto.StartDate,
                EndDate= promotionDto.EndDate,
            };
        }
    }
}
