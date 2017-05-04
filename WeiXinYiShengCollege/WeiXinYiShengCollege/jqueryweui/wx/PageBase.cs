using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Senparc.Weixin.BrowserUtility;
using Senparc.Weixin.MP.Sample.CommonService;

namespace WeiXinYiShengCollege.WebSite.jqueryweui.wx
{
    public class PageBase : System.Web.UI.Page
    {
        protected override void OnLoad(EventArgs e)
        {
            if (WeiXinBusiness.IsEnabledWeixinBrowser() && !WeiXinBusiness.IsSideInWeixinBrowser(Request.RequestContext.HttpContext))
            {
                Response.Write("请在微信内部打开");
                Response.End();
            }

            base.OnLoad(e);
        }
    }
}