using BookingApp.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection.PortableExecutable;
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
        private List<Booking> _bookings = new List<Booking>();
        private User _user;

        public ProfilePage()
        {
            InitializeComponent();

            _user = UserManager.GetUser();
            LoadBookings();

            tbFirstName.Text += _user.FirstName;
            tbLastName.Text += _user.LastName;
            tbMiddleName.Text += _user.MiddleName;
            tbPhoneNumber.Text += _user.PhoneNumber;
            tbEmail.Text += _user.Email;
            tbPassportDetails.Text += _user.PassportDetails;

            LbBookings.ItemsSource = _bookings;
        }

        private void LoadBookings()
        {
            string query = "SELECT " +
                "booking.id, " +
                "hotel.name AS hotel_name, " +
                "room.number AS room_number, " +
                "booking.start_stay_date, " +
                "booking.end_stay_date, " +
                "booking_status.name AS booking_status, " +
                "room_to_hotel.hotel_id, " +
                "booking.status " +
                "FROM " +
                "booking " +
                "JOIN " +
                "room_to_hotel ON booking.room_id = room_to_hotel.room_id " +
                "JOIN " +
                "hotel ON room_to_hotel.hotel_id = hotel.id " +
                "JOIN " +
                "room ON booking.room_id = room.id " +
                "JOIN " +
                "booking_status ON booking.status = booking_status.id " +
                "WHERE " +
                "booking.user_id = @UserId;";
            MySqlParameter mspUser = new MySqlParameter("@UserId", _user.Id);
            DataTable dt = DataBaseManager.ExecuteQuery(query, mspUser);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int id = Convert.ToInt32(dt.Rows[i]["id"]);
                string hotelName = dt.Rows[i]["hotel_name"].ToString();
                int roomNumber = Convert.ToInt32(dt.Rows[i]["room_number"]);
                DateTime startStayDate = (DateTime)dt.Rows[i]["start_stay_date"];
                DateTime endStayDate = (DateTime)dt.Rows[i]["end_stay_date"];
                string status = dt.Rows[i]["status"].ToString();

                Booking booking = new Booking(id, hotelName, roomNumber, startStayDate, endStayDate, status);
                _bookings.Add(booking);
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Window.GetWindow(this)?.Close();
        }
    }
}
