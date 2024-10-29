using Microsoft.Extensions.DependencyInjection;
using SecureLogin2FA.Application.Services;
using SecureLogin2FA.Domain.Interfaces.Services;
using System.Reflection;

namespace SecureLogin2FA.Application
{
    public static class ServiceExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddTransient<ILoginService, LoginService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IUserService, UserService>();
        }
    }
}