using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.Media;
using Senparc.Weixin.MP.Containers;

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
    }
}
