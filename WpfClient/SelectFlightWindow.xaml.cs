using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ServiceModel;
using ServiceLibrary;

namespace WpfClient
{
    /// <summary>
    /// Interaction logic for SelectFlightWindow.xaml
    /// </summary>
    public partial class SelectFlightWindow : Window
    {
        IDbService serviceClient = ChannelFactory<IDbService>.CreateChannel(new BasicHttpBinding(), new EndpointAddress("http://localhost:8200/AirportSolution/service"));
        SeatWindow seatWindow;

        public SelectFlightWindow()
        {
            InitializeComponent();
            FlightListDisplay.DataContext = this.ListAllFlights();
        }

        public List<Flight> ListAllFlights()
        {
            return serviceClient.GetFlights();
        }

        public void RefreshDataGrid()
        {
            FlightListDisplay.DataContext = this.ListAllFlights();
            FlightListDisplay.Items.Refresh();
        }

        public void ShowSelectFlightWindow(SelectFlightWindow sfw, SeatWindow seatWindow)
        {
            sfw.Show();
            this.seatWindow = seatWindow;
        }

        private void RowDoubleClick(object sender, RoutedEventArgs e)
        {
            Flight flight = (Flight)FlightListDisplay.SelectedItem;
            seatWindow.txtFlightId.Text = flight.FlightId;
            this.Close();
        }
    }
}
