/*----------------------------------------------------------------
// Copyright (C) 2006 
// 版权所有。 
//
// 文件名：SqlServer.cs
// 文件功能描述：操作SQLServer数据库
//
// 
// 创建标识：2004-10-22创建 by Lining
//
// 修改标识：
// 修改描述：
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace SNS.Library.Database
{
	/// <summary>
	/// 提供操作SqlServer数据库的各种方法。不能继承此类。
	/// </summary>
	internal sealed class SqlServer : IDatabase
	{
		/// <summary>
        /// 创建一个操作SQLServer数据库的对象
		/// </summary>
		public SqlServer()
		{
		}

		/// <summary>
        /// 创建一个操作SQLServer数据库的对象
		/// </summary>
		/// <param name="userName">用户名称</param>
		/// <param name="password">用户口令</param>
		/// <param name="serverName">数据库服务器的名称</param>
		/// <param name="databaseName">数据库名称</param>
		public SqlServer(string userName,string password,string serverName,string databaseName)
		{
			this._userName = userName;
			this._password = password;
			this._serverName = serverName;
			this._databaseName = databaseName;
		}

		/// <summary>
		/// 创建一个操作SQLServer数据库的对象
		/// </summary>
		/// <param name="userName">用户名称</param>
		/// <param name="password">用户口令</param>
		/// <param name="serverName">数据库服务器的名称</param>
		/// <param name="databaseName">数据库名称</param>
		/// <param name="connectionTimeout">连接超时</param>
		public SqlServer(string userName,string password,string serverName,string databaseName,int connectionTimeout) : 
			this(userName,password,serverName,databaseName)
		{
			this._connectionTimeout = connectionTimeout;
		}

        /// <summary>
        /// 创建一个操作SQLServer数据库的对象
        /// </summary>
        /// <param name="serverName">服务器名称或IP地址</param>
        /// <param name="databaseName">数据库名称</param>
        public SqlServer(string serverName, string databaseName)
        {
            this._serverName = serverName;
            this._databaseName = databaseName;
        }

        /// <summary>
        /// 创建一个操作SQLServer数据库的对象
        /// </summary>
        /// <param name="serverName">服务器名称或IP地址</param>
        /// <param name="databaseName">数据库名称</param>
        /// <param name="connectionTimeout">连接超时</param>
        public SqlServer(string serverName, string databaseName, int connectionTimeout):
            this(serverName,databaseName)
        {
            this._connectionTimeout = connectionTimeout;
        }

		#region 字段

		/// <summary>
		/// 登录数据库的用户名称
		/// </summary>
		private string _userName = "";

		/// <summary>
		/// 登录数据库的用户的口令
		/// </summary>
		private string _password = "";

		/// <summary>
		/// 数据库服务器的名称
		/// </summary>
		private string _serverName = "";

		/// <summary>
		/// 数据库的名称
		/// </summary>
		private string _databaseName = "";

		/// <summary>
		/// 表示SqlServer数据库的一个打开的连接
		/// </summary>
		private SqlConnection _sqlConn = null;

		/// <summary>
		/// 表示要对SqlServer数据库执行的一个Transact-SQL语句或存储过程
		/// </summary>
		private SqlCommand _sqlCmd = null;

		/// <summary>
		/// 表示要在SqlServer数据库中处理的Transact-SQL事务
		/// </summary>
		private SqlTransaction _sqlTrans = null;

		/// <summary>
		/// 在尝试建立连接时终止尝试并生成错误之前所等待的时间
		/// </summary>
		private int _connectionTimeout = 15;

		/// <summary>
		/// 参数集合
		/// </summary>
		private ArrayList _parameters;

		/// <summary>
		/// 执行SQL命令的超时设置
		/// </summary>
		private int _commandTimeout = 20;

        /// <summary>
        /// 数据库服务器的验证模式
        /// </summary>
        private AuthenticationMode _authenticationMode = AuthenticationMode.SQLServerAndWindows;

        /// <summary>
        /// 附件数据文件名称
        /// </summary>
        private string _attachDBFileName = "";

		#endregion 结束字段


		#region 实现IDatabase成员

		#region 属性

		/// <summary>
		/// 获取或设置登录数据库的用户名称
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
		/// 获取或设置登录数据库的用户的口令
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
		/// 获取或设置数据库服务器的名称
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
		/// 获取或设置数据库的名称
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
		/// 获取或设置在尝试建立连接时终止尝试并生成错误之前所等待的时间
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
		/// 获取参数集合
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
		/// 与数据库的连接状态
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
		/// 获取或设置执行SQL命令的超时设置
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
        /// 获取或设置数据库服务器的验证模式
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
        /// 获取或设置附加数据文件名称
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

		#endregion 结束属性


		#region 方法

		/// <summary>
		/// 连接数据库
		/// </summary>
        public void Connect()
        {
            //生成连接字符串
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
		/// 关闭与数据库的连接
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
		/// 对连接执行Transact-SQL语句并返回受影响的行数
		/// </summary>
		/// <param name="sql">要执行的Sql语句</param>
		/// <returns>受影响的行数</returns>
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
		/// 对连接执行Transact-SQL语句并返回查询结果
		/// </summary>
		/// <param name="sql">查询SQL语句</param>
		/// <param name="result">保存查询结果的结果集</param>
		/// <returns>符合条件的记录数</returns>
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
		/// 对连接执行Transact-SQL语句并返回查询结果
		/// </summary>
		/// <param name="sql">查询SQL语句</param>
		/// <returns>保存查询结果的DataTable</returns>
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
		/// 开始数据库事务
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
		/// 提交数据库事务
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
		/// 从挂起状态回滚事务
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
		/// 执行没有返回结果的存储过程
		/// </summary>
		/// <param name="storeProcedureName">存储过程的名称</param>
		/// <returns>受影响的行数</returns>
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

			//添加参数
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
						throw new Exception("参数集合中有非SQLServer类型的参数");
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
		/// 执行带返回结果的存储过程
		/// </summary>
		/// <param name="storeProcedureName">存储过程的名称</param>
		/// <returns>查询结果</returns>
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

			//添加参数
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
						throw new Exception("参数集合中有非SQLServer类型的参数");
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
		/// 得到指定表的主键值
		/// </summary>
		/// <param name="tableName">表名称</param>
		/// <returns>主键值</returns>
		public int GetSequence(string tableName)
		{
			//添加存储过程的参数
			SqlParameter inParameter = new SqlParameter("@TableName",SqlDbType.VarChar);
			inParameter.Value = tableName;
			SqlParameter outParameter = new SqlParameter("@KeyValue",SqlDbType.Int);
			outParameter.Direction = ParameterDirection.Output;
			this.Parameters.Add(inParameter);
			this.Parameters.Add(outParameter);

			//运行存储过程
			this.ExecuteNonQueryStoreProcedure("p_GetSequence");

			return int.Parse(outParameter.Value.ToString());
		}

		/// <summary>
		/// 执行插入语句并且返回当前主键值
		/// </summary>
		/// <param name="insertSql">插入语句</param>
		/// <param name="tableName">数据表名称</param>
		/// <returns>主键值</returns>
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
		/// 执行查询SQL语句并返回DataReader
		/// </summary>
		/// <param name="sql">查询SQL语句</param>
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
		/// 执行查询，并返回查询所返回的结果集中第一行的第一列。忽略额外的列或行
		/// </summary>
		/// <param name="sql">查询SQL语句</或param>
		/// <returns>结果集中第一行的第一列空引用（如果结果集为空）</returns>
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
		/// 执行与释放或重置非托管资源相关的应用程序定义的任务
		/// </summary>
		public void Dispose()
		{
			this.Close();
		}

        /// <summary>
        /// 更新Image类型的字段
        /// </summary>
        /// <param name="tableName">数据表名称</param>
        /// <param name="imageFieldName">Image类型的字段名称</param>
        /// <param name="keyFieldName">关键字段名称</param>
        /// <param name="keyFieldValue">关键字段的取值</param>
        /// <param name="imageContent">图片的字节数组</param>
        /// <returns>执行SQL语句后对数据库的影响行数（1－正常；0－不正常）</returns>
        public int UpdateImageFiled(string tableName, string imageFieldName, string[] keyFieldName,
            string[] keyFieldValue, byte[] imageContent)
        {
            if (keyFieldName == null || keyFieldValue == null)
            {
                throw new Exception("关键字段的名称或取值不能为null！");
            }
            if (keyFieldName.Length != keyFieldValue.Length)
            {
                throw new Exception("关键字段的名称和取值的数量不符！");
            }

            //组织SQL语句
            string strSql = "UPDATE " + tableName + " SET " + imageFieldName + "=@ImageContent " +
                "WHERE";
            for (int i = 0; i < keyFieldName.Length; i++)
            {
                strSql += " " + keyFieldName[i] + "='" + keyFieldValue[i] + "' AND";
            }
            strSql = strSql.Substring(0, strSql.Length - 4);

            //添加参数
            SqlParameter paramImage = new SqlParameter("@ImageContent", SqlDbType.Image);
            paramImage.Value = imageContent;
            this.Parameters.Add(paramImage);

            //执行SQL语句
            return ExecuteNonQuery(strSql);
        }

        #endregion 结束方法

        #endregion 结束实现IDatabase成员
    }

    /// <summary>
    /// SQLServer服务器验证模式枚举
    /// </summary>
    public enum AuthenticationMode
    {
        /// <summary>
        /// Windows验证模式
        /// </summary>
        Windows = 1,

        /// <summary>
        /// SQLServer和Windows混合模式
        /// </summary>
        SQLServerAndWindows = 2
    }
}
