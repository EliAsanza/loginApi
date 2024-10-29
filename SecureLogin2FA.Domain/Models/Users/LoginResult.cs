
using SecureLogin2FA.Domain.Entities;

namespace SecureLogin2FA.Domain.Models.Users
{
    public class LoginResult
    {
        public bool IsSuccessful { get; set; }
        public bool TwoFactorRequired { get; set; }
        public string Message { get; set; }
        public User User { get; set; }
    }
}
