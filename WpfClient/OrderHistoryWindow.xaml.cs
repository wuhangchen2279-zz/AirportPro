﻿using System;
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
    /// Interaction logic for OrderHistoryWindow.xaml
    /// </summary>
    public partial class OrderHistoryWindow : UserControl
    {
        IDbService serviceClient = ChannelFactory<IDbService>.CreateChannel(new BasicHttpBinding(), new EndpointAddress("http://localhost:8200/AirportSolution/service"));

        public OrderHistoryWindow()
        {
            InitializeComponent();
            OrderHistoryDisplay.DataContext = serviceClient.SearchViewOrderByUserId(ClientWindow.userId);
        }
    }
}
