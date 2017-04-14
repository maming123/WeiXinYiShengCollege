using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HospitalBook.WebSite.Home;
using Senparc.Weixin.MP.AdvancedAPIs.Media;
using WeiXinYiShengCollege.Business;


namespace WeiXinYiShengCollege.WebSite.Home.MaterialNews
{
    public partial class MaterialNewsList : ManagePageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            GetMaterialNews();
        }
        private void GetMaterialNews()
        {
           MediaList_NewsResult list = WeiXinBusiness.GetNewsMediaList();
           string str = "";
        }
    }
}