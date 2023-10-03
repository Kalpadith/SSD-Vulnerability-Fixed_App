using System.ComponentModel.DataAnnotations;

namespace DatabaseConfigClassLibrary.Models
{
    public class AddressData
    {
        [Key]
        public string AddressId { get; set; }
        public int Number { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Zipcode { get; set; }
    }
}
