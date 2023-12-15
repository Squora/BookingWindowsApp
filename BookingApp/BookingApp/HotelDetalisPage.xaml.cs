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
using System.Windows.Controls.Primitives;
using static System.Net.Mime.MediaTypeNames;

namespace BookingApp
{
    public partial class HotelDetalisPage : Page
    {
        private List<Room> _rooms = new List<Room>();
        private List<Review> _reviews = new List<Review>();
        private Hotel _selectedHotel;

        public HotelDetalisPage(Hotel selectedHotel)
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
            using (MySqlConnection connection = new MySqlConnection("SERVER=localhost;DATABASE=booking_app;UID=root;PASSWORD=root;"))
            {
                connection.Open();

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

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@HotelId", hotelId);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        _rooms.Clear();

                        while (reader.Read())
                        {
                            int id = reader.GetInt32(reader.GetOrdinal("Id"));
                            string number = reader.GetString(reader.GetOrdinal("Number"));
                            string type = reader.GetString(reader.GetOrdinal("Type"));
                            string costPerNight = reader.GetString(reader.GetOrdinal("Cost_Per_Night"));
                            string availability = reader.GetString(reader.GetOrdinal("Availability"));

                            Room room = new Room(id, number, type, costPerNight, availability);
                            _rooms.Add(room);
                        }
                    }
                }

                connection.Close();
            }
        }

        public void LoadReviewsForHotel(int hotelId)
        {
            using (MySqlConnection connection = new MySqlConnection("SERVER=localhost;DATABASE=booking_app;UID=root;PASSWORD=root;"))
            {
                connection.Open();

                string query = "SELECT r.id, r.text, r.grade, r.date, r.author, r.photo_link " +
                               "FROM review r " +
                               "JOIN hotel_to_review hr ON r.id = hr.review_id " +
                               "WHERE hr.hotel_id = @HotelId";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@HotelId", hotelId);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        _reviews.Clear();

                        while (reader.Read())
                        {
                            int id = reader.GetInt32(reader.GetOrdinal("id"));
                            string text = reader.GetString(reader.GetOrdinal("text"));
                            int grade = reader.GetInt32(reader.GetOrdinal("grade"));
                            DateTime date = reader.GetDateTime(reader.GetOrdinal("date"));
                            string author = reader.GetString(reader.GetOrdinal("author"));

                            Review review = new Review(id , text, grade, date, author);
                            _reviews.Add(review);
                        }
                    }
                }
            }
        }


        private void RoomListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Room selectedRoom = (Room)roomListBox.SelectedItem;

            hotelDetails.Visibility = Visibility.Collapsed;
            roomListBox.Visibility = Visibility.Collapsed;
            reviewListBox.Visibility = Visibility.Collapsed;
            mainFrame.Content = new RoomDetailsPage(selectedRoom);
        }

        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scrollViewer = (ScrollViewer)sender;
            if (e.Delta < 0)
            {
                scrollViewer.LineRight();
            }
            else
            {
                scrollViewer.LineLeft();
            }
            e.Handled = true;
        }
    }
}
