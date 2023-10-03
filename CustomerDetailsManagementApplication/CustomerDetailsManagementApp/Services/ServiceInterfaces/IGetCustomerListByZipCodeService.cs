namespace CustomerDetailsManagementApp.Services.ServiceInterfaces
{
    public interface IGetCustomerListByZipCodeService
    {
        List<object> GetCustomersByZipCode();
    }
}
