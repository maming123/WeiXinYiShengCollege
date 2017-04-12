using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace Module.Utils
{
    public class BaseHandler : IHttpHandler, IRequiresSessionState
    {
        private HttpContext context;
        protected Dictionary<string, Action> dictAction = new Dictionary<string, Action>(StringComparer.CurrentCultureIgnoreCase);
        /// <summary>
        /// 是否来自手机
        /// </summary>
        protected bool IsFromMobile = false;
        /// <summary>
        /// 方法
        /// </summary>
        public string Action
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public int SiteId
        {
            get;
            set;
        }

        public string ValidCode
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Session对象
        /// </summary>
        public HttpSessionState Session
        {
            get
            {
                return context.Session;
            }
        }

        /// <summary>
        /// 请求对象
        /// </summary>
        public HttpRequest Request
        {
            get
            {
                return context.Request;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public HttpResponse Response
        {
            get
            {
                return context.Response;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Expires = -1000;
            this.context = context;

            try
            {
                Action = Request.Params["Action"];
                //兼容 Method add bymaming 20150515
                if (string.IsNullOrEmpty(Action))
                {
                    Action = Request.Params["Method"];
                }
                DoAction(Action);
            }
            catch (ArgumentException aex)
            {
                Response.Write(BaseCommon.ObjectToJson(new { code = ExceptionType.ArgumentError, message = aex.Message }));
            }
            catch (Exception ex)
            {
                Response.Write(BaseCommon.ObjectToJson(new { code = ExceptionType.SystemError, message = ex.Message }));
            }
        }

        /// <summary>
        /// 处理业务事件
        /// </summary>
        /// <param name="actionName">事件方法</param>
        protected virtual void DoAction(string actionName)
        {
            Action action = null;
            if (!dictAction.TryGetValue(actionName, out action))
            {
                throw new ArgumentException("Action Is Not Found");
            }
            action();
        }
    }
}