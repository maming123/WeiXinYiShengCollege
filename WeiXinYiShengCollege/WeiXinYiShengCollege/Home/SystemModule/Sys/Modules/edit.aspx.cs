using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SNS.Library.SystemModules;

namespace MLK.SystemModule.Sys.Modules
{
    public partial class edit : HospitalBook.WebSite.Home.ManagePageBase
    {
        public bool isHaveChild = false;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Response.Cache.SetNoStore();
            if (!IsPostBack)
            {
                int moduleId =int.Parse(Request["ModuleID"]);
                ShowModuleInfo(moduleId);
                chkIsRootModule.Attributes["onClick"] = "setParentArea(this.checked);";
                //ddlistLinkType.Attributes["onChange"] = "setCreateTableCheckBoxDisplay();";

              List<Module.Models.Sys_Module> sysModuleList =  Module.Models.Sys_Module.Query(" where parent_module_id=@0", moduleId).ToList();
               if(sysModuleList!=null && sysModuleList.Count>0)
               {
                   isHaveChild = true;
               }
            }
        }


        #region 私有方法

        /// <summary>
        /// 显示模块信息
        /// </summary>
        /// <param name="moduleID">模块编号</param>
        private void ShowModuleInfo(int moduleID)
        {
            ModuleDAO moduleDAO = ModuleDAOFactory.CreateObject();
            SNS.Library.SystemModules.Module module = moduleDAO.FindByID(moduleID);

            if (module != null)
            {
                txtModuleName.Text = module.Name;
                txtParentID.Value = module.ParentID.ToString();
                if (module.ParentID != 0)
                {
                    txtParentName.Text = moduleDAO.FindByID(module.ParentID).Name;
                }
                else
                {
                    chkIsRootModule.Checked = true;
                    ClientScript.RegisterStartupScript(typeof(string), "<controlBrowseParent>", "<script>document.all.btnBrowseParent.disabled=true;</script>");
                }
                //txtLinkName.Text = module.LinkPath;
                //txtTargetName.Text = module.LinkTarget;
                chkIsDisplay.Checked = module.Display;
                //ddlistLinkType.SelectedValue = module.LinkType;

                
            }
        }

        #endregion 结束私有方法


        

        protected void btnEdit_Click(object sender, System.EventArgs e)
        {
            SNS.Library.SystemModules.Module module = new SNS.Library.SystemModules.Module();
            module.Name = txtModuleName.Text;
            module.ParentID = int.Parse(txtParentID.Value);
            module.LinkPath = "";// txtLinkName.Text;
            module.Display = chkIsDisplay.Checked;
            module.LinkTarget = "";// txtTargetName.Text;
            module.LinkType = "1";//ddlistLinkType.SelectedValue;

            ModuleDAO moduleDAO = ModuleDAOFactory.CreateObject();
            module.ID = int.Parse(Request["ModuleID"]);
            moduleDAO.Update(module);

            //记录日志
            SNS.Library.Logs.LogDAOFactory.Write("用户：修改了模块信息，被修改的模块名称为“" + module.Name + "”。");

            Response.Redirect("Edit.aspx?ModuleID=" + Request["ModuleID"]);
        }
    }
}