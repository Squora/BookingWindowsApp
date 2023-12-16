using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Model
{
    public class Review
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Text { get; set; }
        public int Grade { get; set; }
        public DateTime Date { get; set; }

        public Review(int id, string fullName, string text, int grade, DateTime date) 
        { 
            Id = id;
            FullName = fullName;
            Text = text;
            Grade = grade;
            Date = date;
        }
    }
}
