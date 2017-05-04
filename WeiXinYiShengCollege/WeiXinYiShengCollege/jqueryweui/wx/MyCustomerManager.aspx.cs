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
    public partial class MyCustomerManager : PageBase
    {
        public CustomerManager cM = new CustomerManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            int CustomerManagerId = RequestKeeper.GetFormInt(Request["CustomerManagerId"]);
            CustomerManager cMTmp =  UserBusiness.GetCustomerManagerInfo(CustomerManagerId);
            if (null != cMTmp)
                cM = cMTmp;
        }
    }
}