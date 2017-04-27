using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Module.Models;
using Module.Utils;
using Senparc.Weixin;
using Senparc.Weixin.MP.AdvancedAPIs.MerChant;
using Senparc.Weixin.MP.Sample.CommonService;
using WeiXinYiShengCollege.Business;

namespace WeiXinYiShengCollege.WebSite.Home.OrderManage
{
    public partial class OrderList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                this.txtBeginDate.Text = this.txtEndDate.Text = DateTime.Now.ToString("yyyy年MM月dd日");
            }
        }

        private List<Order> GetOrderListFromApi(int status, DateTime begin, DateTime end)
        {
            GetByFilterResult result = WeiXinBusiness.GetOrderList(status, begin, end);
            if (result != null && result.errcode == ReturnCode.请求成功)

                return result.order_list;
            else
                return null;
        }

        protected void btnSyncFromWeiXin_Click(object sender, EventArgs e)
        {
            int status =Convert.ToInt16(this.ddlOrderStatus.SelectedValue);
            DateTime dtbegin =Convert.ToDateTime(this.txtBeginDate.Text);
            DateTime dtend =Convert.ToDateTime(this.txtEndDate.Text);
            dtend = dtend.AddDays(1).AddSeconds(-1);
            List<Order> orderList = GetOrderListFromApi(status, dtbegin, dtend);
            //数据库订单表
            List<OrderInfo> orderInfoList =new List<OrderInfo>();
            foreach(Order order in orderList)
            {
                OrderInfo orderInfo =new OrderInfo(){
                 BuyerNickName =order.buyer_nick
                 , BuyerOpenId=order.buyer_openid
                 , CreateDateTime=DateTime.Now
                 , OrderCreateDateTime=BaseCommon.GetDateTimeFromTimeStamp(order.order_create_time)
                 ,  OrderCreateTime =Convert.ToInt32(order.order_create_time)
                 , OrderId=order.order_id
                 , OrderStatus=order.order_status
                 , OrderTotalPrice=order.order_total_price
                 , ProductId=order.product_id
                 , ProductName=order.product_name
                 , OrderInfoJson =BaseCommon.ObjectToJson(order)
                 , ProductPrice=order.product_price
                };
                orderInfoList.Add(orderInfo);
            }
            //插入数据库
            OrderBusiness.InsertIntoDB(orderInfoList);
            //获取从数据库
            List<OrderInfo> orderInfoListFromDB = OrderBusiness.GetOrderInfoListFromDB(status, dtbegin, dtend);
            if (orderInfoListFromDB != null)
            {
                this.GridView1.DataSource = orderInfoListFromDB;
                this.GridView1.DataBind();
                lblMsg.Text = "";
            }else
            {
                lblMsg.Text = "没有相关记录信息，请重新查询";
            }

        }
        protected string GetStatus(object status)
        {
            string statusId = status.ToString();
            if (statusId == "2")
                return "待发货";
            else if (statusId == "3")
            {
                return "已发货";
            }
            else if (statusId == "5")
            {
                return "已完成";
            }else
            { return "未知";
            }

        }

        protected void btnFromDB_Click(object sender, EventArgs e)
        {
            int status = Convert.ToInt16(this.ddlOrderStatus.SelectedValue);
            DateTime dtbegin = Convert.ToDateTime(this.txtBeginDate.Text);
            DateTime dtend = Convert.ToDateTime(this.txtEndDate.Text);
            dtend = dtend.AddDays(1).AddSeconds(-1);

            //获取从数据库
            List<OrderInfo> orderInfoListFromDB = OrderBusiness.GetOrderInfoListFromDB(status, dtbegin, dtend);
            if (orderInfoListFromDB != null)
            {
                this.GridView1.DataSource = orderInfoListFromDB;
                this.GridView1.DataBind();
                lblMsg.Text = "";
            }
            else
            {
                lblMsg.Text = "没有相关记录信息，请重新查询";
            }
        }

        protected void btnAddScore_Click(object sender, EventArgs e)
        {
            int status = Convert.ToInt16(this.ddlOrderStatus.SelectedValue);
            DateTime dtbegin = Convert.ToDateTime(this.txtBeginDate.Text);
            DateTime dtend = Convert.ToDateTime(this.txtEndDate.Text);
            dtend = dtend.AddDays(1).AddSeconds(-1);

            //获取从数据库
            List<OrderInfo> orderInfoListFromDB = OrderBusiness.GetOrderInfoListFromDB(status, dtbegin, dtend);
            if (orderInfoListFromDB != null)
            {
                
                foreach (OrderInfo info in orderInfoListFromDB)
                {
                    ////查看是否添加过积分，如果没添加过则添加积分 并记录添加日志
                    ScoreBusiness.AddScore(info);

                }
                MessageBox.Show(Page, "积分添加成功");
            }
            
        }
    }
}
