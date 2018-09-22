using HotelBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;

namespace HotelBooking.Controllers
{
    public class HotelController : ApiController
    {
        HotelOperations hotel = new HotelOperations();
        Logging log = new Logging();
        //GET: api/Hotel
        [HttpGet]
        public async Task<List<AllHotelDetails>> GetAsync()
        {
            List<AllHotelDetails> hotelList = new List<AllHotelDetails>();
            int maxId = log.GetMaxId();
            log.LogGet(maxId);
            await hotel.GetAllHotelsWcfAsync();
            hotel.GetAllHotelsStatic();
            hotelList = hotel.GetAllHotels();
            log.LogGetDone(maxId+1);
            return hotelList;
        }

        // GET: api/Hotel/5
        [HttpGet]
        [Route("api/Hotel/{hotelid}")]
        public async Task<List<RoomsDetails>> GetAsync(int hotelid)
        {
            List<RoomsDetails> rooms = new List<RoomsDetails>();
            int maxId = log.GetMaxId();
            log.LogGetById(maxId);
            HttpClient client = new HttpClient();
            var response = await client.GetAsync("http://localhost:51513/HotelService.svc/hotels/" + hotelid);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                rooms = await response.Content.ReadAsAsync<List<RoomsDetails>>();
            }
            log.LogGetByIdDone(maxId+1);
            return rooms;
        }

        // POST: api/Hotel
        [HttpPost]
        public string Post([FromBody]BookingDetails booking)
        {
            int maxId = log.GetMaxId();
            log.LogPost(maxId);
            string response=hotel.CheckAvailibilty(booking);
            if (response == "Booking successfull")
            {
                hotel.AddBookingToDb(booking);
                log.LogPostDone(maxId+1);
            }
            return response;
        }

        // PUT: api/Hotel/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Hotel/5
        public void Delete(int id)
        {
        }
    }
}
