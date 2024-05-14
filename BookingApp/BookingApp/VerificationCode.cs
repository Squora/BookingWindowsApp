using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace BookingApp
{
    public class VerificationCode 
    {
        public static string? Login { get; set; }
        public static string? Code { get; set; }

        public static void GenerateCode()
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

            Code = code.ToString();
        }

        public static void DeleteByTime()
        {
            Timer timer = new Timer(1 * 60 * 1000);
            timer.Elapsed += (sender, e) => RemoveFromDatabase();
            timer.AutoReset = false;
            timer.Start();
        }

        public static bool VerifyCode(string enteredCode)
        {
            string query = "SELECT code FROM verification_codes WHERE email = @Email";
            MySqlParameter mspEmail = new MySqlParameter("@Email", Login);
            DataTable dt = DataBaseManager.ExecuteQuery(query, mspEmail);
            if (enteredCode == dt.Rows[0]["code"].ToString())
            {
                return true;
            }
            return false;
        }

        public static void AddToDatabase()
        {
            string query = "INSERT INTO verification_codes(email, code) VALUES(@Email, @Code)";
            MySqlParameter mspEmail = new MySqlParameter("@Email", Login);
            MySqlParameter mspCode = new MySqlParameter("@Code", Code);
            DataBaseManager.ExecuteNonQuery(query, mspEmail, mspCode);
        }

        public static void RemoveFromDatabase()
        {
            string deleteQuery = $"DELETE FROM verification_codes WHERE email = @Email";
            MySqlParameter mspEmail = new MySqlParameter("@Email", Login);
            DataBaseManager.ExecuteQuery(deleteQuery, mspEmail);
        }
    }
}
