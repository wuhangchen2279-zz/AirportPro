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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ServiceModel;
using ServiceLibrary;

namespace WpfClient
{
    /// <summary>
    /// Interaction logic for FlightWindow.xaml
    /// </summary>
    public partial class FlightWindow : UserControl
    {
        IDbService serviceClient = ChannelFactory<IDbService>.CreateChannel(new BasicHttpBinding(), new EndpointAddress("http://localhost:8200/AirportSolution/service"));

        public FlightWindow()
        {
            InitializeComponent();
            FlightDisplay.DataContext = this.ListAllFlights();
        }

        public List<Flight> ListAllFlights()
        {
            return serviceClient.GetFlights();
        }

        public void RefreshDataGrid()
        {
            FlightDisplay.DataContext = this.ListAllFlights();
            FlightDisplay.Items.Refresh();
        }

        private void bCreate_Click(object sender, RoutedEventArgs e)
        {
            if (txtFlightId.Text.Trim().Equals(""))
            {
                MessageBox.Show("Please input flight id!");
                return;
            }
            if (txtAirlineId.Text.Trim().Equals(""))
            {
                MessageBox.Show("Please input airline id!");
                return;
            }
            if (DTPDepartTime.Text.Trim().Equals(""))
            {
                MessageBox.Show("Please input depart time!");
                return;
            }
            if (DTPArriveTime.Text.Trim().Equals(""))
            {
                MessageBox.Show("Please input arrive time!");
                return;
            }
            if (txtOrigin.Text.Trim().Equals(""))
            {
                MessageBox.Show("Please input origin!");
                return;
            }
            if (txtDestination.Text.Trim().Equals(""))
            {
                MessageBox.Show("Please input destination!");
                return;
            }
            if (DateTime.Compare(Convert.ToDateTime(DTPDepartTime.Text), Convert.ToDateTime(DTPArriveTime.Text)) >= 0)
            {
                MessageBox.Show("Depart Time should be earlier than Arrive Time");
                return;
            }
            try
            {
                serviceClient.GetFlightById(txtFlightId.Text);
                MessageBox.Show("This flight has exsited in the database");
                return;
            }
            catch (FaultException fe)
            {
                if (MessageBox.Show("Create Now?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                {
                    //do no stuff
                    return;
                }
                else
                {
                    //do yes stuff
                    serviceClient.CreateFlightEntry(txtFlightId.Text, txtAirlineId.Text, Convert.ToDateTime(DTPDepartTime.Text), Convert.ToDateTime(DTPArriveTime.Text), txtOrigin.Text, txtDestination.Text);
                    this.RefreshDataGrid();
                }               
            }
            
        }

        private void bSelect_Click(object sender, RoutedEventArgs e)
        {
            SelectAirlineWindow saw = new SelectAirlineWindow();
            saw.ShowSelectAirlineWindow(saw, this);
        }

        private void bDelete_Click(object sender, RoutedEventArgs e)
        {
            if (txtFlightId.Text.Trim().Equals(""))
            {
                MessageBox.Show("Please input flight id!");
                return;
            }
            try
            { 
                serviceClient.GetFlightById(txtFlightId.Text);
            }
            catch(FaultException fe)
            {
                MessageBox.Show("This flight does not exist in the database!");
                return;
            }

            if (MessageBox.Show("Delete Now?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                //do no stuff
                return;
            }
            else
            {
                try
                {
                    //do yes stuff
                    serviceClient.DeleteFlightById(txtFlightId.Text);
                    this.RefreshDataGrid();
                }
                catch (FaultException fe) 
                {
                    MessageBox.Show("This Flight is referenced by Seat Table!");
                    return;
                }
                
            }
            
        }

        private void bUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (txtFlightId.Text.Trim().Equals(""))
            {
                MessageBox.Show("Please input flight id!");
                return;
            }
            if (txtAirlineId.Text.Trim().Equals(""))
            {
                MessageBox.Show("Please input airline id!");
                return;
            }
            if (DTPDepartTime.Text.Trim().Equals(""))
            {
                MessageBox.Show("Please input depart time!");
                return;
            }
            if (DTPArriveTime.Text.Trim().Equals(""))
            {
                MessageBox.Show("Please input arrive time!");
                return;
            }
            if (txtOrigin.Text.Trim().Equals(""))
            {
                MessageBox.Show("Please input origin!");
                return;
            }
            if (txtDestination.Text.Trim().Equals(""))
            {
                MessageBox.Show("Please input destination!");
                return;
            }
            if (DateTime.Compare(Convert.ToDateTime(DTPDepartTime.Text), Convert.ToDateTime(DTPArriveTime.Text)) >= 0)
            {
                MessageBox.Show("Depart Time should be earlier than Arrive Time");
                return;
            }
            try
            {
                serviceClient.GetFlightById(txtFlightId.Text);
            }
            catch (FaultException fe)
            {
                MessageBox.Show("This flight does not exist in the database!");
                return;
            }

            if (MessageBox.Show("Update Now?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                //do no stuff
                return;
            }
            else
            {
                try
                {
                    //do yes stuff
                    serviceClient.UpdateFlightById(txtFlightId.Text, txtAirlineId.Text, Convert.ToDateTime(DTPDepartTime.Text), Convert.ToDateTime(DTPArriveTime.Text), txtOrigin.Text, txtDestination.Text);
                    this.RefreshDataGrid();
                }
                catch (FaultException fe)
                {
                    MessageBox.Show("Airline Id can not be changed!");
                }
                
            }          
        }

        private void bSearch_Click(object sender, RoutedEventArgs e)
        {
            SearchForm searchForm = new SearchForm();
            searchForm.ShowSearchFormInFlight(searchForm, this);
        }

        private void bViewAll_Click(object sender, RoutedEventArgs e)
        {
            this.RefreshDataGrid();
        }

        private void bClearForm_Click(object sender, RoutedEventArgs e)
        {
            txtFlightId.Text = "";
            txtAirlineId.Text = "";
            txtOrigin.Text = "";
            txtDestination.Text = "";
            DTPArriveTime.Text = "";
            DTPDepartTime.Text = "";
        }


    }
}
