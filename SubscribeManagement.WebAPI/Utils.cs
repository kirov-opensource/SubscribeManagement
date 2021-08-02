using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace SubscribeManagement.WebAPI
{
    public static class Utils
    {
        public static bool IsIpv6(string ip)
        {
            IPAddress address;
            if (IPAddress.TryParse(ip, out address))
            {
                switch (address.AddressFamily)
                {
                    case AddressFamily.InterNetwork:
                        return false;
                    case AddressFamily.InterNetworkV6:
                        return true;
                    default:
                        return false;
                }
            }
            return false;
        }
    }
}
