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
using WPF.MDI;

namespace WpfClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {   
        public AdminWindow()
        {
            InitializeComponent();
            this.Left = 0;
            this.Top = 0;
        }

        private void AirlineWindow_Click(object sender, RoutedEventArgs e)
        {
            MainMdiContainer.Children.Add(new MdiChild()
            {
                Title = "Airline Window",
                Height = 600,
                Width = 600,
                FontSize = 12,
                Content = new AirlineWindow(),
            });
            
        }

        private void FlightWindow_Click(object sender, RoutedEventArgs e)
        {
            
            
            MainMdiContainer.Children.Add(new MdiChild()
            {
                Title = "Flight Window",
                Height = 600,
                Width = 600,
                FontSize = 12,
                Content = new FlightWindow(),
            });
        }

        private void SeatWindow_Click(object sender, RoutedEventArgs e)
        {
            MainMdiContainer.Children.Add(new MdiChild()
            {
                Title = "Seat Window",
                Height = 600,
                Width = 600,
                FontSize = 12,
                Content = new SeatWindow(),
            });
        }

        private void SwitchUser_Click(object sender, RoutedEventArgs e)
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
