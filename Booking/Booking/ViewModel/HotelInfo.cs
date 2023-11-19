using Booking.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Booking.ViewModel
{
    internal class HotelInfo : Window
    {
        public HotelInfo(Hotel hotel)
        {
            //InitializeComponent();
            DataContext = hotel;
        }
    }
}
