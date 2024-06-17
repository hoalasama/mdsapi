using mdswebapi.Dtos.Pharmacy;
using mdswebapi.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace mdswebapi.Mappers
{
    public static class PharmacyMappers
    {
        public static PharmacyDto ToPharmacyDto(this Pharmacy pharmacyModel)
        {
            return new PharmacyDto
            {
                PharId = pharmacyModel.PharId,
                PharName = pharmacyModel.PharName,
                PharPhone = pharmacyModel.PharPhone,
                PharEmail = pharmacyModel.PharEmail,
                PharAddress = pharmacyModel.PharAddress,
                CustomerId = pharmacyModel.CustomerId,
                Medicines = pharmacyModel.Medicines.Select(c => c.ToMedicineDto()).ToList(),
            };
        }

        public static Pharmacy ToPharmacyFromCreateDto(this CreatePharmacyRequestDto PharmacyDto)
        {
            return new Pharmacy
            {
                PharName = PharmacyDto.PharName,
                PharPhone = PharmacyDto.PharPhone,
                PharEmail = PharmacyDto.PharEmail,
                PharAddress = PharmacyDto.PharAddress,
                CustomerId = PharmacyDto.CustomerId,
            };
        }
    }
}
