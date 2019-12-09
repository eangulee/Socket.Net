using Network.Servers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerRoot : MonoBehaviour
{
    public int port = 8885;

    private Server server;
    private void Awake()
    {
        server = new Server(port);
        server.Start();
    }
}
