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
using System.Windows.Markup;

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
            LbRooms.ItemsSource = _rooms;

            LoadReviewsForHotel(_selectedHotel.Id);
            LbReviews.ItemsSource = _reviews;
        }

        private void LbRooms_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scrollViewer = FindChild<ScrollViewer>(LbRooms);

            if (scrollViewer != null)
            {
                if (e.Delta > 0)
                {
                    scrollViewer.LineLeft();
                }
                else
                {
                    scrollViewer.LineRight();
                }

                e.Handled = true;
            }
        }

        private T? FindChild<T>(DependencyObject parent) where T : DependencyObject
        {
            if (parent == null) return null;

            int childCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childCount; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);

                if (child is T)
                {
                    return (T)child;
                }

                T result = FindChild<T>(child);
                if (result != null)
                {
                    return result;
                }
            }

            return null;
        }

    public void LoadRoomsForHotel(int hotelId)
        {
            string query = "SELECT " +
                    "room.id, " +
                    "room_type.name AS type, " +
                    "room.number, " +
                    "room.cost_per_night, " +
                    "availability.name AS availability, " +
                    "room.photos " +
                    "FROM " +
                    "room " +
                    "JOIN " +
                    "room_type ON room.type = room_type.id " +
                    "JOIN " +
                    "availability ON room.availability = availability.id " +
                    "WHERE " +
                    "room.hotel_id = @HotelId;";
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
            string query = "SELECT " +
                "review.id," +
                "review.text, " +
                "review.grade, " +
                "review.date, " +
                "CONCAT(user.first_name, ' ', user.last_name, ' ', user.middle_name) AS full_name, " +
                "review.photo_link " +
                "FROM " +
                "review " +
                "JOIN " +
                "user ON review.user_id = user.id " +
                "WHERE " +
                "review.hotel_id = @hotelId;";
            MySqlParameter mspHotel = new MySqlParameter("@HotelId", hotelId);
            DataTable dt = DataBaseManager.ExecuteQuery(query, mspHotel);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int id = Convert.ToInt32(dt.Rows[i]["id"]);
                string fullName = dt.Rows[i]["full_name"].ToString();
                string text = dt.Rows[i]["text"].ToString();
                int grade = Convert.ToInt32(dt.Rows[i]["grade"]);
                DateTime date = (DateTime)dt.Rows[i]["date"];

                Review review = new Review(id, fullName, text, grade, date);

                _reviews.Add(review);
            }
        }

        private void RoomListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Room selectedRoom = (Room)LbRooms.SelectedItem;
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
