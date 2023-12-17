using BookingApp.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
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
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTextBox.Text;
            string password = PasswordBox.Password;

            string query = "SELECT * FROM user WHERE (phone_number = @Login OR email = @Login) AND password = @Password;";
            MySqlParameter mspLogin = new MySqlParameter("@Login", login);
            MySqlParameter mspPassword = new MySqlParameter("@Password", password);
            DataTable dt = DataBaseManager.ExecuteQuery(query, mspLogin, mspPassword);

            if (dt.Rows.Count > 0)
            {
                int id = Convert.ToInt32(dt.Rows[0]["id"]);
                string firstName = dt.Rows[0]["first_name"].ToString();
                string lastName = dt.Rows[0]["last_name"].ToString();
                string middleName = dt.Rows[0]["middle_name"].ToString();
                string passportDetails = dt.Rows[0]["passport_details"].ToString();
                string phoneNumber = dt.Rows[0]["phone_number"].ToString();
                string email = dt.Rows[0]["email"].ToString();
                string userPassword = dt.Rows[0]["password"].ToString();

                User user = new User(id, firstName, lastName, middleName, passportDetails, phoneNumber, email, userPassword);
                UserManager.SetCurrentUser(user);

                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Window.GetWindow(this)?.Close();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль!");
            }
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            spEnter.Visibility = Visibility.Collapsed;
            spButtons.Visibility = Visibility.Collapsed;

            NavigationService.Navigate(new Uri("RegistrationPage.xaml", UriKind.Relative));
        }

        private void BtnForgotPassword_Click(object sender, RoutedEventArgs e)
        {
            spEnter.Visibility = Visibility.Collapsed;
            spButtons.Visibility = Visibility.Collapsed;

            NavigationService.Navigate(new Uri("PasswordRecoveryPage.xaml", UriKind.Relative));
        }
    }
}
