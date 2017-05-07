using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HospitalBook.WebSite.Home;
using Module.Models;
using Module.Utils;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.AdvancedAPIs.Media;
using Senparc.Weixin.MP.Entities;
using WeiXinYiShengCollege.Business;

namespace WeiXinYiShengCollege.WebSite.Home
{
    public partial class MsgAutoReplyEdit : ManagePageBase
    {
        protected int Id = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            //msgtype=text&Id=4
            Id = RequestKeeper.GetFormInt(Request["Id"]);
            if (!Page.IsPostBack)
            {
                if (Id > 0)
                {
                    this.Button1.Text = "修改";
                    ShowDetail(Id);
                }
                else
                {
                    this.Button1.Text = "新增";
                }
            }

        }
        private void ShowDetail(int id)
        {
            HiddenAllPanel();
            AutoReplyContent arc = MsgAutoReplyBusiness.GetReplyContent(id);
            ddlMsgType.SelectedValue = arc.ResponseMsgType;
            ddlIsDelete.SelectedValue = arc.IsDelete.ToString();
            txtUpKey.Text = arc.UpKey;
            txtUpKey.ReadOnly = true;
            if (arc.ResponseMsgType == ResponseMsgType.Text.ToString().ToLower())
            {
                this.panelText.Visible = true;
                this.txtText.Text = arc.ReplyContent;
            }
            else if (arc.ResponseMsgType == ResponseMsgType.News.ToString().ToLower())
            {
                this.panelNews.Visible = true;
                //绑定所有的图文
                List<Article> list = BaseCommon.JsonToObject<List<Article>>(arc.ReplyContent);
                for (int i = 0; i < list.Count; i++)
                {
                    Article a = list[i];
                    int j = i + 1;
                    ((TextBox)this.panelNews.FindControl("txtTitle_" + j)).Text = a.Title;
                    ((TextBox)this.panelNews.FindControl("txtDescription_" + j)).Text = a.Description;
                    ((TextBox)this.panelNews.FindControl("txtPicUrl_" + j)).Text = a.PicUrl;
                    ((TextBox)this.panelNews.FindControl("txtUrl_" + j)).Text = a.Url;
                }
            }
        }

        protected void ddlMsgType_SelectedIndexChanged(object sender, EventArgs e)
        {
            HiddenAllPanel();
            if (((DropDownList)sender).SelectedValue == ResponseMsgType.News.ToString().ToLower())
            {   //图文
                this.panelNews.Visible = true;
               
            }
            else if (((DropDownList)sender).SelectedValue == ResponseMsgType.Text.ToString().ToLower())
            {
                //文字
                this.panelText.Visible = true;
            }
        }
        private void HiddenAllPanel()
        {
            this.panelNews.Visible = false;
            this.panelText.Visible = false;
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (Id > 0)
            {
                //edit
                AutoReplyContent arc = new AutoReplyContent()
                {
                    CreateDatetime = DateTime.Now
                    ,
                    IsDelete = Convert.ToInt32(ddlIsDelete.SelectedValue)
                    ,
                    ResponseMsgType = ddlMsgType.SelectedValue
                    ,
                    UpKey = txtUpKey.Text.Trim()
                    ,
                    
                    Id = Id
                };
                
                if (this.panelText.Visible)
                {
                    //修改文本
                    arc.ReplyContent = txtText.Text;
                    
                }else if(this.panelNews.Visible)
                {
                    arc.ReplyContent= GetNewsReplyContent();
                }

                arc.Update();
                string cacheKey = string.Format(@"GetReplyContent_{0}", arc.UpKey);
                BaseCommon.CacheRemove(cacheKey);
            }
            else
            {
                //添加文本
                AutoReplyContent arc = new AutoReplyContent()
                {
                    CreateDatetime = DateTime.Now
                    ,
                    IsDelete = Convert.ToInt32(ddlIsDelete.SelectedValue)
                    ,
                    ResponseMsgType = ddlMsgType.SelectedValue
                    ,
                    UpKey = txtUpKey.Text.Trim()
                    ,
                    ReplyContent = txtText.Text
                };
                if (this.panelText.Visible)
                {
                    arc.ReplyContent = txtText.Text;
                }
                else if (this.panelNews.Visible)
                {
                    arc.ReplyContent = GetNewsReplyContent();
                }
                arc.Insert();
            }
            MessageBox.ShowAndRedirect(Page, "操作成功", "MsgAutoReplyManage.aspx");
        }

        private string GetNewsReplyContent()
        {
            List<Article> listNew = new List<Article>();
            for (int i = 0; i < 4; i++)
            {
                Article a = new Article();
                int j = i + 1;
                a.Title = ((TextBox)this.panelNews.FindControl("txtTitle_" + j)).Text.Trim();
                a.Description = ((TextBox)this.panelNews.FindControl("txtDescription_" + j)).Text.Trim();
                a.PicUrl = ((TextBox)this.panelNews.FindControl("txtPicUrl_" + j)).Text.Trim();
                a.Url = ((TextBox)this.panelNews.FindControl("txtUrl_" + j)).Text.Trim();
                if (!string.IsNullOrEmpty(a.Title))
                {
                    listNew.Add(a);
                }
            }
            return  BaseCommon.ObjectToJson(listNew);
        }

        protected void btnSync_Click(object sender, EventArgs e)
        {
           string json =  hdJson.Value;
//            string json = @"{
//    ""media_id"": ""5TfZPgdJm5scOSeNEAnZRgTfUKkAO-bkvSg-9ctSBuo"",
//    ""content"": {
//        ""news_item"": [{
//                ""url"": ""http://mp.weixin.qq.com/s?__biz=MzU5MTA0NDIxNQ==&amp;mid=100000003&amp;idx=1&amp;sn=f63be5974f2aaf49f7af5e12458240c4&amp;chksm=7e344a1b4943c30d279fbd6c5b0f902ccf1d3e08053a1c20995d8d8b528c28e58b3aa790b3fc#rd"",
//                ""thumb_url"": ""http://mmbiz.qpic.cn/mmbiz_jpg/sPiceDcOOVkbH9NB3kdNmBfmg6ncmfJO07cKzyicLvMApnCYsLlxTnXvoibmiasNkia0NadgazxCaOLV9nJU3yG5dsw/0?wx_fmt=jpeg"",
//                ""thumb_media_id"": ""5TfZPgdJm5scOSeNEAnZRnsuxLR47MIGeuQvN2WUh8w"",
//                ""author"": ""mm"",
//                ""title"": ""投票测试"",
//                ""content_source_url"": """",
//                ""content"": """",""digest"":""1"",""show_cover_pic"":""0""}]},""update_time"":1492875466}";
           MediaList_News_Item mediaNews = BaseCommon.JsonToObject<MediaList_News_Item>(json);
           if(null !=mediaNews)
           {
               //清空图文
               for (int i = 0; i <4; i++)
               {
                   int j = i + 1;
                   ((TextBox)this.panelNews.FindControl("txtTitle_" + j)).Text = "";
                   ((TextBox)this.panelNews.FindControl("txtDescription_" + j)).Text = "";
                   ((TextBox)this.panelNews.FindControl("txtPicUrl_" + j)).Text = "";
                   ((TextBox)this.panelNews.FindControl("txtUrl_" + j)).Text = "";
               }
               //给图文1-4赋值

               for (int i = 0; i < mediaNews.content.news_item.Count; i++)
               {
                   Media_News_Content_Item a = mediaNews.content.news_item[i];
                   int j = i + 1;
                   ((TextBox)this.panelNews.FindControl("txtTitle_" + j)).Text = a.title;
                   ((TextBox)this.panelNews.FindControl("txtDescription_" + j)).Text = a.digest;
                   ((TextBox)this.panelNews.FindControl("txtPicUrl_" + j)).Text = a.thumb_url;
                   ((TextBox)this.panelNews.FindControl("txtUrl_" + j)).Text = a.url;
               }
           }
        }
    }
}