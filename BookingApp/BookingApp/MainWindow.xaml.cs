using System;
using System.Collections.Generic;
using System.Data;
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
using System.Configuration;
using MySql.Data.MySqlClient;
using BookingApp.Model;
using ZstdSharp.Unsafe;
using System.Collections.ObjectModel;

namespace BookingApp
{
    public partial class MainWindow : Window
    {
        public static string ConnectionString = "SERVER=localhost;DATABASE=booking_app;UID=root;PASSWORD=root;";
        public static MySqlConnection Connection = new MySqlConnection(ConnectionString);
        private List<Hotel> _hotels = new List<Hotel>();

        public MainWindow()
        {
            InitializeComponent();

            MySqlConnection connection = new MySqlConnection(ConnectionString);
            MySqlCommand command = new MySqlCommand("select * from hotel", connection);
            connection.Open();
            DataTable dt = new DataTable();
            dt.Load(command.ExecuteReader());
            connection.Close();

            List<Hotel> hotelList = new List<Hotel>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int id = Convert.ToInt32(dt.Rows[i]["Id"]);
                string name = dt.Rows[i]["Name"].ToString();
                string address = dt.Rows[i]["Address"].ToString();
                int numberStars = Convert.ToInt32(dt.Rows[i]["Star_count"]);
                string description = dt.Rows[i]["Description"].ToString();

                Hotel hotel = new Hotel(id, name, address, numberStars, description);

                hotelList.Add(hotel);
            }

            DataContext = this;
            _hotels = hotelList;
        }

        private void HotelListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Hotel selectedHotel = (Hotel)hotelListBox.SelectedItem;

            searchPanel.Visibility = Visibility.Collapsed;
            hotelListBox.Visibility = Visibility.Collapsed;
            mainFrame.Content = new HotelDetalisPage(selectedHotel);
        }

        private void TbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = tbSearch.Text.ToLower(); 
            List<Hotel> filteredHotels = _hotels.Where(h => h.Name.ToLower().Contains(searchText) || h.Address.ToLower().Contains(searchText)).ToList();

            hotelListBox.ItemsSource = filteredHotels;
        }
    }
}
