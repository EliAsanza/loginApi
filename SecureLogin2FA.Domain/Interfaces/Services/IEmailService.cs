using System.Threading.Tasks;

namespace SecureLogin2FA.Domain.Interfaces.Services
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(string to, string subject, string body, bool isHtml = true);
    }
}