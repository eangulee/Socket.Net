using Network.Servers;
using Network.Tool;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class ServerRoot : MonoBehaviour
{
    public int udpPort = 8886;
    public int port = 8885;

    private Server server;
    private UDPServer udpServer;
    private void Awake()
    {
        udpServer = new UDPServer();
        InvokeRepeating("BroadcastIPAddress", 0f, 1f);

        server = new Server(port);
        server.Start();
    }

    private void BroadcastIPAddress()
    {
        string ip = IPUtils.GetIpAddress();
        Debug.LogFormat("广播消息{0}", ip);
        byte[] buf = Encoding.Default.GetBytes(ip);
        udpServer.Broadcast(buf, udpPort);
    }
}
