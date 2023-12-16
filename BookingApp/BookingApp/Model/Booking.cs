using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Model
{
    public class Booking
    {
        public int Id { get; set; }
        public string HotelName { get; set; }
        public int RoomNumber { get; set; }
        public DateTime StartStayDate { get; set; }
        public DateTime EndStayDate {  get; set; }
        public string Status { get; set; }

        public Booking(int id, string hotelName, int roomNumber, DateTime startStayDate, DateTime endStayDate, string status)
        {
            Id = id;
            HotelName = hotelName;
            RoomNumber = roomNumber;
            StartStayDate = startStayDate;
            EndStayDate = endStayDate;
            Status = status;
        }
    }
}
