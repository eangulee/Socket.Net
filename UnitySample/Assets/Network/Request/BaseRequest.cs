﻿using Network.Clients;
using Network.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Network.Request
{
    public class BaseRequest
    {
        protected RequestCode requestCode = RequestCode.None;
        protected ActionCode actionCode = ActionCode.None;
        public BaseRequest() { 
        }

        protected void SendRequest(ClientManage clientManage,string data)
        {
            clientManage.SendRequest(requestCode, actionCode, data);
        }

        public virtual void SendRequest() { }
        public virtual void OnResponse(string reasonCode,string data) { }

        public virtual void OnDestroy()
        {
            RequestManager.Instance.RemoveRequest(actionCode);
        }
    }
}
