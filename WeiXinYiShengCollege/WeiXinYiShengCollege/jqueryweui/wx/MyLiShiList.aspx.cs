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
    public partial class MyLiShiList :PageBase
    {
        public List<Sys_User> myLiShiList = new List<Sys_User>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                int ExpertsSysUserId = RequestKeeper.GetFormInt(Request["Id"]);
                List<Sys_User> list = UserBusiness.GetExportsLiShiListByExpertsSysUserId(ExpertsSysUserId);
                if (null != list)
                {
                    myLiShiList = list;
                }
            }
        }

    }
}