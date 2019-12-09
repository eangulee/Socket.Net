using System.Net.Sockets;
using System.Net;
using System.Threading;
using System;
using System.Text;
namespace SocketServer
{
    class Program
    {
        private static int port = 8885;   //端口       
        private static string ip = "127.0.0.1";
        static void Main(string[] args)
        {
            UDPServer udpServer = new UDPServer();
            Console.WriteLine("广播消息{0}", "127.0.0.1");
            byte[] buf = Encoding.Default.GetBytes("127.0.0.1");
            udpServer.Broadcast(buf,8886);
            return;
            TCPServer server =  new TCPServer();
            server.StartListener(ip, port);

            while(true)
            {
                Thread.Sleep(1000);    //等待1秒钟  
                string sendMessage = "client send Message Hellp" + DateTime.Now;
                server.SendMessage(Encoding.ASCII.GetBytes(sendMessage));
                Console.WriteLine("向客户端发送消息：{0}" + sendMessage);
            }
        }
    }
}