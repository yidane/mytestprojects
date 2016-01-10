﻿using System;
using OneGulp.WeChat.MP.Entities;
using WeixinPF.Application;
using WeixinPF.Application.Weixin.Message.Repository;
using WeixinPF.Common;
using WeixinPF.Model.WeiXin.Message;

namespace WeixinPF.WeixinFramework.Common.CustomMessageHandler
{
    /// <summary>
    /// 自定义MessageHandler
    /// </summary>
    public partial class CustomMessageHandler
    {
        private IRequestRuleRepository rBll = DependencyManager.Resolve<IRequestRuleRepository>();
        RequestRuleContent rcBll = new RequestRuleContent();
        WeiXCommFun wxcomm = new WeiXCommFun();

        /// <summary>
        /// 菜单点击事件
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnEvent_ClickRequest(RequestMessageEvent_Click requestMessage)
        {
            IResponseMessageBase reponseMessage = null;
            #region 注释掉
            ////菜单点击，需要跟创建菜单时的Key匹配
            //switch (requestMessage.EventKey)
            //{
            //    case "OneClick":
            //        {
            //            var strongResponseMessage = CreateResponseMessage<ResponseMessageText>();
            //            reponseMessage = strongResponseMessage;
            //            strongResponseMessage.Content = "您点击了底部按钮。\r\n为了测试微信软件换行bug的应对措施，这里做了一个——\r\n换行";
            //        }
            //        break;
            //    case "SubClickRoot_Text":
            //        {
            //            var strongResponseMessage = CreateResponseMessage<ResponseMessageText>();
            //            reponseMessage = strongResponseMessage;
            //            strongResponseMessage.Content = "您点击了子菜单按钮。";
            //        }
            //        break;
            //    case "SubClickRoot_News":
            //        {
            //            var strongResponseMessage = CreateResponseMessage<ResponseMessageNews>();
            //            reponseMessage = strongResponseMessage;
            //            strongResponseMessage.Articles.Add(new Article()
            //            {
            //                Title = "您点击了子菜单图文按钮",
            //                Description = "您点击了子菜单图文按钮，这是一条图文信息。",
            //                PicUrl = "http://weixin.senparc.com/Images/qrcode.jpg",
            //                Url = "http://weixin.senparc.com"
            //            });
            //        }
            //        break;
            //    case "SubClickRoot_Music":
            //        {
            //            var strongResponseMessage = CreateResponseMessage<ResponseMessageMusic>();
            //            reponseMessage = strongResponseMessage;
            //            strongResponseMessage.Music.MusicUrl = "http://weixin.senparc.com/Content/music1.mp3";
            //        }
            //        break;
            //    case "SubClickRoot_Agent"://代理消息
            //        {
            //            //获取返回的XML
            //            DateTime dt1 = DateTime.Now;
            //            reponseMessage = MessageAgent.RequestResponseMessage(this, agentUrl, agentToken, RequestDocument.ToString());
            //            //上面的方法也可以使用扩展方法：this.RequestResponseMessage(this,agentUrl, agentToken, RequestDocument.ToString());

            //            DateTime dt2 = DateTime.Now;

            //            if (reponseMessage is ResponseMessageNews)
            //            {
            //                (reponseMessage as ResponseMessageNews)
            //                    .Articles[0]
            //                    .Description += string.Format("\r\n\r\n代理过程总耗时：{0}毫秒", (dt2 - dt1).Milliseconds);
            //            }
            //        }
            //        break;
            //    case "Member"://托管代理会员信息
            //        {
            //            //原始方法为：MessageAgent.RequestXml(this,agentUrl, agentToken, RequestDocument.ToString());//获取返回的XML
            //            reponseMessage = this.RequestResponseMessage(agentUrl, agentToken, RequestDocument.ToString());
            //        }
            //        break;
            //    case "OAuth"://OAuth授权测试
            //        {
            //            var strongResponseMessage = CreateResponseMessage<ResponseMessageNews>();
            //            strongResponseMessage.Articles.Add(new Article()
            //            {
            //                Title = "OAuth2.0测试",
            //                Description = "点击【查看全文】进入授权页面。\r\n注意：此页面仅供测试（是专门的一个临时测试账号的授权，并非OneGulp.WeChat.MP SDK官方账号，所以如果授权后出现错误页面数正常情况），测试号随时可能过期。请将此DEMO部署到您自己的服务器上，并使用自己的appid和secret。",
            //                Url = "http://weixin.senparc.com/oauth2",
            //                PicUrl = "http://weixin.senparc.com/Images/qrcode.jpg"
            //            });
            //            reponseMessage = strongResponseMessage;
            //        }
            //        break;
            //}
            #endregion
            string keywords = requestMessage.EventKey;
            int apiid = wxcomm.getApiid();


            string EventName = "";
            if (requestMessage.Event.ToString().Trim() != "")
            {
                EventName = requestMessage.Event.ToString();
            }
            else if (requestMessage.EventKey != null)
            {
                EventName += requestMessage.EventKey.ToString();
            }

            if (!wxcomm.ExistApiidAndWxId(apiid, requestMessage.ToUserName))
            {  //验证接收方是否为我们系统配置的帐号，即验证微帐号与微信原始帐号id是否一致，如果不一致，说明【1】配置错误，【2】数据来源有问题
                DependencyManager.Resolve<IResponseMessageLogRepository>().Add(apiid, requestMessage.FromUserName, requestMessage.MsgType.ToString(), EventName, "none", "未取到关键词对应的数据", requestMessage.ToUserName);
                return wxcomm.GetResponseMessageTxtByContent(requestMessage, "验证微帐号与微信原始帐号id不一致，可能原因【1】系统配置错误，【2】非法的数据来源有问题", apiid, "系统错误提醒");
            }
            bool isExist = wxcomm.wxCodeLegal(apiid);
            if (!isExist)
            {
                DependencyManager.Resolve<IResponseMessageLogRepository>().Add(apiid, requestMessage.FromUserName, requestMessage.MsgType.ToString(), EventName, "none", "账号已过期或已被禁用", requestMessage.ToUserName);
                return wxcomm.GetResponseMessageTxtByContent(requestMessage, "账号已过期或已被禁用！", apiid, "系统错误提醒");
            }

            //------关键词钻取  微拍的文字提示 begin-------
            if (keywords == "人工客服" || keywords == "员工客服")
            {
                return this.CreateResponseMessage<ResponseMessageTransfer_Customer_Service>();
            }

            string fStr = FilterTxtRequest(apiid, keywords, requestMessage.FromUserName);
            if (fStr.Trim() != "")
            {
                DependencyManager.Resolve<IResponseMessageLogRepository>().Add(apiid, requestMessage.FromUserName, requestMessage.MsgType.ToString(), EventName, "none", fStr, requestMessage.ToUserName);
                return wxcomm.GetResponseMessageTxtByContent(requestMessage, fStr, apiid, keywords);
            }
            //------关键词钻取  微拍的文字提示 end- -------

            int responseType = 0;
            string modelFunctionName = "";
            int modelFunctionId = 0;
            int rid = rBll.GetRuleIdByKeyWords(apiid, keywords, out responseType, out modelFunctionName, out modelFunctionId);
            if (rid <= 0 || responseType <= 0)
            {

                DependencyManager.Resolve<IResponseMessageLogRepository>().Add(apiid, requestMessage.FromUserName, requestMessage.MsgType.ToString(), EventName, "none", "未取到关键词对应的数据", requestMessage.ToUserName);
                return wxcomm.GetResponseMessageTxtByContent(requestMessage, "未找到对应的内容", apiid, "未取到关键词对应的数据");
            }

            if (modelFunctionId > 0)
            {  //模块功能回复
                return wxcomm.GetModuleResponse(requestMessage, modelFunctionName, modelFunctionId, apiid);
            }

            switch (responseType)
            {
                case 1:
                    //发送纯文字
                    reponseMessage = wxcomm.GetResponseMessageTxt(requestMessage, rid, apiid, keywords);
                    break;
                case 2:
                    //发送多图文
                    reponseMessage = wxcomm.GetResponseMessageNews(requestMessage, rid, apiid, keywords);
                    break;
                case 3:
                    //发送语音
                    reponseMessage = wxcomm.GetResponseMessageeMusic(requestMessage, rid, apiid, keywords);
                    break;
                default:
                    break;
            }

            return reponseMessage;
        }

        public override IResponseMessageBase OnEvent_EnterRequest(RequestMessageEvent_Enter requestMessage)
        {
            var responseMessage = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageText>(requestMessage);
            responseMessage.Content = "您刚才发送了ENTER事件请求。";
            return responseMessage;
        }

        /// <summary>
        /// 获取用户地理位置（高级接口下才能用）
        /// 获取用户地理位置的方式有两种，一种是仅在进入会话时上报一次，一种是进入会话后每隔5秒上报一次。公众号可以在公众平台网站中设置。
        /// 用户同意上报地理位置后，每次进入公众号会话时，都会在进入时上报地理位置，或在进入会话后每5秒上报一次地理位置，上报地理位置以推送XML数据包到开发者填写的URL来实现。
        /// </summary>
        public override IResponseMessageBase OnEvent_LocationRequest(RequestMessageEvent_Location requestMessage)
        {

            string openid = requestMessage.FromUserName;//粉丝用户
            double Latitude = requestMessage.Latitude; //地理位置纬度
            double Longitude = requestMessage.Longitude;//地理位置经度
            DateTime CreateTime = requestMessage.CreateTime;//时间
            double Precision = requestMessage.Precision;//地理位置精度


            //这里是微信客户端（通过微信服务器）自动发送过来的位置信息
            var responseMessage = CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "";
            return responseMessage;//这里也可以返回null（需要注意写日志时候null的问题）
        }

        /// <summary>
        /// 订阅（关注）事件
        /// </summary>
        /// <returns></returns>
        public override IResponseMessageBase OnEvent_SubscribeRequest(RequestMessageEvent_Subscribe requestMessage)
        {
            //TODO:订阅红包被注释了

            //int apiid = wxcomm.getApiid();
            ////------印美图接口 begin------
            ////threeInterface.weipaiInterface wxcf = new threeInterface.weipaiInterface();

            ////wxcf.weipaiSubscribe(requestMessage.FromUserName, apiid);
            ////------印美图接口 end------
            //XjHongBao xjMgr = new XjHongBao();
            //xjMgr.SubscribeHongBao(requestMessage.FromUserName, apiid);
            //return EventProcess(6, requestMessage);

            return null;
        }

        /// <summary>
        /// 退订
        /// 实际上用户无法收到非订阅账号的消息，所以这里可以随便写。
        /// unsubscribe事件的意义在于及时删除网站应用中已经记录的OpenID绑定，消除冗余数据。并且关注用户流失的情况。
        /// </summary>
        /// <returns></returns>
        public override IResponseMessageBase OnEvent_UnsubscribeRequest(RequestMessageEvent_Unsubscribe requestMessage)
        {
            return EventProcess(7, requestMessage);
        }

        private IResponseMessageBase EventProcess(int type, RequestMessageEventBase requestMessage)
        {
            int apiid = wxcomm.getApiid();


            string EventName = "";
            if (requestMessage.Event.ToString().Trim() != "")
            {
                EventName = requestMessage.Event.ToString();
            }


            if (!wxcomm.ExistApiidAndWxId(apiid, requestMessage.ToUserName))
            {  //验证接收方是否为我们系统配置的帐号，即验证微帐号与微信原始帐号id是否一致，如果不一致，说明【1】配置错误，【2】数据来源有问题
                DependencyManager.Resolve<IResponseMessageLogRepository>().Add(apiid, requestMessage.FromUserName, requestMessage.MsgType.ToString(), EventName, "none", "未取到关键词对应的数据", requestMessage.ToUserName);
                return wxcomm.GetResponseMessageTxtByContent(requestMessage, "验证微帐号与微信原始帐号id不一致，可能原因【1】系统配置错误，【2】非法的数据来源", apiid, "系统错误提示");
            }
            bool isExist = wxcomm.wxCodeLegal(apiid);
            if (!isExist)
            {
                DependencyManager.Resolve<IResponseMessageLogRepository>().Add(apiid, requestMessage.FromUserName, requestMessage.MsgType.ToString(), EventName, "none", "账号已过期或已被禁用", requestMessage.ToUserName);
                return wxcomm.GetResponseMessageTxtByContent(requestMessage, "账号已过期或已被禁用！", apiid, "系统错误提示");
            }

            int responseType = 0;
            int rid = rBll.GetRuleIdAndResponseType(apiid, "reqestType=" + type, out responseType);  //取消关注
            if (rid <= 0 || responseType <= 0)
            {
                DependencyManager.Resolve<IResponseMessageLogRepository>().Add(apiid, requestMessage.FromUserName, requestMessage.MsgType.ToString(), EventName, "none", "未取到关键词对应的数据", requestMessage.ToUserName);
                return wxcomm.GetResponseMessageTxtByContent(requestMessage, "欢迎您再次关注！", apiid, "系统错误提示");
            }


            IResponseMessageBase reponseMessage = null;
            switch (responseType)
            {
                case 1:
                    //发送纯文字
                    reponseMessage = wxcomm.GetResponseMessageTxt(requestMessage, rid, apiid, EventName);
                    break;
                case 2:
                    //发送多图文
                    reponseMessage = wxcomm.GetResponseMessageNews(requestMessage, rid, apiid, EventName);
                    break;
                case 3:
                    //发送语音
                    //reponseMessage = wxcomm.GetResponseMessageeMusic(requestMessage, rid, apiid, EventName);
                    reponseMessage = wxcomm.GetResponseMessageVoice(requestMessage, rid, apiid, EventName);
                    break;
                default:
                    break;
            }

            return reponseMessage;

        }

    }
}