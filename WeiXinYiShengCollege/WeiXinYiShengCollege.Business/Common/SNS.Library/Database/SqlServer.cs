/*----------------------------------------------------------------
// Copyright (C) 2006 
// ��Ȩ���С� 
//
// �ļ�����SqlServer.cs
// �ļ���������������SQLServer���ݿ�
//
// 
// ������ʶ��2004-10-22���� by Lining
//
// �޸ı�ʶ��
// �޸�������
//
// �޸ı�ʶ��
// �޸�������
//----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace SNS.Library.Database
{
	/// <summary>
	/// �ṩ����SqlServer���ݿ�ĸ��ַ��������ܼ̳д��ࡣ
	/// </summary>
	internal sealed class SqlServer : IDatabase
	{
		/// <summary>
        /// ����һ������SQLServer���ݿ�Ķ���
		/// </summary>
		public SqlServer()
		{
		}

		/// <summary>
        /// ����һ������SQLServer���ݿ�Ķ���
		/// </summary>
		/// <param name="userName">�û�����</param>
		/// <param name="password">�û�����</param>
		/// <param name="serverName">���ݿ������������</param>
		/// <param name="databaseName">���ݿ�����</param>
		public SqlServer(string userName,string password,string serverName,string databaseName)
		{
			this._userName = userName;
			this._password = password;
			this._serverName = serverName;
			this._databaseName = databaseName;
		}

		/// <summary>
		/// ����һ������SQLServer���ݿ�Ķ���
		/// </summary>
		/// <param name="userName">�û�����</param>
		/// <param name="password">�û�����</param>
		/// <param name="serverName">���ݿ������������</param>
		/// <param name="databaseName">���ݿ�����</param>
		/// <param name="connectionTimeout">���ӳ�ʱ</param>
		public SqlServer(string userName,string password,string serverName,string databaseName,int connectionTimeout) : 
			this(userName,password,serverName,databaseName)
		{
			this._connectionTimeout = connectionTimeout;
		}

        /// <summary>
        /// ����һ������SQLServer���ݿ�Ķ���
        /// </summary>
        /// <param name="serverName">���������ƻ�IP��ַ</param>
        /// <param name="databaseName">���ݿ�����</param>
        public SqlServer(string serverName, string databaseName)
        {
            this._serverName = serverName;
            this._databaseName = databaseName;
        }

        /// <summary>
        /// ����һ������SQLServer���ݿ�Ķ���
        /// </summary>
        /// <param name="serverName">���������ƻ�IP��ַ</param>
        /// <param name="databaseName">���ݿ�����</param>
        /// <param name="connectionTimeout">���ӳ�ʱ</param>
        public SqlServer(string serverName, string databaseName, int connectionTimeout):
            this(serverName,databaseName)
        {
            this._connectionTimeout = connectionTimeout;
        }

		#region �ֶ�

		/// <summary>
		/// ��¼���ݿ���û�����
		/// </summary>
		private string _userName = "";

		/// <summary>
		/// ��¼���ݿ���û��Ŀ���
		/// </summary>
		private string _password = "";

		/// <summary>
		/// ���ݿ������������
		/// </summary>
		private string _serverName = "";

		/// <summary>
		/// ���ݿ������
		/// </summary>
		private string _databaseName = "";

		/// <summary>
		/// ��ʾSqlServer���ݿ��һ���򿪵�����
		/// </summary>
		private SqlConnection _sqlConn = null;

		/// <summary>
		/// ��ʾҪ��SqlServer���ݿ�ִ�е�һ��Transact-SQL����洢����
		/// </summary>
		private SqlCommand _sqlCmd = null;

		/// <summary>
		/// ��ʾҪ��SqlServer���ݿ��д����Transact-SQL����
		/// </summary>
		private SqlTransaction _sqlTrans = null;

		/// <summary>
		/// �ڳ��Խ�������ʱ��ֹ���Բ����ɴ���֮ǰ���ȴ���ʱ��
		/// </summary>
		private int _connectionTimeout = 15;

		/// <summary>
		/// ��������
		/// </summary>
		private ArrayList _parameters;

		/// <summary>
		/// ִ��SQL����ĳ�ʱ����
		/// </summary>
		private int _commandTimeout = 20;

        /// <summary>
        /// ���ݿ����������֤ģʽ
        /// </summary>
        private AuthenticationMode _authenticationMode = AuthenticationMode.SQLServerAndWindows;

        /// <summary>
        /// ���������ļ�����
        /// </summary>
        private string _attachDBFileName = "";

		#endregion �����ֶ�


		#region ʵ��IDatabase��Ա

		#region ����

		/// <summary>
		/// ��ȡ�����õ�¼���ݿ���û�����
		/// </summary>
		public string UserName
		{
			get
			{
				return this._userName;
			}
			set
			{
				this._userName = value;
			}
		}

		/// <summary>
		/// ��ȡ�����õ�¼���ݿ���û��Ŀ���
		/// </summary>
		public string Password
		{
			get
			{
				return this._password;
			}
			set
			{
				this._password = value;
			}
		}

		/// <summary>
		/// ��ȡ���������ݿ������������
		/// </summary>
		public string ServerName
		{
			get
			{
				return this._serverName;
			}
			set
			{
				this._serverName = value;
			}
		}

		/// <summary>
		/// ��ȡ���������ݿ������
		/// </summary>
		public string DatabaseName
		{
			get
			{
				return this._databaseName;
			}
			set
			{
				this._databaseName = value;
			}
		}

		/// <summary>
		/// ��ȡ�������ڳ��Խ�������ʱ��ֹ���Բ����ɴ���֮ǰ���ȴ���ʱ��
		/// </summary>
		public int ConnectionTimeout
		{
			get
			{
				return this._connectionTimeout;
			}
			set
			{
				this._connectionTimeout = value;
			}
		}

		/// <summary>
		/// ��ȡ��������
		/// </summary>
		public ArrayList Parameters
		{
			get
			{
				if(this._parameters == null)
				{
					this._parameters = new ArrayList();
				}
				return this._parameters;
			}
		}

		/// <summary>
		/// �����ݿ������״̬
		/// </summary>
		public ConnectionState ConnectionState
		{
			get
			{
				if(this._sqlConn != null)
				{
					return this._sqlConn.State;
				}
				else
				{
					return ConnectionState.Closed;
				}
			}
		}

		/// <summary>
		/// ��ȡ������ִ��SQL����ĳ�ʱ����
		/// </summary>
		public int CommandTimeout
		{
			get
			{
				return this._commandTimeout;
			}
			set
			{
				this._commandTimeout = value;
			}
		}

        /// <summary>
        /// ��ȡ���������ݿ����������֤ģʽ
        /// </summary>
        public AuthenticationMode AuthenticationMode
        {
            get
            {
                return this._authenticationMode;
            }
            set
            {
                this._authenticationMode = value;
            }
        }

        /// <summary>
        /// ��ȡ�����ø��������ļ�����
        /// </summary>
        public string AttachDBFileName
        {
            get
            {
                return this._attachDBFileName;
            }
            set
            {
                this._attachDBFileName = value;
            }
        }

		#endregion ��������


		#region ����

		/// <summary>
		/// �������ݿ�
		/// </summary>
        public void Connect()
        {
            //���������ַ���
            string strConnection = "";
            if (this._authenticationMode == AuthenticationMode.SQLServerAndWindows)
            {
                strConnection = "data source=" + _serverName + ";user id=" + _userName +
                    ";initial catalog=" + _databaseName + ";password=" + _password + ";Connect Timeout=" +
                    _connectionTimeout + ";pooling=true";
            }
            else
            {
                if (this._attachDBFileName == "")
                {
                    strConnection = "SERVER=" + this._serverName + ";DATABASE=" + this._databaseName +
                        ";INTEGRATED SECURITY=SSPI;POOLING=TRUE;";
                }
                else
                {
                    strConnection = "Data Source=" + this._serverName + ";AttachDbFileName=" +
                        this._attachDBFileName + ";Integrated Security=True;User Instance=False";
                }
            }

            this._sqlConn = new SqlConnection(strConnection);
                this._sqlConn.Open();
        }

		/// <summary>
		/// �ر������ݿ������
		/// </summary>
		public void Close()
		{
				if(this._sqlConn != null)
				{
					if(this._sqlConn.State == ConnectionState.Open)
					{
						this._sqlConn.Close();
					}
					this._sqlConn = null;
				}
				if(this._sqlCmd != null)
				{
					this._sqlCmd = null;
				}
		}

		/// <summary>
		/// ������ִ��Transact-SQL��䲢������Ӱ�������
		/// </summary>
		/// <param name="sql">Ҫִ�е�Sql���</param>
		/// <returns>��Ӱ�������</returns>
		public int ExecuteNonQuery(string sql)
		{
			if(this._sqlCmd == null)
			{
				this._sqlCmd = new SqlCommand(sql,this._sqlConn);
			}
			else
			{
				this._sqlCmd.Parameters.Clear();
				this._sqlCmd.CommandText = sql;
			}
			if(this._parameters != null)
			{
				for(int i = 0; i < this._parameters.Count; i++)
				{
					SqlParameter sqlParameter = (SqlParameter)this._parameters[i];
					this._sqlCmd.Parameters.Add(sqlParameter);
				}
			}
			
				this._sqlCmd.CommandType = CommandType.Text;
				this._sqlCmd.CommandTimeout = this._commandTimeout;
				return this._sqlCmd.ExecuteNonQuery();
		}


		/// <summary>
		/// ������ִ��Transact-SQL��䲢���ز�ѯ���
		/// </summary>
		/// <param name="sql">��ѯSQL���</param>
		/// <param name="result">�����ѯ����Ľ����</param>
		/// <returns>���������ļ�¼��</returns>
		public int ExecuteQuery(string sql,DataTable result)
		{
			SqlDataAdapter sqlDA = new SqlDataAdapter(sql,this._sqlConn);

			try
			{
				sqlDA.SelectCommand.CommandTimeout = this._commandTimeout;
				return sqlDA.Fill(result);
			}
			finally
			{
				sqlDA = null;
			}
		}

		/// <summary>
		/// ������ִ��Transact-SQL��䲢���ز�ѯ���
		/// </summary>
		/// <param name="sql">��ѯSQL���</param>
		/// <returns>�����ѯ�����DataTable</returns>
		public DataTable ExecuteQuery(string sql) 
		{
			SqlDataAdapter sqlDA = new SqlDataAdapter(sql,this._sqlConn);

			try
			{
				DataTable dtResult = new DataTable();
				sqlDA.SelectCommand.CommandTimeout = this._commandTimeout;
				sqlDA.Fill(dtResult);
				return dtResult;
			}
			finally
			{
				sqlDA = null;
			}
		}

		/// <summary>
		/// ��ʼ���ݿ�����
		/// </summary>
		public void BeginTransaction()
		{
				if(this._sqlCmd == null)
				{
					this._sqlCmd = new SqlCommand();
				}

				this._sqlTrans = this._sqlConn.BeginTransaction();
				this._sqlCmd.Connection = this._sqlConn;
				this._sqlCmd.Transaction = this._sqlTrans;
		}

		/// <summary>
		/// �ύ���ݿ�����
		/// </summary>
		public void Commit()
		{
				if(this._sqlTrans != null)
				{
					this._sqlTrans.Commit();
					this._sqlTrans = null;
				}
		}

		/// <summary>
		/// �ӹ���״̬�ع�����
		/// </summary>
		public void Rollback()
		{
				if(this._sqlTrans != null)
				{
					this._sqlTrans.Rollback();
					this._sqlTrans = null;
				}
		}

		/// <summary>
		/// ִ��û�з��ؽ���Ĵ洢����
		/// </summary>
		/// <param name="storeProcedureName">�洢���̵�����</param>
		/// <returns>��Ӱ�������</returns>
		public int ExecuteNonQueryStoreProcedure(string storeProcedureName)
		{
			if(this._sqlCmd == null)
			{
				this._sqlCmd = new SqlCommand(storeProcedureName,this._sqlConn);
			}
			else
			{
				this._sqlCmd.CommandText = storeProcedureName;
				this._sqlCmd.Parameters.Clear();
			}

			this._sqlCmd.CommandType = CommandType.StoredProcedure;

			//��Ӳ���
			if(this._parameters != null)
			{
				foreach(object obj in this._parameters)
				{
					if(obj is SqlParameter)
					{
						SqlParameter parameter = (SqlParameter)obj;
						this._sqlCmd.Parameters.Add(parameter);
					}
					else
					{
						throw new Exception("�����������з�SQLServer���͵Ĳ���");
					}
				}
			}
			
			try
			{
				this._sqlCmd.CommandTimeout = this._commandTimeout;
				return this._sqlCmd.ExecuteNonQuery();
			}
			catch(Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// <summary>
		/// ִ�д����ؽ���Ĵ洢����
		/// </summary>
		/// <param name="storeProcedureName">�洢���̵�����</param>
		/// <returns>��ѯ���</returns>
		public DataTable ExecuteQueryStoreProcedure(string storeProcedureName)
		{
			if(this._sqlCmd == null)
			{
				this._sqlCmd = new SqlCommand(storeProcedureName,this._sqlConn);
			}
			else
			{
				this._sqlCmd.CommandText = storeProcedureName;
			}

			this._sqlCmd.CommandType = CommandType.StoredProcedure;

			//��Ӳ���
			if(this._parameters != null)
			{
				foreach(object obj in this._parameters)
				{
					if(obj is SqlParameter)
					{
						SqlParameter parameter = (SqlParameter)obj;
						this._sqlCmd.Parameters.Add(parameter);
					}
					else
					{
						throw new Exception("�����������з�SQLServer���͵Ĳ���");
					}
				}
			}
			
			try
			{
				SqlDataAdapter sqlDA = new SqlDataAdapter(this._sqlCmd);
				sqlDA.SelectCommand.CommandTimeout = this._commandTimeout;
				DataTable dtResult = new DataTable();
				sqlDA.Fill(dtResult);
				return dtResult;
			}
			catch(Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// <summary>
		/// �õ�ָ���������ֵ
		/// </summary>
		/// <param name="tableName">������</param>
		/// <returns>����ֵ</returns>
		public int GetSequence(string tableName)
		{
			//��Ӵ洢���̵Ĳ���
			SqlParameter inParameter = new SqlParameter("@TableName",SqlDbType.VarChar);
			inParameter.Value = tableName;
			SqlParameter outParameter = new SqlParameter("@KeyValue",SqlDbType.Int);
			outParameter.Direction = ParameterDirection.Output;
			this.Parameters.Add(inParameter);
			this.Parameters.Add(outParameter);

			//���д洢����
			this.ExecuteNonQueryStoreProcedure("p_GetSequence");

			return int.Parse(outParameter.Value.ToString());
		}

		/// <summary>
		/// ִ�в�����䲢�ҷ��ص�ǰ����ֵ
		/// </summary>
		/// <param name="insertSql">�������</param>
		/// <param name="tableName">���ݱ�����</param>
		/// <returns>����ֵ</returns>
		public int ExecuteInsertReturnPK(string insertSql,string tableName)
		{
			string strSql = insertSql + " SELECT IDENT_CURRENT('" + tableName + "')";
			if(this._sqlCmd == null)
			{
				this._sqlCmd = new SqlCommand(strSql,this._sqlConn);
			}
			else
			{
				this._sqlCmd.Parameters.Clear();
				this._sqlCmd.CommandText = strSql;
			}
			if(this._parameters != null)
			{
				for(int i = 0; i < this._parameters.Count; i++)
				{
					SqlParameter sqlParameter = (SqlParameter)this._parameters[i];
					this._sqlCmd.Parameters.Add(sqlParameter);
				}
			}
			
				this._sqlCmd.CommandTimeout = this._commandTimeout;
				this._sqlCmd.CommandType = CommandType.Text;
				return int.Parse(this._sqlCmd.ExecuteScalar().ToString());
		}

		/// <summary>
		/// ִ�в�ѯSQL��䲢����DataReader
		/// </summary>
		/// <param name="sql">��ѯSQL���</param>
		/// <returns>DataReader</returns>
		public IDataReader ExecuteDataReader(string sql)
		{
			if(this._sqlCmd == null)
			{
				this._sqlCmd = new SqlCommand(sql,this._sqlConn);
			}
			else
			{
				this._sqlCmd.Parameters.Clear();
				this._sqlCmd.CommandText = sql;
			}

			if(this._parameters != null)
			{
				for(int i = 0; i < this._parameters.Count; i++)
				{
					SqlParameter sqlParameter = (SqlParameter)this._parameters[i];
					this._sqlCmd.Parameters.Add(sqlParameter);
				}
			}
			
				this._sqlCmd.CommandTimeout = this._commandTimeout;
				this._sqlCmd.CommandType = CommandType.Text;
				return this._sqlCmd.ExecuteReader();
		}

		/// <summary>
		/// ִ�в�ѯ�������ز�ѯ�����صĽ�����е�һ�еĵ�һ�С����Զ�����л���
		/// </summary>
		/// <param name="sql">��ѯSQL���</��param>
		/// <returns>������е�һ�еĵ�һ�п����ã���������Ϊ�գ�</returns>
		public object ExecuteScalar(string sql)
		{
			if(this._sqlCmd == null)
			{
				this._sqlCmd = new SqlCommand(sql,this._sqlConn);
			}
			else
			{
				this._sqlCmd.Parameters.Clear();
				this._sqlCmd.CommandText = sql;
			}

			if(this._parameters != null)
			{
				for(int i = 0; i < this._parameters.Count; i++)
				{
					SqlParameter sqlParameter = (SqlParameter)this._parameters[i];
					this._sqlCmd.Parameters.Add(sqlParameter);
				}
			}

				this._sqlCmd.CommandTimeout = this._commandTimeout;
				this._sqlCmd.CommandType = CommandType.Text;
				return this._sqlCmd.ExecuteScalar();
		}

		/// <summary>
		/// ִ�����ͷŻ����÷��й���Դ��ص�Ӧ�ó����������
		/// </summary>
		public void Dispose()
		{
			this.Close();
		}

        /// <summary>
        /// ����Image���͵��ֶ�
        /// </summary>
        /// <param name="tableName">���ݱ�����</param>
        /// <param name="imageFieldName">Image���͵��ֶ�����</param>
        /// <param name="keyFieldName">�ؼ��ֶ�����</param>
        /// <param name="keyFieldValue">�ؼ��ֶε�ȡֵ</param>
        /// <param name="imageContent">ͼƬ���ֽ�����</param>
        /// <returns>ִ��SQL��������ݿ��Ӱ��������1��������0����������</returns>
        public int UpdateImageFiled(string tableName, string imageFieldName, string[] keyFieldName,
            string[] keyFieldValue, byte[] imageContent)
        {
            if (keyFieldName == null || keyFieldValue == null)
            {
                throw new Exception("�ؼ��ֶε����ƻ�ȡֵ����Ϊnull��");
            }
            if (keyFieldName.Length != keyFieldValue.Length)
            {
                throw new Exception("�ؼ��ֶε����ƺ�ȡֵ������������");
            }

            //��֯SQL���
            string strSql = "UPDATE " + tableName + " SET " + imageFieldName + "=@ImageContent " +
                "WHERE";
            for (int i = 0; i < keyFieldName.Length; i++)
            {
                strSql += " " + keyFieldName[i] + "='" + keyFieldValue[i] + "' AND";
            }
            strSql = strSql.Substring(0, strSql.Length - 4);

            //��Ӳ���
            SqlParameter paramImage = new SqlParameter("@ImageContent", SqlDbType.Image);
            paramImage.Value = imageContent;
            this.Parameters.Add(paramImage);

            //ִ��SQL���
            return ExecuteNonQuery(strSql);
        }

        #endregion ��������

        #endregion ����ʵ��IDatabase��Ա
    }

    /// <summary>
    /// SQLServer��������֤ģʽö��
    /// </summary>
    public enum AuthenticationMode
    {
        /// <summary>
        /// Windows��֤ģʽ
        /// </summary>
        Windows = 1,

        /// <summary>
        /// SQLServer��Windows���ģʽ
        /// </summary>
        SQLServerAndWindows = 2
    }
}
