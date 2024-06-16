using mdswebapi.Models;

namespace mdswebapi.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(Customer customer);
    }
}
