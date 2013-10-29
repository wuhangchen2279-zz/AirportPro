using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceLibrary
{
    public class ViewFlight
    {
        public string FlightId { set; get; }

        public string AirlineId { set; get; }

        public DateTime DepartTime { set; get; }

        public DateTime ArriveTime { set; get; }

        public string Origin { set; get; }

        public string Destination { set; get; }

        public string AirlineName { set; get; }
    }
}
