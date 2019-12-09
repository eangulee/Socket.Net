using Network.Controller;
using Network.Protocol;
using Network.Servers;
using System;

namespace Network.Controller
{
    public class UserController : BaseController
    {
        public UserController()
        {
            requestCode = RequestCode.User;
        }

        public string Login(string data, Client client, Server server)
        {
            string[] strs = data.Split(',');
            Console.WriteLine(string.Format("{0},{1}", ((int)ReturnCode.Success).ToString(), strs[0]));

            return string.Format("{0},{1}", ((int)ReturnCode.Success).ToString(), "欢迎" + strs[0] + "登录...");
        }

        public string Register(string data, Client client, Server server)
        {
            string[] strs = data.Split(',');
            string username = strs[0]; string password = strs[1];
            ControllerManager.reasonCode = ReasonCode.RepeatRegister;
            return ((int)ReturnCode.Fail).ToString();
        }
    }
}
