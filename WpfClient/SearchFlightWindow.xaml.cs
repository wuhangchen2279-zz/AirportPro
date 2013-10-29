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

namespace WpfClient
{
    /// <summary>
    /// Interaction logic for SearchFlightWindow.xaml
    /// </summary>
    public partial class SearchFlightWindow : UserControl
    {
        public SearchFlightWindow()
        {
            InitializeComponent();
        }

        private void bSearch_Click(object sender, RoutedEventArgs e)
        {
            OrderWindow orderWindow = new OrderWindow();
            orderWindow.ShowOrderWindow(orderWindow, this);
        }
    }
}
