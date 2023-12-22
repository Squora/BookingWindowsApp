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
using System.Security.Cryptography.Xml;
using System.Diagnostics;

namespace BookingApp
{
    public partial class MainWindow : Window
    {
        private List<Hotel> _hotels = new List<Hotel>();

        public MainWindow()
        {
            InitializeComponent();
            UpdateUserMenuVisibility();
            LoadHotels();
            SortByRating();
            DataContext = this;
        }

        public void LoadHotels()
        {
            DataTable dt = DataBaseManager.ExecuteQuery("SELECT * FROM hotel");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int id = Convert.ToInt32(dt.Rows[i]["id"]);
                string name = dt.Rows[i]["name"].ToString();
                string address = dt.Rows[i]["address"].ToString();
                int rating = Convert.ToInt32(dt.Rows[i]["rating"]);
                string description = dt.Rows[i]["description"].ToString();

                Hotel hotel = new Hotel(id, name, address, rating, description);

                _hotels.Add(hotel);
            }
        }

        private void ApplyFilters()
        {
            List<int> selectedStars = GetSelectedStars();
            List<Hotel> filteredHotels;
            if (selectedStars.Count == 0)
            {
                filteredHotels = new List<Hotel>(_hotels);
            }
            else
            {
                filteredHotels = _hotels.Where(h => selectedStars.Contains(h.Rating)).ToList();
            }

            HotelListBox.ItemsSource = filteredHotels;
        }

        private void SortByRating()
        {
            List<Hotel> sortedHotels = _hotels;
            sortedHotels.Sort();
            sortedHotels.Reverse();

            HotelListBox.ItemsSource = sortedHotels;
        }

        private void StarFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyFilters();
        }

        private void HotelListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Hotel selectedHotel = (Hotel)HotelListBox.SelectedItem;

            HotelListBox.Visibility = Visibility.Collapsed;
            SpFilters.Visibility = Visibility.Collapsed;
            TbSearch.Visibility = Visibility.Collapsed;
            MainFrame.NavigationService.Navigate(new HotelDetailsPage(selectedHotel));
        }

        private void TbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = TbSearch.Text.ToLower();
            List<Hotel> filteredHotels = _hotels
                .Where(hotel => hotel.Name.ToLower().Contains(searchText) || hotel.Address.ToLower().Contains(searchText)).ToList();

            HotelListBox.ItemsSource = filteredHotels;
        }

        private void BtnUserMenu_Click(object sender, RoutedEventArgs e)
        {
            PopupUserMenu.IsOpen = !PopupUserMenu.IsOpen;
        }

        private void UpdateUserMenuVisibility()
        {
            if (!UserManager.IsLoggedIn)
            {
                BtnLogin.Visibility = Visibility.Visible;
                BtnRegister.Visibility = Visibility.Visible;

                BtnProfile.Visibility = Visibility.Collapsed;
                BtnSettings.Visibility = Visibility.Collapsed;
                BtnLogout.Visibility = Visibility.Collapsed;
            }
            else
            {
                BtnLogin.Visibility = Visibility.Collapsed;
                BtnRegister.Visibility = Visibility.Collapsed;

                BtnProfile.Visibility = Visibility.Visible;
                BtnSettings.Visibility = Visibility.Visible;
                BtnLogout.Visibility = Visibility.Visible;
            }
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            SpMain.Visibility = Visibility.Collapsed;
            HotelListBox.Visibility = Visibility.Collapsed;
            PopupUserMenu.IsOpen = !PopupUserMenu.IsOpen;

            MainFrame.NavigationService.Navigate(new Uri("LoginPage.xaml", UriKind.Relative));
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            SpMain.Visibility = Visibility.Collapsed;
            HotelListBox.Visibility = Visibility.Collapsed;
            PopupUserMenu.IsOpen = !PopupUserMenu.IsOpen;

            MainFrame.NavigationService.Navigate(new Uri("RegistrationPage.xaml", UriKind.Relative));
        }

        private void BtnProfile_Click(object sender, RoutedEventArgs e)
        {
            SpMain.Visibility = Visibility.Collapsed;
            HotelListBox.Visibility = Visibility.Collapsed;
            PopupUserMenu.IsOpen = !PopupUserMenu.IsOpen;

            MainFrame.NavigationService.Navigate(new Uri("ProfilePage.xaml", UriKind.Relative));
        }

        private void BtnSettings_Click(object sender, RoutedEventArgs e)
        {
            SpMain.Visibility = Visibility.Collapsed;
            HotelListBox.Visibility = Visibility.Collapsed;
            PopupUserMenu.IsOpen = !PopupUserMenu.IsOpen;

            MainFrame.NavigationService.Navigate(new Uri("SettingsPage.xaml", UriKind.Relative));
        }

        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            UserManager.ResetUser();
            PopupUserMenu.IsOpen = !PopupUserMenu.IsOpen;

            UpdateUserMenuVisibility();
        }

        private List<int> GetSelectedStars()
        {
            List<int> selectedStars = new List<int>();

            if (Cb1Star.IsChecked == true)
                selectedStars.Add(1);
            if (Cb2Stars.IsChecked == true)
                selectedStars.Add(2);
            if (Cb3Stars.IsChecked == true)
                selectedStars.Add(3);
            if (Cb4Stars.IsChecked == true)
                selectedStars.Add(4);
            if (Cb5Stars.IsChecked == true)
                selectedStars.Add(5);

            return selectedStars;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            ApplyFilters();
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            ApplyFilters();
        }
    }
}
