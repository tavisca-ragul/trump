﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace HotelWCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IHotelService" in both code and config file together.
    [ServiceContract]
    public interface IHotelService
    {
        [OperationContract]
        [WebGet(UriTemplate = "/hotels", ResponseFormat = WebMessageFormat.Json,RequestFormat =WebMessageFormat.Json)]
        List<HotelDetailsCassandra> GetAllHotels();
        [WebGet(UriTemplate = "/hotels/{hotelid}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        List<RoomDetails> GetRoomsByHotelId(string hotelid);
    }
}
