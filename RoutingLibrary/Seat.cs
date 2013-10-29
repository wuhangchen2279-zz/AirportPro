using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace ServiceLibrary
{
    [DataContract]
    public class Seat
    {
        [DataMember]
        public int SeatId { set; get; }

        [DataMember]
        public string SeatCode { set; get; }

        [DataMember]
        public string SeatAvailable { set; get; }

        [DataMember]
        public string FlightId { set; get; }
    }
}