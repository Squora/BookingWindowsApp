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
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
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

                    string checkUserQuery = "SELECT COUNT(*) FROM client WHERE (phone_number = @Login OR email = @Login) AND password = @Password;";

                    using (var cmdCheckUser = new MySqlCommand(checkUserQuery, connection))
                    {
                        cmdCheckUser.Parameters.AddWithValue("@Login", login);
                        cmdCheckUser.Parameters.AddWithValue("@Password", password);

                        int userCount = Convert.ToInt32(cmdCheckUser.ExecuteScalar());

                        if (userCount > 0)
                        {
                            MessageBox.Show("Вход выполнен успешно!");
                        }
                        else
                        {
                            MessageBox.Show("Неверный логин или пароль. Пожалуйста, проверьте введенные данные.");
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

        }

        private void BtnForgotPassword_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
