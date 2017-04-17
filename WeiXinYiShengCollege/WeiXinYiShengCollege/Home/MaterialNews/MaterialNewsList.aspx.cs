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
    public partial class MaterialNewsList : ManagePageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
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

    }
}