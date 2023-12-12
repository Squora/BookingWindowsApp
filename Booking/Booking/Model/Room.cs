using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Model
{
    public class Room
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string Type { get; set; }
        public string CostPerNight { get; set; }
        public string Availability { get; set; }
        public List<string> PhotoLinks { get; set; }

        public Room(int id, string number, string type, string costPerNight, string availability)
        {
            Id = id;
            Number = number;
            Type = type;
            CostPerNight = costPerNight;
            Availability = availability;
        }
    }
}
