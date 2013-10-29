using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ServiceModel;
using ServiceLibrary;

namespace WpfClient
{
    public partial class SearchForm : Form
    {
        AirlineWindow airlineWindow;
        FlightWindow flightWindow;
        SeatWindow seatWindow;

        IDbService serviceClient = ChannelFactory<IDbService>.CreateChannel(new BasicHttpBinding(), new EndpointAddress("http://localhost:8200/AirportSolution/service"));

        public SearchForm()
        {
            InitializeComponent();
        }

        public void ShowSearchFormInAirline(SearchForm searchForm, AirlineWindow airlineWindow)
        {
            this.airlineWindow = airlineWindow;
            searchForm.Show();
        }

        public void ShowSearchFormInFlight(SearchForm searchForm, FlightWindow flightWindow)
        {
            this.flightWindow = flightWindow;
            searchForm.Show();
        }

        public void ShowSearchFormInSeat(SearchForm searchForm, SeatWindow seatWindow)
        {
            this.seatWindow = seatWindow;
            searchForm.Show();
        }

        private void bSearch_Click(object sender, EventArgs e)
        {
            if (airlineWindow != null)
            {
                List<Airline> listAirlines = serviceClient.GetAirlineByKeyword(txtKeyWord.Text);
                airlineWindow.AirlineDisplay.DataContext = listAirlines;
                airlineWindow = null;
                this.Close();
            }

            if (flightWindow != null)
            {
                List<Flight> listFlights = serviceClient.GetFlightByKeyword(txtKeyWord.Text);
                flightWindow.FlightDisplay.DataContext = listFlights;
                flightWindow = null;
                this.Close();
            }

            if (seatWindow != null)
            {
                List<Seat> listSeats = serviceClient.GetSeatByKeyword(txtKeyWord.Text);
                seatWindow.SeatDisplay.DataContext = listSeats;
                seatWindow = null;
                this.Close();
            }
        }
    }
}
