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
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        IDbService serviceClient = ChannelFactory<IDbService>.CreateChannel(new BasicHttpBinding(), new EndpointAddress("http://localhost:8200/AirportSolution/service"));
        
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void bRegister_Click(object sender, RoutedEventArgs e)
        {
            if (txtUserName.Text.Trim().Equals(""))
            {
                MessageBox.Show("Please input your username!");
                return;
            }
            if (CBRole.Text.Trim().Equals("")) 
            {
                MessageBox.Show("Please input your role to login!");
                return;
            }
            if (txtPassword.Password.Trim().Equals(""))
            {
                MessageBox.Show("Please input your password!");
                return;
            }
            if (txtConfirmPassword.Password.Trim().Equals("")) 
            {
                MessageBox.Show("Please input your confirmed password");
                return;
            }
            if (!(txtPassword.Password.Equals(txtConfirmPassword.Password)))
            { 
                MessageBox.Show("Please make sure password and confirmed password are same!");
                return;
            }
            serviceClient.CreateUserEntry(txtUserName.Text, CBRole.Text, txtPassword.Password, txtEmail.Text);
            this.Close();
        }
    }
}
