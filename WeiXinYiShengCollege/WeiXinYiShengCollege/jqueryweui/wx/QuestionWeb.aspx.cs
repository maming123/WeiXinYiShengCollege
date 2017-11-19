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
    public partial class QuestionWeb : PageBase
    {
        public List<SickMusicDic> listSickMusicDic = new List<SickMusicDic>();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            //判断如果openid未获取到那么重新跳转到授权页
            string openid = UserBusiness.GetCookieOpenId();
            if(string.IsNullOrEmpty(openid))
            {
                Response.Redirect("gotoquestion.aspx");
            }
             List<SickMusicDic> listSickMusicDicTmp = QuestionBusiness.GetSickMusicDic();
             if (listSickMusicDicTmp != null)
             {
                 listSickMusicDic = listSickMusicDicTmp;
             }

        }
    }
}