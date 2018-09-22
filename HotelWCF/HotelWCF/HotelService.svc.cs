using Cassandra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace HotelWCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "HotelService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select HotelService.svc or HotelService.svc.cs at the Solution Explorer and start debugging.
    public class HotelService : IHotelService
    {
        public List<HotelDetailsCassandra> GetAllHotels()
        {
            
            List<HotelDetailsCassandra> hotelDetails = new List<HotelDetailsCassandra>();
            Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();
            ISession session = cluster.Connect("hotel");
            string query = "select * from \"HotelDetails\"";
            RowSet dataReader = session.Execute(query);
            foreach(Row row in dataReader)
            {
                HotelDetailsCassandra hotel = new HotelDetailsCassandra();
                hotel.HotelId=Convert.ToInt32(row[0].ToString());
                hotel.AvailableFrom = row[1].ToString();
                hotel.AvailableTill = row[2].ToString();
                hotel.HotelRating = Convert.ToDecimal(row[3].ToString());
                hotelDetails.Add(hotel);
            }
            return hotelDetails;
        }

        public List<RoomDetails> GetRoomsByHotelId(string hotelid)
        {
            List<RoomDetails> RoomsDetails = new List<RoomDetails>();
            Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();
            ISession session = cluster.Connect("hotel");
            string query = "select * from \"hotelrooms\" where hotelid = "+hotelid;
            RowSet dataReader = session.Execute(query);
            foreach (Row row in dataReader)
            {
                RoomDetails room = new RoomDetails();
                room.HotelId = Convert.ToInt32(row[0].ToString());
                room.RoomType = row[1].ToString();
                room.AvailableFrom = row[2].ToString();
                room.AvailableTill = row[3].ToString();
                room.RoomPrice = Convert.ToInt32(row[4].ToString());
                room.RoomsAvailable= Convert.ToInt32(row[5].ToString());
                RoomsDetails.Add(room);
            }
            return RoomsDetails;
        }
    }
}
