using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Module.Utils;
using SNS.Library.SystemModules;

namespace MLK.SystemModule.Sys.Modules
{
    public partial class add : HospitalBook.WebSite.Home.ManagePageBase
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            Response.Cache.SetNoStore();
            if (!IsPostBack)
            {
                chkIsRootModule.Attributes["onClick"] = "setParentArea(this.checked);";
            }
        }


        #region 私有方法






        #endregion 结束私有方法


        

        protected void btnAdd_Click(object sender, System.EventArgs e)
        {
            if(string.IsNullOrEmpty(txtParentID.Value))
            {
                MessageBox.Show(Page, "请选择出版社");
                return;
            }
            SNS.Library.SystemModules.Module module = new SNS.Library.SystemModules.Module();
            module.Name = txtModuleName.Text;
            module.ParentID = int.Parse(txtParentID.Value);
            module.LinkPath = "";// txtLinkName.Text;
            module.LinkTarget = "";//txtTargetName.Text;
            module.Display = chkIsDisplay.Checked;
            module.LinkType = "1";//ddlistLinkType.SelectedValue;
            ModuleDAO moduleDAO = ModuleDAOFactory.CreateObject();
            int iModuleID = moduleDAO.Add(module);

            //记录日志
            SNS.Library.Logs.LogDAOFactory.Write("用户：“" + "”添加了模块信息，模块名称为“" + module.Name + "”。");

            Response.Redirect("Edit.aspx?ModuleID=" + iModuleID);
        }
    }
}