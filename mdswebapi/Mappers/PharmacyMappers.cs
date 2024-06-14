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
                PharLogin = pharmacyModel.PharLogin,
                PharPass = pharmacyModel.PharPass,
                PharName = pharmacyModel.PharName,
                PharPhone = pharmacyModel.PharPhone,
                PharEmail = pharmacyModel.PharEmail,
                PharAddress = pharmacyModel.PharAddress,
                Medicines = pharmacyModel.Medicines.Select(c => c.ToMedicineDto()).ToList(),
            };
        }

        public static Pharmacy ToPharmacyFromCreateDto(this CreatePharmacyRequestDto PharmacyDto)
        {
            return new Pharmacy
            {
                PharLogin = PharmacyDto.PharLogin,
                PharPass = PharmacyDto.PharPass,
                PharName = PharmacyDto.PharName,
                PharPhone = PharmacyDto.PharPhone,
                PharEmail = PharmacyDto.PharEmail,
                PharAddress = PharmacyDto.PharAddress,
            };
        }
    }
}
