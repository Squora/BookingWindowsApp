using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Model
{
    public struct Room
    {
        public int Id { get; private set; }
        public string Number { get; private set; }
        public string Type { get; private set; }
        public string CostPerNight { get; private set; }
        public string Availability { get; private set; }

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
