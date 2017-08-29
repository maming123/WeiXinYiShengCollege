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
    public partial class QuestionResult : PageBase
    {
        public List<SickMusicItem> listMusic = new List<SickMusicItem>();
        protected void Page_Load(object sender, EventArgs e)
        {
            string openId = UserBusiness.GetCookieOpenId();
            Question question = QuestionBusiness.GetQuestion(openId);
            //mobile=" + mobile+"&birthday="+txtBirthday
            //string mobile = RequestKeeper.GetFormString(Request["mobile"]);
            //string birthday = RequestKeeper.GetFormString(Request["birthday"]);
            //Question question = QuestionBusiness.GetQuestion(mobile, Convert.ToDateTime(birthday));
            if (null != question && !string.IsNullOrEmpty(question.Sickness))
            {
                //获取曲目名称
                SickMusicItem music =   QuestionBusiness.GetMusicNameFromSickness(question.Sickness);
                listMusic.Add(music);
                SickMusicItem musicNianGan = QuestionBusiness.GetMusicNameFromNianGan(Convert.ToDateTime(question.Birthday));
                if(null!=musicNianGan && !string.IsNullOrEmpty(musicNianGan.MusicName))
                {
                    listMusic.Add(musicNianGan);
                }
                SickMusicItem musicRiZhi = QuestionBusiness.GetMusicNameFromRiZhi(Convert.ToDateTime(question.Birthday));
                if (null != musicRiZhi && !string.IsNullOrEmpty(musicRiZhi.MusicName))
                {
                    listMusic.Add(musicRiZhi);
                }
            }

        }
    }
}