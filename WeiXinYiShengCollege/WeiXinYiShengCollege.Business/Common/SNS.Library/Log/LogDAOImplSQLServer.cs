using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using SNS.Library.Database;


			/***************************************
			 * �����ƣ�LogDAOImplSQLServer
			 *   ���ܣ�����ϵͳ��־�����ݷ��ʶ����
			 *	       SQLServer�汾
			 *     by��Lining
			 *   ���ڣ�2004-11-30
			 *   ��ע���̳г�����LogDAO
			 **************************************/


namespace SNS.Library.Logs
{
	/// <summary>
	/// ����ϵͳ��־�����ݷ��ʶ����SQLServer�汾��
	/// </summary>
	internal class LogDAOImplSQLServer : LogDAO
	{
		/// <summary>
		/// ��������ϵͳ��־�����ݷ��ʶ���
		/// </summary>
		public LogDAOImplSQLServer()
		{
		}


		#region ��������

		/// <summary>
		/// ���ղ�ѯ������ѯϵͳ��־
		/// </summary>
		/// <param name="pageNumber">��ǰҳ��</param>
		/// <param name="pageSize">ÿҳ��ʾ�ļ�¼��</param>
		/// <param name="beginDateTime">��ʼʱ��</param>
		/// <param name="endDateTime">����ʱ��</param>
		/// <param name="logType">��־����</param>
		/// <param name="orderType">��������</param>
		/// <returns>ϵͳ��־����</returns>
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
		/// ������־�����в�ѯ
		/// </summary>
		/// <param name="types">��־�����</param>
		/// <param name="logInfoKey">��־��Ϣ��ֵ</param>
		/// <param name="returnRecordCount">���صļ�¼��</param>
		/// <returns>��־����</returns>
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

		#endregion ������������
	}
}
