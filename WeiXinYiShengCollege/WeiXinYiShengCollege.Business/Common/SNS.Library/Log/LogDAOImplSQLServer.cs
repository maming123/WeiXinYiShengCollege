using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using SNS.Library.Database;


			/***************************************
			 * 类名称：LogDAOImplSQLServer
			 *   功能：管理系统日志的数据访问对象的
			 *	       SQLServer版本
			 *     by：Lining
			 *   日期：2004-11-30
			 *   备注：继承抽象类LogDAO
			 **************************************/


namespace SNS.Library.Logs
{
	/// <summary>
	/// 管理系统日志的数据访问对象的SQLServer版本。
	/// </summary>
	internal class LogDAOImplSQLServer : LogDAO
	{
		/// <summary>
		/// 创建管理系统日志的数据访问对象
		/// </summary>
		public LogDAOImplSQLServer()
		{
		}


		#region 公开方法

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
		public override DataTable Find(int pageNumber,int pageSize,string beginDateTime,string endDateTime,
			int logType,string orderType)
		{
			IDatabase database = DatabaseFactory.CreateObject();

			try
			{
				SqlParameter paramPageNumber = new SqlParameter("@PageNumber",SqlDbType.Int);
				paramPageNumber.Value = pageNumber;
				database.Parameters.Add(paramPageNumber);

				SqlParameter paramPageSize = new SqlParameter("@PageSize",SqlDbType.Int);
				paramPageSize.Value = pageSize;
				database.Parameters.Add(paramPageSize);

				SqlParameter paramStartDateTime = new SqlParameter("@StartDateTime",SqlDbType.VarChar);
				paramStartDateTime.Value = beginDateTime;
				database.Parameters.Add(paramStartDateTime);

				SqlParameter paramEndDateTime = new SqlParameter("@EndDateTime",SqlDbType.VarChar);
				paramEndDateTime.Value = endDateTime;
				database.Parameters.Add(paramEndDateTime);

				SqlParameter paramLogType = new SqlParameter("@LogType",SqlDbType.Int);
				paramLogType.Value = logType;
				database.Parameters.Add(paramLogType);

				SqlParameter paramOrderType = new SqlParameter("@OrderType",SqlDbType.VarChar);
				paramOrderType.Value = orderType;
				database.Parameters.Add(paramOrderType);

				SqlParameter paramRecordCount = new SqlParameter("@RecordCount",SqlDbType.Int);
				paramRecordCount.Direction = ParameterDirection.Output;
				database.Parameters.Add(paramRecordCount);

				SqlParameter paramPageCount = new SqlParameter("@PageCount",SqlDbType.Int);
				paramPageCount.Direction = ParameterDirection.Output;
				database.Parameters.Add(paramPageCount);

				DataTable dtLog = database.ExecuteQueryStoreProcedure("p_GetLog");
                if (paramRecordCount.Value != null && Convert.ToInt32(paramRecordCount.Value) > 0)
                {
                    this._recordCount = Convert.ToInt32(paramRecordCount.Value);
                    this._pageCount = Convert.ToInt32(paramPageCount.Value);
                }

                return dtLog;
			}
			finally
			{
				database.Close();
			}
		}

		/// <summary>
		/// 按照日志类别进行查询
		/// </summary>
		/// <param name="types">日志的类别</param>
		/// <param name="logInfoKey">日志信息键值</param>
		/// <param name="returnRecordCount">返回的记录数</param>
		/// <returns>日志集合</returns>
		public override DataTable FindLogByType(string[] types,string logInfoKey,int returnRecordCount)
		{
			string strSql = "SELECT TOP " + returnRecordCount + " LOGDATE,DESCRIPT," + 
                "ISNULL(Module_Name,'') AS Module_Name FROM LOG WHERE (";
			foreach(string strType in types)
			{
				strSql += "LOGTYPE=" + strType + " OR ";
			}
			strSql = strSql.Substring(0,strSql.Length - 4) + ")";
			strSql += " AND LOG_INFO='" + logInfoKey + "' ORDER BY LOGDATE DESC";

			return DatabaseFactory.ExecuteQuery(strSql);
		}

		#endregion 结束公开方法
	}
}
