using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_Net_Switcher.Services
{
    public interface IDHCP_Service
    {
        string GetGatewayIP();
        string GetMyIP();
    }
}
