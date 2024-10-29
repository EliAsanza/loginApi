using SecureLogin2FA.Domain.Models.Users;
using System.Threading.Tasks;

namespace SecureLogin2FA.Domain.Interfaces.Services
{
    public interface ILoginService
    {
        Task<LoginResult> LoginAsync(LoginModel loginModel);

        Task<bool> Verify2FATokenAsync(string userName, string token);
    }
}