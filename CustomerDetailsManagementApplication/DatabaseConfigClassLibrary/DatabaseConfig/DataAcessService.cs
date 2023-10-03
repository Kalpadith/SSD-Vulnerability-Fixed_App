using DatabaseConfigClassLibrary.DTO;
using DatabaseConfigClassLibrary.Models;

namespace DatabaseConfigClassLibrary.DatabaseConfig
{
    public class DataAccessService
    {
        private readonly ApplicationDbContext _dbContext;

        public DataAccessService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public UserData GetUserByEmail(string email)
        {
            return _dbContext.UserDatas.FirstOrDefault(u => u.Email == email);
        }

        public void ImportUserData(IEnumerable<UserData> userDataList)
        {
            foreach (var userData in userDataList)
            {
                var newUser = new UserData
                {
                    _id = userData._id,
                    Index = userData.Index,
                    Age = userData.Age,
                    EyeColor = userData.EyeColor,
                    Name = userData.Name,
                    Gender = userData.Gender,
                    Company = userData.Company,
                    Email = userData.Email,
                    Phone = userData.Phone,
                    About = userData.About,
                    Registered = userData.Registered,
                    Latitude = userData.Latitude,
                    Longitude = userData.Longitude,
                    Tags = userData.Tags,
                    AddressId = userData.AddressId
                };
                var trackedUser = _dbContext.UserDatas.Find(userData._id);

                if (trackedUser == null)
                {
                    _dbContext.UserDatas.Add(newUser);
                    _dbContext.SaveChanges();
                }
            }
        }

        public void ImportUserAddress(IEnumerable<AddressData> userAddressList)
        {
            foreach (var address in userAddressList)
            {
                var addressData = new AddressData
                {
                    AddressId = address.AddressId,
                    Number = address.Number,
                    Street = address.Street,
                    City = address.City,
                    State = address.State,
                    Zipcode = address.Zipcode
                };
                _dbContext.UserAddresses.Add(addressData);
                _dbContext.SaveChanges();
            }
        }
    }
}
