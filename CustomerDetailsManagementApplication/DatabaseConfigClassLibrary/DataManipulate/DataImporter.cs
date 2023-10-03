using DatabaseConfigClassLibrary.Models;
using DatabaseConfigClassLibrary.DTO;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DatabaseConfigClassLibrary.DatabaseConfig;
using System.Linq;


namespace DatabaseConfigClassLibrary.DataManipulate
{
    public class DataImporter
    {
        private readonly DataAccessService _dataService;

        public DataImporter(DataAccessService dataService)
        {
            _dataService = dataService;
        }

        string filePath =
            "E:\\FIdenz Training Materials\\Language Specific Project\\CR copy\\Language_Specific_Project\\CustomerDetailsManagementApplication\\DatabaseConfigClassLibrary\\UserData\\UserData.json";

        public void ImportDataFromJson()
        {
            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                var data = JsonConvert.DeserializeObject<List<UserData>>(json);

                var addressDictionary = new Dictionary<string, AddressData>();

                foreach (var User in data)
                {
                    var existingUser = _dataService.GetUserByEmail(User.Email);

                    if (existingUser == null)
                    {
                        var address = User.Address;
                        var addressData = new AddressData
                        {
                            Number = address.Number,
                            Street = address.Street,
                            City = address.City,
                            State = address.State,
                            Zipcode = address.Zipcode,
                            AddressId = GenerateUniqueAddressId()
                        };
                        addressDictionary[addressData.AddressId] = addressData;
                        User.AddressId = addressData.AddressId;
                        User.Address = addressData;
                    }
                    else
                    {
                        Console.WriteLine(
                            $"User with Email {User.Email} already exists, skipping insertion."
                        );
                    }
                }
                _dataService.ImportUserAddress(addressDictionary.Values);
                _dataService.ImportUserData(data);
            }
            else
            {
                Console.WriteLine("The UserData.json file does not exist at the specified path");
            }
        }

        // generate a unique AddressId
        private string GenerateUniqueAddressId()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
