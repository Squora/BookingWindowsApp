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
    public partial class RegistrationPage : Page
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            string firstName = TbFirstName.Text;
            string lastName = TbLastName.Text;
            string middleName = TbMiddleName.Text;
            string passportDetails = TbPassportDetails.Text;
            string phone = TbPhoneNumber.Text;
            string email = TbEmail.Text;
            string password = PasswordBox.Password;

            using (var connection = new MySqlConnection("SERVER=localhost;DATABASE=booking_app;UID=root;PASSWORD=root;"))
            {
                try
                {
                    connection.Open();

                    string checkUserQuery = "SELECT COUNT(*) FROM client WHERE (phone_number = @Phone or passport_details = @PassportDetails);";

                    using (var cmdCheckUser = new MySqlCommand(checkUserQuery, connection))
                    {
                        cmdCheckUser.Parameters.AddWithValue("@Phone", phone);
                        cmdCheckUser.Parameters.AddWithValue("@PassportDetails", passportDetails);

                        int userCount = Convert.ToInt32(cmdCheckUser.ExecuteScalar());

                        if (userCount > 0)
                        {
                            MessageBox.Show("Пользователь с указанным телефоном уже зарегистрирован");
                        }
                        else
                        {
                            string insertUserQuery = "INSERT INTO client (first_name, last_name, middle_name, passport_details, phone_number, email, password) " +
                                                     "VALUES (@FirstName, @LastName, @MiddleName, @PassportDetails, @Phone, @Email, @Password);";

                            using (var cmdInsertUser = new MySqlCommand(insertUserQuery, connection))
                            {
                                cmdInsertUser.Parameters.AddWithValue("@FirstName", firstName);
                                cmdInsertUser.Parameters.AddWithValue("@LastName", lastName);
                                cmdInsertUser.Parameters.AddWithValue("@MiddleName", middleName);
                                cmdInsertUser.Parameters.AddWithValue("@PassportDetails", passportDetails);
                                cmdInsertUser.Parameters.AddWithValue("@Phone", phone);
                                cmdInsertUser.Parameters.AddWithValue("@Email", string.IsNullOrEmpty(email) ? null : email);
                                cmdInsertUser.Parameters.AddWithValue("@Password", password);

                                cmdInsertUser.ExecuteNonQuery();

                                MessageBox.Show("Регистрация успешно завершена!");
                            }

                            svMain.Visibility = Visibility.Collapsed; 
                            mainFrame.Content = new LoginPage();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при регистрации: {ex.Message}");
                }
            }
        }
    }
}
