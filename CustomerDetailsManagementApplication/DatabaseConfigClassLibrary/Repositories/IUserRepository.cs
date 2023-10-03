using DatabaseConfigClassLibrary.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseConfigClassLibrary.Repositories
{
    public interface IUserRepository
    {
        Task<UserData> GetUserByIdAsync(string userId);
        Task UpdateUserAsync(UserData user);
        List<object> GetAllCustomersAndAddresses();
        double? GetLatitude(string userId);
        double? GetLongitude(string userId);
        List<object> GetCustomersByZipCode();
        List<UserData> SearchUsers(string searchText);
        Task<IdentityUser> FindByNameAsync(string userName);
        Task<IList<string>> GetRolesAsync(IdentityUser user);
        Task<bool> CheckPasswordAsync(IdentityUser user, string password);
    }
}
 