using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Module.Models;
using Module.Utils;
using Senparc.Weixin.MP.AdvancedAPIs.User;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Sample.CommonService.Download;
using WeiXinYiShengCollege.Business;

namespace Senparc.Weixin.MP.Sample.CommonService.MessageHandlers.YSMsgHandler
{
    public partial class YSMsgHandler
    {

        private string GetWelcomeInfo()
        {
           return  WeiXinBusiness.GetWelcomeInfo();
        }

        public string GetDownloadInfo(CodeRecord codeRecord)
        {
            return string.Format(@"您已通过二维码验证，
当前选择的版本：v{0}", codeRecord.Version);
        }

        /// <summary>
        /// 通过二维码扫描关注扫描事件
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnEvent_ScanRequest(RequestMessageEvent_Scan requestMessage)
        {
            //通过扫描关注
            var responseMessage = CreateResponseMessage<ResponseMessageText>();

            //EventKey 对应数据库里的Sys_User表的QrCodeScene_id 字段值
            String strMsg = WeiXinBusiness.Subscribe(requestMessage.EventKey, requestMessage.FromUserName);

            //responseMessage.Content = responseMessage.Content ?? string.Format("通过扫描二维码进入，场景值：{0}", requestMessage.EventKey);

            responseMessage.Content += strMsg;

            responseMessage.Content = responseMessage.Content ?? GetWelcomeInfo();

            return responseMessage;
        }

        /// <summary>
        /// 订阅（关注）事件
        /// </summary>
        /// <returns></returns>
        public override IResponseMessageBase OnEvent_SubscribeRequest(RequestMessageEvent_Subscribe requestMessage)
        {
            var responseMessage = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageText>(requestMessage);
            responseMessage.Content = GetWelcomeInfo() + "\r\n\r\n\r\n";
           String strMsg= WeiXinBusiness.Subscribe(requestMessage.EventKey,requestMessage.FromUserName);

           responseMessage.Content += strMsg;

            return responseMessage;
        }

        /// <summary>
        /// 退订
        /// 实际上用户无法收到非订阅账号的消息，所以这里可以随便写。
        /// unsubscribe事件的意义在于及时删除网站应用中已经记录的OpenID绑定，消除冗余数据。并且关注用户流失的情况。
        /// </summary>
        /// <returns></returns>
        public override IResponseMessageBase OnEvent_UnsubscribeRequest(RequestMessageEvent_Unsubscribe requestMessage)
        {
            var responseMessage = base.CreateResponseMessage<ResponseMessageText>();

            Sys_User sUserRequest = UserBusiness.GetUserInfo(requestMessage.FromUserName);
            Sys_User sUser = new Sys_User() { Id = sUserRequest.Id };
            sUser.IsDelete = 1;

            sUser.Update(new String[] {  "IsDelete" });

            responseMessage.Content = "有空再来";
            return responseMessage;
        }


        /// <summary>
        /// 点击事件
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnEvent_ClickRequest(RequestMessageEvent_Click requestMessage)
        {
            IResponseMessageBase reponseMessage = null;
            //菜单点击，需要跟创建菜单时的Key匹配

            List<AutoReplyContent> list = MsgAutoReplyBusiness.GetReplyContentList();
            if (null == list)
            {
                return GetDefaultReply();
            }

            AutoReplyContent arc = list.Find(m => m.UpKey == requestMessage.EventKey.Trim().ToLower());
            if (null == arc)
            {
                return GetDefaultReply();
            }


            if (arc.ResponseMsgType == ResponseMsgType.Text.ToString().ToLower())
            {
                var responseMessage = base.CreateResponseMessage<ResponseMessageText>();
                return WeiXinBusiness.ProcessAutoReplyText(arc, responseMessage);

            }
            else if (arc.ResponseMsgType == ResponseMsgType.News.ToString().ToLower())
            {
                var responseMessage = base.CreateResponseMessage<ResponseMessageNews>();
                return WeiXinBusiness.ProcessAutoReplyNews(arc, responseMessage);
            }
            else
            {
                return GetDefaultReply();
            }


            //return reponseMessage;
        }

   


        

    }
}
