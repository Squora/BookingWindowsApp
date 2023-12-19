using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Model
{
    public class User
    {
        public int Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string MiddleName { get; private set; }
        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }
        public string PassportDetails { get; private set; }
        public string Password { get; private set; }

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
    }
}
