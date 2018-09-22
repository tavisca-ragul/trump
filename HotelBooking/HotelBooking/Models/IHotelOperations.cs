using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Models
{
    interface IHotelOperations
    {
        void GetAllHotelsStatic();
        List<AllHotelDetails> GetAllHotels();
        Task GetAllHotelsWcfAsync();
        void AddBookingToDb(BookingDetails booking);
    }
}
