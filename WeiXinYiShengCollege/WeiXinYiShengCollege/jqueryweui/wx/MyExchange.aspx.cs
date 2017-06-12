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
    public partial class Exchange : PageBase
    {
        public String OpenId = "0";

        public decimal validScore = 0;
        public decimal totalLastScore = 0;
        public decimal totalScore = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                OpenId = RequestKeeper.GetFormString(Request["OpenId"]);
                Sys_User sUser = UserBusiness.GetUserInfo(OpenId);
                if(null !=sUser)
                {
                    totalScore = sUser.Score;
                    totalLastScore = sUser.LastScore;
                    validScore = ScoreBusiness.GetValidExchangeScore(OpenId, sUser.LastScore);
                }
            }
        }

    }
}