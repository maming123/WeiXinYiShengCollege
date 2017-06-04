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
            //检查是否已同意免责声明
            CheckExceptions(linkType,moduleId);
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

        private  void CheckExceptions(int linkType,int moduleId)
        {
            ExceptionsType et = ExceptionsType.经典方剂;
            if (linkType == (int)SysModuleLinkType.临证参考)
            {
                et = ExceptionsType.临证参考;
            }
            else if (linkType == (int)SysModuleLinkType.经典方剂)
            {
                et = ExceptionsType.经典方剂;
            }
            int userId = UserBusiness.GetCookieUserId();
            bool isHaveAgreeExceptions = MedicineBusiness.IsHaveAgreeExceptions(userId, et);
            if (!isHaveAgreeExceptions)
            {
                //临证参考，目前写死了 2就是临证参考 如果没有点免责声明 那么就不能进入目录列表
                //跳转到免责页面
                Response.Redirect(string.Format(@"ExceptionsNotice.aspx?moduleId={0}&linkType={1}",moduleId,linkType));
            }
        }
    }
}