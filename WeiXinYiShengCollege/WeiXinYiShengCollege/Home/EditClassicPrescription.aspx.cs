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
    /// 经典方剂
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
                try
                {
                    ClassPrescription md = BaseCommon.JsonToObject<ClassPrescription>(str);
                    if (null != md)
                    {
                        //txtbingzheng.Text = md.病症;
                        //txtbianzheng.Text = md.辩证;
                        txtlaiyuan.Text = md.来源;
                        txtzucheng.Text = md.组成;
                        txtgongxiao.Text = md.功效;
                        txtzhuzhi.Text = md.主治;
                        txtyongfa.Text = md.用法;
                        txtqita.Text = md.其他;
                    }
                }catch(Exception ex)
                {
                    SNS.Library.Logs.LogDAOFactory.Write(ex.Message + ex.Source, SNS.Library.Logs.LogType.Error);
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int r=0;
            Sys_Point point = new Sys_Point();

            ClassPrescription md = new ClassPrescription()
            {
                功效 =txtgongxiao.Text
                , 来源=txtlaiyuan.Text
                , 其他=txtqita.Text
                , 用法=txtyongfa.Text
                , 主治 =txtzhuzhi.Text
                , 组成 =txtzucheng.Text
            };
            string strJson = BaseCommon.ObjectToJson(md);
            
            if (string.IsNullOrEmpty(lblPointId.Text))
            {
                point.ModuleId = Convert.ToInt32(this.lblModule.Text);
                point.Content = strJson;
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

                point.Content = strJson;
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