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
using Booking.Model;
using ZstdSharp.Unsafe;
using System.Collections.ObjectModel;
using Booking.ViewModel;

namespace Booking
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Hotel> Hotels { get; set; }
        public static string ConnectionString = "SERVER=localhost;DATABASE=booking_app;UID=root;PASSWORD=root;";
        public static MySqlConnection Connection = new MySqlConnection(ConnectionString);
        private List<Hotel> allHotels;

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
            Hotels = new ObservableCollection<Hotel>(hotelList);
        }

        private void HotelListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Hotel selectedHotel = (Hotel)hotelListBox.SelectedItem;

            if (selectedHotel != null)
            {
                searchPanel.Visibility = Visibility.Collapsed;
                hotelListBox.Visibility = Visibility.Collapsed;

                backButton.Visibility = Visibility.Visible;
                hotelDetailsPanel.Visibility = Visibility.Visible;

                hotelDetailsPanel.DataContext = selectedHotel;
            }
            else
            {
                searchPanel.Visibility = Visibility.Visible;
                hotelListBox.Visibility = Visibility.Visible;

                backButton.Visibility = Visibility.Collapsed;
                hotelDetailsPanel.Visibility = Visibility.Collapsed;
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchText = tbSearch.Text.ToLower();

            if (allHotels == null)
            {
                allHotels = ((IEnumerable<Hotel>)hotelListBox.ItemsSource).ToList();
            }

            var filteredHotels = allHotels
                .Where(hotel => hotel.Name.ToLower().StartsWith(searchText))
                .OrderBy(hotel => hotel.Name)
                .ToList();

            hotelListBox.ItemsSource = filteredHotels;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            hotelListBox.SelectedItem = null;

            HotelListBox_SelectionChanged(hotelListBox, null);
        }
    }
}
