using Largest.Application.DTOs;

namespace Largest.Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<string?> LoginAsync(string email, string password);
        Task<ServiceResponseDto> RegisterAsync(UserRegisterDto request);
    }
}   