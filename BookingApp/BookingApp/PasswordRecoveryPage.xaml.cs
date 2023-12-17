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
using Org.BouncyCastle.Utilities.Net;
using System.Xml.Linq;
using BookingApp.Model;
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
                string _code = GenerateRecoveryCode();
                string query = "INSERT INTO password_reset_codes(user_id, code) VALUES(@UserId, @Code)";
                MySqlParameter mspUser = new MySqlParameter("@UserId", _userId);
                MySqlParameter mspCode = new MySqlParameter("@Code", _code);
                DataBaseManager.ExecuteNonQuery(query, mspUser, mspCode);
                SpRecover.Visibility = Visibility.Collapsed;
                SpCodeEnter.Visibility = Visibility.Visible;
                SendRecoveryEmail(userLogin, _code);
                MessageBox.Show($"Инструкции по восстановлению пароля отправлены на {userLogin}");
            }
            else
            {
                MessageBox.Show("Пользователя с такой почтой не существует");
            }
        }

        public static string GenerateRecoveryCode()
        {
            int length = 4;
            const string validChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

            Random random = new Random();
            StringBuilder code = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                int index = random.Next(validChars.Length);
                code.Append(validChars[index]);
            }

            return code.ToString();
        }

        public int GetUserByEmail(string email)
        {
            string query = "SELECT id FROM user WHERE email = @Email";
            MySqlParameter mspEmail = new MySqlParameter("@Email", email);
            DataTable dt = DataBaseManager.ExecuteQuery(query, mspEmail);

            return (dt.Rows.Count > 0) ? Convert.ToInt32(dt.Rows[0]["id"]) : -1;
        }

        private void SendRecoveryEmail(string toEmail, string code)
        {
            string smtpServer = "smtp.yandex.ru";
            int smtpPort = 587;
            string senderEmail = "t.testeron@yandex.ru";
            string senderPassword = "kncszmpjfqlhlert";

            using (SmtpClient client = new SmtpClient(smtpServer, smtpPort))
            {
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(senderEmail, senderPassword);
                client.EnableSsl = true;

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(senderEmail);
                mail.To.Add(toEmail);
                mail.Subject = code;
                //mail.Body = body;

                client.Send(mail);
            }
        }

        private void TbCode_TextChanged(object sender, TextChangedEventArgs e)
        {
            string query = "SELECT code FROM password_reset_codes WHERE user_id = @UserId";
            MySqlParameter mspUser = new MySqlParameter("@UserId", _userId);
            DataTable dt = DataBaseManager.ExecuteQuery(query, mspUser);
            if (TbCode.Text == dt.Rows[0]["code"].ToString())
            {
                SpCodeEnter.Visibility = Visibility.Collapsed;
                SpChangePassword.Visibility = Visibility.Visible;
                string deleteQuery = $"DELETE FROM password_reset_codes WHERE user_id = @UserId";
                DataBaseManager.ExecuteQuery(deleteQuery, mspUser);
            }
        }

        private void BtnChangePassword_Click(object sender, RoutedEventArgs e)
        {
            if (TbNewPassword.Text == TbRepeatNewPassword.Text)
            {
                string query = "UPDATE user SET password = @NewPassword WHERE id = @UserId;";
                MySqlParameter mspPassword = new MySqlParameter("@NewPassword", TbNewPassword.Text);
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
    }
}

