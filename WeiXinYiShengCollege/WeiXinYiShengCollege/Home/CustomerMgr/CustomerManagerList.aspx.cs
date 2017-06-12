using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HospitalBook.WebSite.Home;
using Module.Models;
using WeiXinYiShengCollege.Business;

namespace WeiXinYiShengCollege.WebSite.Home.CustomerMgr
{
    public partial class CustomerManagerList : ManagePageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                FillGridView();
            }
        }
        private void FillGridView()
        {
            this.GridView1.DataSource = null;
            this.GridView1.DataBind();

            List<CustomerManager> list = UserBusiness.GetCustomerManagerList();
            this.GridView1.DataSource = list;
            this.GridView1.DataBind();
        }
    }
}