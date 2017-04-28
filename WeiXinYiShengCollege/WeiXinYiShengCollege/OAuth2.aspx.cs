using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Senparc.Weixin.MP.Sample.CommonService;

namespace WeiXinYiShengCollege.WebSite
{
    public partial class OAuth2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string redirectUrl = string.Format(@"{0}", Request.Url.AbsoluteUri.Replace("oauth2.aspx", "jqueryweui/wx/goto.aspx"));
            String strUrl = WeiXinBusiness.GetAuthorizeUrl(redirectUrl);
            Response.Redirect(strUrl);
        }
    }
}