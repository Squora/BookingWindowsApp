using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
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
            string hasedPassword = PasswordManager.HashPassword(password);

            string query = "INSERT INTO user (first_name, last_name, middle_name, passport_details, phone_number, email, password) " +
                                            "VALUES (@FirstName, @LastName, @MiddleName, @PassportDetails, @Phone, @Email, @Password);";
            MySqlParameter mspFirstName = new MySqlParameter("@FirstName", firstName);
            MySqlParameter mspLastName = new MySqlParameter("@LastName", lastName);
            MySqlParameter mspMiddleName = new MySqlParameter("@MiddleName", middleName);
            MySqlParameter mspPassportDetails = new MySqlParameter("@PassportDetails", passportDetails);
            MySqlParameter mspPhone = new MySqlParameter("@Phone", phone);
            MySqlParameter mspEmail = new MySqlParameter("@Email", email);
            MySqlParameter mspPassword = new MySqlParameter("@Password", hasedPassword);

            if (!IsUserExist(phone, passportDetails) && VerificationCode.VerifyCode(TbCheckCode.Text))
            {
                VerificationCode.RemoveFromDatabase();
                LblCheckCode.Visibility = Visibility.Visible;
                TbCheckCode.Visibility = Visibility.Visible;
                DataBaseManager.ExecuteNonQuery(query, mspFirstName, mspLastName, mspMiddleName, mspPassportDetails, mspPhone, mspEmail, mspPassword);

                MessageBox.Show("Регистрация успешно завершена!");
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

        private void CheckAndShowPasswordStrength(string password)
        {
            PasswordManager.Strength strength = PasswordManager.CheckStrength(password);

            switch (strength)
            {
                case PasswordManager.Strength.Weak:
                    LblPasswordStrength.Content = "Слабый";
                    LblPasswordStrength.Foreground = Brushes.Red;
                    break;
                case PasswordManager.Strength.Medium:
                    LblPasswordStrength.Content = "Средний";
                    LblPasswordStrength.Foreground = Brushes.Orange;
                    break;
                case PasswordManager.Strength.Strong:
                    LblPasswordStrength.Content = "Сильный";
                    LblPasswordStrength.Foreground = Brushes.Green;
                    break;
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Window.GetWindow(this)?.Close();
        }

        private void TbFirstName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !ContainsOnlyLetters(e.Text);
            string text;
            if (e.Text.Length < 1)
            {
                text = e.Text.Trim().ToUpper();
            }
            else
            {
                text = e.Text.Trim().ToLower();
            }

            e.Source = text;
        }

        private void TbLastName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !ContainsOnlyLetters(e.Text);
        }

        private void TbMiddleName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !ContainsOnlyLetters(e.Text);
        }

        private void TbPassportDetails_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !ContainsOnlyDigits(e.Text);

            TextBox textBox = (TextBox)sender;
            if (textBox.Text.Length == 4)
            {
                textBox.Text += " ";
                textBox.CaretIndex = textBox.Text.Length;
            }
        }

        private void TbPhoneNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !ContainsOnlyDigits(e.Text);
        }

        private void TbEmail_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Text.Contains("@") && e.Text == "@")
            {
                e.Handled = true;
            }
        }

        private bool ContainsOnlyDigits(string input)
        {
            return input.All(char.IsDigit);
        }

        private bool ContainsOnlyLetters(string input)
        {
            return input.All(char.IsLetter);
        }

        public bool IsEmailValid(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        private void TbEmail_LostFocus(object sender, RoutedEventArgs e)
        {
            if (IsEmailValid(TbEmail.Text))
            {
                VerificationCode.GenerateCode();
                VerificationCode.AddToDatabase();
                VerificationCode.DeleteByTime();
                string head = "Верификационный код";
                string body = $"Ваш код для верификации: {VerificationCode.Code}";
                EmailSender.Send(TbEmail.Text, head, body);
                LblCheckCode.Visibility = Visibility.Visible;
                TbCheckCode.Visibility = Visibility.Visible;
            }
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            CheckAndShowPasswordStrength(PasswordBox.Password);
        }
    }
}
