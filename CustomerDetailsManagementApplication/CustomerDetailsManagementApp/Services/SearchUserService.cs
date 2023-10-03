using System;
using System.Collections.Generic;
using CustomerDetailsManagementApp.Services.ServiceInterfaces;
using DatabaseConfigClassLibrary.Models;
using DatabaseConfigClassLibrary.Repositories;

namespace CustomerDetailsManagementApp.Services
{
    public class SearchUserService : ISearchUserService
    {
        private readonly IUserRepository _userRepository;

        public SearchUserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public List<UserData> SearchUsers(string searchText)
        {
            try
            {
                return _userRepository.SearchUsers(searchText);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
