using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HospitalBook.WebSite.Home;
using Module.Models;
using Module.Utils;
using WeiXinYiShengCollege.Business.Common.Models;

namespace WeiXinYiShengCollege.WebSite.Home
{
    /// <summary>
    /// 临证参考
    /// </summary>
    public partial class EditClassicPrescription : ManagePageBase
    {
        public int moduleId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request["ModuleID"] != null)
                {
                    moduleId = Convert.ToInt32(Request["ModuleID"]);
                    lblModule.Text = moduleId.ToString();
                }
                ShowDetail(moduleId);
            }
        }
        private void ShowDetail(int moduleId)
        {
            Sys_Module unit = Sys_Module.Single((object)moduleId);
            Sys_Point point = Sys_Point.FirstOrDefault(@"where ModuleId=@0", moduleId);
            txttitle.Text = unit.MODULE_NAME;
            if (point != null && point.Id > 0)
            {
                lblPointId.Text = point.Id.ToString();
                string str = point.Content;
                txtContent.Text = str;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int r=0;
            Sys_Point point = new Sys_Point();
            
            if (string.IsNullOrEmpty(lblPointId.Text))
            {
                point.ModuleId = Convert.ToInt32(this.lblModule.Text);
                point.Content = txtContent.Text;
                point.Title = txttitle.Text;
                point.CreateDateTime = DateTime.Now;
               object objr= point.Insert();
                r =Convert.ToInt32(objr);
                if (r > 0)
                {
                    MessageBox.Show(Page, "添加成功");
                }
            }
            else
            {
                point = Sys_Point.Single((object)Convert.ToInt32(lblPointId.Text));

                point.Content = txtContent.Text;
                point.Title = txttitle.Text;
                point.UpdateDateTime = DateTime.Now;

                 r= point.Update();
                 if (r > 0)
                 {
                     MessageBox.Show(Page, "更新成功");
                 }
            }
            
        }
    }
}