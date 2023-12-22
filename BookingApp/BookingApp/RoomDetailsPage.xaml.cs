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
            _user = UserManager.GetUser();

            SpRoomDetails.DataContext = _selectedRoom;
        }

        private void BtnBook_Click(object sender, RoutedEventArgs e)
        {
            if (UserManager.IsLoggedIn)
            {
                if (IsDatesCorrect())
                {
                    string insertQuery = "INSERT INTO booking (user_id, room_id, start_stay_date, end_stay_date, status)" +
                                $" VALUES (@UserId, @RoomId, @StartStayDate, @EndStayDate, @Status);";
                    MySqlParameter mspUser = new MySqlParameter("@UserId", _user.Id);
                    MySqlParameter mspRoom = new MySqlParameter("@RoomId", _selectedRoom.Id);
                    MySqlParameter mspStartDate = new MySqlParameter("@StartStayDate", DpStart.SelectedDate);
                    MySqlParameter mspEndDate = new MySqlParameter("@EndStayDate", DpEnd.SelectedDate);
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
            else
            {
                MessageBox.Show("Вы не вошли в аккаунт");
            }
        }

        private bool IsDatesCorrect()
        {
            return DpStart.SelectedDate != null && DpEnd.SelectedDate != null &&
                DpStart.SelectedDate > DateTime.Now && DpEnd.SelectedDate > DpStart.SelectedDate;
        }
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
