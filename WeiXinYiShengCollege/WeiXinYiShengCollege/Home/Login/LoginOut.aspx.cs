using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using HospitalBook.WebSite.Home;
using Module.Utils;

namespace HospitalBookWebSite.Home.Login
{
    public partial class LoginOut : ManagePageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(CurrentUser!=null && CurrentUser.Id>0)
            {
                FormsAuthentication.SignOut();
                Response.Redirect(Request["U"]);
                //var httpCookie = HttpContext.Current.Request.Cookies["Activity"];
                //if (httpCookie != null)
                //{
                //    httpCookie.Expires = DateTime.Now.AddSeconds(-60);
                //    System.Web.HttpContext.Current.Response.Cookies.Add(httpCookie);
                //    //System.Web.Security.FormsAuthentication.SignOut();
                //}
            }
        }
    }
}