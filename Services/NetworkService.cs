using MikrotikDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SG_Net_Switcher.Services
{
    public class NetworkService
    {
        private readonly HttpClient _client;
        private readonly IDHCP_Service dhcpServices;
        public NetworkService(IDHCP_Service dHCP_Service)
        {
            _client = new HttpClient();
            dhcpServices = dHCP_Service;
        }

        public string SendCommand(string ScriptName)
        {
            if (!IsWifiConnected())
                return "Wifi не подключен";

            try
            {
                using (var conn = new MKConnection(GetRouterIp(), "robot", "password"))
                {
                    conn.Open();
                    var cmd = conn.CreateCommand("system script run");
                    cmd.Parameters.Add("number", ScriptName);
                    cmd.ExecuteNonQuery();
                    return "Выполнено";
                }
            }
            catch (Exception ex) 
            { 
               return "Ошибка выполнения: " + ex.Message;
            }
        }

        public string GetRouterIp()
        {
            if (IsWifiConnected())
            {
                var gatewayAddress = dhcpServices.GetGatewayIP();
                return gatewayAddress;
            }
            return "0.0.0.0";
        }
        public string GetMyIp()
        {
            if (IsWifiConnected())
            {
                var MyIpAddress = dhcpServices.GetMyIP();
                return MyIpAddress;
            }
            return "0.0.0.0";
        }

        public bool IsWifiConnected()
        {
            NetworkAccess accessType = Connectivity.Current.NetworkAccess;
            if (accessType == NetworkAccess.Internet)
            {
                IEnumerable<ConnectionProfile> profiles = Connectivity.Current.ConnectionProfiles;
                if (profiles.Contains(ConnectionProfile.WiFi))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
