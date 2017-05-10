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
            AutoReplyContent arc = AutoReplyContent.SingleOrDefault("where UpKey='默认欢迎语'");
            string welcomeStr = "欢迎关注易生大健康";
            if(null!=arc)
            {
                welcomeStr = arc.ReplyContent;
            }
            return welcomeStr;
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
            Subscribe(requestMessage.EventKey, requestMessage.FromUserName, responseMessage);

            //responseMessage.Content = responseMessage.Content ?? string.Format("通过扫描二维码进入，场景值：{0}", requestMessage.EventKey);

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
            Subscribe(requestMessage.EventKey,requestMessage.FromUserName, responseMessage);
            
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

   


        #region 自定义处理逻辑

        /// <summary>
        /// 订阅关注逻辑
       /// </summary>
       /// <param name="eventKey"></param>
       /// <param name="FromUserName">OpenId</param>
       /// <param name="responseMessage"></param>
        private static void Subscribe(String eventKey, String FromUserName, ResponseMessageText responseMessage)
        {
            //订阅，需要插入到数据库 新创建用户
            Sys_User sUserRequest = UserBusiness.GetUserInfo(FromUserName);

            UserInfoJson userInfoJson = AdvancedAPIs.UserApi.Info(WeiXinBusiness.Appid, FromUserName);
            String userInfoJsonStr = BaseCommon.ObjectToJson(userInfoJson);
            string nickName = userInfoJson.nickname;
            string headImgUrl = userInfoJson.headimgurl;
            Area provinceArea, cityArea;
            AreaBusiness.Insert(userInfoJson.province, userInfoJson.city, out provinceArea, out cityArea);
            int provinceId = provinceArea.Id;
            int cityId = cityArea.Id;
            // 根据场景值获取parentId
            string lishiName="";
            int parentId = GetParentIdFromEventKey(eventKey,out lishiName);
            if (sUserRequest == null || (sUserRequest != null && sUserRequest.Id <= 0))
            {//新用户
                Sys_User newUser = new Sys_User()
                {
                    ApproveFlag = Convert.ToInt32(ApproveFlag.未认证),
                    City = cityId,
                    CompanyName = "",
                    CreateDateTime = DateTime.Now,
                    CustomerManagerId = 0,
                    Email = "",
                    IsDelete = 0,
                    Mobile = 0,
                    NickName = nickName,
                    OpenId = FromUserName,
                    ParentId = parentId,
                    PassWord = "",
                    Province = provinceId,
                    QrCodeScene_id = 0,
                    Remark = "",
                    Score = 0,
                    LastScore=0,
                    UserInfoJson = userInfoJsonStr,
                    UserLevel = Convert.ToInt32(UserLevel.未分配),
                    UserType = Convert.ToInt32(UserType.未分配),
                    HeadImgUrl = headImgUrl
                };
                newUser.Insert();

                if (!string.IsNullOrEmpty(eventKey))
                {
                    responseMessage.Content += string.Format(@"恭喜！{0}已经成为您的私人医生。", lishiName);// + eventKey;
                }
            }
            else
            {
                
                //存在 假如存在就恢复删除标识
                Sys_User sUser = new Sys_User() { Id = sUserRequest.Id };
                sUser.IsDelete = 0;
                sUser.Province = provinceId;
                sUser.City = cityId;
                sUser.HeadImgUrl = headImgUrl;
                sUser.UpdateDateTime = DateTime.Now;
                sUser.UserInfoJson = userInfoJsonStr;

                // 已分配理事的粉丝可以更换理事 所以去掉&& sUserRequest.ParentId==0)
                if (sUserRequest.UserType != (int)UserType.理事类型 )
                {
                    if (parentId > 0)
                    {//并且parentId>0 才更新parentid 针对已是粉丝但又关注了公众号的情况不做修改
                        sUser.ParentId = parentId;
                        sUser.Update(new String[] { "ParentId", "UserInfoJson", "IsDelete", "Province", "City", "HeadImgUrl", "UpdateDateTime" });
                        if (!string.IsNullOrEmpty(eventKey))
                        {
                            responseMessage.Content += string.Format(@"恭喜！{0}已经成为您的私人医生。", lishiName);// + eventKey;
                        }
                    }else
                    {
                        
                        sUser.Update(new String[] {  "UserInfoJson", "IsDelete", "Province", "City", "HeadImgUrl", "UpdateDateTime" });
                    }

                }else
                {
                    sUser.Update(new String[] {  "UserInfoJson", "IsDelete", "Province", "City", "HeadImgUrl", "UpdateDateTime" });
                }

                

            }
        }

        /// <summary>
        /// 看是否携带场景值并且场景值>0，如果携带 证明是谁的粉丝 那么通过场景值（QrCodeScene_id）获取对应的Id
        /// </summary>
        /// <param name="eventKey"></param>
        /// <returns></returns>
        private static int GetParentIdFromEventKey(String eventKey,out String lishiName)
        {
            lishiName="";
            int parentId = 0;
            if (!string.IsNullOrEmpty(eventKey))
            {   //有场景值
                //responseMessage.Content += "\r\n============\r\n新用户场景值：" + requestMessage.EventKey;
                
                //首次关注时推送的时间有字符串qrscene  qrscene_1
                if(eventKey.Contains("qrscene_"))
                {
                    eventKey = eventKey.Replace("qrscene_","");
                }

                int eventKeytmp = 0;
                if (Int32.TryParse(eventKey, out eventKeytmp))
                {
                    //粉丝可以设置ParentID
                    Sys_User sParentUser = Sys_User.SingleOrDefault("where QrCodeScene_id=@0", eventKeytmp);
                    if (sParentUser != null && sParentUser.Id > 0)
                    {
                        parentId = sParentUser.Id;
                        lishiName =sParentUser.NickName;
                    }
                }
            }
            return parentId;
        }

        #endregion

    }
}
