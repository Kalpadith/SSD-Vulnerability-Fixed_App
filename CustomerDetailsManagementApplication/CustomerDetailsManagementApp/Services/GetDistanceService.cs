using CustomerDetailsManagementApp.Services.ServiceInterfaces;
using System;

namespace CustomerDetailsManagementApp.Services
{
    public class GetDistanceService : IGetDistanceService
    {
        public double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            const double EarthRadiusKm = 6371.0;

            lat1 = DegreesToRadians(lat1);
            lon1 = DegreesToRadians(lon1);
            lat2 = DegreesToRadians(lat2);
            lon2 = DegreesToRadians(lon2);

            double dLat = DegreesToRadians(lat2 - lat1);
            double dLon = DegreesToRadians(lon2 - lon1);
            double a =
                Math.Sin(dLat / 2) * Math.Sin(dLat / 2)
                + Math.Sin(dLon / 2) * Math.Sin(dLon / 2) * Math.Cos(lat1) * Math.Cos(lat2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            double distanceKm = EarthRadiusKm * c;
            return Math.Round(distanceKm, 6);
        }

        private double DegreesToRadians(double degrees)
        {
            return degrees * Math.PI / 180;
        }
    }
}
