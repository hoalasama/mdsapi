using mdswebapi.Dtos.Pharmacy;
using mdswebapi.Models;

namespace mdswebapi.Interfaces
{
    public interface IPharmacyRepo
    {
        Task<List<Pharmacy>> GetAllAsync();
        Task<Pharmacy?> GetByIdAsync(int? id);
        Task<Pharmacy?> GetByIdAsync1(string? id);
        Task<Pharmacy> CreateAsync(Pharmacy pharmacyModel);
        Task<Pharmacy?> UpdateAsync(int id, UpdatePharmacyRequestDto pharmacyDto);
        Task<Pharmacy?> DeleteAsync(int id);
        Task<bool> PharmacyExists(int id);
        Task<bool> PharmacyExists1(string id);
    }
}
