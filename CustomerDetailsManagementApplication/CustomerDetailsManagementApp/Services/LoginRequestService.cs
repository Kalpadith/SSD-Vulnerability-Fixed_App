using CustomerDetailsManagementApp.Services.ServiceInterfaces;

namespace CustomerDetailsManagementApp.Services
{
    public class LoginRequestService : ILoginRequestService
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
