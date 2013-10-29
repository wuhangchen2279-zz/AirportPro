using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ServiceLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService" in both code and config file together.
    [ServiceContract]
    public interface IDbService
    {
        #region User
        [OperationContract]
        void CreateUserEntry(string userName, string role, string password, string email);

        [OperationContract]
        void DeleteUserById(int userId);

        [OperationContract]
        void UpdateUserById(int userId, string userName, string role, string password, string email);

        [OperationContract]
        User GetUserByName(string userName);

        [OperationContract]
        List<User> GetUserByKeyword(string keyword);

        [OperationContract]
        List<User> GetUsers();
        #endregion

        #region Airline
        [OperationContract]
        void CreateAirlineEntry(string airlineId, string airlineName, string bookingPhone, string website);

        [OperationContract]
        void DeleteAirlineById(string airlineId);

        [OperationContract]
        void UpdateAirlineById(string airlineId, string airlineName, string bookingPhone, string website);

        [OperationContract]
        Airline GetAirlineById(string airlineId);
        
        [OperationContract]
        List<Airline> GetAirlineByKeyword(string keyword);

        [OperationContract]
        List<Airline> GetAirlines();
        #endregion

        #region Flight
        [OperationContract]
        void CreateFlightEntry(string flightId, string airlineId, DateTime departTime, DateTime arriveTime, string origin, string destination);

        [OperationContract]
        void DeleteFlightById(string flightId);

        [OperationContract]
        void UpdateFlightById(string flightId, string airlineId, DateTime departTime, DateTime arriveTime, string origin, string destination);

        [OperationContract]
        Flight GetFlightById(string flightId);

        [OperationContract]
        List<Flight> GetFlightByKeyword(string keyword);

        [OperationContract]
        List<Flight> GetFlights();
        #endregion

        #region Seat
        [OperationContract]
        void CreateSeatEntry(string seatCode, string seatAvailable, string flightId);

        [OperationContract]
        void DeleteSeatById(int seatId);

        [OperationContract]
        void UpdateSeatById(int seatId, string seatCode, string seatAvailable, string flightId);

        [OperationContract]
        void UpdateSeatAvailable(string seatCode, string flightId);

        [OperationContract]
        List<Seat> GetSeatByKeyword(string keyword);

        [OperationContract]
        List<Seat> GetSeatByFlightId(string flightId);

        [OperationContract]
        List<Seat> GetSeats();        
        #endregion

        #region Order
        [OperationContract]
        void CreateOrderEntry(int userId, string flightId, string seatCode);
        #endregion

        #region ViewFlight
        [OperationContract]
        List<ViewFlight> SearchFlightForClient(string origin, string destination, DateTime departTime);
        #endregion

        #region ViewOrder
        [OperationContract]
        List<ViewOrder> SearchViewOrderByUserId(int userId);
        #endregion

        #region ConcurrentControl
        [OperationContract]
        bool CheckLogin(string userName, string password, string role);
        #endregion
    }
}
