using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBooking.Models
{
    public class RoomsDetails
    {
        public int HotelId { get; set; }
        public string RoomType { get; set; }
        public string AvailableFrom { get; set; }
        public string AvailableTill { get; set; }
        public int RoomPrice { get; set; }
        public int RoomsAvailable { get; set; }
    }
}