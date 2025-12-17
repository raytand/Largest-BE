using Largest.Domain.Entities;

namespace Largest.Application.Interfaces;

public interface IUserRepository
{
    Task<User?> GetUserByEmailAsync(string email);
    Task AddUserAsync(User user);
    Task<User?> GetByUsernameAsync(string username);
}
