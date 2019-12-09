﻿using Network.Protocol;
using Network.Singleton;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Network.Request
{
    public class RequestManager : Singleton<RequestManager>
    {
        public RequestManager() { }

        private Dictionary<ActionCode, BaseRequest> requestDict = new Dictionary<ActionCode, BaseRequest>();

        public void AddRequest(ActionCode actionCode, BaseRequest request)
        {
            requestDict.Add(actionCode, request);
        }
        public void RemoveRequest(ActionCode actionCode)
        {
            requestDict.Remove(actionCode);
        }
        public void HandleReponse(ActionCode actionCode, ReasonCode reasonCode, string data)
        {
            BaseRequest request;
            requestDict.TryGetValue(actionCode, out request);
            if (request == null)
            {
                Debug.Log("无法得到ActionCode[" + actionCode + "]对应的Request类"); return;
            }
            request.OnResponse(GetReason(reasonCode), data);
        }
        public string GetReason(ReasonCode reasonCode)
        {
            switch (reasonCode)
            {
                case ReasonCode.IllegalCharacter:
                    return "输入数据含有非法字符";
                case ReasonCode.None:
                    return "";
                case ReasonCode.UsernameOrPwdError:
                    return "用户名或者密码错误";
                case ReasonCode.DatabaseException:
                    return "数据库异常";
                case ReasonCode.RepeatRegister:
                    return "重复注册";
            }
            return "";
        }
    }
}
