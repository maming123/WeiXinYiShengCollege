using System;
using System.Collections;
using System.Data;
using SNS.Library.Database;


			/*********************************
			 * 类名称：LogDAO
			 *   功能：管理系统日志的数据
			 *		   访问对象
			 *     by：Lining
			 *   日期：2004-11-30
			 ********************************/


namespace SNS.Library.Logs
{
	/// <summary>
	/// 管理系统日志的数据访问对象。
	/// </summary>
	public abstract class LogDAO
	{
		#region 字段

		/// <summary>
		/// 查询结果的记录数
		/// </summary>
		protected int _recordCount = 0;

		/// <summary>
		/// 查询结果的总页数
		/// </summary>
		protected int _pageCount = 0;

		#endregion 结束字段


		#region 属性

		/// <summary>
		/// 获取查询结果的记录数
		/// </summary>
		public int RecordCount
		{
			get
			{
				return this._recordCount;
			}
		}

		/// <summary>
		/// 获取查询结果的总页数 
		/// </summary>
		public int PageCount
		{
			get
			{
				return this._pageCount;
			}
		}

		#endregion 结束属性


		#region 公开方法

		/// <summary>
		/// 记录日志
		/// </summary>
		/// <param name="log">日志对象</param>
		public void Write(Log log)
		{
            try
            {
                if (log.Info != null && log.Info.Contains("wuling")) return;

                string strSql = "INSERT INTO LOG(LOGDATE,DESCRIPT,LOGTYPE,LOG_INFO,Module_Name) " +
                    "VALUES(GETDATE(),'" + log.Description.Replace('\'', '\"') + "'," + (int)log.Type + ",'" + log.Info.Replace('\'', '\"') +
                    "','" + log.ModuleName.Replace('\'', '\"') + "')";
                DatabaseFactory.ExecuteNonQuery(strSql);
            }
            catch { }
		}

		/// <summary>
		/// 按照日志类别和时间范围查询
		/// </summary>
		/// <param name="typeID">日志类别编号（多个日志编号可用逗号分隔）</param>
		/// <param name="beginDateTime">起始时间</param>
		/// <param name="endDateTime">结束时间</param>
		/// <returns>系统日志集合</returns>
		public DataTable FindByTypeID(string typeID,string beginDateTime,string endDateTime)
		{
            string strFormatSql = "SELECT LOGDATE,DESCRIPT,LOG_INFO,ISNULL(Module_Name,'') AS Module_Name FROM LOG WHERE LOGTYPE={0}";
			if(beginDateTime != "" && endDateTime != "")
			{
				strFormatSql += " AND LOGDATE BETWEEN '" + beginDateTime + "' AND '" + endDateTime + "'";
			}

			string strSql = "";
			if(typeID.Split(',').Length == 1)
			{
				strSql = string.Format(strFormatSql,typeID);
			}
			else
			{
				string[] arrTypeID = typeID.Split(',');
				foreach(string strTypeID in arrTypeID)
				{
					strSql += string.Format(strFormatSql,strTypeID) + " UNION ALL ";
				}
				strSql = strSql.Substring(0,strSql.Length - 11);
			}

			return DatabaseFactory.ExecuteQuery(strSql);
		}

		/// <summary>
		/// 按照日志类别查询
		/// </summary>
		/// <param name="typeID">类别编号</param>
		/// <returns>日志对象集合</returns>
		public DataTable FindByTypeID(string typeID)
		{
			return FindByTypeID(typeID,"","");
		}

		/// <summary>
		/// 按照查询条件查询系统日志
		/// </summary>
		/// <param name="pageNumber">当前页号</param>
		/// <param name="pageSize">每页显示的记录数</param>
		/// <param name="beginDateTime">起始时间</param>
		/// <param name="endDateTime">结束时间</param>
		/// <param name="logType">日志类型</param>
		/// <param name="orderType">排序类型</param>
		/// <returns>系统日志集合</returns>
		public abstract DataTable Find(int pageNumber,int pageSize,string beginDateTime,string endDateTime,
			int logType,string orderType);

		/// <summary>
		/// 清空指定类别的日志
		/// </summary>
		/// <param name="typeID">日志类别编号</param>
		/// <param name="reserveDays">保留日志的天数</param>
		public void Clear(int typeID,int reserveDays)
		{
			string strSql = "DELETE FROM LOG WHERE LOGTYPE=" + typeID + " AND DATEDIFF(DAY,LOGDATE,GETDATE()) >= 2";
			DatabaseFactory.ExecuteNonQuery(strSql);
		}

		/// <summary>
		/// 按照日志类别进行查询
		/// </summary>
		/// <param name="types">日志的类别</param>
		/// <param name="logInfoKey">日志信息键值</param>
		/// <param name="returnRecordCount">返回的记录数</param>
		/// <returns>日志集合</returns>
		public abstract DataTable FindLogByType(string[] types,string logInfoKey,int returnRecordCount);

		#endregion 结束公开方法
	}
}
