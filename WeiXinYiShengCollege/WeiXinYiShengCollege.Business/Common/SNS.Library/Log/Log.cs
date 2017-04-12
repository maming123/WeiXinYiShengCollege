using System;


			/******************************
			 * 类名称：Log
			 *   功能：表示一条系统日志
			 *     by：Lining
			 *   日期：2004-11-30
			 *   备注：不能继承此类
			 *****************************/


namespace SNS.Library.Logs
{
	/// <summary>
	/// 表示一条系统日志。不能继承此类。
	/// </summary>
	public sealed class Log
	{
		/// <summary>
		/// 创建一条系统日志
		/// </summary>
		public Log()
		{
		}


		#region 字段

		/// <summary>
		/// 日志编号
		/// </summary>
		private int _id = 0;

		/// <summary>
		/// 记录日志的时间
		/// </summary>
		private string _dateTime = "";

		/// <summary>
		/// 日志的描述
		/// </summary>
		private string _description = "";

		/// <summary>
		/// 日志类别
		/// </summary>
		private LogType _logType = 0;

		/// <summary>
		/// 日志信息（存放一些附加信息，如用户名称）
		/// </summary>
		private string _info = "";

        /// <summary>
        /// 模块名称
        /// </summary>
        private string _moduleName = "";

		#endregion 结束字段


		#region 属性

		/// <summary>
		/// 获取或设置日志编号
		/// </summary>
		public int ID
		{
			get
			{
				return this._id;
			}
			set
			{
				this._id = value;
			}
		}

		/// <summary>
		/// 获取或设置记录日志的时间
		/// </summary>
		public string DateTime
		{
			get
			{
				return this._dateTime;
			}
			set
			{
				this._dateTime = value;
			}
		}

		/// <summary>
		/// 获取或设置日志描述
		/// </summary>
		public string Description
		{
			get
			{
				return this._description;
			}
			set
			{
				this._description = value;
			}
		}

		/// <summary>
		/// 获取或设置日志的类别
		/// </summary>
		public LogType Type
		{
			get
			{
				return this._logType;
			}
			set
			{
				this._logType = value;
			}
		}

		/// <summary>
		/// 获取或设置日志信息
		/// </summary>
		public string Info
		{
			get
			{
				return this._info;
			}
			set
			{
				this._info = value;
			}
		}

        /// <summary>
        /// 获取或设置模块名称
        /// </summary>
        public string ModuleName
        {
            get
            {
                return this._moduleName;
            }
            set
            {
                this._moduleName = value;
            }
        }

		#endregion 结束属性
	}


    /// <summary>
    /// 系统日志类型枚举
    /// </summary>
    public enum LogType
    {
        /// <summary>
        /// 操作日志
        /// </summary>
        Operation = 9,

        /// <summary>
        /// 错误日志
        /// </summary>
        Error = 10,

        /// <summary>
        /// 程序日志
        /// </summary>
        Application = 11,
        
        /// <summary>
        /// 通讯日志
        /// </summary>
        Communication = 12,

        /// <summary>
        /// 数据库同步日志
        /// </summary>
        DatabaseReplication = 13,

        /// <summary>
        /// 调试日志
        /// </summary>
        Debug = 300
    }
}
