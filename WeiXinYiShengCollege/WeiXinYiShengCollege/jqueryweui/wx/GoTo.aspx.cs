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

namespace WeiXinYiShengCollege.WebSite.jqueryweui.wx
{
    public partial class GoTo : PageBase
    {
        public Sys_User sUser = new Sys_User();

        protected void Page_Load(object sender, EventArgs e)
        {
            string code = RequestKeeper.GetFormString(Request["code"]);
            if (!string.IsNullOrEmpty(code))
            {
                String OpenId = "";
                //获取Accesstoken 进而获取openId 通过OpenId获取用户信息
                try
                {
                    OpenId = WeiXinBusiness.GetOpenIdFromOAuthAccessToken(code);

                }catch(Exception ex)
                {
                    //出现错误 记录错误日志
                    SNS.Library.Logs.LogDAOFactory.Write(ex.Message+ex.Source+ex.StackTrace,"",Convert.ToString((int)ErrorCode.NotFindOpenId), SNS.Library.Logs.LogType.Error);
                    Response.Write("微信获取的OpenId不正确，请重新进入公众号，错误码:" + Convert.ToString((int)ErrorCode.NotFindOpenId));
                    Response.End();
                }

                Sys_User tmpUser = UserBusiness.GetUserInfoForIsNotDelete(OpenId);

                if(null == tmpUser)
                {
                    //不存在该用户，并且OpenId长度》0则新创建该用户
                    if(string.IsNullOrEmpty(OpenId))
                    {
                        SNS.Library.Logs.LogDAOFactory.Write("OpenId为空", "",Convert.ToString((int)ErrorCode.NotFindOpenId), SNS.Library.Logs.LogType.Error);
                        Response.Write("个人中心异常，请重新进入公众号或者联系管理人员，错误码:" + Convert.ToString((int)ErrorCode.NotFindOpenId));
                        Response.End();

                    }else
                    {
                        //重新执行订阅逻辑（不知什么原因有可能在订阅时未将用户信息插入）
                        WeiXinBusiness.Subscribe("", OpenId);
                        tmpUser = UserBusiness.GetUserInfo(OpenId);
                    }
                }


                if (tmpUser != null)
                {
                    UserBusiness.WriteCookie(tmpUser);
                    sUser = tmpUser;
                    if (sUser.ParentId > 0)
                    {
                        //认为是粉丝
                        //判断如果没完善信息则取完善 手机号，姓名，完善后跳转到 FansProfile.aspx
                        if (sUser.Mobile <= 0)
                        {   //未完善信息
                            Response.Redirect("FansProfileComplete.aspx?OpenId=" + sUser.OpenId);
                        }
                        else
                        {
                            //已完善信息
                            Response.Redirect("FansProfile.aspx?OpenId=" + sUser.OpenId);
                        }

                    }
                    else
                    {
                        //认为是理事

                        //判断如果没完善信息并且未认证者则去跳转到完善 手机号，姓名， 省、市、单位名称页面 完善后 跳转到Profile.aspx 
                        //但在未通过审核前只显示待审核中
                        if (sUser.ApproveFlag == 0)
                        {
                            //只有未认证情况才跳去完善页面
                            Response.Redirect("ProfileApprove.aspx?OpenId=" + sUser.OpenId);
                        }else
                        {
                            Response.Redirect("Profile.aspx?OpenId=" + sUser.OpenId);
                        }
                    }
                }else
                {
                    Response.Write("个人中心异常，请重新进入公众号或者联系管理人员,错误码:" + Convert.ToString((int)ErrorCode.NotFindUser));
                    Response.End();
                }

                //Response.Write(openId+BaseCommon.ObjectToJson(sUser));

            }
            else
            {
                Response.Write("获取code失败,请重试");
                Response.End();
            }
        }

        /// <summary>
        /// 获取url中的code
        /// </summary>
        /// <param name="strUrl"></param>
        /// <returns></returns>
        private string GetCode(string strUrl)
        {
            Uri uri = new Uri(strUrl);
            String[] strArray = uri.Query.Replace("?", "").Split('&');
            string codevalue = "";
            foreach (string s in strArray)
            {
                string[] sArr = s.Split('=');
                if (sArr.Length == 2)
                {
                    if (sArr[0].ToLower() == "code")
                    {

                        codevalue = sArr[1];
                        break;
                    }
                }
            }
            return codevalue;

        }
        private void SetPageNoCache()
        {
            Response.Buffer = true;
            Response.ExpiresAbsolute = System.DateTime.Now.AddSeconds(-1);
            Response.Expires = 0;
            Response.CacheControl = "no-cache";
            Response.AppendHeader("Pragma", "No-Cache");
        }
    }
}