using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.Media;
using Senparc.Weixin.MP.Containers;
using Senparc.Weixin.MP.AdvancedAPIs.QrCode;
using System.IO;
using Senparc.Weixin.MP.AdvancedAPIs.OAuth;
using Senparc.Weixin.MP.AdvancedAPIs.User;
using Senparc.Weixin.MP.Entities;
using Module.Models;
using Module.Utils;
using Senparc.Weixin.MP.AdvancedAPIs.MerChant;
using System.Web;
using WeiXinYiShengCollege.Business;

namespace Senparc.Weixin.MP.Sample.CommonService
{
   public class WeiXinBusiness
    {
       public static readonly string Appid =ConfigurationManager.AppSettings["WeixinAppId"];
       public static readonly string EncodingAESKey = ConfigurationManager.AppSettings["WeixinEncodingAESKey"];
       public static readonly string Token = ConfigurationManager.AppSettings["WeixinToken"];

       public static readonly string AppSecret = ConfigurationManager.AppSettings["WeixinAppSecret"];

       #region 自定义处理逻辑

       public static string GetWelcomeInfo()
       {
           AutoReplyContent arc = AutoReplyContent.SingleOrDefault("where UpKey='默认欢迎语'");
           string welcomeStr = "欢迎关注易生大健康";
           if (null != arc)
           {
               welcomeStr = arc.ReplyContent;
           }
           return welcomeStr;
       }

       /// <summary>
       /// 订阅关注逻辑
       /// </summary>
       /// <param name="eventKey"></param>
       /// <param name="FromUserName">OpenId</param>
       public static string Subscribe(String eventKey, String FromUserName)
       {
           string returnMsg = "";
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
           string lishiName = "";
           int parentId = GetParentIdFromEventKey(eventKey, out lishiName);
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
                   LastScore = 0,
                   UserInfoJson = userInfoJsonStr,
                   UserLevel = Convert.ToInt32(UserLevel.未分配),
                   UserType = Convert.ToInt32(UserType.未分配),
                   HeadImgUrl = headImgUrl
               };
               newUser.Insert();

               if (!string.IsNullOrEmpty(eventKey))
               {
                   returnMsg = string.Format(@"恭喜！{0}已经成为您的健康顾问。", lishiName);// + eventKey;
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
               if (sUserRequest.UserType != (int)UserType.理事类型)
               {
                   if (parentId > 0)
                   {//并且parentId>0 才更新parentid 针对已是粉丝但又关注了公众号的情况不做修改
                       sUser.ParentId = parentId;
                       sUser.Update(new String[] { "ParentId", "UserInfoJson", "IsDelete", "Province", "City", "HeadImgUrl", "UpdateDateTime" });
                       if (!string.IsNullOrEmpty(eventKey))
                       {
                           returnMsg = string.Format(@"恭喜！{0}已经成为您的健康顾问。", lishiName);// + eventKey;
                       }
                   }
                   else
                   {

                       sUser.Update(new String[] { "UserInfoJson", "IsDelete", "Province", "City", "HeadImgUrl", "UpdateDateTime" });
                   }

               }
               else
               {
                   sUser.Update(new String[] { "UserInfoJson", "IsDelete", "Province", "City", "HeadImgUrl", "UpdateDateTime" });
                   returnMsg = GetWelcomeInfo();
               }
           }
           return returnMsg;
       }

       /// <summary>
       /// 看是否携带场景值并且场景值>0，如果携带 证明是谁的粉丝 那么通过场景值（QrCodeScene_id）获取对应的Id
       /// </summary>
       /// <param name="eventKey"></param>
       /// <returns></returns>
       private static int GetParentIdFromEventKey(String eventKey, out String lishiName)
       {
           lishiName = "";
           int parentId = 0;
           if (!string.IsNullOrEmpty(eventKey))
           {   //有场景值
               //responseMessage.Content += "\r\n============\r\n新用户场景值：" + requestMessage.EventKey;

               //首次关注时推送的时间有字符串qrscene  qrscene_1
               if (eventKey.Contains("qrscene_"))
               {
                   eventKey = eventKey.Replace("qrscene_", "");
               }

               int eventKeytmp = 0;
               if (Int32.TryParse(eventKey, out eventKeytmp))
               {
                   //粉丝可以设置ParentID
                   Sys_User sParentUser = Sys_User.SingleOrDefault("where QrCodeScene_id=@0", eventKeytmp);
                   if (sParentUser != null && sParentUser.Id > 0)
                   {
                       parentId = sParentUser.Id;
                       lishiName = sParentUser.NickName;
                   }
               }
           }
           return parentId;
       }

       #endregion

       /// <summary>
       /// 全局注册appid和appsecret 获取token
       /// </summary>
       public static void AccessTokenRegister()
       {
           AccessTokenContainer.Register(Appid,AppSecret);
       }

       /// <summary>
       /// 获取永久素材列表
       /// </summary>
       /// <param name="offset"></param>
       /// <param name="count"></param>
       /// <returns></returns>
       public static MediaList_NewsResult GetNewsMediaList(int offset)
       {
           MediaList_NewsResult r = MediaApi.GetNewsMediaList(Appid, offset, 20);
           return r;
       }

       /// <summary>
       /// 生成二维码
       /// </summary>
       /// <param name="sceneId"></param>
       /// <returns></returns>
       public static CreateQrCodeResult CreateQrCode(int sceneId)
       {
         CreateQrCodeResult r =  QrCodeApi.Create(Appid, 0, sceneId, Senparc.Weixin.MP.QrCode_ActionName.QR_LIMIT_SCENE);
         return r;
       }
       /// <summary>
       /// 获取二维码图片
       /// </summary>
       /// <param name="sceneId"></param>
       /// <returns></returns>
       public static String GetQrCodeImgUrl(int sceneId)
       {
           CreateQrCodeResult r = QrCodeApi.Create(Appid, 0, sceneId, Senparc.Weixin.MP.QrCode_ActionName.QR_LIMIT_SCENE);
           String qrcodeurl = QrCodeApi.GetShowQrCodeUrl(r.ticket);
           return qrcodeurl;
       }
       /// <summary>
       /// 获取二维码图片
       /// </summary>
       /// <param name="sceneId"></param>
       /// <returns></returns>
       public static MemoryStream GetQrCodeImgStream(int sceneId)
       {
           CreateQrCodeResult r = QrCodeApi.Create(Appid, 0, sceneId, Senparc.Weixin.MP.QrCode_ActionName.QR_LIMIT_SCENE);
           MemoryStream ms =new MemoryStream() ;
           QrCodeApi.ShowQrCode(r.ticket,ms);
           return ms;
       }

       /// <summary>
       /// 获取redirectUrl?code=xxx
       /// </summary>
       /// <param name="redirectUrl"></param>
       /// <returns></returns>
       public static string GetAuthorizeUrl(string redirectUrl)
       {

           String strUrl = OAuthApi.GetAuthorizeUrl(Appid, redirectUrl, "none", OAuthScope.snsapi_base);
           
           return strUrl;
       }

       /// <summary>
       /// 获取redirectUrl?code=xxx 弹出授权也没
       /// </summary>
       /// <param name="redirectUrl"></param>
       /// <returns></returns>
       public static string GetAuthorizeUrlByPopAutherPage(string redirectUrl)
       {

           String strUrl = OAuthApi.GetAuthorizeUrl(Appid, redirectUrl, "none", OAuthScope.snsapi_userinfo);

           return strUrl;
       }

       /// <summary>
       /// 通过code获取AuthAccessToken
       /// </summary>
       /// <param name="code"></param>
       /// <param name="getNewToken"></param>
       /// <returns></returns>
       public static String GetOpenIdFromOAuthAccessToken(string code, bool getNewToken = false)
       {
           OAuthAccessTokenResult result = OAuthApi.GetAccessToken(Appid, AppSecret, code);
         
           return result.openid;
       }

       /// <summary>
       /// 通过Api 获取用户信息
       /// </summary>
       /// <param name="openId"></param>
       /// <returns></returns>
       public static UserInfoJson GetUserInfoFromApi(string openId)
       {
           UserInfoJson userInfoJson = AdvancedAPIs.UserApi.Info(Appid, openId);
           return userInfoJson;
       }

       /// <summary>
       /// 当用户微信信息有更新时，从微信api获取用户信息然后更新本地的用户信息
       /// </summary>
       /// <param name="eventKey"></param>
       /// <param name="FromUserName">OpenId</param>
       public static int UpdateSysUser(String FromUserName)
       {
           
           //获取用户信息
           Sys_User sUserRequest = UserBusiness.GetUserInfo(FromUserName);
           //从api获取微信信息
           UserInfoJson userInfoJson = AdvancedAPIs.UserApi.Info(WeiXinBusiness.Appid, FromUserName);
           String userInfoJsonStr = BaseCommon.ObjectToJson(userInfoJson);
           if (null != sUserRequest 
               && null != userInfoJson 
               && sUserRequest.UserInfoJson.Trim() == userInfoJsonStr.Trim())
           {
               return 0;
           }

           string nickName = userInfoJson.nickname;
           string headImgUrl = userInfoJson.headimgurl;
           Area provinceArea, cityArea;
           AreaBusiness.Insert(userInfoJson.province, userInfoJson.city, out provinceArea, out cityArea);
           int provinceId = provinceArea.Id;
           int cityId = cityArea.Id;
           
            //存在 假如存在就恢复删除标识
            Sys_User sUser = new Sys_User() { Id = sUserRequest.Id };
            sUser.Province = provinceId;
            sUser.City = cityId;
            sUser.HeadImgUrl = headImgUrl;
            sUser.UpdateDateTime = DateTime.Now;
            sUser.UserInfoJson = userInfoJsonStr;
            int r =  sUser.Update(new String[] { "UserInfoJson", "Province", "City", "HeadImgUrl", "UpdateDateTime" });
            SNS.Library.Logs.LogDAOFactory.Write("同步用户微信信息到本地", "", nickName + "|" + sUserRequest.Mobile, SNS.Library.Logs.LogType.Application);
            return r;
       }



       #region 消息回复逻辑

       /// <summary>
       /// 获取到自动回复的内容，加工后返回信息
       /// </summary>
       /// <param name="arc"></param>
       public static ResponseMessageText ProcessAutoReplyText(AutoReplyContent arc,ResponseMessageText responseMsgText)
       {
           responseMsgText.Content = arc.ReplyContent;
           return responseMsgText;

       }
       public static ResponseMessageNews ProcessAutoReplyNews(AutoReplyContent arc, ResponseMessageNews responseMsgText)
       {
           List<Article> list = GetArticleList(arc.ReplyContent);
           responseMsgText.ArticleCount = list.Count;
           responseMsgText.Articles.AddRange(list);
           return responseMsgText;
       }
       public static List<Article> GetArticleList(String strJson)
       {
           List<Article> list =   BaseCommon.JsonToObject<List<Article>>(strJson);
           return list;
       }
       

       #endregion

       #region 微信小店逻辑

       /// <summary>
       /// 根据订单状态/创建时间获取订单详情
       /// </summary>
       /// <param name="status">订单状态(不带该字段-全部状态, 2-待发货, 3-已发货, 5-已完成, 8-维权中, )</param>
       /// <param name="beginTime">订单创建时间起始时间(不带该字段则不按照时间做筛选)</param>
       /// <param name="endTime">订单创建时间终止时间(不带该字段则不按照时间做筛选)</param>
       /// <returns></returns>
        public static GetByFilterResult GetOrderList( int? status, DateTime? beginTime, DateTime? endTime)
       {
          string accessToken = AccessTokenContainer.TryGetAccessToken(Appid, AppSecret);

            return OrderApi.GetByFilterOrder(accessToken,status, beginTime,  endTime);

       }

       #endregion

       /// <summary>
       /// 判断是否在微信浏览器内
       /// </summary>
       /// <param name="httpContext"></param>
       /// <returns></returns>
        public static bool IsSideInWeixinBrowser(HttpContextBase httpContext)
       {
           bool isIn = BrowserUtility.BrowserUtility.SideInWeixinBrowser(httpContext);

           return isIn ;
       }
       /// <summary>
       /// 判断是否启用检查微信浏览器 true 启用判断
       /// </summary>
       /// <returns></returns>
       public static bool IsEnabledWeixinBrowser()
        {
            string str = ConfigurationManager.AppSettings["SideInWeixinBrowser"];
            bool isCheck = false;
            if (!string.IsNullOrEmpty(str) && Convert.ToInt32(str) == 1)
            {
                isCheck = true;
            }
            return isCheck;
        }


    }
}
