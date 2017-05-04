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
    public partial class DoctorWorkView : PageBase
    {
        public string OpenId = "";
        public DoctorWorkSchedule dws = new DoctorWorkSchedule();
        public DoctorInfo docInfo = new DoctorInfo();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                OpenId = RequestKeeper.GetFormString(Request["OpenId"]);
                int id = RequestKeeper.GetFormInt(Request["Id"]);
                dws = GetDoctorWorkSchedule(OpenId, id);
                docInfo = GetDoctorInfo(dws.DoctorId);

            }
        }

        private DoctorWorkSchedule GetDoctorWorkSchedule(string openId, int id)
        {
            DoctorWorkSchedule dws = DoctorBusiness.GetDoctorWorkSchedule(openId, id);
            return null == dws ? new DoctorWorkSchedule() : dws;

        }
        private DoctorInfo GetDoctorInfo(int doctorId)
        {
            DoctorInfo di = DoctorBusiness.GetDoctorInfo(doctorId);
            return null == di ? new DoctorInfo() : di;
        }
    }
}