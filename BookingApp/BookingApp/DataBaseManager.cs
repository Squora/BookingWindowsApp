using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp
{
    public static class DataBaseManager
    {
        private static readonly string _connection = "SERVER=localhost;DATABASE=booking_app;UID=root;PASSWORD=root;";

        public static DataTable ExecuteQuery(string query)
        {
            using (MySqlConnection connection = new MySqlConnection(_connection))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        DataTable dataTable = new DataTable();
                        dataTable.Load(command.ExecuteReader());
                        return dataTable;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при выполнении запроса: {ex.Message}");
                        throw;
                    }
                }
            }
        }

        public static DataTable ExecuteQuery(string query, params MySqlParameter[] parameters)
        {
            using (MySqlConnection connection = new MySqlConnection(_connection))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    try
                    {
                        command.Parameters.AddRange(parameters);
                        connection.Open();
                        DataTable dataTable = new DataTable();
                        dataTable.Load(command.ExecuteReader());
                        return dataTable;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при выполнении запроса: {ex.Message}");
                        throw;
                    }
                }
            }
        }

        public static void ExecuteNonQuery(string query)
        {
            using (MySqlConnection connection = new MySqlConnection(_connection))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при выполнении запроса: {ex.Message}");
                        throw;
                    }
                }
            }
        }

        public static int ExecuteNonQuery(string query, params MySqlParameter[] parameters)
        {
            using (MySqlConnection connection = new MySqlConnection(_connection))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    try
                    {
                        command.Parameters.AddRange(parameters);
                        connection.Open();
                        return command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при выполнении запроса: {ex.Message}");
                        throw;
                    }
                }
            }
        }
    }
}
