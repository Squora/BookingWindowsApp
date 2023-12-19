using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp
{
    public static class UserManager
    {
        private static User? _user;
        public static bool IsLogined { get { return _user is not null; } }

        public static User? GetUser()
        {
            return _user;
        }

        public static void SetUser(User user)
        {
            _user = user;
        }

        public static void ResetUser()
        {
            _user = null;
        }
    }
}
