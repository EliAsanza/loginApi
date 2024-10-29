namespace SecureLogin2FA.Domain.Models.Users
{
    public class Verify2FAModel
    {
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
