using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Model
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int NumberStars { get; set; }
        public string Description { get; set; }
        public List<string> PhotoLinks { get; set; }

        public Hotel(int id, string name, string address, int numberStars, string description)
        {
            Id = id;
            Name = name;
            Address = address;
            NumberStars = numberStars;
            Description = description;
        }
    }
}
