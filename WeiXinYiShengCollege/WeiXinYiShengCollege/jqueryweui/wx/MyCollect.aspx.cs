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
    public partial class MyCollect : PageBase
    {
        public List<dynamic> myCollect = new List<dynamic>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
              List<dynamic> tmp =  MedicineBusiness.GetMyCollectMedicine(UserBusiness.GetCookieUserId());

              if (tmp != null && tmp.Count > 0)
                {
                    myCollect =tmp;
                }
            }
        }
    }
}