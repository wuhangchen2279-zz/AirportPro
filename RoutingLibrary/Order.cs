using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ServiceLibrary
{
    [DataContract]
    class Order
    {
        [DataMember]
        public int OrderId { set; get; }

        [DataMember]
        public int UserId { set; get; }

        [DataMember]
        public DateTime OrderDate { set; get; }

        [DataMember]
        public string FlightId { set; get; }

        [DataMember]
        public string SeatCode { set; get; }
    }
}
