using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Model
{
    public class Hotel : IComparable<Hotel>
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Address { get; private set; }
        public int Rating { get; private set; }
        public string Description { get; private set; }
        public List<string> PhotoLinks { get; private set; }

        public Hotel(int id, string name, string address, int rating, string description)
        {
            Id = id;
            Name = name;
            Address = address;
            Rating = rating;
            Description = description;
        }

        public int CompareTo(Hotel other)
        {
            return Rating.CompareTo(other.Rating);
        }
    }
}
