using System;


			/*************************************
			 * 类名称：LogDAOFactory
			 *   功能：创建管理系统日志的数据访问
			 *		   对象的工厂
			 *     by：Lining
			 *   日期：2004-11-30
			 *   备注：不能继承此类
			 *************************************/


namespace SNS.Library.Logs
{
	/// <summary>
	/// 创建管理系统日志的数据访问对象的工厂。
	/// </summary>
	public sealed class LogDAOFactory
    {
        #region 公开静态方法

        /// <summary>
		/// 创建管理系统日志的数据访问对象
		/// </summary>
		/// <returns>管理系统日志的数据访问对象</returns>
		public static LogDAO CreateObject()
		{
			return new LogDAOImplSQLServer();
		}

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="description">日志描述</param>
        public static void Write(string description)
        {
            Write(description, LogType.Operation);
        }

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="description">日志描述</param>
        /// <param name="type">日志类型</param>
        public static void Write(string description,LogType type)
        {
            string strUserName = "";

            if (System.Web.HttpContext.Current.Session["SStaffNumber"] != null)
            {
                strUserName = System.Web.HttpContext.Current.Session["SStaffNumber"].ToString();
            }

            string strModuleName = "";
            if (System.Web.HttpContext.Current.Session["ModuleName"] != null)
            {
                strModuleName = System.Web.HttpContext.Current.Session["ModuleName"].ToString();
            }

            Write(description, strModuleName, strUserName, type);
        }

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="description">日志描述</param>
        /// <param name="moduleName">模块名称</param>
        /// <param name="userName">用户名称</param>
        /// <param name="type">日志类型</param>
        public static void Write(string description, string moduleName, string userName, LogType type)
        {
            Log log = new Log();
            log.Description = description;
            log.DateTime = DateTime.Now.ToShortTimeString();
            log.ModuleName = moduleName;
            log.Info = userName;
            log.Type = type;

            CreateObject().Write(log);
        }

        #endregion 结束静态公开方法
    }
}
