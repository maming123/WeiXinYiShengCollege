using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Module.Models;
using Module.Utils;
using WeiXinYiShengCollege.Business;

namespace WeiXinYiShengCollege.WebSite.jqueryweui.wx
{
    public partial class ProfileEdit : System.Web.UI.Page
    {

        public String OpenId = "0";
        public Sys_User sUser = new Sys_User();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                OpenId = RequestKeeper.GetFormString(Request["OpenId"]);
                ShowDetail(OpenId);
            }
        }

        private void ShowDetail(string openId)
        {
            Sys_User sUserTmp = UserBusiness.GetUserInfo(openId);
            sUser = sUserTmp;
        }
    }
}