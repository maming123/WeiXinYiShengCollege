using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Module.Models;
using WeiXinYiShengCollege.Business;

namespace WeiXinYiShengCollege.WebSite.Home.ScoreManage
{
    public partial class ExchangeScoreList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                this.txtBeginDate.Text = this.txtEndDate.Text = DateTime.Now.ToString("yyyy年MM月dd日");
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "PaySure")
            {
                int editId = Convert.ToInt32(e.CommandArgument);
                

                ExchangeLog exchange = new ExchangeLog() { 
                 Id=editId
                 , PayDateTime=DateTime.Now
                 , PayStatus=(int)PayStatus.HavePayed
                };
                int r = exchange.Update(new string[] { "PayDateTime", "PayStatus" });
               
                if (r>0)
                {
                    FillGridView();
                    
                }
            }
        }
        protected string GetPayStatus(object status)
        {
            if (status.ToString() == "0")
                return "未支付";
            else if (status.ToString() == "1")
                return "申请支付中";
            else if (status.ToString() == "2")
            {
                return "已支付";
            }
            else
                return "";
            //return Enum.GetName(typeof(PayStatus), status);
        }

        private void FillGridView()
        {
            this.GridView1.DataSource = null;
            this.GridView1.DataBind();

            int payStatus =Convert.ToInt32(this.ddlPayStatus.SelectedValue);
            DateTime dtbegin =Convert.ToDateTime(this.txtBeginDate.Text);
            DateTime dtend =Convert.ToDateTime(this.txtEndDate.Text);
            dtend = dtend.AddDays(1).AddSeconds(-1);
           
            long mobile =Convert.ToInt64(this.txtMobile.Text.Trim()==""?"0":this.txtMobile.Text.Trim());
            List<ExchangeLog> list = ScoreBusiness.GetExchangeLogList(payStatus, dtbegin, dtend, mobile);
            this.GridView1.DataSource = list;
            this.GridView1.DataBind();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lbtn = ((LinkButton)e.Row.FindControl("lbtnEdit"));
                lbtn.OnClientClick = "return confirm('确定支付？');";
                if (lbtn.ToolTip == Convert.ToString((int)PayStatus.HavePayed))
                {
                    lbtn.Visible = false;
                }
            }
           // ((LinkButton)e.Row.FindControl("lbtnEdit")).Visible = false;
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            FillGridView();
        }
    }
}