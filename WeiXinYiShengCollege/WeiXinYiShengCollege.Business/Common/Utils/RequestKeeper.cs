using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Module.Utils
{
   public class RequestKeeper
    {
        /// <summary>
        /// 获取查询字符串字符值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetQuerryString(string key)
        {
            string s = GetQuerry(key);
            return InputText(s);
        }

        /// <summary>
        ///获取查询字符串数字值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int GetQuerryInt(string key)
        {
            int i = 0;
            int.TryParse(GetQuerryString(key), out i);
            return i;
        }

        #region Utilities

        private static string GetQuerry(string key)
        {
            string s = "";

            if (System.Web.HttpContext.Current.Request.QueryString[key] != null)
            {
                s = System.Web.HttpContext.Current.Request.QueryString[key];
            }

            return s;
        }

        #endregion


        /// <summary>
        /// 获取表单字符值
        /// </summary>
        /// <param name="strText"></param>
        /// <returns></returns>
        public static string GetFormString(string strText)
        {
            return InputText(strText);
        }

        /// <summary>
        /// 获取表单数字值
        /// </summary>
        /// <param name="strText"></param>
        /// <returns></returns>
        public static int GetFormInt(string strText)
        {
            strText = GetFormString(strText);
            int i = 0;
            int.TryParse(strText, out i);
            return i;
        }
        /// <summary>
        /// 获取表单数字值
        /// </summary>
        /// <param name="strText"></param>
        /// <returns></returns>
        public static long GetFormLong(string strText)
        {
            strText = GetFormString(strText);
            long i = 0;
            long.TryParse(strText, out i);
            return i;
        }

        #region Utilities

        /// <summary>
        /// 验证是否为正整数
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsInt(string str)
        {
            return Regex.IsMatch(str, @"^[0-9]*$");
        }

        /// <summary>
        /// 验证是否为日期格式的字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsDateString(string str)
        {
            return Regex.IsMatch(str, @"(\d{4})-(\d{1,2})-(\d{1,2})");
        }


        /// <summary>
        /// HH:mm:ss
        /// </summary>
        /// <returns>日期字符串</returns>
        public static string GetTime()
        {
            return GetDateTime("HH:mm:ss", null);
        }

        /// <summary>
        /// yyyy-MM-dd
        /// </summary>
        /// <returns>日期字符串</returns>
        public static string GetDate()
        {
            return GetDateTime("yyyy-MM-dd", null);
        }

        /// <summary>
        /// yyyy-MM-dd HH:mm:ss
        /// </summary>
        /// <returns>日期字符串</returns>
        public static string GetDateTime()
        {
            return GetDateTime("yyyy-MM-dd HH:mm:ss", null);
        }

        /// <summary>
        /// yyyy-MM-dd HH:mm:ss
        /// </summary>
        /// <param name="adddays">需要增加的天数</param>
        /// <returns>日期字符串</returns>
        public static string GetDateTime(int adddays)
        {
            return DateTime.Now.AddDays(adddays).ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 自定义日期
        /// </summary>
        /// <param name="formats">日期格式 如：yyyy-MM-dd</param>
        /// <param name="defaultd">默认日期 如：2010-10-10</param>
        /// <returns>日期字符串</returns>
        public static string GetDateTime(string formats, string defaultd)
        {
            if (string.IsNullOrEmpty(formats)) { formats = "yyyy-MM-dd"; }
            if (string.IsNullOrEmpty(defaultd)) { defaultd = DateTime.Now.ToString("yyyy-MM-dd"); }

            string d = "";

            try
            {
                d = DateTime.Now.ToString(formats);
            }
            catch
            {
                d = Convert.ToDateTime(defaultd).ToString("yyyy-MM-dd");
            }

            return d;
        }

        /// <summary>
        /// 清除所有脚本
        /// </summary>
        /// <param name="inputText"></param>
        /// <returns></returns>
        private static string InputText(string inputText)
        {
            if (inputText == null) return "";

            inputText = Regex.Replace(inputText, "[\\s]{2,}", " ");
            inputText = Regex.Replace(inputText, "(<[b|B][r|R]/*>)+|(<[p|P](.|\\n)*?>)", "\n");
            inputText = Regex.Replace(inputText, "(\\s*&[n|N][b|B][s|S][p|P];\\s*)+", " ");
            inputText = Regex.Replace(inputText, "<(.|\\n)*?>", "");
            inputText = inputText.Replace("'", "''");

            return Filter(inputText);
        }

        /// <summary>
        /// 过滤危险字符
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Filter(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return "";
            }

            string p = @"exec[\s]{1,}|insert[\s]{1,}into[\s]{1,}|select[\s\S]{1,}from|delete[\s]{1,}|update[\s]{1,}|truncate[\s]{1,}table|--";

            var matches = Regex.Matches(input, p, RegexOptions.IgnoreCase);

            return matches.Cast<Match>().Aggregate(input, (current, m) => current.Replace(m.Value, " "));
        }

        #endregion
    }
}
