using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace ServiceLibrary
{
    [DataContract]
    public class Flight
    {
        [DataMember]
        public string FlightId { set; get; }

        [DataMember]
        public string AirlineId { set; get; }

        [DataMember]
        public DateTime DepartTime { set; get; }

        [DataMember]
        public DateTime ArriveTime { set; get; }

        [DataMember]
        public string Origin { set; get; }

        [DataMember]
        public string Destination { set; get; }
    }
}