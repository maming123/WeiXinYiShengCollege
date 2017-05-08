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
                InitddlExpertsLiShiUserId(openId);
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
            var db = CoreDB.GetInstance();
            try
            {
                db.BeginTransaction();
                Sys_User u = UserBusiness.GetUserInfo(lblOpenId.Text);

                u.ApproveFlag = Convert.ToInt16(ddlApprove.SelectedValue);
                u.City = Convert.ToInt32(ddlCity.SelectedValue);
                u.Province = Convert.ToInt32(ddlProvince.SelectedValue);
                u.CompanyName = txtCompanyName.Text;
                u.CustomerManagerId = Convert.ToInt32(ddlCustomerManagerId.SelectedValue);

                u.Score = Convert.ToDecimal(txtScore.Text) * 100;
                u.LastScore = Convert.ToDecimal(txtLastScore.Text) * 100;
                u.Remark = txtRemark.Text;
                u.NickName = txtNickName.Text;
                u.Mobile = Convert.ToInt64(txtMobile.Text);
                u.UserLevel = Convert.ToInt16(ddlUserLevel.SelectedValue);
                u.UserType = Convert.ToInt16(ddlUserType.SelectedValue);
                u.ParentId = Convert.ToInt32(ddlParentId.SelectedValue);

                if (u.ApproveFlag == (int)ApproveFlag.已认证 && u.QrCodeScene_id == 0)
                {
                    //执行操作生成场景ID 用于理事生成二维码
                    //QrCodeScene_id =理事的个数+1
                    int lishiNum = UserBusiness.GetLishiUserCount();
                    u.QrCodeScene_id = lishiNum + 1;
                }
                if (u.UserType == (int)UserType.粉丝类型)
                {//粉丝类型强制变成QR值为0 
                    u.QrCodeScene_id = 0;
                }

                int r = db.Update(u);
                if (r <= 0)
                {
                    db.AbortTransaction();
                    MessageBox.Show(Page, "更新用户信息失败");
                    return;
                }

                //查找
                if(hidExpertsLiShiId.Value=="0")
                {
                    if (ddlUserType.SelectedValue == Convert.ToInt32(UserType.理事类型).ToString())
                    {
                        //新增 新分配
                        //如果没有对应关系则插入
                        ExportsLiShi expertsLiShiInsert = new ExportsLiShi()
                        {
                            ExpertsSysUserId = Convert.ToInt32(ddlExpertsLiShi.SelectedValue)
                                ,
                            LiShiSysUserId = u.Id
                                ,
                            CreateDateTime = DateTime.Now
                        };
                        object objInsertExpert = db.Insert(expertsLiShiInsert);
                        if (Convert.ToInt32(objInsertExpert) <= 0)
                        {
                            db.AbortTransaction();
                            MessageBox.Show(Page, "插入荣誉理事ExportsLiShi表失败");
                            return;
                        }
                    }

                }else
                {
                    if (ddlUserType.SelectedValue == Convert.ToInt32(UserType.理事类型).ToString())
                    {

                        //之前已有值  如果这次选择的是更新
                        ExportsLiShi expertsLiShiUpdate = new ExportsLiShi()
                        {
                            ExpertsSysUserId = Convert.ToInt32(ddlExpertsLiShi.SelectedValue)
                                ,
                            LiShiSysUserId = u.Id
                                ,
                            CreateDateTime = DateTime.Now
                            ,
                            Id = Convert.ToInt32(hidExpertsLiShiId.Value)
                        };
                        if (Convert.ToInt32(ddlExpertsLiShi.SelectedValue) == 0)
                        {
                            //删除 专家理事的关系
                            int rdelete = db.Delete<ExportsLiShi>((object)hidExpertsLiShiId.Value);
                            if (rdelete <= 0)
                            {
                                db.AbortTransaction();
                                MessageBox.Show(Page, "删除荣誉理事ExportsLiShi表失败");
                                return;
                            }
                        }
                        else
                        {
                            int objUpdate = db.Update(expertsLiShiUpdate);
                            if (objUpdate <= 0)
                            {
                                db.AbortTransaction();
                                MessageBox.Show(Page, "更新荣誉理事ExportsLiShi表失败");
                                return;
                            }
                        }
                    }else if(ddlUserType.SelectedValue == Convert.ToInt32(UserType.粉丝类型).ToString())
                    {
                        //降级成粉丝 那么对应的把 专家理事和理事的关系删掉
                        int rdelete = db.Delete<ExportsLiShi>((object)hidExpertsLiShiId.Value);
                        if (rdelete <= 0)
                        {
                            db.AbortTransaction();
                            MessageBox.Show(Page, "删除荣誉理事ExportsLiShi表失败");
                            return;
                        }
                    
                    }
                }

                db.CompleteTransaction();
                MessageBox.Show(Page, "修改成功");
                return;
            }
            catch (Exception ex)
            {
                db.AbortTransaction();
                LogHelper.WriteLogError(typeof(UserEdit), ex);
            }
            finally
            {
                db.CloseSharedConnection();
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

        /// <summary>
        /// 初始化荣誉理事列表
        /// </summary>
        private void InitddlExpertsLiShiUserId(string openId)
        {
            List<Sys_User> sUserList = Sys_User.Query("where parentid=@0 and OpenId!=@1 and UserLevel=3 order by Mobile asc", 0, openId).ToList();
            foreach (Sys_User cm in sUserList)
            {
                ddlExpertsLiShi.Items.Add(new ListItem() { Text = cm.NickName + "|" + cm.Mobile, Value = cm.Id.ToString() });
            }
            ddlExpertsLiShi.Items.Insert(0, new ListItem() { Text = "请选择荣誉理事", Value = "0" });
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
                
                //读取专家理事
                ExportsLiShi exportsLiShi = UserBusiness.GetExportsLiShiByLiShiSysUserId(u.Id);
                if(null != exportsLiShi)
                {
                    ddlExpertsLiShi.SelectedValue=exportsLiShi.ExpertsSysUserId.ToString();
                    //自增ID
                    hidExpertsLiShiId.Value = exportsLiShi.Id.ToString();
                }else
                {
                    hidExpertsLiShiId.Value = "0";
                }

                SetDDLStatus();

            }
        }

        protected void ddlUserType_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetDDLStatus();
        }

        private void SetDDLStatus()
        {

            if (ddlUserType.SelectedValue == Convert.ToInt32(UserType.粉丝类型).ToString())
            {   
                ddlUserLevel.SelectedValue = "0";
                ddlApprove.SelectedValue = "0";
                ddlCustomerManagerId.SelectedValue = "0";
                ddlExpertsLiShi.SelectedValue = "0";
                //用户类型
                ddlUserLevel.Enabled = false;
                //审核状态
                ddlApprove.Enabled = false;
                //客户经理
                ddlCustomerManagerId.Enabled = false;
                //普通理事
                ddlParentId.Enabled = true;
                //专家理事
                ddlExpertsLiShi.Enabled = false;
                
            }
            else
            {
                ddlUserLevel.Enabled = true;
                ddlApprove.Enabled = true;
                ddlParentId.Enabled = false;
                ddlExpertsLiShi.Enabled = true;
                ddlCustomerManagerId.Enabled = true;
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