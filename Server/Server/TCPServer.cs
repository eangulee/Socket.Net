using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace SocketServer
{
    public class TCPServer
    {
        private byte[] result = new byte[1024];
        Socket serverSocket;
        public bool isListening { get; private set; }
        private Dictionary<string, Socket> clients = new Dictionary<string, Socket>();
        public void StartListener(String ip = "127.0.0.1", int port = 8850)
        {
            if (isListening) return;
            isListening = true;
            //服务器IP地址  
            IPAddress idAddress = IPAddress.Parse(ip);
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(new IPEndPoint(idAddress, port));  //绑定IP地址：端口  
            serverSocket.Listen(10);    //设定最多10个排队连接请求  
            Console.WriteLine("启动监听{0}成功", serverSocket.LocalEndPoint.ToString());
            //通过Clientsoket发送数据  
            Thread listenerThread = new Thread(ListenClientConnect);
            listenerThread.Start();
            Console.ReadLine();
        }

        /// <summary>  
        /// 监听客户端连接  
        /// </summary>  
        private void ListenClientConnect()
        {
            while (true)
            {
                Socket clientSocket = serverSocket.Accept();
                clients[clientSocket.RemoteEndPoint.ToString()] = clientSocket;
                SendMessage(Encoding.ASCII.GetBytes("Server Say Hello"));
                Thread receiveThread = new Thread(ReceiveMessage);
                receiveThread.Start(clientSocket);
            }
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        public void SendMessage(byte[] bytes)
        {
            foreach (var kvp in clients)
            {
                if (kvp.Value != null && kvp.Value.Connected)
                    kvp.Value.Send(bytes);
            }
        }

        /// <summary>  
        /// 接收消息  
        /// </summary>  
        /// <param name="clientSocket"></param>  
        private void ReceiveMessage(object clientSocket)
        {
            Socket client = (Socket)clientSocket;
            while (true)
            {
                try
                {
                    //通过clientSocket接收数据  
                    int receiveNumber = client.Receive(result);
                    Console.WriteLine("接收客户端{0}消息{1}", client.RemoteEndPoint.ToString(), Encoding.ASCII.GetString(result, 0, receiveNumber));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    client.Shutdown(SocketShutdown.Both);
                    client.Close();
                    break;
                }
            }
        }
    }
}
