using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;
using System.Timers;
using System.Data;

namespace BookingApp
{
    public static class EmailSender
    {
        private static readonly string _smtpServer = "smtp.yandex.ru"; 
        private static readonly int _smtpPort = 587; 
        private static readonly string _senderEmail = "t.testeron@yandex.ru";
        private static readonly string _senderPassword = "kncszmpjfqlhlert";

        public static void Send(string recipientEmail, string head, string body)
        {
            VerificationCode.GenerateCode();
            VerificationCode.AddToDatabase();
            VerificationCode.DeleteByTime();
            try
            {
                using (SmtpClient client = new SmtpClient(_smtpServer, _smtpPort))
                {
                    client.EnableSsl = true;
                    client.Credentials = new NetworkCredential(_senderEmail, _senderPassword);

                    MailMessage mailMessage = new MailMessage(_senderEmail, recipientEmail)
                    {
                        Subject = head,
                        Body = body
                    };

                    client.Send(mailMessage);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при выполнении запроса: {ex.Message}");
                throw;
            }
        }
    }
}
