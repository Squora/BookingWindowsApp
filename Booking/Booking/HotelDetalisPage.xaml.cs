using Booking.Model;
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

namespace Booking
{
    public partial class HotelDetalisPage : Page
    {
        private List<Room> _rooms = new List<Room>();
        private Hotel _selectedHotel;

        public HotelDetalisPage(Hotel selectedHotel)
        {
            _selectedHotel = selectedHotel;

            InitializeComponent();

            DataContext = _selectedHotel;

            LoadRoomsForHotel(_selectedHotel.Id);

            roomListBox.ItemsSource = _rooms;
        }

        public void LoadRoomsForHotel(int hotelId)
        {
            using (MySqlConnection connection = new MySqlConnection("SERVER=localhost;DATABASE=booking_app;UID=root;PASSWORD=root;"))
            {
                connection.Open();

                //string query = "SELECT room.* FROM room JOIN room_to_hotel ON room.id = room_to_hotel.room_id " +
                //    "WHERE room_to_hotel.hotel_id = @HotelId;";

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

        private void RoomListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
