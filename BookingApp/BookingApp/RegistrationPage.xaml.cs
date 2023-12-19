using MySql.Data.MySqlClient;
using System;
using System.Collections;
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

            string query = "INSERT INTO user (first_name, last_name, middle_name, passport_details, phone_number, email, password) " +
                                            "VALUES (@FirstName, @LastName, @MiddleName, @PassportDetails, @Phone, @Email, @Password);";
            MySqlParameter mspFirstName = new MySqlParameter("@FirstName", firstName);
            MySqlParameter mspLastName = new MySqlParameter("@LastName", lastName);
            MySqlParameter mspMiddleName = new MySqlParameter("@MiddleName", middleName);
            MySqlParameter mspPassportDetails = new MySqlParameter("@PassportDetails", passportDetails);
            MySqlParameter mspPhone = new MySqlParameter("@Phone", phone);
            MySqlParameter mspEmail = new MySqlParameter("@Email", email);
            MySqlParameter mspPassword = new MySqlParameter("@Password", password);

            if (!IsUserExist(phone, passportDetails) && IsPasswordStrong(password))
            {
                DataBaseManager.ExecuteNonQuery(query, mspFirstName, mspLastName, mspMiddleName, mspPassportDetails, mspPhone, mspEmail, mspPassword);
                MessageBox.Show("Регистрация успешно завершена!");

                //svMain.Visibility = Visibility.Collapsed;
                NavigationService.Navigate(new Uri("LoginPage.xaml", UriKind.Relative));
            }
        }

        private bool IsUserExist(string phone, string passportDetails)
        {
            string query = "SELECT COUNT(*) FROM user WHERE (phone_number = @Phone or passport_details = @PassportDetails);";
            MySqlParameter mspPhone = new MySqlParameter("@Phone", phone);
            MySqlParameter mspPassportDetails = new MySqlParameter("@PassportDetails", passportDetails);
            DataTable dt = DataBaseManager.ExecuteQuery(query, mspPhone, mspPassportDetails);

            int userCount = Convert.ToInt32(dt.Rows[0]["COUNT(*)"]);

            if (userCount > 0)
            {
                MessageBox.Show("Пользователь с указанным телефоном уже зарегистрирован");
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsPasswordStrong(string password)
        {
            PasswordValidator.Strength strength = PasswordValidator.CheckStrength(password);
            bool isStrong = false;

            switch (strength)
            {
                case PasswordValidator.Strength.Weak:
                    LblPasswordStrength.Content = "Слабый";
                    LblPasswordStrength.Foreground = Brushes.Red;
                    isStrong = false;
                    break;
                case PasswordValidator.Strength.Medium:
                    LblPasswordStrength.Content = "Средний";
                    LblPasswordStrength.Foreground = Brushes.Orange;
                    isStrong = false;
                    break;
                case PasswordValidator.Strength.Strong:
                    LblPasswordStrength.Content = "Сильный";
                    LblPasswordStrength.Foreground = Brushes.Green;
                    isStrong = true;
                    break;
            }

            return isStrong;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Window.GetWindow(this)?.Close();
        }
    }
}
