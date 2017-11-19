using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Module.Models;
using Module.Utils;
using WeiXinYiShengCollege.Business.Common.Models;

namespace WeiXinYiShengCollege.Business
{
    public class QuestionBusiness
    {
        /// <summary>
        /// 年干 六十甲子顺序表中的年干
        /// </summary>
        public static readonly  Dictionary<int, string> NianGan = new Dictionary<int, string>() {
        { 1, "甲" }, { 2, "乙" }, { 3, "丙" }, { 4, "丁" }, { 5, "戊" }, { 6, "己" }, { 7, "庚" }, { 8, "辛" }, { 9, "壬" }, { 10, "癸" },
        { 11, "甲" }, { 12, "乙" }, { 13, "丙" }, { 14, "丁" }, { 15, "戊" }, { 16, "己" }, { 17, "庚" }, { 18, "辛" }, { 19, "壬" }, { 20, "癸" },
        { 21, "甲" }, { 22, "乙" }, { 23, "丙" }, { 24, "丁" }, { 25, "戊" }, { 26, "己" }, { 27, "庚" }, { 28, "辛" }, { 29, "壬" }, { 30, "癸" },
        { 31, "甲" }, { 32, "乙" }, { 33, "丙" }, { 34, "丁" }, { 35, "戊" }, { 36, "己" }, { 37, "庚" }, { 38, "辛" }, { 39, "壬" }, { 40, "癸" },
        { 41, "甲" }, { 42, "乙" }, { 43, "丙" }, { 44, "丁" }, { 45, "戊" }, { 46, "己" }, { 47, "庚" }, { 48, "辛" }, { 49, "壬" }, { 50, "癸" },
        { 51, "甲" }, { 52, "乙" }, { 53, "丙" }, { 54, "丁" }, { 55, "戊" }, { 56, "己" }, { 57, "庚" }, { 58, "辛" }, { 59, "壬" }, { 60, "癸" },
        };
        /// <summary>
        /// 日支 六十甲子顺序表中的日支
        /// </summary>
        public static readonly Dictionary<int, string> RiZhi = new Dictionary<int, string>() {
        { 1, "子" }, { 2, "丑" }, { 3, "寅" }, { 4, "卯" }, { 5, "辰" }, { 6, "巳" }, { 7, "午" }, { 8, "未" }, { 9, "申" }, { 10, "酉" },{ 11, "戌" },{ 12, "亥" },
        { 13, "子" }, { 14, "丑" }, { 15, "寅" }, { 16, "卯" }, { 17, "辰" }, { 18, "巳" }, { 19, "午" }, { 20, "未" },{ 21, "申" }, { 22, "酉" }, { 23, "戌" }, { 24, "亥" },
        { 25, "子" }, { 26, "丑" }, { 27, "寅" }, { 28, "卯" }, { 29, "辰" }, { 30, "巳" }, { 31, "午" }, { 32, "未" }, { 33, "申" }, { 34, "酉" }, { 35, "戌" }, { 36, "亥" },
        { 37, "子" }, { 38, "丑" }, { 39, "寅" }, { 40, "卯" }, { 41, "辰" }, { 42, "巳" }, { 43, "午" }, { 44, "未" }, { 45, "申" }, { 46, "酉" }, { 47, "戌" }, { 48, "亥" },
        { 49, "子" }, { 50, "丑" }, { 51, "寅" }, { 52, "卯" }, { 53, "辰" }, { 54, "巳" }, { 55, "午" }, { 56, "未" }, { 57, "申" }, { 58, "酉" }, { 59, "戌" }, { 60, "亥" }
        };

        /// <summary>
        /// 是否完成过调查问卷
        /// </summary>
        /// <param name="openId"></param>
        /// <returns></returns>
        public static bool IsExist(string openId)
        {
            Question q = Question.SingleOrDefault("where OpenId=@0", openId);

            if (null != q && q.Id > 0)

                return true;
            else
                return false;
        }

        /// <summary>
        /// 添加问卷调查内容
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        public static bool Add(Question question)
        {
            object obj =question.Insert();
            if (Convert.ToInt32(obj) > 0)
            {
                return true;
            }
            else
                return false;
        }
        /// <summary>
        /// 通过手机号和生日获取病症
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="birthday"></param>
        /// <returns></returns>
        public static Question GetQuestion(string mobile ,DateTime birthday)
        {
            Question q = Question.SingleOrDefault("where Mobile=@0 and Birthday=@1", mobile, birthday);

            if (null != q && q.Id > 0)

                return q;
            else
                return null;
        }
        /// <summary>
        /// 通过OpenId获取病症
        /// </summary>
        /// <param name="openId"></param>
        /// <returns></returns>
        public static Question GetQuestion(string openId)
        {
            Question q = Question.SingleOrDefault("where OpenId=@0", openId);

            if (null != q && q.Id > 0)

                return q;
            else
                return null;
        }

        /// <summary>
        /// 获取病症曲目字典表 status=1 曲目字典表
        /// </summary>
        /// <returns></returns>
        public static List<SickMusicDic> GetSickMusicDic()
        {
            List<SickMusicDic> list = new List<SickMusicDic>();
            list = SickMusicDic.Query("where status=1").ToList();
            return list;
        }

        /// <summary>
        /// 根据病症类型获取曲目列表
        /// </summary>
        /// <param name="sickName"></param>
        /// <returns></returns>
        public static List<SickMusicItem> GetMusicNameFromSicknessStatus(int status)
        {
            string strWhere = "";
            if(status==1)
            {
                strWhere = "where Status=1 ";
            }else
            {
                strWhere = "where Status in (2,3) ";
            }
            List<SickMusicDic> sickMusicList = SickMusicDic.Query(strWhere).ToList();
            if (null != sickMusicList)
            {
                List<SickMusicItem> list = new List<SickMusicItem>();
                foreach (SickMusicDic sickMusic in sickMusicList)
                {
                    SickMusicItem s = BaseCommon.JsonToObject<SickMusicItem>(sickMusic.MusicList);
                    list.Add(s);
                }
                return list;
            }
            else
                return new List<SickMusicItem>();
        }

        /// <summary>
        /// 根据病症获取曲目列表
        /// </summary>
        /// <param name="sickName"></param>
        /// <returns></returns>
        public static SickMusicItem GetMusicNameFromSickness(string sickName)
        {
           SickMusicDic sickMusic = SickMusicDic.SingleOrDefault("where SickName=@0 ", sickName);
           if (null != sickMusic)
           {
               SickMusicItem list = BaseCommon.JsonToObject<SickMusicItem>(sickMusic.MusicList);
               return list;
           }
           else
               return new SickMusicItem();
        }

        public static SickMusicItem GetMusicNameFromNianGan(DateTime birthday)
        {
            string sickName = GetSickNameFromNianGan(birthday);
            return GetMusicNameFromSickness(sickName);
        }
        public static SickMusicItem GetMusicNameFromRiZhi(DateTime birthday)
        {
            string sickName = GetSickNameFromRiZhi(birthday);
            return GetMusicNameFromSickness(sickName);
        }

        /// <summary>
        /// 通过生日获取年干
        /// </summary>
        /// <param name="birthday"></param>
        /// <returns></returns>
        private static string GetSickNameFromNianGan(DateTime birthday)
        {
            //年干：1900-2100年年干=年尾数-3；例如1969年年干=9-3=6   ； 2012年年干=2-3=9（2不足减补10）
           char[] chars= birthday.Year.ToString().ToArray();
           int yearLastNum = Convert.ToInt32(chars[3]);
           int niangan = yearLastNum - 3;
           if (niangan < 0)
               return "年干-"+NianGan[niangan + 10];
           else
               return "年干-" + NianGan[niangan];
        }
        /// <summary>
        /// 通过生日获取日支
        /// </summary>
        /// <param name="birthday"></param>
        /// <returns></returns>
        private static string GetSickNameFromRiZhi(DateTime birthday)
        {
            //Z=8C+[C/4]+[5y]+[y/4]+[3*(m+1)/5]+d+7+i
            //其中C是世纪数减1 ，奇数月i=0，偶数月i=6,y是年份后两位，m是月份,d是日数,[]表示取整数，1月和2月按照上一年的13月和14月来算，因此C和y也要按上一年的年份来取值，Z除以12的余数是地支。
            int C = 21-1;
            int i = 0;
            if (birthday.Month % 2 == 1)
                i = 0;
            else
            {
                i = 6;
            }
            int d = birthday.Day;
            int m = birthday.Month;
            int y =0;
            char[] chars = birthday.Year.ToString().ToArray();
            int yearLast2Num = Convert.ToInt32(chars[2]+chars[3]);
            y = yearLast2Num;
            if(m==1)
            {
                m = 13;
                char[] chars1 = birthday.AddYears(-1).ToString().ToArray();
                int yearLast2Num1 = Convert.ToInt32(chars[2] + chars[3]);
                y = yearLast2Num1;
            }
            if(m==2)
            {
                m = 14;
                char[] chars2 = birthday.AddYears(-1).ToString().ToArray();
                int yearLast2Num2 = Convert.ToInt32(chars[2] + chars[3]);
                y = yearLast2Num2;
            }
            int Z = 8 * C + C / 4 + 5 * y + y / 4 + 3 * (m + 1) / 5 + d + 7 + i;

            if (Z%12 >0)
                return "日支-" + RiZhi[Z%12];
            else
                return "日支-";
        }



        


    }
}
