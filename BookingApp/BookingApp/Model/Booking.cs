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
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }

        public Booking(int id, DateOnly startDate, DateOnly endDate) 
        {
            Id = id;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
