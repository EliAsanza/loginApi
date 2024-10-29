using SecureLogin2FA.Domain.Entities;
using System.Threading.Tasks;

namespace SecureLogin2FA.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUserByEmail(string email);
    }
}
