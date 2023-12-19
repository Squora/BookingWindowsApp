using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Model
{
    public class Booking
    {
        public int Id { get; private set; }
        public string HotelName { get; private set; }
        public int RoomNumber { get; private set; }
        public DateTime StartStayDate { get; private set; }
        public DateTime EndStayDate {  get; private set; }
        public string Status { get; private set; }

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
