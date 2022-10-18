using Etkinlik.Core.DTOs;
using Etkinlik.Core.Models;

namespace Etkinlik.Core.Services
{
    public interface IAuthService
    {       
        Task<CustomResponseDto<string>> LoginSync(LoginDto loginDto);
        Task<CustomResponseDto<string>> SingUp(Company company);
    }
}
