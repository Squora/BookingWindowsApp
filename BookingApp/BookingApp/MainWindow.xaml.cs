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

            DataContext = this;
        }

        public void LoadHotels()
        {
            DataTable dt = DataBaseManager.ExecuteQuery("SELECT * FROM hotel");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int id = Convert.ToInt32(dt.Rows[i]["Id"]);
                string name = dt.Rows[i]["Name"].ToString();
                string address = dt.Rows[i]["Address"].ToString();
                int numberStars = Convert.ToInt32(dt.Rows[i]["Star_count"]);
                string description = dt.Rows[i]["Description"].ToString();

                Hotel hotel = new Hotel(id, name, address, numberStars, description);

                _hotels.Add(hotel);
            }
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
            List<Hotel> filteredHotels = _hotels.Where(h => h.Name.ToLower().Contains(searchText) || h.Address.ToLower().Contains(searchText)).ToList();

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
    }
}
