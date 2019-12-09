using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

namespace Network.Tool
{
    public static class IPUtils
    {
        public static string GetIpAddress()
        {
            string hostName = Dns.GetHostName();   //获取本机名
            IPHostEntry localhost = Dns.GetHostEntry(hostName);
            //IPHostEntry localhost = Dns.GetHostEntry(hostName);   //获取IPv6地址
            IPAddress localaddr = localhost.AddressList[1];

            return localaddr.ToString();
        }
    }
}
