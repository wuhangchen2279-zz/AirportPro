using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceLibrary
{
    public class ViewOrder
    {
        public string AirlineName { set; get; }
        public string AirlineId { set; get; }
        public DateTime DepartTime { set; get; }
        public DateTime ArriveTime { set; get; }
        public string Origin { set; get; }
        public string Destination { set; get; }
        public int OrderId { set; get; }
        public int UserId { set; get; }
        public DateTime OrderDate { set; get; }
        public string SeatCode { set; get; }
        public string UserName { set; get; }
        public string FlightId { set; get; }
    }
}
