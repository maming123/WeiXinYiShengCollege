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
    public partial class EditPoint : ManagePageBase
    {
        public int moduleId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request["ModuleID"] != null)
                {
                    moduleId = Convert.ToInt32(Request["ModuleID"]);
                }
                ShowDetail(moduleId);
            }
        }
        private void ShowDetail(int moduleId)
        {
            Sys_Module unit = Sys_Module.Single((object)moduleId);
            Sys_Point point = Sys_Point.FirstOrDefault(@"where ModulelId=@0", moduleId);
            if (point != null && point.Id > 0)
            {
                lblPointId.Text = point.Id.ToString();
                lblModule.Text = unit.MODULE_NAME;
                string str = point.Content;
                Medicine md =  BaseCommon.JsonToObject<Medicine>(str);
                if (null != md)
                {
                    txtbingzheng.Text = md.病症;
                    txtbianzheng.Text = md.辩证;
                    txterxue.Text = md.外治法.耳穴;
                    txtjingyanfang.Text = md.内治法.经验方;
                    txtjinji.Text = md.禁忌;
                    txtjizhu.Text = md.外治法.脊柱;
                    txtshiliao.Text = md.外治法.食疗;
                    txtxuewei.Text = md.外治法.穴位;
                    txtyundong.Text = md.外治法.运动;
                    txtzhenghou.Text = md.症候;
                    txtzhongchengyao.Text = md.内治法.中成药;
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int r=0;
            Sys_Point point = new Sys_Point();

            Medicine md = new Medicine()
            {
                辩证 = txtbianzheng.Text
                ,
                病症 = txtbingzheng.Text
                ,
                禁忌 = txtjinji.Text
                ,
                症候 = txtzhenghou.Text
                ,
                内治法 = new InternalMethod()
                {
                    经验方 = txtjingyanfang.Text
                    ,
                    中成药 = txtzhongchengyao.Text
                }
                ,
                外治法 = new ExternalMethod()
                {
                    耳穴 = txterxue.Text
                    ,
                    脊柱 = txtjizhu.Text
                    ,
                    食疗 = txtshiliao.Text
                    ,
                    穴位 = txtxuewei.Text
                    ,
                    运动 = txtyundong.Text
                }
            };
            string strJson = BaseCommon.ObjectToJson(md);
            if (string.IsNullOrEmpty(lblPointId.Text))
            {
                point.ModulelId = Convert.ToInt32(this.lblModule.Text);
                point.Content = strJson;
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