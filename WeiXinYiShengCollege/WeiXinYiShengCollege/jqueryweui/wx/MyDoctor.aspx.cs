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
    public partial class MyDoctor : PageBase
    {
        public Sys_User cM = new Sys_User();
        protected void Page_Load(object sender, EventArgs e)
        {
            string openID = RequestKeeper.GetFormString(Request["OpenId"]);
            Sys_User cMTmp = UserBusiness.GetUserInfo(openID);

            if (null != cMTmp)
            {
              Sys_User cMDoctor =  UserBusiness.GetUserInfoById(cMTmp.ParentId);
                if(null !=cMDoctor)
                {
                    cM = cMDoctor;
                }

            }
                
        }
    }
}