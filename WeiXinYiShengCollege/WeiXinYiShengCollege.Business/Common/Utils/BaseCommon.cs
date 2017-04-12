using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Caching;
using Module.DataAccess;
using Newtonsoft.Json;

namespace Module.Utils
{
    public class BaseCommon
    {
        /// <summary>
        /// 对象序列化成json
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ObjectToJson(object obj)
        {
            return obj == null ? "" : JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// json反序列化成对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T JsonToObject<T>(string json)
        {
            T t = JsonConvert.DeserializeObject<T>(json);

            return t;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <param name="anonymousTypeObject"></param>
        /// <returns></returns>
        public static T JsonToObject<T>(string json, T anonymousTypeObject)
        {
            T t = JsonConvert.DeserializeAnonymousType<T>(json, anonymousTypeObject);

            return t;
        }

        /// <summary>
        /// datatable to JSON
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="dtName"></param>
        /// <returns></returns>
        public static string DataTableToJSON(DataTable dt, string dtName)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);

            using (JsonWriter jw = new JsonTextWriter(sw))
            {
                JsonSerializer ser = new JsonSerializer();
                jw.WriteStartObject();
                jw.WritePropertyName(dtName);
                jw.WriteStartArray();
                foreach (DataRow dr in dt.Rows)
                {
                    jw.WriteStartObject();

                    foreach (DataColumn dc in dt.Columns)
                    {
                        jw.WritePropertyName(dc.ColumnName);
                        ser.Serialize(jw, dr[dc].ToString());
                    }

                    jw.WriteEndObject();
                }
                jw.WriteEndArray();
                jw.WriteEndObject();

                sw.Close();
                jw.Close();

            }

            return sb.ToString();
        }

        /// <summary>
        /// JsonEncode
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string JsonEncode(string value)
        {
            if (String.IsNullOrEmpty(value))
            {
                return value;
            }

            value = value.Replace("\\", "\\\\");
            value = value.Replace("\"", "\\\"");

            return value;
        }

        /// <summary>
        /// 返回指定查询的结果
        /// </summary>
        /// <param name="sql">输入的sql语句</param>
        /// <param name="OPS">参数</param>
        /// <param name="connStr"> </param>
        /// <returns>返回的结果</returns>
        public static object GetObjectByPro(string sql, SqlParameter[] OPS, string connStr)
        {
            return SqlHelper.GetDataByPro(sql, OPS, connStr).Rows[0][0];
        }
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key"></param>
        public static T GetCache<T>(string key)
        {
            if (HasCache(key))
            {
                return (T)HttpRuntime.Cache[key];
            }

            return default(T);
        }

        /// <summary>
        /// 缓存是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool HasCache(string key)
        {
            return HttpRuntime.Cache[key] != null;
        }

        /// <summary>
        /// 缓存移除
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static void CacheRemove(string key)
        {
            if (HttpRuntime.Cache[key] != null)
                HttpRuntime.Cache.Remove(key);
        }
        /// <summary>
        /// 插入缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="absoluteExpiration">过期时间</param>
        public static void CacheInsert(string key, object value, DateTime absoluteExpiration)
        {
            if (value != null)
            {
                HttpRuntime.Cache.Insert(key, value, null, absoluteExpiration, Cache.NoSlidingExpiration);
            }
        }

        /// <summary>
        /// 插入缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="slidingExpiration">相对过期时间间隔</param>
        public static void CacheInsert(string key, object value, TimeSpan slidingExpiration)
        {
            if (value != null)
            {
                HttpRuntime.Cache.Insert(key, value, null, Cache.NoAbsoluteExpiration, slidingExpiration);
            }
        }

        /// <summary>
        /// 根据请求的页面得到根目录需要的../的字符串
        /// </summary>
        /// <returns></returns>
        public static string GetRootDes()
        {
            string str = System.Web.HttpContext.Current.Request.PhysicalApplicationPath.ToLower();
            string strAll = System.Web.HttpContext.Current.Request.PhysicalPath.ToLower();
            string[] matchCol = strAll.Replace(str, "").Split(new string[] { "\\" }, StringSplitOptions.None);
            string strReturn = "";
            for (int i = 0; i < matchCol.Length - 1; i++)
            {
                strReturn += @"../";
            }
            return strReturn;
        }

        /// <summary>
        /// 在指定目录和文件名下 保存文本
        /// </summary>
        /// <param name="path"></param>
        /// <param name="content"></param>
        public static void SaveFile(string path,string content)
        {
            if (!File.Exists(path))
            {
                using (FileStream fs = File.Create(path))
                {
                    Byte[] info = new UTF8Encoding(true).GetBytes(content);
                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);
                }
            }
            else
            {
                System.IO.StreamWriter sw = new StreamWriter(path, false);
                sw.Write(content);
                sw.Close();
                sw.Dispose();
            }
        }
        /// <summary>
        /// 读取指定路径下的文件
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string ReadFile(string path)
        {
            if (!File.Exists(path))
            {
                return "";
                
            }
            else
            {
               return  File.ReadAllText(path, Encoding.UTF8);
            }
        }


        /// <summary>
        /// 日期字符串时间格式化成另一种形式 例如：yyyyMMdd（inFormater）----》yyyy.MM.dd（outFormater）
        /// </summary>
        /// <param name="timeStr"></param>
        /// <param name="inFormater"></param>
        /// <param name="outFormater"></param>
        /// <returns></returns>
        public static string TimeParse(object timeStr, string inFormater, string outFormater)
        {
            return DateTime.ParseExact(timeStr.ToString(), inFormater, System.Globalization.CultureInfo.CurrentCulture).ToString(outFormater);
        }

        /// <summary>
        /// MD5 混淆加密
        /// </summary>
        /// <param name="input"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string MD5(string input, string key)
        {
            //key = 'bab8af935901d5b86ccb1d27c4985c32'; 
            //Tokentoken=MD5+key MD5 32key 
            //Md5(13822332274+bab8af935901d5b86ccb1d27c4985c32)  加密后字符串为 ff5f2db01bada64fdf619139518f6d87
            //string key = "bab8af935901d5b86ccb1d27c4985c32";

            byte[] result = Encoding.UTF8.GetBytes(input + "+" + key);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(result);
            string r = BitConverter.ToString(output).Replace("-", "").ToLower();
            return r;
        }

        /// <summary>
        /// MD5 混淆
        /// </summary>
        /// <param name="input"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string MD5(string str)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] inBytes = System.Text.Encoding.UTF8.GetBytes(str);
            byte[] outBytes = md5.ComputeHash(inBytes);
            string outString = "";
            for (int i = 0; i < outBytes.Length; i++)
            {
                outString += outBytes[i].ToString("x2");
            }
            return outString;
        }
    }
}