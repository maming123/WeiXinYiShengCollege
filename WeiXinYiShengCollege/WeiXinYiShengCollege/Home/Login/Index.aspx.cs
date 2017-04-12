using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Module.Utils;

namespace DMedia.FetionActivity.WebSite.Utils.Login.home
{
    public partial class Index : System.Web.UI.Page
    {
        

        private HttpContext content = HttpContext.Current;
        protected void Page_Load(object sender, EventArgs e)
        {
          
            if (IsPostBack)
            {
                Login();
            }
        }

      

        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        private void Login()
        {

            string loginUserName = RequestKeeper.GetFormString(Request.Form["mobile"]);
            string password = RequestKeeper.GetFormString(Request.Form["password"]);
            string url = Request.Form["returnUrl"];

            //验证码
            string code = RequestKeeper.GetFormString(Request.Form["verifyCode"]);

            //手机号不正确： 
            //密码错误
            //正确
            int r = Login(loginUserName, password, code);
           
            if (r == ExceptionType.ValidCodeError)
            {
                //验证码错误
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "aaa", "<script> $(function(){ Login.setWarningPassword(Login.verifyCodeError); $(\"#returnUrl\").val('" + url + "'); });</script>", false);
                return;
            }
          
            if (r == ExceptionType.PasswordError)
            {
                //密码错误
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "aaa", "<script> $(function(){ Login.setWarningPassword(Login.passwordError); $(\"#returnUrl\").val('" + url + "'); });</script>", false);
                return;
            }
            if (r == ExceptionType.Success)
            {
                //成功登录 
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "aaa", "<script> $(function(){ $(\"input[type='submit']\").removeClass(\"login-btn\").addClass(\"login-btn2\"); });</script>", false);
                //跳转原因：绕开 IE7,8 下 iframe https  跨域访问http 显示无权限问题
                Response.Redirect("/home/index.aspx");
            }
        }

        /// <summary>
        /// 验证图形码 true:相等 false：不相等
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static bool IsValidCode(string verifyCode)
        {
            string code = verifyCode;
            //服务器存的验证码
            string sessionCHECK_CODE = System.Web.HttpContext.Current.Session[QuickLoginBusiness.CHECK_CODE] != null ? System.Web.HttpContext.Current.Session[QuickLoginBusiness.CHECK_CODE].ToString() : "";

            bool result = false;
            if (!string.IsNullOrEmpty(code) && !string.IsNullOrEmpty(sessionCHECK_CODE))
            {
                result = sessionCHECK_CODE.ToLower().Equals(code.ToLower().Trim());
            }
            return result;
        }
        /// <summary>
        /// 登录处理 并返回结果
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="password"></param>
        /// <param name="tempPassword"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static int Login(string loginUserName, string password, string code)
        {
            int r = ExceptionType.Error;

            if (!IsValidCode(code))
            {
                //验证码错误
                r = ExceptionType.ValidCodeError;
                return r;
            }




            bool islogin = QuickLoginBusiness.IsHaveUser(loginUserName, password);
            if (islogin)
            {
                QuickLoginBusiness.WriteQuickLoginCookie(loginUserName);
                r = ExceptionType.Success;
            }
            else
            {
                //密码错误
                r = ExceptionType.PasswordError;
            }
            return r;
        }

    }
}