using mdswebapi.Dtos.Medicine;
using mdswebapi.Models;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace mdswebapi.Mappers
{
    public static class MedicineMappers
    {
        public static MedicineDto ToMedicineDto(this Medicine medicineModel)
        {
            return new MedicineDto
            {
                MedId = medicineModel.MedId,
                MedName = medicineModel.MedName,
                MedDesc = medicineModel.MedDesc,
                MedDiscount = medicineModel.MedDiscount,
                MedPicture = medicineModel.MedPicture,
                MedPrice = medicineModel.MedPrice,
                MedRemain = medicineModel.MedRemain,
                CateId = medicineModel.CateId,
                PharId = medicineModel.PharId,
                Reivews = medicineModel.Reviews.Select(c => c.ToReviewDto()).ToList(),
            };
        }

        public static AfterUpdateMedicineDto ToMedicineDto1(this Medicine medicineModel)
        {
            return new AfterUpdateMedicineDto
            {
                MedId = medicineModel.MedId,
                MedName = medicineModel.MedName,
                MedDesc = medicineModel.MedDesc,
                MedDiscount = medicineModel.MedDiscount,
                MedPicture = medicineModel.MedPicture,
                MedPrice = medicineModel.MedPrice,
                MedRemain = medicineModel.MedRemain,
                CateId = medicineModel.CateId,
            };
        }

        public static Medicine ToMedicineFromCreate(this CreateMedicineDto medicineDto, int pharId)
        {
            return new Medicine
            {
                MedName = medicineDto.MedName,
                MedDesc = medicineDto.MedDesc,
                MedDiscount = medicineDto.MedDiscount,
                MedPicture = medicineDto.MedPicture,
                MedPrice = medicineDto.MedPrice,
                MedRemain = medicineDto.MedRemain,
                CateId = medicineDto.CateId,    
                PharId = pharId,
            };
        }
        public static Medicine ToMedicineFromUpdate(this UpdateMedicineRequestDto medicineDto)
        {
            return new Medicine
            {
                MedName = medicineDto.MedName,
                MedDesc = medicineDto.MedDesc,
                MedDiscount = medicineDto.MedDiscount,
                MedPicture = medicineDto.MedPicture,
                MedPrice = medicineDto.MedPrice,
                MedRemain = medicineDto.MedRemain,
                CateId = medicineDto.CateId,
            };
        }
    }
}
