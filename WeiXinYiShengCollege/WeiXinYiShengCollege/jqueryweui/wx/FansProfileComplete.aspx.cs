using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Module.Utils;

namespace WeiXinYiShengCollege.WebSite.jqueryweui.wx
{
    public partial class FansProfileComplete : PageBase
    {
        public String OpenId = "0";
        protected void Page_Load(object sender, EventArgs e)
        {
            OpenId = RequestKeeper.GetFormString(Request["OpenId"]);
        }
    }
}