using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
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
using System.Xml.Linq;
using MySql.Data.MySqlClient;
using System.Data;

namespace BookingApp
{
    public partial class PasswordRecoveryPage : Page
    {
        private int _userId;

        public PasswordRecoveryPage()
        {
            InitializeComponent();

            SpCodeEnter.Visibility = Visibility.Collapsed;
            SpChangePassword.Visibility = Visibility.Collapsed;
        }

        private void BtnRecoverPassword_Click(object sender, RoutedEventArgs e)
        {
            string userLogin = TbLogin.Text;
            _userId = GetUserByEmail(userLogin);
            if (_userId > 0)
            {
                SpRecover.Visibility = Visibility.Collapsed;
                SpCodeEnter.Visibility = Visibility.Visible;
                VerificationCode.GenerateCode();
                VerificationCode.AddToDatabase();
                VerificationCode.DeleteByTime();
                string head = "Верификационный код";
                string body = $"Ваш код для верификации: {VerificationCode.Code}";
                EmailSender.Send(userLogin, head, body);
                MessageBox.Show($"Инструкции по восстановлению пароля отправлены на {userLogin}");
            }
            else
            {
                MessageBox.Show("Пользователя с такой почтой не существует");
            }
        }

        public int GetUserByEmail(string email)
        {
            string query = "SELECT id FROM user WHERE email = @Email";
            MySqlParameter mspEmail = new MySqlParameter("@Email", email);
            DataTable dt = DataBaseManager.ExecuteQuery(query, mspEmail);

            return (dt.Rows.Count > 0) ? Convert.ToInt32(dt.Rows[0]["id"]) : -1;
        }

        private void TbCode_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(VerificationCode.VerifyCode(TbCode.Text))
            {
                VerificationCode.RemoveFromDatabase();
                SpCodeEnter.Visibility = Visibility.Collapsed;
                SpChangePassword.Visibility = Visibility.Visible;
            }
        }

        private void BtnChangePassword_Click(object sender, RoutedEventArgs e)
        {
            if (PbNewPassword.Password == PbRepeatNewPassword.Password)
            {
                string hasedPassword = PasswordManager.HashPassword(PbNewPassword.Password);
                string query = "UPDATE user SET password = @NewPassword WHERE id = @UserId;";
                MySqlParameter mspPassword = new MySqlParameter("@NewPassword", hasedPassword);
                MySqlParameter mspUser = new MySqlParameter("@UserId", _userId);
                int rowAffected = DataBaseManager.ExecuteNonQuery(query, mspPassword, mspUser);

                if (rowAffected > 0)
                {
                    MessageBox.Show("Пароль успешно изменен!");

                    NavigationService.Navigate(new Uri("LoginPage.xaml", UriKind.Relative));
                }
            }
            else
            {
                MessageBox.Show("Пароли не совпадают.");
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

        private void PbNewPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            CheckAndShowPasswordStrength(PbNewPassword.Password);
        }
    }
}

