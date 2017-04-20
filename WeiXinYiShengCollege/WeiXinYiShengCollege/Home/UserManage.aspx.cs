using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HospitalBook.WebSite.Home;
using Module.Models;
using WeiXinYiShengCollege.Business;

namespace HospitalBookWebSite.Home
{
    public partial class UserManage : ManagePageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                InitddlCustomerManagerId();
                InitddlProvince();
                InitddlCity(0);
            }
        }
        private void InitddlCustomerManagerId()
        {
            List<CustomerManager> cstmaglist = CustomerManager.Query("").ToList();
            foreach (CustomerManager cm in cstmaglist)
            {
                ddlCustomerManagerId.Items.Add(new ListItem() { Text = cm.Name + "|" + cm.Mobile, Value = cm.Id.ToString() });
            }
            ddlCustomerManagerId.Items.Insert(0, new ListItem() { Text = "请选择", Value = "0" });
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
            ddlCity.Items.Insert(0,new ListItem() { Text = "不详", Value = "0" });
        }
        protected void ddlProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitddlCity(Convert.ToInt32(((DropDownList)sender).SelectedValue));
        }


    }
}