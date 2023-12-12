using Booking.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace Booking
{
    public partial class RoomDetailsPage : Page
    {
        private Room _selectedRoom;

        public RoomDetailsPage(Room selectedRoom)
        {
            InitializeComponent();

            _selectedRoom = selectedRoom;

            roomDetails.DataContext = _selectedRoom;
        }

        private void BtnBook_Click(object sender, RoutedEventArgs e)
        {
            roomDetails.Visibility = Visibility.Collapsed;
            btnBook.Visibility = Visibility.Collapsed;

            mainFrame.Content = new BookingPage(_selectedRoom);
        }
    }
}
