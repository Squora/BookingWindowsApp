using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Model
{
    public struct Review
    {
        public int Id { get; private set; }
        public string FullName { get; private set; }
        public string Text { get; private set; }
        public int Grade { get; private set; }
        public DateTime Date { get; private set; }

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
