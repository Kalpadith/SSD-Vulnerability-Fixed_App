namespace CustomerDetailsManagementApp.Services.ServiceInterfaces
{
    public interface IGetAllCustomerListService
    {
        List<object> GetAllCustomersAndAddresses();
    }
}
