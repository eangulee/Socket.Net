using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace SocketClient
{
    public class TCPClient
    {
        Socket clientSocket;
        private byte[] result = new byte[1024];
        public bool isConnect { get; private set; }


        public void Connect(String ip = "127.0.0.1", int port = 8850)
        {
            if (isConnect) return;

            //设定服务器IP地址  
            IPAddress idAddress = IPAddress.Parse(ip);
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                clientSocket.Connect(new IPEndPoint(idAddress, 8885)); //配置服务器IP与端口
                isConnect = true;
                Console.WriteLine("连接服务器成功");
                Thread receiveThread = new Thread(ReceiveMessage);
                receiveThread.Start();
            }
            catch
            {
                isConnect = false;
                Console.WriteLine("连接服务器失败，请按回车键退出！");
                return;
            }
        }

        public void SendMessage(byte[] bytes)
        {
            if (!isConnect) return;
            try
            {
                Thread.Sleep(1000);
                clientSocket.Send(bytes);
            }
            catch
            {
                clientSocket.Shutdown(SocketShutdown.Both);
                clientSocket.Close();
                isConnect = false;
            }
        }

        /// <summary>  
        /// 接收消息  
        /// </summary>  
        /// <param name="clientSocket"></param>  
        private void ReceiveMessage()
        {
            while (true)
            {
                try
                {
                    //通过clientSocket接收数据  
                    int receiveNumber = clientSocket.Receive(result);
                    Console.WriteLine("接收服务端{0}消息{1}", clientSocket.RemoteEndPoint.ToString(), Encoding.ASCII.GetString(result, 0, receiveNumber));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    clientSocket.Shutdown(SocketShutdown.Both);
                    clientSocket.Close();
                    isConnect = false;
                    break;
                }
            }
        }
    }
}
