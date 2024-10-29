using AutoMapper;
using SecureLogin2FA.Domain.Entities;
using SecureLogin2FA.Domain.Interfaces.Repositories;
using SecureLogin2FA.Domain.Interfaces.Services;
using SecureLogin2FA.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SecureLogin2FA.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<User> CreateUserAsync(UserModel userModel)
        {
            var userEntity = _mapper.Map<User>(userModel);

            return await _userRepository.AddAsync(userEntity);
        }

        public async Task<IEnumerable<UserModel>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserModel>>(users);
        }
    }
}