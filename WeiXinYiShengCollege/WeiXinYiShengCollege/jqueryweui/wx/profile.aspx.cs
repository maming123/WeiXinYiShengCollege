using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Module.Models;
using Module.Utils;
using Senparc.Weixin.MP.Sample.CommonService;
using WeiXinYiShengCollege.Business;

namespace WeiXinYiShengCollege.WebSite.wx
{
    public partial class profile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string code =RequestKeeper.GetFormString(Request["code"]);
            if(!string.IsNullOrEmpty(code))
            {
                //获取Accesstoken 进而获取openId 通过OpenId获取用户信息
                string openId = WeiXinBusiness.GetOpenIdFromOAuthAccessToken(code);
                Sys_User sUser =UserBusiness.GetUserInfo(openId);
                Response.Write(openId+BaseCommon.ObjectToJson(sUser));
               
            }else
            {
                Response.Write("获取code失败,请重试");
            }
        }
    }
}