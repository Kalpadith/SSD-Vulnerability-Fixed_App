using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseConfigClassLibrary.DatabaseConfig;
using DatabaseConfigClassLibrary.Models;
using DatabaseConfigClassLibrary.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DatabaseConfigClassLibrary.RepositoryImpl
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public UserRepository(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<UserData> GetUserByIdAsync(string userId)
        {
            return await _context.UserDatas.FirstOrDefaultAsync(u => u._id == userId);
        }

        public async Task UpdateUserAsync(UserData user)
        {
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public List<object> GetAllCustomersAndAddresses()
        {
            try
            {
                var customersAndAddresses = _context.UserDatas
                    .Join(
                        _context.UserAddresses,
                        userData => userData.AddressId,
                        addressData => addressData.AddressId,
                        (userData, addressData) => new { userData, addressData }
                    )
                    .ToList();

                return customersAndAddresses
                    .Select(data => new { data.userData, data.addressData })
                    .ToList<object>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public double? GetLatitude(string userId)
        {
            try
            {
                var user = _context.UserDatas.FirstOrDefault(u => u._id == userId);
                return user?.Latitude;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public double? GetLongitude(string userId)
        {
            try
            {
                var user = _context.UserDatas.FirstOrDefault(u => u._id == userId);
                return user?.Longitude;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<object> GetCustomersByZipCode()
        {
            try
            {
                var customersAndAddresses = _context.UserDatas
                    .Join(
                        _context.UserAddresses,
                        userData => userData.AddressId,
                        addressData => addressData.AddressId,
                        (userData, addressData) => new { userData, addressData }
                    )
                    .ToList();

                var groupedCustomers = customersAndAddresses
                    .GroupBy(data => data.addressData.Zipcode)
                    .Select(
                        group =>
                            new
                            {
                                ZipCode = group.Key,
                                Customers = group.Select(data => data.userData).ToList()
                            }
                    )
                    .ToList();

                return groupedCustomers
                    .Select(item => new { item.ZipCode, item.Customers })
                    .ToList<object>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<UserData> SearchUsers(string searchText)
        {
            try
            {
                var matchedUsers = _context.UserDatas
                    .Where(
                        u =>
                            u._id.Contains(searchText)
                            || u.Name.Contains(searchText)
                            || u.Company.Contains(searchText)
                            || u.Email.Contains(searchText)
                            || u.Phone.Contains(searchText)
                    )
                    .ToList();

                return matchedUsers;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IdentityUser> FindByNameAsync(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }

        public async Task<IList<string>> GetRolesAsync(IdentityUser user)
        {
            return await _userManager.GetRolesAsync(user);
        }

        public async Task<bool> CheckPasswordAsync(IdentityUser user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }
    }
}
