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

namespace Senparc.Weixin.MP.Sample.CommonService
{
   public class WeiXinBusiness
    {
       public static readonly string Appid =ConfigurationManager.AppSettings["WeixinAppId"];
       public static readonly string EncodingAESKey = ConfigurationManager.AppSettings["WeixinEncodingAESKey"];
       public static readonly string Token = ConfigurationManager.AppSettings["WeixinToken"];

       public static readonly string AppSecret = ConfigurationManager.AppSettings["WeixinAppSecret"];

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

    }
}
