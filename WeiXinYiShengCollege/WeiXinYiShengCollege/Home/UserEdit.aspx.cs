using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HospitalBook.WebSite.Home;
using Module.Models;
using Module.Utils;
using WeiXinYiShengCollege.Business;

namespace WeiXinYiShengCollege.WebSite.Home
{
    public partial class UserEdit : ManagePageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string openId = RequestKeeper.GetFormString(Request["OpenId"]);
                int id = RequestKeeper.GetFormInt(Request["Id"]);

                InitddlCustomerManagerId();
                InitddlProvince();
                InitddlCity(0);
                InitddlParentUserId(openId);

                if(String.IsNullOrEmpty(openId))
                {//证明只是查看用户信息
                    openId = UserBusiness.GetUserInfoById(id).OpenId;
                    this.btnEdit.Visible = false;
                }
                ShowDetail(openId);
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Sys_User u = UserBusiness.GetUserInfo(lblOpenId.Text);

            u.ApproveFlag = Convert.ToInt16(ddlApprove.SelectedValue);
            u.City = Convert.ToInt32(ddlCity.SelectedValue);
            u.Province = Convert.ToInt32(ddlProvince.SelectedValue);
            u.CompanyName = txtCompanyName.Text;
            u.CustomerManagerId = Convert.ToInt32(ddlCustomerManagerId.SelectedValue);

            u.Score = Convert.ToDecimal(txtScore.Text)*100;
            u.LastScore = Convert.ToDecimal(txtLastScore.Text) * 100;
            u.Remark = txtRemark.Text;
            u.NickName = txtNickName.Text;
            u.Mobile = Convert.ToInt64(txtMobile.Text);
            u.UserLevel = Convert.ToInt16(ddlUserLevel.SelectedValue);
            u.UserType = Convert.ToInt16(ddlUserType.SelectedValue);
            u.ParentId = Convert.ToInt32(ddlParentId.SelectedValue);

            if(u.ApproveFlag == (int)ApproveFlag.已认证 && u.QrCodeScene_id==0)
            {
                //执行操作生成场景ID 用于理事生成二维码
                //QrCodeScene_id =理事的个数+1
                int lishiNum =UserBusiness.GetLishiUserCount();
                u.QrCodeScene_id = lishiNum + 1;
            }
            if(u.UserType == (int)UserType.粉丝类型)
            {//粉丝类型强制变成QR值为0 
                u.QrCodeScene_id = 0;
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
                ddlCustomerManagerId.Items.Add(new ListItem() { Text = cm.Name+"|"+cm.Mobile, Value = cm.Id.ToString() });
            }
            ddlCustomerManagerId.Items.Insert(0, new ListItem() { Text = "请选择", Value = "0" });
        }

        /// <summary>
        /// 初始化理事列表
        /// </summary>
        private void InitddlParentUserId(string openId)
        {
            List<Sys_User> sUserList = Sys_User.Query("where parentid=@0 and OpenId!=@1 order by Mobile asc", 0, openId).ToList();
            foreach (Sys_User cm in sUserList)
            {
                ddlParentId.Items.Add(new ListItem() { Text = cm.NickName + "|" + cm.Mobile, Value = cm.Id.ToString() });
            }
            ddlParentId.Items.Insert(0, new ListItem() { Text = "请选择理事", Value = "0" });
        }

        private void InitddlProvince()
        {
            List<Area> list = AreaBusiness.GetProvinceList();
            foreach (Area area in list)
            {
                ddlProvince.Items.Add(new ListItem() { Text = area.AreaName, Value = area.Id.ToString() });
            }
            ddlProvince.Items.Insert(0, new ListItem() { Text = "不详", Value = "0" });
        }
        private void InitddlCity(int provinceId)
        {
            ddlCity.Items.Clear();
            List<Area> list = AreaBusiness.GetCityList(provinceId);
            foreach (Area area in list)
            {
                ddlCity.Items.Add(new ListItem() { Text = area.AreaName, Value = area.Id.ToString() });
            }
            ddlCity.Items.Insert(0, new ListItem() { Text = "不详", Value = "0" });
        }


        private void ShowDetail(string openid)
        {
            Sys_User u = UserBusiness.GetUserInfo(openid);
            if (u != null && u.Id > 0)
            {
                lblId.Text = u.Id.ToString();
                lblOpenId.Text = u.OpenId;
                lblQrCodeScene_id.Text = u.QrCodeScene_id.ToString();

                //Sys_User uParent = Sys_User.SingleOrDefault((object)u.ParentId);
                //if (uParent != null && uParent.Id > 0)
                //{
                //    lblParentId.Text = uParent.NickName;
                //}

                txtCompanyName.Text = u.CompanyName;
                txtMobile.Text = u.Mobile.ToString();
                txtNickName.Text = u.NickName;
                txtRemark.Text = u.Remark;
                txtScore.Text = Convert.ToString(u.Score/100);
                txtLastScore.Text =  Convert.ToString(u.LastScore / 100);
                ddlProvince.SelectedValue = u.Province.ToString();
                InitddlCity(u.Province??0);
                ddlCity.SelectedValue = u.City.ToString();
                ddlApprove.SelectedValue = u.ApproveFlag.ToString();
                ddlCustomerManagerId.SelectedValue = u.CustomerManagerId.ToString();
                ddlUserLevel.SelectedValue = u.UserLevel.ToString();
                ddlUserType.SelectedValue = u.UserType.ToString();
                ddlParentId.SelectedValue = u.ParentId.ToString();
                txtUserInfoJson.Text = u.UserInfoJson;
                
                
            }
        }

        protected void ddlUserType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlUserType.SelectedValue == Convert.ToInt32(UserType.粉丝类型).ToString())
            {
                ddlUserLevel.Enabled = false;
                ddlApprove.Enabled = false;
                ddlUserLevel.SelectedValue = "0";
                ddlApprove.SelectedValue = "0";
                ddlCustomerManagerId.SelectedValue = "0";
            }
            else
            {
                ddlUserLevel.Enabled = true;
                ddlApprove.Enabled = true;
            }
        }

        protected void ddlProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitddlCity(Convert.ToInt32(((DropDownList)sender).SelectedValue));
        }

        protected void ddlUserLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlUserLevel.SelectedValue ==  Convert.ToInt32(UserLevel.荣誉理事).ToString())
            {
                ddlParentId.SelectedValue = "0";
            }
        }
    }
}