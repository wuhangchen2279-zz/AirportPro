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
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        SearchFlightWindow searchFlightWindow;
        IDbService serviceClient = ChannelFactory<IDbService>.CreateChannel(new BasicHttpBinding(), new EndpointAddress("http://localhost:8200/AirportSolution/service"));
        
        public OrderWindow()
        {
            InitializeComponent();           
        }

        public void ShowOrderWindow(OrderWindow orderWindow, SearchFlightWindow searchFlightWindow)
        {
            this.searchFlightWindow = searchFlightWindow;
            FlightSearchDisplay.DataContext = serviceClient.SearchFlightForClient(searchFlightWindow.txtFrom.Text, searchFlightWindow.txtTo.Text, Convert.ToDateTime(searchFlightWindow.DPDepartDate.SelectedDate.Value));
            orderWindow.Show();
        }

        private void CellChange_Handler(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                ViewFlight flight = (ViewFlight)FlightSearchDisplay.SelectedItem;
                CBSeatCode.ItemsSource = serviceClient.GetSeatByFlightId(flight.FlightId);
                CBSeatCode.DisplayMemberPath = "SeatCode";
            }
            catch (NullReferenceException er)
            {
                CBSeatCode.SelectedItem = null;
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (FlightSearchDisplay.SelectedItem == null)
            {
                MessageBox.Show("Please Select a Row in the above flight searched display!");
                return;
            }
            if(CBSeatCode.SelectedItem == null)
            {
                MessageBox.Show("Please select a seat code!");
                return;
            }
            if (MessageBox.Show("Order Now?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                //do no stuff
                return;
            }
            else
            {
                ViewFlight flight = (ViewFlight)FlightSearchDisplay.SelectedItem;
                serviceClient.CreateOrderEntry(ClientWindow.userId, flight.FlightId, CBSeatCode.Text);
                serviceClient.UpdateSeatAvailable(CBSeatCode.Text, flight.FlightId);
                this.Close();
            }
            
        }
    }
}
