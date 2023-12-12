using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Model
{
    public class Review
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int Grade { get; set; }
        public DateTime Date { get; set; }
        public string Author { get; set; }
        public List<string> PhotoLinks { get; set; }

        public Review(int id, string text, int grade, DateTime date, string author) 
        { 
            Id = id;
            Text = text;
            Grade = grade;
            Date = date;
            Author = author;
        }
    }
}
