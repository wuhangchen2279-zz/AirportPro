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
    /// Interaction logic for AirlineWindow.xaml
    /// </summary>
    public partial class AirlineWindow : UserControl
    {
        IDbService serviceClient = ChannelFactory<IDbService>.CreateChannel(new BasicHttpBinding(), new EndpointAddress("http://localhost:8200/AirportSolution/service"));
        
        public AirlineWindow()
        {
            InitializeComponent();
            AirlineDisplay.DataContext = this.ListAllAirlines();
        }

        public List<Airline> ListAllAirlines() 
        {
            return serviceClient.GetAirlines();
        }

        public void RefreshDataGrid() 
        {
            AirlineDisplay.DataContext = this.ListAllAirlines();
            AirlineDisplay.Items.Refresh();
        }

        private void bCreate_Click(object sender, RoutedEventArgs e)
        {
            if (txtAirlineId.Text.Trim().Equals(""))
            {
                MessageBox.Show("Please input airline id!");
                return;
            }
            if (txtAirlineName.Text.Trim().Equals(""))
            {
                MessageBox.Show("Please input airline name!");
                return;
            }
            try
            {
                serviceClient.GetAirlineById(txtAirlineId.Text);
                MessageBox.Show("This airline has exsited in the database");
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
                    serviceClient.CreateAirlineEntry(txtAirlineId.Text, txtAirlineName.Text, txtBookingPhone.Text, txtWebsite.Text);
                    this.RefreshDataGrid();
                }
                
            }
        }

        private void bDelete_Click(object sender, RoutedEventArgs e)
        {
            if (txtAirlineId.Text.Trim().Equals(""))
            {
                MessageBox.Show("Please input airline id!");
                return;
            }
            try
            {
                serviceClient.GetAirlineById(txtAirlineId.Text);
            }
            catch (FaultException fe)
            {
                MessageBox.Show("This airline does not exist in the database!");
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
                    serviceClient.DeleteAirlineById(txtAirlineId.Text);
                    this.RefreshDataGrid();
                }
                catch
                {
                    MessageBox.Show("This airline are referenced by Flight table!");
                    return;
                }
                
            }
        }

        private void bUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (txtAirlineId.Text.Trim().Equals(""))
            {
                MessageBox.Show("Please input airline id!");
                return;
            }
            if (txtAirlineName.Text.Trim().Equals(""))
            {
                MessageBox.Show("Please input airline name!");
                return;
            }
            try
            {
                serviceClient.GetAirlineById(txtAirlineId.Text);
            }
            catch (FaultException fe)
            {
                MessageBox.Show("This airline does not exist in the database!");
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
                    serviceClient.UpdateAirlineById(txtAirlineId.Text, txtAirlineName.Text, txtBookingPhone.Text, txtWebsite.Text);
                    this.RefreshDataGrid();
                }
                catch (FaultException fe)
                {
                    MessageBox.Show("Airline Id can not be changed!");   
                }
            }
            
        }

        private void bViewAll_Click(object sender, RoutedEventArgs e)
        {
            this.RefreshDataGrid();
        }

        private void bClearForm_Click(object sender, RoutedEventArgs e)
        {
            txtAirlineId.Text = "";
            txtAirlineName.Text = "";
            txtBookingPhone.Text = "";
            txtWebsite.Text = "";
        }

        private void bSearch_Click(object sender, RoutedEventArgs e)
        {
            SearchForm searchForm = new SearchForm();
            searchForm.ShowSearchFormInAirline(searchForm, this);
        }



    }
}
