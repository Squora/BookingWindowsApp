using BookingApp.Model;
using MySql.Data.MySqlClient;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace BookingApp
{
    public partial class RoomDetailsPage : Page
    {
        private Room _selectedRoom;
        private const int _awaitingConfirmationStatus = 1;

        public RoomDetailsPage(Room selectedRoom)
        {
            InitializeComponent();

            _selectedRoom = selectedRoom;

            roomDetails.DataContext = _selectedRoom;
        }

        private void BtnBook_Click(object sender, RoutedEventArgs e)
        {
            if (startDatePicker.SelectedDate != null)
            {
                using (var connection = new MySqlConnection("SERVER=localhost;DATABASE=booking_app;UID=root;PASSWORD=root;"))
                {
                    try
                    {
                        connection.Open();

                        string insertBookingQuery = "INSERT INTO booking (client_id, room_id, start_stay_date, end_stay_date, status)" +
                            " VALUES (@ClientId, @RoomId, @StartStayDate, @EndStayDate, @Status);";

                        using (var cmd = new MySqlCommand(insertBookingQuery, connection))
                        {
                            cmd.Parameters.AddWithValue("@ClientId", 1);
                            cmd.Parameters.AddWithValue("@RoomId", _selectedRoom.Id);
                            cmd.Parameters.AddWithValue("@StartStayDate", startDatePicker.SelectedDate);
                            cmd.Parameters.AddWithValue("@EndStayDate", endDatePicker.SelectedDate);
                            cmd.Parameters.AddWithValue("@Status", _awaitingConfirmationStatus);
                            cmd.ExecuteNonQuery();
                        }

                        MessageBox.Show("Комната успешно забронирована!");

                        connection.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при бронировании комнаты: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите даты пребывания");
            }
        }
    }
}
