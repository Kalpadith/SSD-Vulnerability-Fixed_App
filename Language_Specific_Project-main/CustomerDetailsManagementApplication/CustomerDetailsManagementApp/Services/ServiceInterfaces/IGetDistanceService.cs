namespace CustomerDetailsManagementApp.Services.ServiceInterfaces
{
    public interface IGetDistanceService
    {
        double CalculateDistance(double lat1, double lon1, double lat2, double lon2);
    }
}
