using AutoMapper;
using SecureLogin2FA.Domain.Entities;
using SecureLogin2FA.Domain.Models.Users;

namespace SecureLogin2FA.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<UserModel, User>();
            CreateMap<User, UserModel>();
        }
    }
}
