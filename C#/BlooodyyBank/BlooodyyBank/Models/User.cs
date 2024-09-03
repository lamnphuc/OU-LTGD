using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlooodyyBank.Models
{
    public class User
    {
        public User(string username, string password, string fullName, string email)
        {
            Username = username;
            Password = password;
            FullName = fullName;
            Email = email;
        }

        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
    }
}
