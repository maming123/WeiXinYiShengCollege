/*----------------------------------------------------------------
// Copyright (C) 2006 
// 版权所有。 
//
// 文件名：DatabaseFactory.cs
// 文件功能描述：创建操作数据库的对象的工厂
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
using System.Xml;
using System.Data;
using System.Management;
using System.IO;
using System.Configuration;

namespace SNS.Library.Database
{
	/// <summary>
	/// 提供创建维操作数据库的对象的方法。不能继承此类。
	/// </summary>
    //[System.Serializable]
	public class DatabaseFactory
	{

		#region 静态字段 

		/// <summary>
		/// 用户名称
		/// </summary>
		private static string s_userName = "";

		/// <summary>
		/// 口令
		/// </summary>
		private static string s_password = "";

		/// <summary>
		/// 服务器名称
		/// </summary>
		private static string s_serverName = "";

		/// <summary>
		/// 数据库名称
		/// </summary>
		private static string s_databaseName = "";

		/// <summary>
		/// 超时时间设置
		/// </summary>
		private static int s_timeout = 0;

		///
		private static string s_AreaCode = "";

        /// <summary>
        /// 数据库服务器的验证模式
        /// </summary>
        private static AuthenticationMode s_authenticationMode = AuthenticationMode.SQLServerAndWindows;

        /// <summary>
        /// 附加数据文件名称
        /// </summary>
        private static string s_attachDBFileName = "";


        private static bool s_BBError = false;

		#endregion 结束静态字段  


        #region 公开静态方法
        public static string GetMyCode()
        {
            return s_AreaCode;
        }


        /// <summary>
		/// 创建操作数据库的对象
		/// </summary>
		/// <returns>操作数据库的对象</returns>
        public static IDatabase CreateObject()
        {
            Exception exc = null;
            try
            {
                return DatabaseFactory.CreateObject(-1,true);
            }
            catch(Exception e) 
            {
                exc = e;
            }

            try
            {
                DatabaseFactory.s_serverName = "";
				DatabaseFactory.s_AreaCode = "";
                return DatabaseFactory.CreateObject(-1, false);
            }
            catch (Exception ex)
            {
                if (s_BBError)
                {
                    throw exc;
                }
                else
                {
                    throw ex;
                }
            }
        }


		/// <summary>
		/// 创建操作数据库的对象
		/// </summary>
		/// <param name="serverName">数据库服务器的名称</param>
		/// <param name="databaseName">数据库名称</param>
		/// <param name="userName">用户名称</param>
		/// <param name="password">口令</param>
		/// <returns>操作数据库的对象</returns>
		public static IDatabase CreateObject(string serverName,string databaseName,string userName,string password)
		{
			return DatabaseFactory.CreateObject(serverName,databaseName,userName,password,60);
		}

		/// <summary>
		/// 创建操作数据库的对象
		/// </summary>
		/// <param name="serverName">数据库服务器的名称</param>
		/// <param name="databaseName">数据库名称</param>
		/// <param name="userName">用户名称</param>
		/// <param name="password">口令</param>
		/// <param name="timeout">连接超时</param>
		/// <returns>操作数据库的对象</returns>
		public static IDatabase CreateObject(string serverName,string databaseName,string userName,
			string password,int timeout)
		{
				IDatabase database = new SqlServer(userName,password,serverName,databaseName,timeout);
				database.Connect();
				return database;
		}

		/// <summary>
		/// 对连接执行Transact-SQL语句并返回受影响的行数
		/// </summary>
		/// <param name="sql">要执行的Sql语句</param>
		/// <returns>受影响的行数</returns>
		public static int ExecuteNonQuery(string sql)
		{
			using(IDatabase database = CreateObject())
			{
				return database.ExecuteNonQuery(sql);
			}
		}

		/// <summary>
		/// 对连接执行Transact-SQL语句并返回查询结果
		/// </summary>
		/// <param name="sql">查询SQL语句</param>
		/// <returns>保存查询结果的结果集</returns>
		public static DataTable ExecuteQuery(string sql)
		{
			using(IDatabase database = CreateObject())
			{
				DataTable dtResult = new DataTable();
				database.ExecuteQuery(sql,dtResult);
				return dtResult;
			}
		}

		/// <summary>
		/// 得到指定表的主键值
		/// </summary>
		/// <param name="tableName">表名称</param>
		/// <returns>主键值</returns>
		public static int GetSequence(string tableName)
		{
			using(IDatabase database = CreateObject())
			{
				return database.GetSequence(tableName);
			}
		}

		/// <summary>
		/// 执行插入语句并且返回当前主键值
		/// </summary>
		/// <param name="insertSql">插入语句</param>
		/// <param name="tableName">数据表名称</param>
		/// <returns>主键值</returns>
		public static int ExecuteInsertReturnPK(string insertSql,string tableName)
		{
			using(IDatabase database = CreateObject())
			{
				return database.ExecuteInsertReturnPK(insertSql,tableName);
			}
		}

		/// <summary>
		/// 执行查询，并返回查询所返回的结果集中第一行的第一列。忽略额外的列或行
		/// </summary>
		/// <param name="sql">查询SQL语句</param>
		/// <returns>结果集中第一行的第一列或空引用（如果结果集为空）</returns>
		public static object ExecuteScalar(string sql)
		{
			using(IDatabase database = CreateObject())
			{
				return database.ExecuteScalar(sql);
			}
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
        public static int UpdateImageFiled(string tableName, string imageFieldName, string[] keyFieldName,
            string[] keyFieldValue, byte[] imageContent)
        {
            return CreateObject().UpdateImageFiled(tableName, imageFieldName, keyFieldName, keyFieldValue, imageContent);
        }

        #endregion 结束静态方法


        #region 私有静态方法

        /// <summary>
        /// 创建操作数据库的对象
        /// </summary>
        /// <param name="timeout">超时秒数</param>
        /// <param name="isNew">是否是新版本的方法（新版本的方法用户名和数据库名称不加密）</param>
        /// <returns>操作数据库的对象</returns>
        private static IDatabase CreateObject(int timeout,bool isNew)
        {
            //Data Source=127.0.0.1;Initial Catalog=pubmlk;User ID=mlkdb;Password=mlkdb@2015
            string Conn = ConfigurationManager.ConnectionStrings["Core"].ConnectionString;
            string [] strArr =Conn.Split(';');
                s_serverName=strArr[0].Split('=')[1];
                s_databaseName = strArr[1].Split('=')[1]; ;
                s_userName = strArr[2].Split('=')[1]; ;
                s_password = strArr[3].Split('=')[1]; ;
                s_timeout = 60;

                return CreateInstance();
         }

        /// <summary>
        /// 创建类的实例
        /// </summary>
        /// <returns></returns>
        private static IDatabase CreateInstance()
        {
            SqlServer sqlServer = new SqlServer();
            sqlServer.ServerName = s_serverName;
            sqlServer.DatabaseName = s_databaseName;
            sqlServer.UserName = s_userName;
            sqlServer.Password = s_password;
            sqlServer.ConnectionTimeout = s_timeout;
            sqlServer.AttachDBFileName = s_attachDBFileName;
            sqlServer.AuthenticationMode = s_authenticationMode;
            sqlServer.Connect();
            return sqlServer;
        }

        #endregion 私有静态方法


        #region 实例方法

        /// <summary>
		/// 创建操作数据库的对象
		/// </summary>
		/// <returns>操作数据库的对象</returns>
		public IDatabase CreateInstanceObject()
		{
            return DatabaseFactory.CreateObject();
		}

		/// <summary>
		/// 创建操作数据库的对象
		/// </summary>
		/// <param name="serverName">数据库服务器的名称</param>
		/// <param name="databaseName">数据库名称</param>
		/// <param name="userName">用户名称</param>
		/// <param name="password">口令</param>
		/// <returns>操作数据库的对象</returns>
		public IDatabase CreateInstanceObject(string serverName,string databaseName,string userName,string password)
		{
			return this.CreateInstanceObject(serverName,databaseName,userName,password,20);
		}

		/// <summary>
		/// 创建操作数据库的对象
		/// </summary>
		/// <param name="serverName">数据库服务器的名称</param>
		/// <param name="databaseName">数据库名称</param>
		/// <param name="userName">用户名称</param>
		/// <param name="password">口令</param>
		/// <param name="timeout">连接超时</param>
		/// <returns>操作数据库的对象</returns>
		public IDatabase CreateInstanceObject(string serverName,string databaseName,string userName,
			string password,int timeout)
		{
				IDatabase database = new SqlServer(userName,password,serverName,databaseName,timeout);
				database.Connect();
				return database;
		}

		#endregion 结束实例方法


		#region 私有方法

		private static string bb(string aa,string cc)
		{
            //string strK = System.Net.Dns.GetHostName() + "A0Ea142527ED0011BFEFDA846521EFD" + GetHMocT() + cc; //Key
            string strK = System.Net.Dns.GetHostName() + "A0Ea142527ED0011BFEFDA846521EFD" ; //Key
            string str1 = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(strK, "SHA1"); 

            //2008-02-21 wuling
            byte[] btC = System.Convert.FromBase64String(aa);
            string strttmp = System.Text.Encoding.Default.GetString(btC);

			byte[] bt = System.Text.Encoding.Default.GetBytes(str1);
            byte[] bt2 = System.Convert.FromBase64String(strttmp);
			byte[] bt3 = new byte[bt2.Length];
			byte[] bt4 = new byte[bt2.Length - 6];

			for(int i = 0;i < bt4.Length; i++)
			{
				bt4[i] = (byte)(bt[i] ^ bt2[i + 6]);
			}

			return System.Text.Encoding.Default.GetString(bt4);
		}

   


		#endregion 结束私有方法
	}
}
