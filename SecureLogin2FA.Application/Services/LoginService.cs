using SecureLogin2FA.Domain.Entities;
using SecureLogin2FA.Domain.Interfaces.Repositories;
using SecureLogin2FA.Domain.Interfaces.Services;
using SecureLogin2FA.Domain.Models.Users;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System;

namespace SecureLogin2FA.Application.Services
{
    public class LoginService : ILoginService
    {
        private readonly IEmailService _emailService;
        private readonly IUserRepository _userRepository;

        public LoginService(IEmailService emailService, IUserRepository userRepository)
        {
            _emailService = emailService;
            _userRepository = userRepository;
        }

        public async Task<LoginResult> LoginAsync(LoginModel loginModel)
        {
            var result = new LoginResult
            {
                IsSuccessful = false,
                TwoFactorRequired = false
            };

            var user = await _userRepository.GetUserByEmail(loginModel.Email);

            if (user == null || user.PasswordHash != loginModel.Password)
            {
                result.Message = "Invalid email or password.";
                return result;
            }

            if (user.TwoFactorEnabled)
            {
                var tokenSent = await Generate2FATokenAsync(user);
                if (tokenSent)
                {
                    result.IsSuccessful = true;
                    result.TwoFactorRequired = true;
                    result.Message = "2FA token sent to email.";
                }
                else
                {
                    result.IsSuccessful = false;
                    result.TwoFactorRequired = true;
                    result.Message = "Failed to send 2FA token.";
                }

                result.User = user;
                return result;
            }

            result.User = user;
            result.IsSuccessful = true;
            result.Message = "Login successful.";
            return result;
        }

        private async Task<bool> Generate2FATokenAsync(User user)
        {
            var token = GenerateToken();

            user.TwoFactorSecret = token;
            user.TwoFactorEnabledOn = DateTime.UtcNow;
            await _userRepository.UpdateAsync(user);

            var body = Prepare2FATokenEmailBody(token);

            return await _emailService.SendEmailAsync(user.Email, "TEST 2FA Token", body);
        }

        public async Task<bool> Verify2FATokenAsync(string userName, string token)
        {
            var user = await _userRepository.GetUserByEmail(userName);
            if (user == null || user.TwoFactorSecret != token)
            {
                return false;
            }

            if (user.TwoFactorEnabledOn.HasValue && (DateTime.UtcNow - user.TwoFactorEnabledOn.Value).TotalMinutes <= 5)
            {
                user.TwoFactorSecret = null;
                user.TwoFactorEnabledOn = null;
                await _userRepository.UpdateAsync(user);
                return true;
            }

            return false;
        }

        private string Prepare2FATokenEmailBody(string token)
        {
            return $@"
            <html>
                <body>
                    <h2>Two-Factor Authentication Code</h2>
                    <p>Your verification code is:</p>
                    <h3 style='color:blue;'>{token}</h3>
                    <p>Please enter this code in the application to complete your login.</p>
                    <p>If you didn’t request this code, please ignore this email.</p>
                </body>
            </html>";
        }

        private string GenerateToken()
        {
            using var rng = new RNGCryptoServiceProvider();
            var randomBytes = new byte[4];
            rng.GetBytes(randomBytes);
            int token = BitConverter.ToInt32(randomBytes, 0) % 1000000;
            return Math.Abs(token).ToString("D6");
        }
    }
}