using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseConfigClassLibrary.Models
{
    public class UserData
    {
        [Key]
        [Column("Id")]
        public string? _id { get; set; }
        public int? Index { get; set; }
        public int? Age { get; set; }
        public string? EyeColor { get; set; }
        public string? Name { get; set; }
        public string? Gender { get; set; }
        public string? Company { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? About { get; set; }
        public string? Registered { get; set; }
        public long? Latitude { get; set; }
        public float? Longitude { get; set; }
        public List<string>? Tags { get; set; }
        public AddressData? Address { get; set; }
        public string AddressId { get; set; }
    }
}
