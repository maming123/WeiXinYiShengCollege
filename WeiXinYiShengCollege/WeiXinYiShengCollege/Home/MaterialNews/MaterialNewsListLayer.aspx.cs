using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HospitalBook.WebSite.Home;
using Module.Utils;
using Senparc.Weixin.MP.AdvancedAPIs.Media;
using Senparc.Weixin.MP.Sample.CommonService;
using Senparc.Weixin.MP.Sample.CommonService.ModelForPageShow;
using WeiXinYiShengCollege.Business;


namespace WeiXinYiShengCollege.WebSite.Home.MaterialNews
{
    public partial class MaterialNewsListLayer : ManagePageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            { InitDDL();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int offset = Convert.ToInt32(DropDownList1.SelectedValue);
            GetMaterialNews(offset);
        }
        private void GetMaterialNews(int offset)
        {
            this.Label1.Text = "";
            this.GridView1.DataSource = null;
            this.GridView1.DataBind();
            
            MediaList_NewsResult r = WeiXinBusiness.GetNewsMediaList(offset);
            if((int)r.errcode == 0)
            {
                List<NewsList> list = new List<NewsList>();
                if (r.item.Count > 0)
                {
                    for (int i = 0; i < r.item.Count; i++)
                    {
                        MediaList_News_Item newsitem = r.item[i];
                        NewsList d = new NewsList();
                        d.media_id = newsitem.media_id;
                        d.author = newsitem.content.news_item[0].author;
                        d.title = newsitem.content.news_item[0].title;
                        for(int j=0;j<newsitem.content.news_item.Count;j++)
                        {
                            newsitem.content.news_item[j].content="";
                        }
                        
                        d.Json = BaseCommon.ObjectToJson(newsitem);
                        list.Add(d);
                    }

                    this.GridView1.DataSource = list;
                    this.GridView1.DataBind();
                }else
                {
                    this.Label1.Text = string.Format(@"获取到{0}条数据",r.item_count);
                }

            }else
            {
                Response.Write(r.errmsg);
            }
            
        }

        private void InitDDL()
        {
            for(int i=0;i<50;i++)
            {
                int v = (i * 20 - 1) < 0 ? 0 : (i * 20 - 1);

                this.DropDownList1.Items.Add(new ListItem("第"+(i+1).ToString()+"页", v.ToString()));
            }
            this.DropDownList1.SelectedValue = "0";
            //            <asp:ListItem Selected="True" Value="0">1-20条</asp:ListItem>
            //<asp:ListItem Value="19">21-40条</asp:ListItem>
            //<asp:ListItem Value="39">41-60条</asp:ListItem>
            //<asp:ListItem Value="59">61-80条</asp:ListItem>
            //<asp:ListItem Value="79">81-100条</asp:ListItem>
            //<asp:ListItem Value="99">101-120条</asp:ListItem>
            //<asp:ListItem Value="119">121-140条</asp:ListItem>
            //<asp:ListItem Value="139">141-160条</asp:ListItem>
            //<asp:ListItem Value="159">161-180条</asp:ListItem>
            //<asp:ListItem Value="179">181-200条</asp:ListItem>
            //<asp:ListItem Value="199">181-200条</asp:ListItem>
        }
    }
}