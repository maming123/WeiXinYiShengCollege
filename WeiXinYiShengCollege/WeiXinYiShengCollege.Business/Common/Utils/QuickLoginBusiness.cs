using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Module.DataAccess;
using System.Web.Security;
using System.Web;
using Module.Models;

namespace Module.Utils
{
    public class QuickLoginBusiness
    {
        public const string CHECK_CODE = "CodePngCheckCode";
        /// <summary>
        /// 生成一个六位的随机数
        /// </summary>
        /// <returns></returns>
        public static int RandSixNum()
        {
            return Parser.globalRandom.Next(100000, 999999);
        }

        public static void WriteQuickLoginCookie(string loginUserName)
        {
            //记录cookie 认为登录
            if (!string.IsNullOrEmpty(loginUserName))
            {
                //HttpCookie cookieName = new HttpCookie("HBUserId");
                ////cookieName.Value = EncryptTools.DESEncrypt(mobile.ToString());
                //HttpContext.Current.Response.AppendCookie(cookieName);
                string newCookie = string.Format("{0}", loginUserName);
                FormsAuthentication.SetAuthCookie(newCookie, false);
                
            }

        }

        public static bool IsHaveUser(string loginUsername, string passWord)
        {
            bool b = false;
            Sys_AdminUser user = Sys_AdminUser.SingleOrDefault(@"where LoginUserName=@0 and PassWord=@1"
                ,loginUsername,passWord);
            if(user!=null && user.Id>0)
            {
                b = true;
            }
            return b;
        }

    }
}
