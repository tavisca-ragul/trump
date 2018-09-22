using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBooking.Models
{
    public class HotelDetailsCassandra
    {
        public int HotelId { get; set; }
        public string AvailableFrom { get; set; }
        public string AvailableTill { get; set; }
        public Decimal HotelRating { get; set; }
    }
}