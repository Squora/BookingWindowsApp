using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp
{
    public partial class ProfilePage : Page
    {
        private User _user;

        public ProfilePage()
        {
            InitializeComponent();

            _user = UserManager.CurrentUser;

            tbFirstName.Text += _user.FirstName;
            tbLastName.Text += _user.LastName;
            tbMiddleName.Text += _user.MiddleName;
            tbPhoneNumber.Text += _user.PhoneNumber;
            tbEmail.Text += _user.Email;
            tbPassportDetails.Text += _user.PassportDetails;
        }
    }
}
