using Cassandra;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace HotelBooking.Models
{
    public class HotelOperations : IHotelOperations
    {
        List<HotelDetailsCassandra> HotelListCasandra = new List<HotelDetailsCassandra>();
        List<HotelDetailsStatic> HotelListJson = new List<HotelDetailsStatic>();
        public List<AllHotelDetails> GetAllHotels()
        {
            List<AllHotelDetails> HotelList = new List<AllHotelDetails>();
            foreach (var hotel in HotelListCasandra)
            {
                AllHotelDetails Hotel = new AllHotelDetails();
                Hotel.HotelId = hotel.HotelId;
                Hotel.AvailableFrom = hotel.AvailableFrom;
                Hotel.AvailableTill = hotel.AvailableTill;
                Hotel.HotelRating = hotel.HotelRating;
                HotelDetailsStatic result = HotelListJson.Find(x => x.HotelId == hotel.HotelId);
                Hotel.HotelName = result.HotelName;
                Hotel.HotelPolicy = result.HotelPolicy;
                Hotel.HotelDescription = result.HotelDescription;
                Hotel.HotelContactNumber = result.HotelContactNumber;
                Hotel.HotelAmenities = result.HotelAmenities;
                Hotel.HotelAddress = result.HotelAddress;
                Hotel.HotelImageURL = result.HotelImageURL;
                HotelList.Add(Hotel);
            }
            return HotelList;
        }
        public void GetAllHotelsStatic()
        {
            var path = "C:\\Users\\ajoshi\\source\\repos\\HotelBooking\\HotelBooking\\bin\\HotelList.json";      
            using (StreamReader streamReader = new StreamReader(path))
            {
                var readData = streamReader.ReadToEnd();
                HotelListJson = JsonConvert.DeserializeObject<List<HotelDetailsStatic>>(readData);
            }
        }
        public string CheckAvailibilty(BookingDetails booking)
        {
            int rooms = 0;
            string status = "";
            Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();
            ISession session = cluster.Connect("hotel");
            string query = "select * from \"hotelrooms\" where hotelid = " + booking.HotelId + " and roomtype='" + booking.RoomType+"'";
            RowSet dataReader = session.Execute(query);
            foreach (Row row in dataReader)
            {
                rooms = Convert.ToInt32(row[5]);
                if (Convert.ToInt32(row[5]) == 0)
                {
                    status = "No Rooms Available";
                }
                else if (Convert.ToInt32(row[5]) - booking.NumberOfRooms >= 0)
                {
                    status = "Booking successfull";
                }
                else
                {
                    status = "Enough rooms not available";
                }
            }
            if (status == "Booking successfull")
            {
                Cluster cluster2 = Cluster.Builder().AddContactPoint("127.0.0.1").Build();
                ISession session2 = cluster.Connect("hotel");
                rooms = rooms - booking.NumberOfRooms;
                string query2 = "update hotel.\"hotelrooms\" SET roomsavailable=" + rooms + " where  hotelid=" + booking.HotelId + " and roomtype='" + booking.RoomType + "';";
                RowSet dataReader2 = session.Execute(query2);
            }
            return status;
        }
        public async Task GetAllHotelsWcfAsync()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync("http://localhost:51513/HotelService.svc/hotels");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                HotelListCasandra = await response.Content.ReadAsAsync<List<HotelDetailsCassandra>>();
            }
        }
        public void AddBookingToDb(BookingDetails booking)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Data Source = TAVDESK092\\SQLEXPRESS; Initial Catalog = Hotel ; Integrated Security = True";
            conn.Open();
            string query = "insert into Bookings(UserId,HotelId,RoomType,NumberOfRooms,PricePerRoom,DateFrom,DateTo)values("+booking.UserId+","+booking.HotelId+",'"+booking.RoomType+"',"+booking.NumberOfRooms+","+booking.PricePerRoom+",'"+booking.DateFrom+"','"+booking.DateTo+"');";
            SqlCommand sqlCommand = new SqlCommand(query, conn);
            sqlCommand.ExecuteNonQuery();
        }
    }
}