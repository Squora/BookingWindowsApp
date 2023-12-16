using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Model
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string PassportDetails { get; set; }
        public string Password { get; set; }

        public User(int id, string firstName, string lastName, string middleName, string email, string phoneNumber, 
            string passportDetails, string password)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
            Email = email;
            PhoneNumber = phoneNumber;
            PassportDetails = passportDetails;
            Password = password;
        }

        public void Rename(string newName)
        {

        }
    }
}
