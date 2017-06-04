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
    public partial class modulelist : HospitalBook.WebSite.Home.ManagePageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                linkBtnDelete.Attributes["onClick"] = "return confirm('您确定要删除当前模块吗？');";
                this.ShowRootModules();

                if (treeModules.Nodes.Count > 0)
                {
                    TreeNode currentNode = treeModules.Nodes[0];
                    currentNode.ChildNodes.Clear();
                    this.ShowChildModules(currentNode);
                }
            }
            else
            {
                if (Request["hiddenTxtModuleID"] == null || Request["hiddenTxtModuleID"] == "")
                {
                    if (treeModules.Nodes.Count > 0)
                    {
                        string strUrl = "";
                        if (treeModules.SelectedNode != null)
                        {
                            strUrl = "Edit.aspx?ModuleID=" + treeModules.SelectedNode.Value;
                        }
                        else
                        {
                            strUrl = "Edit.aspx?ModuleID=" + treeModules.Nodes[0].Value;
                        }
                        ClientScript.RegisterStartupScript(typeof(string), "showEditWindow()", "<script>window.open('" + strUrl + "','ifrmModuleInfo');</script>");
                    }
                }
                else
                {
                    //模块排序（设置到最后）
                    string strModuleID = Request["hiddenTxtModuleID"].Split('$')[0];
                    if (Request["hiddenTxtModuleID"].ToUpper().EndsWith("UP"))
                    {
                        ModuleDAOFactory.CreateObject().MoveOrderToUp(strModuleID);
                    }
                    else if (Request["hiddenTxtModuleID"].ToUpper().EndsWith("DOWN"))
                    {
                        ModuleDAOFactory.CreateObject().MoveOrderToDown(strModuleID);
                    }
                    else
                    {
                        ModuleDAOFactory.CreateObject().SetOrderToEnd(strModuleID);
                    }
                    Response.Redirect("ModuleList.aspx?ModuleID=" + Request["ModuleID"]);
                }
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

                    if (i == 0)
                    {
                        //rootNode.Text = rootModule.Name + "&nbsp;&nbsp;[<a href=\"#\" onClick=\"setModuleOrder('" +
                        //    rootModule.ID + "$Last');\">排到最后</a>]&nbsp;<a href=\"#\" onClick=\"setModuleOrder('" +
                        //    rootModule.ID + "$Down');\"><img border=\"0\" src=\"" +
                        //    SNS.Library.Tools.StyleHelper.GetStyle("winfo/LevelDown.gif") + "\"></a>";
                        rootNode.Text = rootModule.Name ;
                    }
                    else if (i == rootModules.Count - 1)
                    {
                        //rootNode.Text = rootModule.Name + "&nbsp;&nbsp;<a href=\"#\" onClick=\"setModuleOrder('" +
                        //    rootModule.ID + "$Up');\"><img border=\"0\" src=\"" +
                        //    SNS.Library.Tools.StyleHelper.GetStyle("winfo/LevelUp.gif") + "\"></a>";
                        rootNode.Text = rootModule.Name;
                    }
                    else
                    {
                        //rootNode.Text = rootModule.Name + "&nbsp;&nbsp;[<a href=\"#\" onClick=\"setModuleOrder('" +
                        //    rootModule.ID + "');\">排到最后</a>]&nbsp;<a href=\"#\" onClick=\"setModuleOrder('" +
                        //    rootModule.ID + "$Up');\"><img border=\"0\" src=\"" +
                        //    SNS.Library.Tools.StyleHelper.GetStyle("winfo/LevelUp.gif") + "\"></a>" +
                        //    "&nbsp;<a href=\"#\" onClick=\"setModuleOrder('" +
                        //    rootModule.ID + "$Down');\"><img border=\"0\" src=\"" +
                        //    SNS.Library.Tools.StyleHelper.GetStyle("winfo/LevelDown.gif") + "\"></a>";
                        rootNode.Text = rootModule.Name ;
                    }

                    rootNode.Value = rootModule.ID.ToString();
                    treeModules.Nodes.Add(rootNode);

                    this.ShowChildModules(rootNode);
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
            ModuleDAO moduleDAO = ModuleDAOFactory.CreateObject();
            if (childModules != null)
            {
                for (int i = 0; i < childModules.Count; i++)
                {
                    SNS.Library.SystemModules.Module childModule = (SNS.Library.SystemModules.Module)childModules[i];

                    TreeNode childNode = new TreeNode();

                    if (i == 0)
                    {
                        //childNode.Text = childModule.Name + "&nbsp;&nbsp;[<a href=\"#\" onClick=\"setModuleOrder('" +
                        //    childModule.ID + "');\">排到最后</a>]&nbsp;<a href=\"#\" onClick=\"setModuleOrder('" +
                        //    childModule.ID + "$Down');\"><img border=\"0\" src=\"" +
                        //    SNS.Library.Tools.StyleHelper.GetStyle("winfo/LevelDown.gif") + "\"></a>";
                        childNode.Text = childModule.Name ;
                    }
                    else if (i == childModules.Count - 1)
                    {
                        //childNode.Text = childModule.Name + "&nbsp;&nbsp;<a href=\"#\" onClick=\"setModuleOrder('" +
                        //    childModule.ID + "$Up');\"><img border=\"0\" src=\"" +
                        //    SNS.Library.Tools.StyleHelper.GetStyle("winfo/LevelUp.gif") + "\"></a>";
                        childNode.Text = childModule.Name ;
                    }
                    else
                    {
                        //childNode.Text = childModule.Name + "&nbsp;&nbsp;[<a href=\"#\" onClick=\"setModuleOrder('" +
                        //    childModule.ID + "');\">排到最后</a>]&nbsp;<a href=\"#\" onClick=\"setModuleOrder('" +
                        //    childModule.ID + "$Up');\"><img border=\"0\" src=\"" +
                        //    SNS.Library.Tools.StyleHelper.GetStyle("winfo/LevelUp.gif") + "\"></a>" +
                        //    "&nbsp;<a href=\"#\" onClick=\"setModuleOrder('" +
                        //    childModule.ID + "$Down');\"><img border=\"0\" src=\"" +
                        //    SNS.Library.Tools.StyleHelper.GetStyle("winfo/LevelDown.gif") + "\"></a>";
                        childNode.Text = childModule.Name ;
                    }

                    childNode.Value = childModule.ID.ToString();
                    parentNode.ChildNodes.Add(childNode);

                    this.ShowChildModules(childNode);
                }
                parentNode.Expanded = false;
            }

            if (treeModules.SelectedNode != null)
            {
                treeModules.SelectedNode.Expanded = true;
            }
        }

        #endregion 结束私有方法

        protected void linkBtnDelete_Click(object sender, System.EventArgs e)
        {
            ModuleDAO moduleDAO = ModuleDAOFactory.CreateObject();

            int iModuleID = int.Parse(treeModules.SelectedNode.Value);
            SNS.Library.SystemModules.Module module = moduleDAO.FindByID(iModuleID);
            string strModuleName = (module != null ? module.Name : "");

            moduleDAO.Delete(iModuleID);

            //记录日志
            string strLog =  "(" + Request.UserHostAddress + ")" + "删除模块“" +
                strModuleName + "”。";
            SNS.Library.Logs.LogDAOFactory.Write(strLog);

            Response.Redirect("ModuleList.aspx");
        }

        protected void treeModules_SelectedNodeChanged(object sender, EventArgs e)
        {
            TreeNode currentNode = treeModules.SelectedNode;
            currentNode.ChildNodes.Clear();
            this.ShowChildModules(currentNode);
        }
    }
}