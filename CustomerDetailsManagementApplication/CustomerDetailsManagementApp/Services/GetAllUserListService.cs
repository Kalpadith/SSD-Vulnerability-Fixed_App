using System;
using System.Collections.Generic;
using CustomerDetailsManagementApp.Services.ServiceInterfaces;
using DatabaseConfigClassLibrary.DatabaseConfig;
using DatabaseConfigClassLibrary.Repositories;

namespace CustomerDetailsManagementApp.Services
{
    public class GetAllCustomerListService : IGetAllCustomerListService
    {
        private readonly IUserRepository _userRepository;

        public GetAllCustomerListService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public List<object> GetAllCustomersAndAddresses()
        {
            try
            {
                return _userRepository.GetAllCustomersAndAddresses();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
