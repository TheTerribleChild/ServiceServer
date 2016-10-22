using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public static class WebUtility
    {
        public static int GetNextAvailablePortNumber()
        {
            int availablePort = -1;
            using (TcpClient portChecker = new TcpClient())
            {
                for (int port = 1025; port <= 65536; port++)
                {
                    try
                    {
                        portChecker.Connect("Localhost", port);
                        availablePort = port;
                        break;
                    }
                    catch (Exception) { }
                }
            }
            return availablePort;
        }

        public static bool IsPortOpen(int port)
        {
            bool available = false;

            if (port < 1025 || port > 65536)
                return false;

            using (TcpClient portChecker = new TcpClient())
            {
                try
                {
                    portChecker.Connect("Localhost", port);
                    available = true;
                }
                catch (Exception) { }
            }
            return available;
        }
    }
}
