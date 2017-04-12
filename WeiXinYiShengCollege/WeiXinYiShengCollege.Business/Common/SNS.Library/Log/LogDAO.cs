using System;
using System.Collections;
using System.Data;
using SNS.Library.Database;


			/*********************************
			 * �����ƣ�LogDAO
			 *   ���ܣ�����ϵͳ��־������
			 *		   ���ʶ���
			 *     by��Lining
			 *   ���ڣ�2004-11-30
			 ********************************/


namespace SNS.Library.Logs
{
	/// <summary>
	/// ����ϵͳ��־�����ݷ��ʶ���
	/// </summary>
	public abstract class LogDAO
	{
		#region �ֶ�

		/// <summary>
		/// ��ѯ����ļ�¼��
		/// </summary>
		protected int _recordCount = 0;

		/// <summary>
		/// ��ѯ�������ҳ��
		/// </summary>
		protected int _pageCount = 0;

		#endregion �����ֶ�


		#region ����

		/// <summary>
		/// ��ȡ��ѯ����ļ�¼��
		/// </summary>
		public int RecordCount
		{
			get
			{
				return this._recordCount;
			}
		}

		/// <summary>
		/// ��ȡ��ѯ�������ҳ�� 
		/// </summary>
		public int PageCount
		{
			get
			{
				return this._pageCount;
			}
		}

		#endregion ��������


		#region ��������

		/// <summary>
		/// ��¼��־
		/// </summary>
		/// <param name="log">��־����</param>
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
		/// ������־����ʱ�䷶Χ��ѯ
		/// </summary>
		/// <param name="typeID">��־����ţ������־��ſ��ö��ŷָ���</param>
		/// <param name="beginDateTime">��ʼʱ��</param>
		/// <param name="endDateTime">����ʱ��</param>
		/// <returns>ϵͳ��־����</returns>
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
		/// ������־����ѯ
		/// </summary>
		/// <param name="typeID">�����</param>
		/// <returns>��־���󼯺�</returns>
		public DataTable FindByTypeID(string typeID)
		{
			return FindByTypeID(typeID,"","");
		}

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
		public abstract DataTable Find(int pageNumber,int pageSize,string beginDateTime,string endDateTime,
			int logType,string orderType);

		/// <summary>
		/// ���ָ��������־
		/// </summary>
		/// <param name="typeID">��־�����</param>
		/// <param name="reserveDays">������־������</param>
		public void Clear(int typeID,int reserveDays)
		{
			string strSql = "DELETE FROM LOG WHERE LOGTYPE=" + typeID + " AND DATEDIFF(DAY,LOGDATE,GETDATE()) >= 2";
			DatabaseFactory.ExecuteNonQuery(strSql);
		}

		/// <summary>
		/// ������־�����в�ѯ
		/// </summary>
		/// <param name="types">��־�����</param>
		/// <param name="logInfoKey">��־��Ϣ��ֵ</param>
		/// <param name="returnRecordCount">���صļ�¼��</param>
		/// <returns>��־����</returns>
		public abstract DataTable FindLogByType(string[] types,string logInfoKey,int returnRecordCount);

		#endregion ������������
	}
}
