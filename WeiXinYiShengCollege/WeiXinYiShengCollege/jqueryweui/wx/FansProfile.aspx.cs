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
    public partial class FansProfile : System.Web.UI.Page
    {

        public Sys_User sUser = new Sys_User();

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                string OpenId = RequestKeeper.GetFormString(Request["OpenId"]);
                Sys_User tmpUser = UserBusiness.GetUserInfo(OpenId);
                if(tmpUser!=null)
                {
                    sUser = tmpUser;
                }
            }
            
        }
    }
}