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
    public partial class MedicineList : PageBase
    {
        public List<Sys_Module> mySysModuleList = new List<Sys_Module>();
        protected void Page_Load(object sender, EventArgs e)
        {
            int moduleId = RequestKeeper.GetFormInt(Request["moduleId"]);
            int linkType = RequestKeeper.GetFormInt(Request["linkType"]);
           List<Sys_Module>  tmp = MedicineBusiness.GetSysModuleList(moduleId);

           if (null != tmp && tmp.Count>0)
           {
               mySysModuleList = tmp;
           }
           else
           {
               //跳转到具体的药方页面
               if (linkType == (int)SysModuleLinkType.临证参考)
               {

                   Response.Redirect("MedicalGuide.aspx?moduleId=" + moduleId);

               }else if(linkType==(int)SysModuleLinkType.经典方剂)
               {
                   Response.Redirect("ClassicPrescription.aspx?moduleId=" + moduleId);
               }
           }

        }
    }
}