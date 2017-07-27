using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Module.Models;
using Module.Utils;
using WeiXinYiShengCollege.Business;
using WeiXinYiShengCollege.Business.Common.Models;

namespace WeiXinYiShengCollege.WebSite.jqueryweui.wx
{
    /// <summary>
    /// 经典方剂
    /// </summary>
    public partial class ClassicPrescription : PageBase
    {
        public Sys_Point sysPoint = new Sys_Point();
        public ClassPrescription cp = new ClassPrescription();
        public bool IsHaveZan = false;
        public bool IsCollect = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            int moduleId = RequestKeeper.GetFormInt(Request["moduleId"]);
            Sys_Point tmp = MedicineBusiness.GetSysPoint(moduleId);
            if(null!=tmp && tmp.Id>0)
            {
                sysPoint = tmp;

                 cp = MedicineBusiness.GetClassPrescriptionFromContent(tmp.Content);

                //update seecount
                tmp.SeeCount++;

                tmp.Update(new String[]{ "SeeCount" });
                IsHaveZan = MedicineBusiness.IsHaveZan(UserBusiness.GetCookieUserId(), tmp.Id);
                IsCollect = MedicineBusiness.IsCollect(UserBusiness.GetCookieUserId(), tmp.Id);
            }
        }
    }
}
