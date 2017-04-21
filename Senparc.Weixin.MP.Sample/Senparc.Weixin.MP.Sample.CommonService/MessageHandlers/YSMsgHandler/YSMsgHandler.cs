using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Module.Models;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Entities.Request;
using Senparc.Weixin.MP.MessageHandlers;
using Senparc.Weixin.MP.Sample.CommonService.YSMsgHandler;
using WeiXinYiShengCollege.Business;

namespace Senparc.Weixin.MP.Sample.CommonService.MessageHandlers.YSMsgHandler
{
    public partial class YSMsgHandler : MessageHandler<YSMsgContext>
    {
        /*
         * 重要提示：v1.5起，MessageHandler提供了一个DefaultResponseMessage的抽象方法，
         * DefaultResponseMessage必须在子类中重写，用于返回没有处理过的消息类型（也可以用于默认消息，如帮助信息等）；
         * 其中所有原OnXX的抽象方法已经都改为虚方法，可以不必每个都重写。若不重写，默认返回DefaultResponseMessage方法中的结果。
         */

        private string appId = WeiXinBusiness.Appid;
        private string appSecret = WeiXinBusiness.AppSecret;

        public override Entities.IResponseMessageBase DefaultResponseMessage(Entities.IRequestMessageBase requestMessage)
        {
            /* 所有没有被处理的消息会默认返回这里的结果，
           * 因此，如果想把整个微信请求委托出去（例如需要使用分布式或从其他服务器获取请求），
           * 只需要在这里统一发出委托请求，如：
           * var responseMessage = MessageAgent.RequestResponseMessage(agentUrl, agentToken, RequestDocument.ToString());
           * return responseMessage;
           */

            return GetDefaultReply();
            //var responseMessage = this.CreateResponseMessage<ResponseMessageText>();
            //responseMessage.Content = GetWelcomeInfo();
            //return responseMessage;
        }



        /// <summary>
        /// 模板消息集合（Key：checkCode，Value：OpenId）
        /// </summary>
        public static Dictionary<string, string> TemplateMessageCollection = new Dictionary<string, string>();

        public YSMsgHandler(Stream inputStream, PostModel postModel, int maxRecordCount = 0)
            : base(inputStream, postModel, maxRecordCount)
        {
            //这里设置仅用于测试，实际开发可以在外部更全局的地方设置，
            //比如MessageHandler<MessageContext>.GlobalWeixinContext.ExpireMinutes = 3。
            WeixinContext.ExpireMinutes = 3;

            if (!string.IsNullOrEmpty(postModel.AppId))
            {
                appId = postModel.AppId;//通过第三方开放平台发送过来的请求
            }

            //在指定条件下，不使用消息去重
            base.OmitRepeatedMessageFunc = requestMessage =>
            {
                var textRequestMessage = requestMessage as RequestMessageText;
                if (textRequestMessage != null && textRequestMessage.Content == "容错")
                {
                    return false;
                }
                return true;
            };
        }

        public YSMsgHandler(RequestMessageBase requestMessage)
            : base(requestMessage)
        {
        }

        public override void OnExecuting()
        {
            //测试MessageContext.StorageData
            if (CurrentMessageContext.StorageData == null)
            {
                CurrentMessageContext.StorageData = 0;
            }
            base.OnExecuting();
        }

        public override void OnExecuted()
        {
            base.OnExecuted();
            CurrentMessageContext.StorageData = ((int)CurrentMessageContext.StorageData) + 1;
        }

        public IResponseMessageBase GetDefaultReply()
        {
            var responseMessage = base.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = GetWelcomeInfo();
            return responseMessage;
        }

        /// <summary>
        /// 处理文字请求
        /// </summary>
        /// <returns></returns>
        public override IResponseMessageBase OnTextRequest(RequestMessageText requestMessage)
        {
            //TODO:这里的逻辑可以交给Service处理具体信息，参考OnLocationRequest方法或/Service/LocationSercice.cs

            #region //书中例子
            //if (requestMessage.Content == "你好")
            //{
            //    var responseMessage = base.CreateResponseMessage<ResponseMessageNews>();
            //    var title = "Title";
            //    var description = "Description";
            //    var picUrl = "PicUrl";
            //    var url = "Url";
            //    responseMessage.Articles.Add(new Article()
            //    {
            //        Title = title,
            //        Description = description,
            //        PicUrl = picUrl,
            //        Url = url
            //    });
            //    return responseMessage;
            //}
            //else if (requestMessage.Content == "Senparc")
            //{
            //    //相似处理逻辑
            //}
            //else
            //{
            //    //...
            //}
            #endregion

            List<AutoReplyContent> list = MsgAutoReplyBusiness.GetReplyContentList();
            if (null == list)
            {
                return GetDefaultReply();
            }

            AutoReplyContent arc = list.Find(m => m.UpKey == requestMessage.Content.Trim().ToLower());
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

            

        }

    }
}
