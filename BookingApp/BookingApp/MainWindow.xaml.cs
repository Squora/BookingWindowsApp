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


        private bool _isChecked1Star;
        public bool IsChecked1Star
        {
            get { return _isChecked1Star; }
            set
            {
                if (_isChecked1Star != value)
                {
                    _isChecked1Star = value;
                }
            }
        }

        private bool _isChecked2Stars;
        public bool IsChecked2Stars
        {
            get { return _isChecked2Stars; }
            set
            {
                if (_isChecked2Stars != value)
                {
                    _isChecked2Stars = value;
                }
            }
        }

        private bool _isChecked3Stars;
        public bool IsChecked3Stars
        {
            get { return _isChecked3Stars; }
            set
            {
                if (_isChecked3Stars != value)
                {
                    _isChecked3Stars = value;
                }
            }
        }

        private bool _isChecked4Stars;
        public bool IsChecked4Stars
        {
            get { return _isChecked4Stars; }
            set
            {
                if (_isChecked4Stars != value)
                {
                    _isChecked4Stars = value;
                }
            }
        }

        private bool _isChecked5Stars;
        public bool IsChecked5Stars
        {
            get { return _isChecked5Stars; }
            set
            {
                if (_isChecked5Stars != value)
                {
                    _isChecked5Stars = value;
                }
            }
        }

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
            List<Hotel> filteredHotels;
            if (!IsChecked1Star && !IsChecked2Stars && !IsChecked3Stars && !IsChecked4Stars && !IsChecked5Stars)
            {
                filteredHotels = new List<Hotel>(_hotels);
            }
            else
            {
                filteredHotels = _hotels
                    .Where(hotel => (IsChecked1Star && hotel.Rating == 1) || (IsChecked2Stars && hotel.Rating == 2)
                    || (IsChecked3Stars && hotel.Rating == 3) || (IsChecked4Stars && hotel.Rating == 4) || (IsChecked5Stars && hotel.Rating == 5))
                    .ToList();
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

            SearchPanel.Visibility = Visibility.Collapsed;
            HotelListBox.Visibility = Visibility.Collapsed;
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
            if (UserManager.CurrentUser == null)
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
            SearchPanel.Visibility = Visibility.Collapsed;
            HotelListBox.Visibility = Visibility.Collapsed;
            PopupUserMenu.IsOpen = !PopupUserMenu.IsOpen;

            MainFrame.NavigationService.Navigate(new Uri("LoginPage.xaml", UriKind.Relative));
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            SearchPanel.Visibility = Visibility.Collapsed;
            HotelListBox.Visibility = Visibility.Collapsed;
            PopupUserMenu.IsOpen = !PopupUserMenu.IsOpen;

            MainFrame.NavigationService.Navigate(new Uri("RegistrationPage.xaml", UriKind.Relative));
        }

        private void BtnProfile_Click(object sender, RoutedEventArgs e)
        {
            SearchPanel.Visibility = Visibility.Collapsed;
            HotelListBox.Visibility = Visibility.Collapsed;
            PopupUserMenu.IsOpen = !PopupUserMenu.IsOpen;

            MainFrame.NavigationService.Navigate(new Uri("ProfilePage.xaml", UriKind.Relative));
        }

        private void BtnSettings_Click(object sender, RoutedEventArgs e)
        {
            SearchPanel.Visibility = Visibility.Collapsed;
            HotelListBox.Visibility = Visibility.Collapsed;
            PopupUserMenu.IsOpen = !PopupUserMenu.IsOpen;

            MainFrame.NavigationService.Navigate(new Uri("SettingsPage.xaml", UriKind.Relative));
        }

        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            UserManager.Logout();
            PopupUserMenu.IsOpen = !PopupUserMenu.IsOpen;

            UpdateUserMenuVisibility();
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
