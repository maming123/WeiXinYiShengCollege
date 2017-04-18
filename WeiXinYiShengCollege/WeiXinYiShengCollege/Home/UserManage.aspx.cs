using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HospitalBook.WebSite.Home;
using Module.Models;

namespace HospitalBookWebSite.Home
{
    public partial class UserManage : ManagePageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                InitddlCustomerManagerId();
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
    }
}