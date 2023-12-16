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
        private User _user;
        private const int _awaitingConfirmationStatus = 1;

        public RoomDetailsPage(Room selectedRoom)
        {
            InitializeComponent();

            _selectedRoom = selectedRoom;
            _user = UserManager.CurrentUser;

            roomDetails.DataContext = _selectedRoom;
        }

        private void BtnBook_Click(object sender, RoutedEventArgs e)
        {
            if (IsDatesCorrect())
            {
                string insertQuery = "INSERT INTO booking (user_id, room_id, start_stay_date, end_stay_date, status)" +
                            $" VALUES (@UserId, @RoomId, @StartStayDate, @EndStayDate, @Status);";
                MySqlParameter mspUser = new MySqlParameter("@UserId", _user.Id);
                MySqlParameter mspRoom = new MySqlParameter("@RoomId", _selectedRoom.Id);
                MySqlParameter mspStartDate = new MySqlParameter("@StartStayDate", startDatePicker.SelectedDate);
                MySqlParameter mspEndDate = new MySqlParameter("@EndStayDate", endDatePicker.SelectedDate);
                MySqlParameter mspStatus = new MySqlParameter("@Status", _awaitingConfirmationStatus);
                int rowAffected = DataBaseManager.ExecuteNonQuery(insertQuery, mspUser, mspRoom, mspStartDate, mspEndDate, mspStatus);
                if (rowAffected > 0)
                {
                    MessageBox.Show("Комната успешно забронирована!");
                }
                else
                {
                    MessageBox.Show("Произошла ошибка");
                }
            }
            else
            {
                MessageBox.Show("Выберите даты пребывания");
            }
        }

        private bool IsDatesCorrect()
        {
            return startDatePicker.SelectedDate != null && endDatePicker.SelectedDate != null &&
                startDatePicker.SelectedDate > DateTime.Now && endDatePicker.SelectedDate > startDatePicker.SelectedDate;
        }
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
