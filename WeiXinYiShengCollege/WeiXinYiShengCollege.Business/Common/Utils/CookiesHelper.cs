using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Module.Utils
{
    public class CookiesHelper
    {
        /// <summary> 
        /// 获得Cookie 
        /// </summary>
        /// <param name="cookieName"></param> 
        /// <returns></returns>
        public static HttpCookie GetCookie(string cookieName)
        {
            HttpRequest request = HttpContext.Current.Request;
            if (request != null) 
                return request.Cookies[cookieName];
            return null;
        }
        /// <summary> 
        /// 添加Cookie 
        /// </summary>
        /// <param name="cookie"></param> 
        public static void AddCookie(HttpCookie cookie)
        {
            HttpResponse response = HttpContext.Current.Response;
            if (response != null)
            {
                //指定客户端脚本是否可以访问[默认为false] 
                cookie.HttpOnly = true;
                //指定统一的Path，比便能通存通取 
                cookie.Path = "/";
                //设置跨域,这样在其它二级域名下就都可以访问到了 //
                //cookie.Domain = "chinesecoo.com"; 
                response.AppendCookie(cookie);
            }
        }
        /// <summary>
        /// 设置Cookie
        /// </summary>
        /// <param name="cookieName"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetCookie(string cookieName, string value)
        {
            HttpResponse response = HttpContext.Current.Response;
            if (response != null)
            {
                HttpCookie cookie = response.Cookies[cookieName];
                if (cookie != null)
                {
                    cookie.Value = value;
                    response.SetCookie(cookie);
                }
            }
        }
        /// <summary>
        /// 设置Cookie
        /// </summary>
        /// <param name="cookieName"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetCookie(string cookieName, string key, string value)
        { 
            SetCookie(cookieName, key, value, null); 
        }

        /// <summary>
        /// /// 设置Cookie 
        /// </summary>
        /// <param name="cookieName"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expires"></param>
        public static void SetCookie(string cookieName, string key, string value, DateTime? expires)
        {
            HttpResponse response = HttpContext.Current.Response;
            if (response != null)
            {
                HttpCookie cookie = response.Cookies[cookieName];
                if (cookie != null)
                {
                    if (!string.IsNullOrEmpty(key) && cookie.HasKeys)
                        cookie.Values.Set(key, value);
                    else if (!string.IsNullOrEmpty(value))
                        cookie.Value = value;
                    if (expires != null)
                        cookie.Expires = expires.Value;
                    response.SetCookie(cookie);
                }
            }
        }

        /// <summary>
        /// 清除指定Cookie
        /// </summary>
        /// <param name="cookiename">cookiename</param>
        public static void ClearCookie(string cookiename)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookiename];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddYears(-3);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }
        /// <summary>
        /// 获取或者对Cookie赋值
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public static void Cookie(string name,string value)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[name];
            if (cookie != null)
            {
                SetCookie(name, value);
            }
            else
            {
                AddCookie(new HttpCookie(name, value));
            }
        }
    }
}