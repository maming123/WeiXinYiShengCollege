using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Module.Utils;

namespace WeiXinYiShengCollege.WebSite.jqueryweui.wx
{
    public partial class ExceptionsNotice : System.Web.UI.Page
    {
        public int moduleId = 0;
        public int linkType = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                 moduleId = RequestKeeper.GetFormInt(Request["moduleId"]);
                 linkType = RequestKeeper.GetFormInt(Request["linkType"]);
            }
        }
    }
}