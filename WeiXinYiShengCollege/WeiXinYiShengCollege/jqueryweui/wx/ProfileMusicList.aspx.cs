using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Module.Models;
using Module.Utils;
using WeiXinYiShengCollege.Business;
using WeiXinYiShengCollege.Business.Common.Models;

namespace WeiXinYiShengCollege.WebSite.jqueryweui.wx
{
    public partial class ProfileMusicList : PageBase
    {
        public List<SickMusicItem> listMusic = new List<SickMusicItem>();
        protected void Page_Load(object sender, EventArgs e)
        {
            // UserBusiness.WriteCookie(new Sys_User() { OpenId = "od_wK1iAymw_T0jC10JOzfq1vgvY" });
            //判断如果openid未获取到那么重新跳转到授权页
            string openid = UserBusiness.GetCookieOpenId();
            int status = RequestKeeper.GetFormInt(Request["Status"]);
            //获取曲目名称
            listMusic = QuestionBusiness.GetMusicNameFromSicknessStatus(status);
        }
    }
}