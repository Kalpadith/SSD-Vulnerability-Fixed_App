using DatabaseConfigClassLibrary.DTO;

namespace CustomerDetailsManagementApp.Services.ServiceInterfaces
{
    public interface IEditUserService
    {
        Task<(bool success, string message)> EditUserAsync(string _id, UserUpdateDTO userUpdate);
    }
}
