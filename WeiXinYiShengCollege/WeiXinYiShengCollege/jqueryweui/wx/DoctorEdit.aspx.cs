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
    public partial class DoctorEdit : System.Web.UI.Page
    {
        public string OpenId = "";
        public DoctorInfo docInfo = new DoctorInfo();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                int id = RequestKeeper.GetFormInt(Request["Id"]);
                docInfo = GetDoctorInfo(id);
                OpenId = docInfo.CreatorOpenId;
            }
        }


        private DoctorInfo GetDoctorInfo(int doctorId)
        {
            DoctorInfo di = DoctorBusiness.GetDoctorInfo(doctorId);
            return null == di ? new DoctorInfo() : di;
        }
    }
}