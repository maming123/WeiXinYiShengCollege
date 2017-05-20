using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using log4net.Config;
using Senparc.Weixin.MP.Sample.CommonService;
using WeiXinYiShengCollege.Business;

namespace WeiXinYiShengCollege.WebSite
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            //应用程序启动时，自动加载配置log4Net  
            XmlConfigurator.Configure();
            WeiXinBusiness.AccessTokenRegister();
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

            try
            {
                Exception exc = Server.GetLastError();
                if (exc.InnerException != null)
                    exc = exc.InnerException;

                SNS.Library.Tools.WebTools.WriteLog(exc.Message + exc.Source + exc.StackTrace, SNS.Library.Logs.LogType.Error);

                // string loginStr = string.Format(@"<a href='/home/account/login.aspx'>登录</a>");
                // Response.Write(exc.Message + exc.Source + exc.StackTrace + loginStr);

            }
            catch (Exception ex)
            {
                SNS.Library.Tools.WebTools.WriteLog(ex.Message + ex.Source + ex.StackTrace, SNS.Library.Logs.LogType.Error);

                Response.Write("服务器开小差了，请关闭后重新进入公众号");
                Response.End();
                //string loginStr = string.Format(@"<a href='/view/account/login.aspx'>登录</a>");
                //Response.Write(ex.Message + ex.Source + ex.StackTrace + loginStr);
            }

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}