/*----------------------------------------------------------------
// Copyright (C) 2006 
// 版权所有。 
//
// 文件名：IDatabase.cs
// 文件功能描述：操作数据库的接口
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
using System.Collections;

namespace SNS.Library.Database
{
	/// <summary>
	/// 操作数据库的接口
	/// </summary>
	public interface IDatabase : System.IDisposable
	{
		#region 属性

		/// <summary>
		/// 获取或设置登录数据库的用户名称
		/// </summary>
		string UserName
		{
			get;
			set;
		}

		/// <summary>
		/// 获取或设置登录数据库的用户的口令
		/// </summary>
		string Password
		{
			get;
			set;
		}

		/// <summary>
		/// 获取或设置数据库服务器的名称
		/// </summary>
		string ServerName
		{
			get;
			set;
		}

		/// <summary>
		/// 获取或设置数据库的名称
		/// </summary>
		string DatabaseName
		{
			get;
			set;
		}

		/// <summary>
		/// 获取或设置在尝试建立连接时终止尝试并生成错误之前所等待的时间
		/// </summary>
		int ConnectionTimeout
		{
			get;
			set;
		}

		/// <summary>
		/// 获取参数集合
		/// </summary>
		ArrayList Parameters
		{
			get;
		}

		/// <summary>
		/// 获取与数据库的连接状态
		/// </summary>
		ConnectionState ConnectionState
		{
			get;
		}

		/// <summary>
		/// 获取或设置执行SQL命令的超时设置
		/// </summary>
		int CommandTimeout
		{
			get;
			set;
		}

        /// <summary>
        /// 获取或设置数据库服务器的验证模式
        /// </summary>
        AuthenticationMode AuthenticationMode
        {
            get;
            set;
        }

        /// <summary>
        /// 获取或设置附加数据文件名称
        /// </summary>
        string AttachDBFileName
        {
            get;
            set;
        }

		#endregion 结束属性


		#region 方法

		/// <summary>
		/// 连接数据库
		/// </summary>
		void Connect();

		/// <summary>
		/// 关闭与数据库的连接
		/// </summary>
		void Close();

		/// <summary>
		/// 对连接执行Transact-SQL语句并返回受影响的行数
		/// </summary>
		/// <param name="sql">要执行的Sql语句</param>
		/// <returns>受影响的行数</returns>
		int ExecuteNonQuery(string sql);

		/// <summary>
		/// 对连接执行Transact-SQL语句并返回查询结果
		/// </summary>
		/// <param name="sql">查询SQL语句</param>
		/// <param name="result">保存查询结果的结果集</param>
		/// <returns>符合条件的记录数</returns>
		int ExecuteQuery(string sql,DataTable result);

		/// <summary>
		/// 对连接执行Transact-SQL语句并返回查询结果
		/// </summary>
		/// <param name="sql">查询SQL语句</param>
		/// <returns>保存查询结果的DataTable</returns>
		DataTable ExecuteQuery(string sql);

		/// <summary>
		/// 开始数据库事务
		/// </summary>
		void BeginTransaction();

		/// <summary>
		/// 提交数据库事务
		/// </summary>
		void Commit();

		/// <summary>
		/// 从挂起状态回滚事务
		/// </summary>
		void Rollback();

		/// <summary>
		/// 执行没有返回结果的存储过程
		/// </summary>
		/// <param name="storeProcedureName">存储过程的名称</param>
		/// <returns>受影响的行数</returns>
		int ExecuteNonQueryStoreProcedure(string storeProcedureName);

		/// <summary>
		/// 执行带返回结果的存储过程
		/// </summary>
		/// <param name="storeProcedureName">存储过程的名称</param>
		/// <returns>查询结果</returns>
		DataTable ExecuteQueryStoreProcedure(string storeProcedureName);

		/// <summary>
		/// 执行插入语句并且返回当前主键值
		/// </summary>
		/// <param name="insertSql">插入语句</param>
		/// <param name="tableName">数据表名称</param>
		/// <returns>主键值</returns>
		int ExecuteInsertReturnPK(string insertSql,string tableName);

		/// <summary>
		/// 得到指定表的主键值
		/// </summary>
		/// <param name="tableName">表名称</param>
		/// <returns>主键值</returns>
		int GetSequence(string tableName);

		/// <summary>
		/// 执行查询SQL语句并返回DataReader
		/// </summary>
		/// <param name="sql">查询SQL语句</param>
		/// <returns>DataReader</returns>
		IDataReader ExecuteDataReader(string sql);

		/// <summary>
		/// 执行查询，并返回查询所返回的结果集中第一行的第一列。忽略额外的列或行
		/// </summary>
		/// <param name="sql">查询SQL语句</param>
		/// <returns>结果集中第一行的第一列或空引用（如果结果集为空）</returns>
        object ExecuteScalar(string sql);

        /// <summary>
        /// 更新Image类型的字段
        /// </summary>
        /// <param name="tableName">数据表名称</param>
        /// <param name="imageFieldName">Image类型的字段名称</param>
        /// <param name="keyFieldName">关键字段名称</param>
        /// <param name="keyFieldValue">关键字段的取值</param>
        /// <param name="imageContent">图片的字节数组</param>
        /// <returns>执行SQL语句后对数据库的影响行数（1－正常；0－不正常）</returns>
        int UpdateImageFiled(string tableName, string imageFieldName, string[] keyFieldName,
            string[] keyFieldValue, byte[] imageContent);

		#endregion 结束方法
	}
}
