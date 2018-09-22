using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBooking.Models
{
    public class AllHotelDetails
    {
        public int HotelId { get; set; }
        public string HotelName { get; set; }
        public string HotelAddress { get; set; }
        public string HotelDescription { get; set; }
        public string HotelContactNumber { get; set; }
        public string HotelAmenities { get; set; }
        public List<string> HotelImageURL { get; set; }
        public string HotelPolicy { get; set; }
        public string AvailableFrom { get; set; }
        public string AvailableTill { get; set; }
        public Decimal HotelRating { get; set; }
    }
}