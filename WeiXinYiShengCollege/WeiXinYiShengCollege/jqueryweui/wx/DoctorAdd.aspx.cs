using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Module.Utils;

namespace WeiXinYiShengCollege.WebSite.jqueryweui.wx
{
    public partial class DoctorAdd : PageBase
    
    {
        public string OpenId = "";
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                OpenId = RequestKeeper.GetFormString(Request["OpenId"]);
                
            }
        }

    }
}