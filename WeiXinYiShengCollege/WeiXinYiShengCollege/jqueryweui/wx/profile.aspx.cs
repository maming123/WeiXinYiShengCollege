using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Module.Models;
using Module.Utils;
using Senparc.Weixin.MP.Sample.CommonService;
using WeiXinYiShengCollege.Business;

namespace WeiXinYiShengCollege.WebSite.wx
{
    public partial class profile : System.Web.UI.Page
    {

        public Sys_User sUser = new Sys_User();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            string code =RequestKeeper.GetFormString(Request["code"]);
            if(!string.IsNullOrEmpty(code))
            {
                //获取Accesstoken 进而获取openId 通过OpenId获取用户信息
               String  OpenId = WeiXinBusiness.GetOpenIdFromOAuthAccessToken(code);

                Sys_User tmpUser = UserBusiness.GetUserInfo(OpenId);
                if (tmpUser != null)
                {
                    sUser = tmpUser;
                    if(sUser.ParentId>0)
                    {
                        //认为是粉丝
                        //判断如果没完善信息则取完善 手机号，姓名，完善后跳转到 FansProfile.aspx
                        if(sUser.Mobile<=0)
                        {
                            Response.Redirect("FansProfileComplete.aspx?Id=" + sUser.Id);
                        }

                    }else
                    {
                        //认为是理事
                        
                        //判断如果没完善信息并且未认证者则去跳转到完善 手机号，姓名， 省、市、单位名称页面 完善后 跳转到Profile.aspx 
                        //但在未通过审核前只显示待审核中
                        if (sUser.ApproveFlag == 0)
                        {   
                            //只有未认证情况才跳去完善页面
                            Response.Redirect("ProfileApprove.aspx?Id=" + sUser.Id);
                        }
                    }
                }
                
                //Response.Write(openId+BaseCommon.ObjectToJson(sUser));
               
            }else
            {
                Response.Write("获取code失败,请重试");
            }
        }
    }
}