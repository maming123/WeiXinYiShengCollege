﻿using System;
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

    }
}
