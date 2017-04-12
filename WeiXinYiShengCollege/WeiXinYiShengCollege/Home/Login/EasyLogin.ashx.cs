using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.SessionState;
using Module.Utils;

using System.Data.SqlClient;
using System.Data;
 
using System.Web.Security;

namespace DMedia.FetionActivity.WebSite.Home.Login
{
    public class EasyLogin : IHttpHandler, IRequiresSessionState
    {
        private const string CHECK_CODE = "CodePngCheckCode";

        private HttpContext context;

        /// <summary>
        /// 
        /// </summary>
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public HttpSessionState Session
        {
            get
            {
                return context.Session;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public HttpRequest Request
        {
            get
            {
                return context.Request;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public HttpResponse Response
        {
            get
            {
                return context.Response;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Expires = -1000;
            this.context = context;



            string result = ProcessData();
            Response.Write(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string ProcessData()
        {
            Uri referUrl = Request.UrlReferrer;
            string domain = Request.Url.AbsoluteUri;
            if (!CheckUrlRefer(referUrl, domain))
            {
                return ExceptionType.SystemError.ToString();
            }

            string key = Parser.ParserString(Request["key"]);
            if (string.IsNullOrEmpty(key))
            {
                return ExceptionType.ArgumentError.ToString();
            }

            if (String.Compare(key, "Login", true) == 0)
            {
                if (!VerifyCode())
                {
                    return "-3";
                }

                return Login();
            }

            if (String.Compare(key, "CheckMobileByVerifyCode", true) == 0)
            {
                if (!VerifyCode())
                {
                    return "-3";
                }

                //return CheckMobile();
                return "";
            }
            
            else
            {
                return ExceptionType.ArgumentError.ToString();
            }

        }


        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        private string Login()
        {
            //long mobile = Parser.ParserLong(context.Request["Mobile"]);
            //string password = Parser.ParserString(context.Request["Password"]);
            //int fetionId = Parser.ParseInt(context.Request["Mobile"]);
            //int tempPassword = Parser.ParseInt(context.Request["Password"]);

            //bool flag = false;

            ////移动手机号
            //if (BaseCommon.CheckMobile(mobile))
            //{
            //    flag = DMedia.FetionActivity.Module.WpaServices.VerifyPassword(mobile, password);
            //    if (flag)
            //    {
            //        QuickLoginBusiness.WriteQuickLoginCookie(mobile);
            //        return "1";
            //    }
            //    else
            //    {
            //        flag = CheckTempPass(mobile, tempPassword);
            //        if (flag)
            //        {
            //            QuickLoginBusiness.WriteQuickLoginCookie(mobile);
            //            return "1";
            //        }
            //        else
            //        {
            //            return ExceptionType.PasswordError.ToString();
            //        }
            //    }
            //}
            //else
            //{
            //    flag = DMedia.FetionActivity.Module.WpaServices.VerifyPasswordByFetionId(fetionId, password);
            //    if (flag)
            //    {
            //        QuickLoginBusiness.WriteQuickLoginCookie(fetionId);
            //        return "1";
            //    }
            //    else
            //    {
            //        return ExceptionType.PasswordError.ToString();
            //    }
            //}
            return "";
        }

        /// <summary>
        /// 验证图形码
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private bool VerifyCode()
        {

            string code = Parser.ParserString(context.Request["verify"]);
            bool result = false;
            if (!string.IsNullOrEmpty(code) && context.Session[CHECK_CODE] != null)
            {
                result = context.Session[CHECK_CODE].ToString().ToLower().Equals(code.ToString().ToLower().Trim());
            }
            return result;
        }

        /// <summary>
        /// 上次请求域名与本次请求域名一致性
        /// </summary>
        /// <param name="urlReferrer">上次请求Uri</param>
        /// <param name="absoluteUri">当前请求Uri</param>
        /// <returns>true/false</returns>
        private bool CheckUrlRefer(Uri urlReferrer, string absoluteUri)
        {
            string tempUrlReferrer = string.Empty;

            if (urlReferrer != null && urlReferrer.ToString().Length > 0)
                tempUrlReferrer = urlReferrer.ToString();

            if (!string.IsNullOrEmpty(tempUrlReferrer) && !string.IsNullOrEmpty(absoluteUri))
            {
                tempUrlReferrer = tempUrlReferrer.Substring(tempUrlReferrer.IndexOf(":") + 3);
                tempUrlReferrer = tempUrlReferrer.Substring(0, tempUrlReferrer.IndexOf('/'));
                absoluteUri = absoluteUri.Substring(absoluteUri.IndexOf(":") + 3);
                absoluteUri = absoluteUri.Substring(0, absoluteUri.IndexOf('/'));

                if (tempUrlReferrer.Equals(absoluteUri))
                    return true;
            }

            return false;
        }

        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentTime"></param>
        /// <param name="lastTime"></param>
        /// <returns></returns>
        private bool CheckTimeout(DateTime lastTime)
        {
            try
            {
                TimeSpan span = DateTime.Now - lastTime;
                return span.TotalSeconds > 5 * 60;
            }
            catch
            {
                return true;
            }
        }
    }
}
