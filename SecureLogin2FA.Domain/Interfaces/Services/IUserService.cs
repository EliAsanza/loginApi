using SecureLogin2FA.Domain.Entities;
using SecureLogin2FA.Domain.Models.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SecureLogin2FA.Domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<User> CreateUserAsync(UserModel user);
        Task<IEnumerable<UserModel>> GetAllUsersAsync();

    }
}