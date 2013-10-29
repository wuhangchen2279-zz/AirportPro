using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// Add the following namespaces so we can use the service routing model
// You need to add a reference to these namespaces for the project
using System.ServiceModel;
using System.ServiceModel.Routing;

namespace ServerHost
{
    class Program
    {
        static void Main(string[] args)
        {
            // Setup routing servicec
            ServiceHost routingHost = new ServiceHost(typeof(RoutingService));

            try
            {
                // Host the routing service and wait for connections
                routingHost.Open();
                Console.WriteLine("Routing service has been started now.");

                // Close connection if user hits enter
                Console.WriteLine("Please press <ENTER> to stop service...");
                Console.Read();
                routingHost.Close();
            }
            catch (Exception e)
            {
                // Print error message if exception is raised
                Console.WriteLine(e.Message);
            }
            Console.Read();
        }
    }
}
