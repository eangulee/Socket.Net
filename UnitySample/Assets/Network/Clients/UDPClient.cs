using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

namespace Network.Clients
{
    public class UDPClient
    {
        private int port;
        public UDPClient(int port)
        {
            this.port = port;
            Thread receThread = new Thread(new ThreadStart(RecvThread));
            receThread.IsBackground = true;
            receThread.Start();
        }

        private void RecvThread()
        {
            UdpClient client = new UdpClient(new IPEndPoint(IPAddress.Any, port));
            IPEndPoint endpoint = new IPEndPoint(IPAddress.Any, 0);
            while (true)
            {
                byte[] buf = client.Receive(ref endpoint);
                string msg = Encoding.Default.GetString(buf);
                Debug.Log("收到广播消息：" + msg);
                Thread.Sleep(1000);
            }
        }
    }
}
