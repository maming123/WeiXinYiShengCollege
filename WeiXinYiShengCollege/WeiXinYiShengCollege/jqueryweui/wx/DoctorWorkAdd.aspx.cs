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
    public partial class DoctorWorkAdd : PageBase
    {
        public string OpenId = "";
        public List<DoctorInfo> listD = new List<DoctorInfo>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                OpenId = RequestKeeper.GetFormString(Request["OpenId"]);
                listD = GetDoctorList(OpenId);
            }
        }
        protected List<DoctorInfo> GetDoctorList(string openId)
        {
            List<DoctorInfo> list = DoctorBusiness.GetDoctorList(openId);
            if (null == list)
                return new List<DoctorInfo>();
            else
                return list;
        }

    }
}