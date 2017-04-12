using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Module.Models;

namespace HospitalBook.Module
{
   public class LoginAdminUser
    {
        private int _Id = 0;

        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private string _NickName = "";

        public string NickName
        {
            get { return _NickName; }
            set { _NickName = value; }
        }
        private string _LoginUserName="";

        public string LoginUserName
        {
          get { return _LoginUserName; }
          set { _LoginUserName = value; }
        }

        public static LoginAdminUser GetCurrentUser()
        {
            LoginAdminUser user = new LoginAdminUser();

            if (HttpContext.Current.Request.IsAuthenticated)
            {
                #region 如果登录了，直接返回Identity.Name
                string identity = HttpContext.Current.User.Identity.Name;
                user.LoginUserName = identity;
                Sys_AdminUser admin = Sys_AdminUser.SingleOrDefault(@"where LoginUserName=@0", user.LoginUserName);
                if(admin!=null)
                {
                    user.Id = admin.Id;
                    user.NickName = admin.NickName;
                }
                return user;
                #endregion
            }
            else
            {
                return null;
            }
        }
    }
}