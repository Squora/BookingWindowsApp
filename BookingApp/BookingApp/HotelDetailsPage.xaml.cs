using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using MySql.Data.MySqlClient;
using System.Data;
using static System.Net.Mime.MediaTypeNames;

namespace BookingApp
{
    public partial class HotelDetailsPage : Page
    {
        private List<Room> _rooms = new List<Room>();
        private List<Review> _reviews = new List<Review>();
        private Hotel _selectedHotel;

        public HotelDetailsPage(Hotel selectedHotel)
        {
            InitializeComponent();

            _selectedHotel = selectedHotel;
            DataContext = _selectedHotel;

            LoadRoomsForHotel(_selectedHotel.Id);
            roomListBox.ItemsSource = _rooms;

            LoadReviewsForHotel(_selectedHotel.Id);
            reviewListBox.ItemsSource = _reviews;
        }

        public void LoadRoomsForHotel(int hotelId)
        {
            string query = "SELECT " +
                    "room.id, " +
                    "room_to_hotel.room_id, " +
                    "room_type.name AS Type, " +
                    "room.number, " +
                    "room.cost_per_night, " +
                    "availability.name AS Availability, " +
                    "room.photos " +
                    "FROM " +
                    "room_to_hotel " +
                    "JOIN " +
                    "room ON room_to_hotel.room_id = room.id " +
                    "JOIN " +
                    "room_type ON room.type = room_type.id " +
                    "JOIN " +
                    "availability ON room.availability = availability.id " +
                    "WHERE " +
                    "room_to_hotel.hotel_id = @HotelId;";
            MySqlParameter mspHotel = new MySqlParameter("@HotelId", hotelId);
            DataTable dt = DataBaseManager.ExecuteQuery(query, mspHotel);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int id = Convert.ToInt32(dt.Rows[i]["id"]);
                string number = dt.Rows[i]["number"].ToString();
                string type = dt.Rows[i]["type"].ToString();
                string costPerNight = dt.Rows[i]["cost_per_night"].ToString();
                string availability = dt.Rows[i]["availability"].ToString();

                Room room = new Room(id, number, type, costPerNight, availability);

                _rooms.Add(room);
            }
        }

        public void LoadReviewsForHotel(int hotelId)
        {
            string query = "SELECT r.id, r.text, r.grade, r.date, r.author, r.photo_link " +
                           "FROM review r " +
                           "JOIN review_to_hotel rh ON r.id = rh.review_id " +
                           "WHERE rh.hotel_id = @HotelId";
            MySqlParameter mspHotel = new MySqlParameter("@HotelId", hotelId);
            DataTable dt = DataBaseManager.ExecuteQuery(query, mspHotel);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int id = Convert.ToInt32(dt.Rows[i]["id"]);
                string text = dt.Rows[i]["text"].ToString();
                int grade = Convert.ToInt32(dt.Rows[i]["grade"]);
                DateTime date = (DateTime)dt.Rows[i]["date"];
                string author = dt.Rows[i]["author"].ToString();

                Review review = new Review(id, text, grade, date, author);

                _reviews.Add(review);
            }
        }


        private void RoomListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Room selectedRoom = (Room)roomListBox.SelectedItem;
            NavigationService.Navigate(new RoomDetailsPage(selectedRoom));
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Window.GetWindow(this)?.Close();
        }
    }
}
