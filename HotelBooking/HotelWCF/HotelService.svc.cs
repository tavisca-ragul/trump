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
            HotelDetailsCassandra hotel = new HotelDetailsCassandra();
            List<HotelDetailsCassandra> hotelDetails = new List<HotelDetailsCassandra>();
            Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();
            ISession session = cluster.Connect("hotel");
            string query = "select * from hotel";
            RowSet dataReader = session.Execute(query);
            foreach(Row row in dataReader)
            {
                hotel.HotelId=Convert.ToInt32(row[0].ToString());
                hotel.AvailableFrom = row[1].ToString();
                hotel.AvailableTill = row[2].ToString();
                hotel.HotelRating = Convert.ToDecimal(row[3].ToString());
                hotelDetails.Add(hotel);
            }
            return hotelDetails;
        }
    }
}
