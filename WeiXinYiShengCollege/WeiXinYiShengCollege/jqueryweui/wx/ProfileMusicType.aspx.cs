﻿using System;
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
    public partial class ProfileMusicType : PageBase
    {
        public string OpenId = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                OpenId = RequestKeeper.GetFormString(Request["OpenId"]);
                //int parentId = RequestKeeper.GetFormInt(Request["Id"]);
                //PageList<List<Sys_User>> pageList = UserBusiness.GetMyFansList(parentId, 1, 10000);
                //if(pageList!=null && pageList.Source!=null &&pageList.Source.Count>0)
                //{
                //    myFansList = pageList.Source;
                //}
            }
        }
    }
}