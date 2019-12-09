using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace SocketServer
{
    public class UDPServer
    {
        UdpClient udpClient;
        public void Broadcast(byte[] bytes,int port)
        {
            if (udpClient == null)
                udpClient = new UdpClient(new IPEndPoint(IPAddress.Any, 0));
            IPEndPoint endpoint = new IPEndPoint(IPAddress.Broadcast, port);

            while (true)
            {
                lock (udpClient)
                {
                    udpClient.Send(bytes, bytes.Length, endpoint);
                }
                Thread.Sleep(1000);
            }
        }
    }
}
