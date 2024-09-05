using System;

namespace BlooodyyBank.Models
{
    public class Donor
    {
        public string Hospital { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string BloodType { get; set; }
        public int UserID { get; set; }

        public Donor(string hospital, string name, string gender, string email, string address, string phoneNumber, string bloodType, int userId)
        {
            Hospital = hospital;
            Name = name;
            Gender = gender;
            Email = email;
            Address = address;
            PhoneNumber = phoneNumber;
            BloodType = bloodType;
            UserID = userId;
        }
    }
}
