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
    public partial class DoctorManage : PageBase
    {
        public string OpenId = "";
        public List<DoctorInfo> listDoctor = new List<DoctorInfo>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                OpenId = RequestKeeper.GetFormString(Request["OpenId"]);
                listDoctor = DoctorBusiness.GetDoctorList(OpenId);
            }
        }
    }
}