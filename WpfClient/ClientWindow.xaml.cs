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
using WPF.MDI;

namespace WpfClient
{
    /// <summary>
    /// Interaction logic for ClientWindow.xaml
    /// </summary>
    public partial class ClientWindow : Window
    {
        public static int userId; //{ set; get;}
        
        public ClientWindow()
        {
            InitializeComponent();
            this.Left = 0;
            this.Top = 0;
        }

        private void SearchFlight_Click(object sender, RoutedEventArgs e)
        {

            MainMdiContainer.Children.Add(new MdiChild()
            {
                Title = "Search Flight Window",
                Height = 299,
                Width = 484,
                FontSize = 12,
                Content = new SearchFlightWindow(),
            });

        }

        private void OrderHistory_Click(object sender, RoutedEventArgs e)
        {
            MainMdiContainer.Children.Add(new MdiChild()
            {
                Title = "Order History Window",
                Height = 600,
                Width = 900,
                FontSize = 12,
                Content = new OrderHistoryWindow(),
            });

        }

        private void SwtchUser_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();           
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
