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

namespace WeiXinYiShengCollege.Business
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

    }
}
