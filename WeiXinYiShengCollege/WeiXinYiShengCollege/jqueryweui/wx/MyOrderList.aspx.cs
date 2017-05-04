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
    public partial class MyOrderList : PageBase
    {
        public List<OrderInfo> myOrderList = new List<OrderInfo>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string openId = RequestKeeper.GetFormString(Request["OpenId"]);
                List<OrderInfo> list = OrderBusiness.GetOrderInfoListFromDB(openId);
                if (null != list)
                {
                    myOrderList = list;
                }
            }
        }
        protected string GetStatus(object status)
        {
            string statusId = status.ToString();
            if (statusId == "2")
                return "待发货";
            else if (statusId == "3")
            {
                return "已发货";
            }
            else if (statusId == "5")
            {
                return "已完成";
            }
            else
            {
                return "未知";
            }

        }
    }
}