using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.Media;

namespace WeiXinYiShengCollege.Business
{
   public class WeiXinBusiness
    {
       public static readonly string  Appid =ConfigurationManager.AppSettings["WeixinAppId"];
       public static MediaList_NewsResult GetNewsMediaList()
       {
          MediaList_NewsResult r = MediaApi.GetNewsMediaList(Appid,0,20);
          return r;
       }
    }
}
