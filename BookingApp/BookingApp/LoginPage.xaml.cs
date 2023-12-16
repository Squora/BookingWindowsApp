using BookingApp.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
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

            using (var connection = new MySqlConnection("SERVER=localhost;DATABASE=booking_app;UID=root;PASSWORD=root;"))
            {
                try
                {
                    connection.Open();

                    string checkUserQuery = "SELECT * FROM client WHERE (phone_number = @Login OR email = @Login) AND password = @Password;";

                    using (var cmdCheckUser = new MySqlCommand(checkUserQuery, connection))
                    {
                        cmdCheckUser.Parameters.AddWithValue("@Login", login);
                        cmdCheckUser.Parameters.AddWithValue("@Password", password);

                        using (var reader = cmdCheckUser.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                if (reader.Read())
                                {
                                    int id = reader.GetInt32("id");
                                    string firstName = reader.GetString("first_name");
                                    string lastName = reader.GetString("last_name");
                                    string middleName = reader.GetString("middle_name");
                                    string passportDetails = reader.GetString("passport_details");
                                    string phoneNumber = reader.GetString("phone_number");
                                    string email = reader.IsDBNull(reader.GetOrdinal("email")) ? null : reader.GetString("email");
                                    string userPassword = reader.GetString("password");

                                    User user = new User(id, firstName, lastName, middleName, passportDetails, phoneNumber, email, userPassword);
                                    UserManager.SetCurrentUser(user);

                                    MainWindow mainWindow = new MainWindow();
                                    mainWindow.Show();
                                    Window.GetWindow(this)?.Close();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Неверный логин или пароль. Пожалуйста, проверьте введенные данные.");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при входе: {ex.Message}");
                }
            }
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            spEnter.Visibility = Visibility.Collapsed;
            spButtons.Visibility = Visibility.Collapsed;

            mainFrame.Content = new RegistrationPage();
        }

        private void BtnForgotPassword_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
