using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SNS.Library.SystemModules;

namespace MLK.SystemModule.Sys.Modules
{
    public partial class selectparentmodule : HospitalBook.WebSite.Home.ManagePageBase
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Response.Cache.SetNoStore();
            if (!IsPostBack)
            {
                this.ShowRootModules();
            }
        }


        #region 私有方法

        /// <summary>
        /// 显示根模块
        /// </summary>
        private void ShowRootModules()
        {
            ArrayList rootModules = ModuleDAOFactory.CreateObject().FindRootModules();
            if (rootModules != null)
            {
                for (int i = 0; i < rootModules.Count; i++)
                {
                    SNS.Library.SystemModules.Module rootModule = (SNS.Library.SystemModules.Module)rootModules[i];

                    TreeNode rootNode = new TreeNode();
                    rootNode.Text = rootModule.Name + "&nbsp;&nbsp;&nbsp;[<a href='#' onClick=\"selectParentModule('" +
                        rootModule.ID + "','" + rootModule.Name + "');\">选定</a>]";
                    rootNode.Value = rootModule.ID.ToString();

                    tvModules.Nodes.Add(rootNode);
                }
            }
        }

        /// <summary>
        /// 添加子模块
        /// </summary>
        /// <param name="parentNode">父节点</param>
        private void ShowChildModules(TreeNode parentNode)
        {
            int iParentModuleID = int.Parse(parentNode.Value);
            ArrayList childModules = ModuleDAOFactory.CreateObject().FindByParentModuleID(iParentModuleID);
            if (childModules != null)
            {
                for (int i = 0; i < childModules.Count; i++)
                {
                    SNS.Library.SystemModules.Module childModule = (SNS.Library.SystemModules.Module)childModules[i];

                    TreeNode childNode = new TreeNode();
                    childNode.Text = childModule.Name + "&nbsp;&nbsp;&nbsp;[<a href='#' onClick=\"selectParentModule('" +
                        childModule.ID + "','" + childModule.Name + "');\">选定</a>]";
                    childNode.Value = childModule.ID.ToString();

                    parentNode.ChildNodes.Add(childNode);
                }
                parentNode.Expanded = true;
            }
        }

        #endregion 结束私有方法


        

        protected void tvModules_SelectedNodeChanged(object sender, EventArgs e)
        {
            
            this.ShowChildModules(this.tvModules.SelectedNode);
        }
    }
}