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

namespace WeiXinYiShengCollege.WebSite.jqueryweui.wx
{
    public partial class GoToQuestionResult : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string code = RequestKeeper.GetFormString(Request["code"]);
            if (!string.IsNullOrEmpty(code))
            {
                String OpenId = "";
                //获取Accesstoken 进而获取openId 通过OpenId获取用户信息
                
                    if (code == "aa")
                    {
                        //test 的openid
                        OpenId = "od_wK1iAymw_T0jC10JOzfq1vgvY";
                    }
                    else
                    {
                        OpenId = WeiXinBusiness.GetOpenIdFromOAuthAccessToken(code);
                       
                    }
                    //write openid to cookie
                    Sys_User tmpUser = new Sys_User() { OpenId = OpenId };
                    UserBusiness.WriteCookie(tmpUser);
                    Response.Redirect("questionresult.aspx");
                    Response.End();
                
            }
            else
            {
                Random rd = new Random();
                int r = rd.Next(1, 100000000);
                string redirectUrl = string.Format(@"{0}?r={1}", Request.Url.AbsoluteUri.ToLower(), r);
                //获取公众号的code用来获取openid
                String strUrl = WeiXinBusiness.GetAuthorizeUrlByPopAutherPage(redirectUrl);
                Response.Redirect(strUrl);
            }
            
        }
    }
}