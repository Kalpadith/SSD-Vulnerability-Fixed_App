using DatabaseConfigClassLibrary.Models;

namespace CustomerDetailsManagementApp.Services.ServiceInterfaces
{
    public interface ISearchUserService
    {
        List<UserData> SearchUsers(string searchText);
    }
}
