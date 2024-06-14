using mdswebapi.Dtos.Category;
using mdswebapi.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace mdswebapi.Mappers
{
    public static class CategoryMappers
    {
        public static CategoryDto ToCategoryDto(this Category categoryModel)
        {
            return new CategoryDto
            {
                CateId = categoryModel.CateId,
                CateDesc = categoryModel.CateDesc,
                CateName = categoryModel.CateName,
            };
        }

        public static Category CreateCategoryRequestDto(this CreateCategoryRequestDto categoryDto)
        {
            return new Category
            {
                CateDesc = categoryDto.CateDesc,
                CateName = categoryDto.CateName,
            };
        }
    }
}
