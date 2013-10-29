using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ServiceLibrary;
using System.Data.Objects;

namespace DatabaseBackupService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service" in code, svc and config file together.
    public class Service : IDbService
    {
        #region User
        public void CreateUserEntry(string userName, string role, string password, string email)
        {
            using (AirportDbEntities de = new AirportDbEntities())
            {
                user u = new user();

                u.UserName = userName;
                u.Role = role;

                string inPassword = CryptorEngine.Encrypt(password.Trim(), true);
                u.Password = inPassword;

                u.Email = email;

                de.AddTousers(u);
                de.SaveChanges();
            }
        }


        public void DeleteUserById(int userId)
        {
            using (AirportDbEntities de = new AirportDbEntities())
            {
                user user = (from u in de.users
                             where u.UserId == userId
                             select u).First();
                de.users.DeleteObject(user);
                de.SaveChanges();
            }
        }

        public void UpdateUserById(int userId, string userName, string role, string password, string email)
        {
            using (AirportDbEntities de = new AirportDbEntities())
            {
                user user = (from u in de.users
                             where u.UserId == userId
                             select u).First();
                user.UserId = userId;
                user.UserName = userName;
                user.Role = role;

                string inPassword = CryptorEngine.Encrypt(password.Trim(), true);
                user.Password = inPassword;

                user.Email = email;
                de.SaveChanges();
            }
        }

        public User GetUserByName(string userName)
        {
            User userResult = new User();

            using (AirportDbEntities de = new AirportDbEntities())
            {
                user user = (from u in de.users
                             where u.UserName == userName
                             select u).First();
                userResult.UserId = user.UserId;
                userResult.UserName = user.UserName;
                userResult.Password = user.Password;
                userResult.Role = user.Role;
                userResult.Email = user.Email;

                return userResult;
            }
        }

        public List<User> GetUserByKeyword(string keyword)
        {
            List<User> listUsers = new List<User>();

            using (AirportDbEntities de = new AirportDbEntities())
            {
                var users = from u in de.users
                            where u.UserName.ToLower().Contains(keyword.ToLower())
                            || u.Role.ToLower().Contains(keyword.ToLower())
                            || u.Email.ToLower().Contains(keyword.ToLower())
                            select u;

                foreach (user rowUser in users)
                {
                    User u = new User();
                    u.UserId = rowUser.UserId;
                    u.UserName = rowUser.UserName;
                    u.Role = rowUser.Role;
                    u.Password = rowUser.Password;
                    u.Email = rowUser.Email;
                    listUsers.Add(u);
                }

                return listUsers;
            }
        }

        public List<User> GetUsers()
        {
            List<User> listUsers = new List<User>();

            using (AirportDbEntities de = new AirportDbEntities())
            {
                var users = from u in de.users select u;

                foreach (user rowUser in users)
                {
                    User u = new User();
                    u.UserId = rowUser.UserId;
                    u.UserName = rowUser.UserName;
                    u.Role = rowUser.Role;
                    u.Password = rowUser.Password;
                    listUsers.Add(u);
                }

                return listUsers;
            }

        }
        #endregion

        #region Airline
        public void CreateAirlineEntry(string airlineId, string airlineName, string bookingPhone, string website)
        {
            using (AirportDbEntities de = new AirportDbEntities())
            {
                airline a = new airline();

                a.AirlineId = airlineId;
                a.AirlineName = airlineName;
                a.BookingPhone = bookingPhone;
                a.Website = website;

                de.AddToairlines(a);
                de.SaveChanges();
            }
        }

        public void DeleteAirlineById(string airlineId)
        {
            using (AirportDbEntities de = new AirportDbEntities())
            {
                airline airline = (from a in de.airlines
                                   where a.AirlineId == airlineId
                                   select a).First();
                de.airlines.DeleteObject(airline);
                de.SaveChanges();
            }
        }

        public void UpdateAirlineById(string airlineId, string airlineName, string bookingPhone, string website)
        {
            using (AirportDbEntities de = new AirportDbEntities())
            {
                airline airline = (from a in de.airlines
                                   where a.AirlineId == airlineId
                                   select a).First();
                airline.AirlineId = airlineId;
                airline.AirlineName = airlineName;
                airline.BookingPhone = bookingPhone;
                airline.Website = website;
                de.SaveChanges();
            }
        }

        public Airline GetAirlineById(string airlineId)
        {
            using (AirportDbEntities de = new AirportDbEntities())
            {
                Airline airline = new Airline();

                var rowAirline = (from a in de.airlines
                                  where a.AirlineId == airlineId
                                  select a).First();

                if (rowAirline == null)
                {
                    return null;
                }

                airline.AirlineId = rowAirline.AirlineId;
                airline.AirlineName = rowAirline.AirlineName;
                airline.Website = rowAirline.Website;
                airline.BookingPhone = rowAirline.BookingPhone;

                return airline;
            }
        }

        public List<Airline> GetAirlineByKeyword(string keyword)
        {
            List<Airline> listAirlines = new List<Airline>();

            using (AirportDbEntities de = new AirportDbEntities())
            {
                var airlines = from a in de.airlines
                               where a.AirlineId.ToLower().Contains(keyword.ToLower())
                               || a.AirlineName.ToLower().Contains(keyword.ToLower())
                               || a.BookingPhone.ToLower().Contains(keyword.ToLower())
                               || a.Website.ToLower().Contains(keyword.ToLower())
                               select a;

                foreach (airline rowAirline in airlines)
                {
                    Airline a = new Airline();
                    a.AirlineId = rowAirline.AirlineId;
                    a.AirlineName = rowAirline.AirlineName;
                    a.BookingPhone = rowAirline.BookingPhone;
                    a.Website = rowAirline.Website;
                    listAirlines.Add(a);
                }

                return listAirlines;
            }
        }

        public List<Airline> GetAirlines()
        {
            List<Airline> listAirlines = new List<Airline>();

            using (AirportDbEntities de = new AirportDbEntities())
            {
                var airlines = from a in de.airlines select a;

                foreach (airline rowAirline in airlines)
                {
                    Airline a = new Airline();
                    a.AirlineId = rowAirline.AirlineId;
                    a.AirlineName = rowAirline.AirlineName;
                    a.BookingPhone = rowAirline.BookingPhone;
                    a.Website = rowAirline.Website;
                    listAirlines.Add(a);
                }

                return listAirlines;
            }
        }
        #endregion

        #region Flight
        public void CreateFlightEntry(string flightId, string airlineId, DateTime departTime, DateTime arriveTime, string origin, string destination)
        {
            using (AirportDbEntities de = new AirportDbEntities())
            {
                flight f = new flight();

                f.FlightId = flightId;
                f.AirlineId = airlineId;
                f.DepartTime = departTime;
                f.ArriveTime = arriveTime;
                f.Origin = origin;
                f.Destination = destination;

                de.AddToflights(f);
                de.SaveChanges();
            }
        }

        public void DeleteFlightById(string flightId)
        {
            using (AirportDbEntities de = new AirportDbEntities())
            {
                flight flight = (from f in de.flights
                                 where f.FlightId == flightId
                                 select f).First();
                de.flights.DeleteObject(flight);
                de.SaveChanges();
            }
        }

        public void UpdateFlightById(string flightId, string airlineId, DateTime departTime, DateTime arriveTime, string origin, string destination)
        {
            using (AirportDbEntities de = new AirportDbEntities())
            {
                flight flights = (from f in de.flights
                                  where f.FlightId == flightId
                                  select f).First();
                flights.AirlineId = airlineId;
                flights.FlightId = flightId;
                flights.DepartTime = departTime;
                flights.ArriveTime = arriveTime;
                flights.Origin = origin;
                flights.Destination = destination;
                de.SaveChanges();
            }
        }

        public Flight GetFlightById(string flightId)
        {
            using (AirportDbEntities de = new AirportDbEntities())
            {
                Flight flight = new Flight();

                var rowFlight = (from f in de.flights
                                 where f.FlightId == flightId
                                 select f).First();

                if (rowFlight == null)
                {
                    return null;
                }

                flight.FlightId = rowFlight.FlightId;
                flight.AirlineId = rowFlight.AirlineId;
                flight.DepartTime = rowFlight.DepartTime;
                flight.ArriveTime = rowFlight.ArriveTime;
                flight.Origin = rowFlight.Origin;
                flight.Destination = rowFlight.Destination;

                return flight;
            }
        }

        public List<Flight> GetFlightByKeyword(string keyword)
        {
            List<Flight> listFlights = new List<Flight>();

            using (AirportDbEntities de = new AirportDbEntities())
            {
                var flights = from f in de.flights
                              where f.AirlineId.ToLower().Contains(keyword.ToLower())
                              || f.FlightId.ToLower().Contains(keyword.ToLower())
                              || f.Origin.ToLower().Contains(keyword.ToLower())
                              || f.Destination.ToLower().Contains(keyword.ToLower())
                              select f;

                foreach (flight rowFlight in flights)
                {
                    Flight f = new Flight();
                    f.FlightId = rowFlight.FlightId;
                    f.AirlineId = rowFlight.AirlineId;
                    f.DepartTime = rowFlight.DepartTime;
                    f.ArriveTime = rowFlight.ArriveTime;
                    f.Origin = rowFlight.Origin;
                    f.Destination = rowFlight.Destination;
                    listFlights.Add(f);
                }

                return listFlights;
            }
        }

        public List<Flight> GetFlights()
        {
            List<Flight> listFlights = new List<Flight>();

            using (AirportDbEntities de = new AirportDbEntities())
            {
                var flights = from f in de.flights select f;

                foreach (flight rowFlight in flights)
                {
                    Flight f = new Flight();
                    f.FlightId = rowFlight.FlightId;
                    f.AirlineId = rowFlight.AirlineId;
                    f.ArriveTime = rowFlight.ArriveTime;
                    f.DepartTime = rowFlight.DepartTime;
                    f.Origin = rowFlight.Origin;
                    f.Destination = rowFlight.Destination;
                    listFlights.Add(f);
                }

                return listFlights;
            }
        }
        #endregion

        #region Seat
        public void CreateSeatEntry(string seatCode, string seatAvailable, string flightId)
        {
            using (AirportDbEntities de = new AirportDbEntities())
            {
                seat s = new seat();

                s.SeatCode = seatCode;
                s.SeatAvailable = seatAvailable;
                s.FlightId = flightId;

                de.AddToseats(s);
                de.SaveChanges();
            }
        }

        public void DeleteSeatById(int seatId)
        {
            using (AirportDbEntities de = new AirportDbEntities())
            {
                seat seat = (from s in de.seats
                             where s.SeatId == seatId
                             select s).First();
                de.seats.DeleteObject(seat);
                de.SaveChanges();
            }
        }

        public void UpdateSeatById(int seatId, string seatCode, string seatAvailable, string flightId)
        {
            using (AirportDbEntities de = new AirportDbEntities())
            {
                seat seats = (from s in de.seats
                              where s.SeatId == seatId
                              select s).First();
                seats.SeatCode = seatCode;
                seats.SeatId = seatId;
                seats.SeatAvailable = seatAvailable;
                seats.FlightId = flightId;
                de.SaveChanges();
            }
        }

        public void UpdateSeatAvailable(string seatCode, string flightId)
        {
            using (AirportDbEntities de = new AirportDbEntities())
            {
                seat seats = (from s in de.seats
                              where s.SeatCode == seatCode
                              && s.FlightId == flightId
                              select s).First();
                seats.SeatCode = seatCode;
                seats.SeatId = seats.SeatId;
                seats.SeatAvailable = "N";
                seats.FlightId = flightId;
                de.SaveChanges();
            }
        }

        public List<Seat> GetSeatByFlightId(string flightId)
        {
            List<Seat> listSeats = new List<Seat>();

            using (AirportDbEntities de = new AirportDbEntities())
            {
                var seats = from s in de.seats
                            where s.FlightId == flightId && s.SeatAvailable.Equals("Y")
                            select s;

                foreach (seat rowSeat in seats)
                {
                    Seat a = new Seat();
                    a.SeatId = rowSeat.SeatId;
                    a.SeatCode = rowSeat.SeatCode;
                    a.SeatAvailable = rowSeat.SeatAvailable;
                    a.FlightId = rowSeat.FlightId;
                    listSeats.Add(a);
                }

                return listSeats;
            }
        }

        public List<Seat> GetSeatByKeyword(string keyword)
        {
            List<Seat> listSeats = new List<Seat>();

            using (AirportDbEntities de = new AirportDbEntities())
            {
                var seats = from s in de.seats
                            where s.SeatCode.ToLower().Contains(keyword.ToLower())
                            || s.SeatAvailable.ToLower().Contains(keyword.ToLower())
                            || s.FlightId.ToLower().Contains(keyword.ToLower())
                            select s;

                foreach (seat rowSeat in seats)
                {
                    Seat a = new Seat();
                    a.SeatId = rowSeat.SeatId;
                    a.SeatCode = rowSeat.SeatCode;
                    a.SeatAvailable = rowSeat.SeatAvailable;
                    a.FlightId = rowSeat.FlightId;
                    listSeats.Add(a);
                }

                return listSeats;
            }
        }

        public List<Seat> GetSeats()
        {
            List<Seat> listSeats = new List<Seat>();

            using (AirportDbEntities de = new AirportDbEntities())
            {
                var seats = from s in de.seats select s;

                foreach (seat rowSeat in seats)
                {
                    Seat s = new Seat();
                    s.SeatId = rowSeat.SeatId;
                    s.SeatAvailable = rowSeat.SeatAvailable;
                    s.SeatCode = rowSeat.SeatCode;
                    s.FlightId = rowSeat.FlightId;
                    listSeats.Add(s);
                }

                return listSeats;
            }
        }
        #endregion

        #region Order
        public void CreateOrderEntry(int userId, string flightId, string seatCode)
        {
            using (AirportDbEntities de = new AirportDbEntities())
            {
                order o = new order();

                o.UserId = userId;
                o.FlightId = flightId;
                o.OrderDate = DateTime.Now;
                o.SeatCode = seatCode;

                de.AddToorders(o);
                de.SaveChanges();
            }
        }
        #endregion

        #region ViewFlight
        public List<ViewFlight> SearchFlightForClient(string origin, string destination, DateTime departTime)
        {
            List<ViewFlight> listFlights = new List<ViewFlight>();

            using (AirportDbEntities de = new AirportDbEntities())
            {
                var flights = from vf in de.viewflights
                              where vf.Origin.ToLower().Contains(origin.ToLower())
                              && vf.Destination.ToLower().Contains(destination.ToLower())
                              && EntityFunctions.TruncateTime(vf.DepartTime) == EntityFunctions.TruncateTime(departTime.Date)
                              select vf;

                foreach (viewflight rowFlight in flights)
                {
                    ViewFlight v = new ViewFlight();
                    v.FlightId = rowFlight.FlightId;
                    v.AirlineId = rowFlight.AirlineId;
                    v.AirlineName = rowFlight.AirlineName;
                    v.DepartTime = rowFlight.DepartTime;
                    v.ArriveTime = rowFlight.ArriveTime;
                    v.Origin = rowFlight.Origin;
                    v.Destination = rowFlight.Destination;
                    listFlights.Add(v);
                }

                return listFlights;
            }
        }
        #endregion

        #region ViewOrder
        public List<ViewOrder> SearchViewOrderByUserId(int userId)
        {
            List<ViewOrder> listOrders = new List<ViewOrder>();

            using (AirportDbEntities de = new AirportDbEntities())
            {
                var orders = from vo in de.vieworders
                             where vo.UserId == userId
                             select vo;

                foreach (vieworder rowOrder in orders)
                {
                    ViewOrder v = new ViewOrder();
                    v.AirlineName = rowOrder.AirlineName;
                    v.AirlineId = rowOrder.AirlineId;
                    v.DepartTime = rowOrder.DepartTime;
                    v.ArriveTime = rowOrder.ArriveTime;
                    v.Origin = rowOrder.Origin;
                    v.Destination = rowOrder.Destination;
                    v.OrderId = rowOrder.OrderId;
                    v.UserId = rowOrder.UserId;
                    v.OrderDate = rowOrder.OrderDate;
                    v.SeatCode = rowOrder.SeatCode;
                    v.UserName = rowOrder.UserName;
                    v.FlightId = rowOrder.FlightId;
                    listOrders.Add(v);
                }

                return listOrders;
            }
        }
        #endregion

        #region ConcurrentControl
        public bool CheckLogin(string userName, string password, string role)
        {
            bool result;

            using (AirportDbEntities de = new AirportDbEntities())
            {
                string inPassword = CryptorEngine.Encrypt(password.Trim(), true);
                int num;

                num = (from u in de.users
                       where u.UserName == userName
                       && u.Password == inPassword
                       && u.Role == role
                       select u).Count();


                if (num != 0)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }

                return result;
            }
        }
        #endregion
    }
}
