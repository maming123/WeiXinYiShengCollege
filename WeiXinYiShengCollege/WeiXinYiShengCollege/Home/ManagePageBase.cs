using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Module.Utils;
using HospitalBook.Module;

namespace HospitalBook.WebSite.Home
{
    public class ManagePageBase : System.Web.UI.Page
    {
        /// <summary>
        /// 是否登录
        /// </summary>
        protected bool IsLogin
        {
            get
            {
                return CurrentUser != null;
            }
        }
        /// <summary>
        /// 当前用户
        /// </summary>
        protected LoginAdminUser CurrentUser
        {
            get
            {
                return LoginAdminUser.GetCurrentUser();
            }
        }
        protected override void OnLoad(EventArgs e)
        {
            if (CurrentUser == null)
            {
                //用户没有参与则创建
                Response.Write("<script language=javascript>window.top.location='/home/login/index.aspx';</script>"); Response.End();

            }

            base.OnLoad(e);
        }
    }

}
