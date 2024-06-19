using mdswebapi.Dtos.Review;
using mdswebapi.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace mdswebapi.Mappers
{
    public static class ReviewMappers
    {
        public static ReviewDto ToReviewDto(this Review reviewModel)
        {
            return new ReviewDto
            {
                ReviewId = reviewModel.ReviewId,
                ReviewContent = reviewModel.ReviewContent,
                CustomerId = reviewModel.CustomerId,
                MedId = reviewModel.MedId,
            };
        }
        public static Review ToReviewFromCreate(this CreateReviewDto reviewDto)
        {
            return new Review
            {
                ReviewContent = reviewDto.ReviewContent,
                MedId = reviewDto.MedId,
                CustomerId = reviewDto.CustomerId,
            };
        }
    }
}
