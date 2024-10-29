using Microsoft.EntityFrameworkCore;
using SecureLogin2FA.Domain.Entities;
using SecureLogin2FA.Domain.Interfaces.Repositories;
using SecureLogin2FA.Infrastructure.Persistence;
using System.Threading.Tasks;

namespace SecureLogin2FA.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
            return user;
        }
    }
}
