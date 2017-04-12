/*----------------------------------------------------------------
// Copyright (C) 2006 
// ��Ȩ���С� 
//
// �ļ�����DatabaseFactory.cs
// �ļ����������������������ݿ�Ķ���Ĺ���
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
using System.Xml;
using System.Data;
using System.Management;
using System.IO;
using System.Configuration;

namespace SNS.Library.Database
{
	/// <summary>
	/// �ṩ����ά�������ݿ�Ķ���ķ��������ܼ̳д��ࡣ
	/// </summary>
    //[System.Serializable]
	public class DatabaseFactory
	{

		#region ��̬�ֶ� 

		/// <summary>
		/// �û�����
		/// </summary>
		private static string s_userName = "";

		/// <summary>
		/// ����
		/// </summary>
		private static string s_password = "";

		/// <summary>
		/// ����������
		/// </summary>
		private static string s_serverName = "";

		/// <summary>
		/// ���ݿ�����
		/// </summary>
		private static string s_databaseName = "";

		/// <summary>
		/// ��ʱʱ������
		/// </summary>
		private static int s_timeout = 0;

		///
		private static string s_AreaCode = "";

        /// <summary>
        /// ���ݿ����������֤ģʽ
        /// </summary>
        private static AuthenticationMode s_authenticationMode = AuthenticationMode.SQLServerAndWindows;

        /// <summary>
        /// ���������ļ�����
        /// </summary>
        private static string s_attachDBFileName = "";


        private static bool s_BBError = false;

		#endregion ������̬�ֶ�  


        #region ������̬����
        public static string GetMyCode()
        {
            return s_AreaCode;
        }


        /// <summary>
		/// �����������ݿ�Ķ���
		/// </summary>
		/// <returns>�������ݿ�Ķ���</returns>
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
		/// �����������ݿ�Ķ���
		/// </summary>
		/// <param name="serverName">���ݿ������������</param>
		/// <param name="databaseName">���ݿ�����</param>
		/// <param name="userName">�û�����</param>
		/// <param name="password">����</param>
		/// <returns>�������ݿ�Ķ���</returns>
		public static IDatabase CreateObject(string serverName,string databaseName,string userName,string password)
		{
			return DatabaseFactory.CreateObject(serverName,databaseName,userName,password,60);
		}

		/// <summary>
		/// �����������ݿ�Ķ���
		/// </summary>
		/// <param name="serverName">���ݿ������������</param>
		/// <param name="databaseName">���ݿ�����</param>
		/// <param name="userName">�û�����</param>
		/// <param name="password">����</param>
		/// <param name="timeout">���ӳ�ʱ</param>
		/// <returns>�������ݿ�Ķ���</returns>
		public static IDatabase CreateObject(string serverName,string databaseName,string userName,
			string password,int timeout)
		{
				IDatabase database = new SqlServer(userName,password,serverName,databaseName,timeout);
				database.Connect();
				return database;
		}

		/// <summary>
		/// ������ִ��Transact-SQL��䲢������Ӱ�������
		/// </summary>
		/// <param name="sql">Ҫִ�е�Sql���</param>
		/// <returns>��Ӱ�������</returns>
		public static int ExecuteNonQuery(string sql)
		{
			using(IDatabase database = CreateObject())
			{
				return database.ExecuteNonQuery(sql);
			}
		}

		/// <summary>
		/// ������ִ��Transact-SQL��䲢���ز�ѯ���
		/// </summary>
		/// <param name="sql">��ѯSQL���</param>
		/// <returns>�����ѯ����Ľ����</returns>
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
		/// �õ�ָ���������ֵ
		/// </summary>
		/// <param name="tableName">������</param>
		/// <returns>����ֵ</returns>
		public static int GetSequence(string tableName)
		{
			using(IDatabase database = CreateObject())
			{
				return database.GetSequence(tableName);
			}
		}

		/// <summary>
		/// ִ�в�����䲢�ҷ��ص�ǰ����ֵ
		/// </summary>
		/// <param name="insertSql">�������</param>
		/// <param name="tableName">���ݱ�����</param>
		/// <returns>����ֵ</returns>
		public static int ExecuteInsertReturnPK(string insertSql,string tableName)
		{
			using(IDatabase database = CreateObject())
			{
				return database.ExecuteInsertReturnPK(insertSql,tableName);
			}
		}

		/// <summary>
		/// ִ�в�ѯ�������ز�ѯ�����صĽ�����е�һ�еĵ�һ�С����Զ�����л���
		/// </summary>
		/// <param name="sql">��ѯSQL���</param>
		/// <returns>������е�һ�еĵ�һ�л�����ã���������Ϊ�գ�</returns>
		public static object ExecuteScalar(string sql)
		{
			using(IDatabase database = CreateObject())
			{
				return database.ExecuteScalar(sql);
			}
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
        public static int UpdateImageFiled(string tableName, string imageFieldName, string[] keyFieldName,
            string[] keyFieldValue, byte[] imageContent)
        {
            return CreateObject().UpdateImageFiled(tableName, imageFieldName, keyFieldName, keyFieldValue, imageContent);
        }

        #endregion ������̬����


        #region ˽�о�̬����

        /// <summary>
        /// �����������ݿ�Ķ���
        /// </summary>
        /// <param name="timeout">��ʱ����</param>
        /// <param name="isNew">�Ƿ����°汾�ķ������°汾�ķ����û��������ݿ����Ʋ����ܣ�</param>
        /// <returns>�������ݿ�Ķ���</returns>
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
        /// �������ʵ��
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

        #endregion ˽�о�̬����


        #region ʵ������

        /// <summary>
		/// �����������ݿ�Ķ���
		/// </summary>
		/// <returns>�������ݿ�Ķ���</returns>
		public IDatabase CreateInstanceObject()
		{
            return DatabaseFactory.CreateObject();
		}

		/// <summary>
		/// �����������ݿ�Ķ���
		/// </summary>
		/// <param name="serverName">���ݿ������������</param>
		/// <param name="databaseName">���ݿ�����</param>
		/// <param name="userName">�û�����</param>
		/// <param name="password">����</param>
		/// <returns>�������ݿ�Ķ���</returns>
		public IDatabase CreateInstanceObject(string serverName,string databaseName,string userName,string password)
		{
			return this.CreateInstanceObject(serverName,databaseName,userName,password,20);
		}

		/// <summary>
		/// �����������ݿ�Ķ���
		/// </summary>
		/// <param name="serverName">���ݿ������������</param>
		/// <param name="databaseName">���ݿ�����</param>
		/// <param name="userName">�û�����</param>
		/// <param name="password">����</param>
		/// <param name="timeout">���ӳ�ʱ</param>
		/// <returns>�������ݿ�Ķ���</returns>
		public IDatabase CreateInstanceObject(string serverName,string databaseName,string userName,
			string password,int timeout)
		{
				IDatabase database = new SqlServer(userName,password,serverName,databaseName,timeout);
				database.Connect();
				return database;
		}

		#endregion ����ʵ������


		#region ˽�з���

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

   


		#endregion ����˽�з���
	}
}
