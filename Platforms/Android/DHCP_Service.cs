using Android.Content;
using Android.Net;
using Android.Net.Wifi;
using Android.SE.Omapi;
using Android.Text.Format;
using SG_Net_Switcher.Platforms.Android;
using SG_Net_Switcher.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: Dependency(typeof(DHCP_Service))]
namespace SG_Net_Switcher.Platforms.Android
{
    public class DHCP_Service : IDHCP_Service
    {
        public string GetGatewayIP()
        {
            var dhcpInfo = GetDhcpInfo();
            int gateway = dhcpInfo.Gateway;
            string gatewayIP = Formatter.FormatIpAddress(gateway);
            return gatewayIP;
        }

        public string GetMyIP()
        {
            var dhcpInfo = GetDhcpInfo();
            int MyIp = dhcpInfo.IpAddress;
            string MyIP = Formatter.FormatIpAddress(MyIp);
            return MyIP;
        }

        private DhcpInfo GetDhcpInfo()
        {
            WifiManager wifiManager = (WifiManager)global::Android.App.Application.Context.GetSystemService(Context.WifiService);
            var dhcpInfo = wifiManager.DhcpInfo;
            return dhcpInfo;
        }
    }
}
