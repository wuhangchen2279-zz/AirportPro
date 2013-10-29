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
    /// Interaction logic for SelectWindow.xaml
    /// </summary>
    public partial class SelectAirlineWindow : Window
    {
        IDbService serviceClient = ChannelFactory<IDbService>.CreateChannel(new BasicHttpBinding(), new EndpointAddress("http://localhost:8200/AirportSolution/service"));
        FlightWindow flightWindow;

        public SelectAirlineWindow()
        {
            InitializeComponent();
            AirlineListDisplay.DataContext = this.ListAllAirlines();
        }

        public List<Airline> ListAllAirlines()
        {
            return serviceClient.GetAirlines();
        }

        public void RefreshDataGrid()
        {
            AirlineListDisplay.DataContext = this.ListAllAirlines();
            AirlineListDisplay.Items.Refresh();
        }

        public void ShowSelectAirlineWindow(SelectAirlineWindow saw, FlightWindow flightWindow)
        {
            saw.Show();
            this.flightWindow = flightWindow;
        }

        private void RowDoubleClick(object sender, RoutedEventArgs e)
        { 
            Airline airline = (Airline)AirlineListDisplay.SelectedItem;
            flightWindow.txtAirlineId.Text = airline.AirlineId;
            this.Close();
        }
    }
}
