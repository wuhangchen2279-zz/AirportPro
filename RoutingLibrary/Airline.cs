using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace ServiceLibrary
{
    [DataContract]
    public class Airline
    {
        [DataMember]
        public string AirlineId { set; get; }

        [DataMember]
        public string AirlineName { set; get; }

        [DataMember]
        public string BookingPhone { set; get; }

        [DataMember]
        public string Website { set; get; }
    }
}