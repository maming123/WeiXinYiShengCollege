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
    public partial class MyExchangeList : PageBase
    {
        public List<ExchangeLog> myExchangeList = new List<ExchangeLog>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string OpenId = RequestKeeper.GetFormString(Request["OpenId"]);
                List<ExchangeLog> list = ScoreBusiness.GetExchangeLogList(OpenId);
                if (list != null)
                {
                    myExchangeList = list;
                }
            }
        }
    }
}