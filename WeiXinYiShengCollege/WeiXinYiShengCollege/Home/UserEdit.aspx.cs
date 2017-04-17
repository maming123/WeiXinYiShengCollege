using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Module.Models;
using Module.Utils;
using WeiXinYiShengCollege.Business;

namespace WeiXinYiShengCollege.WebSite.Home
{
    public partial class UserEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string openId = RequestKeeper.GetFormString(Request["OpenId"]);
                InitddlCustomerManagerId();
                ShowDetail(openId);
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Sys_User u = UserBusiness.GetUserInfo(lblOpenId.Text);

            u.ApproveFlag = Convert.ToInt16(ddlApprove.SelectedValue);
            u.City = ddlCity.SelectedValue;
            u.Province = ddlProvince.SelectedValue;
            u.CompanyName = txtCompanyName.Text;
            u.CustomerManagerId = Convert.ToInt32(ddlCustomerManagerId.SelectedValue);

            u.Score = Convert.ToInt32(txtScore.Text);
            u.Remark = txtRemark.Text;
            u.NickName = txtNickName.Text;
            u.Mobile = Convert.ToInt64(txtMobile.Text);
            u.UserLevel = Convert.ToInt16(ddlUserLevel.SelectedValue);
            u.UserType = Convert.ToInt16(ddlUserType.SelectedValue);

            if(u.ApproveFlag == (int)ApproveFlag.已认证 && u.QrCodeScene_id==0)
            {
                //执行操作生成场景ID 用于理事生成二维码
                //QrCodeScene_id =理事的个数+1
                int lishiNum =UserBusiness.GetLishiUserCount();
                u.QrCodeScene_id = lishiNum + 1;
            }

            int r = u.Update();
            if(r>0)
            {
                MessageBox.Show(Page,"保存成功");
            }
        }
        private void InitddlCustomerManagerId()
        {
            List<CustomerManager> cstmaglist = CustomerManager.Query("").ToList();
            foreach (CustomerManager cm in cstmaglist)
            {
                ddlCustomerManagerId.Items.Add(new ListItem() { Text = cm.Name, Value = cm.Id.ToString() });
            }
            ddlCustomerManagerId.Items.Insert(0, new ListItem() { Text = "请选择", Value = "0" });
        }


        private void ShowDetail(string openid)
        {
            Sys_User u = UserBusiness.GetUserInfo(openid);
            if (u != null && u.Id > 0)
            {
                lblId.Text = u.Id.ToString();
                lblOpenId.Text = u.OpenId;
                lblQrCodeScene_id.Text = u.QrCodeScene_id.ToString();

                Sys_User uParent = Sys_User.SingleOrDefault((object)u.ParentId);
                if (uParent != null && uParent.Id > 0)
                {
                    lblParentId.Text = uParent.NickName;
                }

                txtCompanyName.Text = u.CompanyName;
                txtMobile.Text = u.Mobile.ToString();
                txtNickName.Text = u.NickName;
                txtRemark.Text = u.Remark;
                txtScore.Text = u.Score.ToString();

                ddlProvince.SelectedValue = u.Province;
                ddlCity.SelectedValue = u.City;
                ddlApprove.SelectedValue = u.ApproveFlag.ToString();
                ddlCustomerManagerId.SelectedValue = u.CustomerManagerId.ToString();
                ddlUserLevel.SelectedValue = u.UserLevel.ToString();
                ddlUserType.SelectedValue = u.UserType.ToString();
               
                
            }
        }

        protected void ddlUserType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlUserType.SelectedValue == Convert.ToInt32(UserType.粉丝类型).ToString())
            {
                ddlUserLevel.Enabled = false;
                ddlApprove.Enabled = false;
            }
            else
            {
                ddlUserLevel.Enabled = true;
                ddlApprove.Enabled = true;
            }
        }
    }
}