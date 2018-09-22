using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBooking.Models
{
    public class BookingDetails
    {
        public int HotelId { get; set; }
        public int UserId { get; set; }
        public int BookingNumber { get; set; }
        public int NumberOfRooms { get; set; }
        public int PricePerRoom { get; set; }
        public string RoomType { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public string BookedAtTime { get; set; }
    }
}