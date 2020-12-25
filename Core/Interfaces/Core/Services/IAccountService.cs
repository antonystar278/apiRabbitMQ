using Core.Entities;
using Core.Models.Authentication;
using System.Threading.Tasks;

namespace Core.Interfaces.Core.Services
{
    public interface IAccountService
    {
        Task<AuthenticateResponse> AuthenticateAsync(AuthenticateRequest model);
        Task<ApplicationUser> GetByIdAsync(int id);
    }
}
