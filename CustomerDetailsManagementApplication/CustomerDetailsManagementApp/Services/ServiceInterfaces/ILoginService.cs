namespace CustomerDetailsManagementApp.Services.ServiceInterfaces
{
    public interface ILoginService
    {
        string GenerateJwtToken(string username, string role);
    }
}
