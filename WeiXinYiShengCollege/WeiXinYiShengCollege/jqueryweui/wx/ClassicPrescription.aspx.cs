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
    /// <summary>
    /// 经典方剂
    /// </summary>
    public partial class ClassicPrescription : PageBase
    {

        public Sys_Point sysPoint = new Sys_Point();

        protected void Page_Load(object sender, EventArgs e)
        {
            int moduleId = RequestKeeper.GetFormInt(Request["moduleId"]);
            Sys_Point tmp = MedicineBusiness.GetSysPoint(moduleId);
            if(null!=tmp && tmp.Id>0)
            {
                sysPoint = tmp;
                //update seecount
                tmp.SeeCount++;
                tmp.Update();
            }
        }
    }
}
