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
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        IDbService serviceClient = ChannelFactory<IDbService>.CreateChannel(new BasicHttpBinding(), new EndpointAddress("http://localhost:8200/AirportSolution/service"));

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void bRegister_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow register = new RegisterWindow();
            register.Show();
        }

        private void bLogin_Click(object sender, RoutedEventArgs e)
        {
            if (txtUserName.Text.Equals("")) 
            {
                MessageBox.Show("Please input the user name");
                return;
            }
            if (txtPasword.Password.Equals(""))
            {
                MessageBox.Show("Please input the password");
                return;
            }
            if(CBUserRole.Text.Equals(""))
            {
                MessageBox.Show("Please select the user role");
                return;
            }
            bool loginAllow = serviceClient.CheckLogin(txtUserName.Text, txtPasword.Password, CBUserRole.Text);
            if (loginAllow == true)
            {
                if(CBUserRole.Text.Equals("Client"))
                {
                    User u = serviceClient.GetUserByName(txtUserName.Text);
                    ClientWindow clientWindow = new ClientWindow();
                    ClientWindow.userId = u.UserId;
                    clientWindow.Show();
                    this.Close();
                }

                if (CBUserRole.Text.Equals("Admin"))
                {
                    AdminWindow adminWindow = new AdminWindow();
                    adminWindow.Show();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Wrong password or username, or wrong selection of role!");
                return;
            }
        }
    }
}
