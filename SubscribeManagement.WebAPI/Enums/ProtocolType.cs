using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubscribeManagement.WebAPI.Enums
{
    public enum ProtocolType
    {
        Custom = 0,
        Socks = 1 << 0,
        Shadowsocks = 1 << 1,
        VMess = 1 << 2,
        VLESS = 1 << 3,
        Trojan = 1 << 4
    }
}
