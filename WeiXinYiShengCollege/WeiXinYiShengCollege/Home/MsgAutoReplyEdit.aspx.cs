using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Module.Models;
using Module.Utils;
using Senparc.Weixin.MP;
using WeiXinYiShengCollege.Business;

namespace WeiXinYiShengCollege.WebSite.Home
{
    public partial class MsgAutoReplyEdit : System.Web.UI.Page
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
           AutoReplyContent arc =  MsgAutoReplyBusiness.GetReplyContent(id);
           ddlMsgType.SelectedValue = arc.ResponseMsgType;
           ddlIsDelete.SelectedValue = arc.IsDelete.ToString();
           txtUpKey.Text = arc.UpKey;
           txtUpKey.ReadOnly = true;
           if(arc.ResponseMsgType== ResponseMsgType.Text.ToString().ToLower())
           {
               this.panelText.Visible = true;
               this.txtText.Text = arc.ReplyContent;
           }else if(arc.ResponseMsgType == ResponseMsgType.News.ToString().ToLower())
           {
               this.panelNews.Visible = true;

           }
        }

        protected void ddlMsgType_SelectedIndexChanged(object sender, EventArgs e)
        {
            HiddenAllPanel();
            if(((DropDownList)sender).SelectedValue==ResponseMsgType.News.ToString().ToLower())
            {//图文
                this.panelNews.Visible = true;
            }else if(((DropDownList)sender).SelectedValue==ResponseMsgType.Text.ToString().ToLower())
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
            if(Id>0)
            {
                //edit
                if (this.panelText.Visible)
                {
                    //修改文本
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
                        ReplyContent = txtText.Text.Trim()
                        ,Id= Id
                    };
                    arc.Update();
                }
            }else
            {
                if(this.panelText.Visible)
                {
                    //添加文本
                    AutoReplyContent arc = new AutoReplyContent() {  
                     CreateDatetime=DateTime.Now
                     , IsDelete=Convert.ToInt32(ddlIsDelete.SelectedValue)
                     , ResponseMsgType=ddlMsgType.SelectedValue
                     , UpKey=txtUpKey.Text.Trim()
                     ,  ReplyContent=txtText.Text.Trim()
                    };
                    arc.Insert();
                }
            }
        }
    }
}