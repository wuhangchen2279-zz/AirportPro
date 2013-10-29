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
    /// Interaction logic for SeatWindow.xaml
    /// </summary>
    public partial class SeatWindow : UserControl
    {
        IDbService serviceClient = ChannelFactory<IDbService>.CreateChannel(new BasicHttpBinding(), new EndpointAddress("http://localhost:8200/AirportSolution/service"));

        public SeatWindow()
        {
            InitializeComponent();
            SeatDisplay.DataContext = this.ListAllSeats();
        }

        public List<Seat> ListAllSeats()
        {
            return serviceClient.GetSeats();
        }

        public void RefreshDataGrid()
        {
            SeatDisplay.DataContext = this.ListAllSeats();
            SeatDisplay.Items.Refresh();
        }

        private void bClearForm_Click(object sender, RoutedEventArgs e)
        {
            txtFlightId.Text = "";
            CBSeatAvailable.SelectedItem = null;
            txtSeatCode.Text = "";
        }

        private void bSelect_Click(object sender, RoutedEventArgs e)
        {
            SelectFlightWindow sfw = new SelectFlightWindow();
            sfw.ShowSelectFlightWindow(sfw, this);
        }

        private void bCreate_Click(object sender, RoutedEventArgs e)
        {
            if (txtSeatCode.Text.Trim().Equals(""))
            {
                MessageBox.Show("Please input seat code!");
                return;
            }
            if (CBSeatAvailable.Text.Trim().Equals(""))
            {
                MessageBox.Show("Please input seat available!");
                return;
            }
            if (txtFlightId.Text.Trim().Equals(""))
            {
                MessageBox.Show("Please input flight id!");
                return;
            }

            if (MessageBox.Show("Create Now?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                //do no stuff
                return;
            }
            else
            {
                //do yes stuff
                serviceClient.CreateSeatEntry(txtSeatCode.Text, CBSeatAvailable.Text, txtFlightId.Text);
                this.RefreshDataGrid();
            }
            
        }

        private void bDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Delete Now?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                //do no stuff
                return;
            }
            else
            {
                //do yes stuff
                Seat s = (Seat)SeatDisplay.SelectedItem;
                serviceClient.DeleteSeatById(s.SeatId);
                this.RefreshDataGrid();
            }
            
        }

        private void bUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (txtSeatCode.Text.Trim().Equals(""))
            {
                MessageBox.Show("Please input seat code!");
                return;
            }
            if (CBSeatAvailable.Text.Trim().Equals(""))
            {
                MessageBox.Show("Please input seat available!");
                return;
            }
            if (txtFlightId.Text.Trim().Equals(""))
            {
                MessageBox.Show("Please input flight id!");
                return;
            }

            if (MessageBox.Show("Update Now?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                //do no stuff
                return;
            }
            else
            {
                //do yes stuff
                Seat s = (Seat)SeatDisplay.SelectedItem;
                serviceClient.UpdateSeatById(s.SeatId, txtSeatCode.Text, CBSeatAvailable.Text, txtFlightId.Text);
                this.RefreshDataGrid();
            }
            
        }

        private void bSearch_Click(object sender, RoutedEventArgs e)
        {
            SearchForm searchForm = new SearchForm();
            searchForm.ShowSearchFormInSeat(searchForm, this);
        }

        private void bViewAll_Click(object sender, RoutedEventArgs e)
        {
            this.RefreshDataGrid();
        }



        private void ChangeCell_Handler(object sender, SelectedCellsChangedEventArgs e)
        {
            Seat s = (Seat)SeatDisplay.SelectedItem;
            try
            {
                if (s.SeatAvailable.Trim().Equals("Y"))
                {
                    CBSeatAvailable.SelectedIndex = 0;
                }
                if (s.SeatAvailable.Trim().Equals("N"))
                {
                    CBSeatAvailable.SelectedIndex = 1;
                }
            }
            catch (NullReferenceException er)
            {
                CBSeatAvailable.SelectedItem = null;
            }
            
            
        }
    }
}
