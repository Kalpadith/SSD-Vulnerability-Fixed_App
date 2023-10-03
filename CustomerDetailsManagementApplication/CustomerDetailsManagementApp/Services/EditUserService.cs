using AutoMapper;
using DatabaseConfigClassLibrary.DatabaseConfig;
using DatabaseConfigClassLibrary.DTO;
using DatabaseConfigClassLibrary.Repositories;
using DatabaseConfigClassLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using CustomerDetailsManagementApp.Services.ServiceInterfaces;

namespace CustomerDetailsManagementApp.Services
{
    public class EditUserService : IEditUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public EditUserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<(bool success, string message)> EditUserAsync(
            string _id,
            UserUpdateDTO userUpdate
        )
        {
            try
            {
                var user = await _userRepository.GetUserByIdAsync(_id);

                if (user == null)
                {
                    return (false, "User not found");
                }

                _mapper.Map(userUpdate, user);

                await _userRepository.UpdateUserAsync(user);

                return (true, "User updated successfully");
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}");
            }
        }
    }
}
