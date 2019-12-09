using System.Net;
using System.Net.Sockets;
using System.Threading;
using System;
using System.Text;
namespace SocketClient
{
    class Program
    {
        private static int port = 8885;   //端口       
        private static string ip = "127.0.0.1";
        static void Main(string[] args)
        {
            UDPClient udpClient = new UDPClient(8886);
            Console.ReadLine();
            return;
            TCPClient client = new TCPClient();
            client.Connect(ip, port);
            //通过 clientSocket 发送数据  
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(1000);    //等待1秒钟  
                string sendMessage = "client send Message Hellp" + DateTime.Now;
                client.SendMessage(Encoding.ASCII.GetBytes(sendMessage));
                Console.WriteLine("向服务器发送消息：{0}" + sendMessage);
            }
            Console.WriteLine("发送完毕，按回车键退出");
            Console.ReadLine();
        }
    }
}