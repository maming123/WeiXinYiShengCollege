/*----------------------------------------------------------------
// Copyright (C) 2006 
// ��Ȩ���С� 
//
// �ļ�����IDatabase.cs
// �ļ������������������ݿ�Ľӿ�
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
using System.Collections;

namespace SNS.Library.Database
{
	/// <summary>
	/// �������ݿ�Ľӿ�
	/// </summary>
	public interface IDatabase : System.IDisposable
	{
		#region ����

		/// <summary>
		/// ��ȡ�����õ�¼���ݿ���û�����
		/// </summary>
		string UserName
		{
			get;
			set;
		}

		/// <summary>
		/// ��ȡ�����õ�¼���ݿ���û��Ŀ���
		/// </summary>
		string Password
		{
			get;
			set;
		}

		/// <summary>
		/// ��ȡ���������ݿ������������
		/// </summary>
		string ServerName
		{
			get;
			set;
		}

		/// <summary>
		/// ��ȡ���������ݿ������
		/// </summary>
		string DatabaseName
		{
			get;
			set;
		}

		/// <summary>
		/// ��ȡ�������ڳ��Խ�������ʱ��ֹ���Բ����ɴ���֮ǰ���ȴ���ʱ��
		/// </summary>
		int ConnectionTimeout
		{
			get;
			set;
		}

		/// <summary>
		/// ��ȡ��������
		/// </summary>
		ArrayList Parameters
		{
			get;
		}

		/// <summary>
		/// ��ȡ�����ݿ������״̬
		/// </summary>
		ConnectionState ConnectionState
		{
			get;
		}

		/// <summary>
		/// ��ȡ������ִ��SQL����ĳ�ʱ����
		/// </summary>
		int CommandTimeout
		{
			get;
			set;
		}

        /// <summary>
        /// ��ȡ���������ݿ����������֤ģʽ
        /// </summary>
        AuthenticationMode AuthenticationMode
        {
            get;
            set;
        }

        /// <summary>
        /// ��ȡ�����ø��������ļ�����
        /// </summary>
        string AttachDBFileName
        {
            get;
            set;
        }

		#endregion ��������


		#region ����

		/// <summary>
		/// �������ݿ�
		/// </summary>
		void Connect();

		/// <summary>
		/// �ر������ݿ������
		/// </summary>
		void Close();

		/// <summary>
		/// ������ִ��Transact-SQL��䲢������Ӱ�������
		/// </summary>
		/// <param name="sql">Ҫִ�е�Sql���</param>
		/// <returns>��Ӱ�������</returns>
		int ExecuteNonQuery(string sql);

		/// <summary>
		/// ������ִ��Transact-SQL��䲢���ز�ѯ���
		/// </summary>
		/// <param name="sql">��ѯSQL���</param>
		/// <param name="result">�����ѯ����Ľ����</param>
		/// <returns>���������ļ�¼��</returns>
		int ExecuteQuery(string sql,DataTable result);

		/// <summary>
		/// ������ִ��Transact-SQL��䲢���ز�ѯ���
		/// </summary>
		/// <param name="sql">��ѯSQL���</param>
		/// <returns>�����ѯ�����DataTable</returns>
		DataTable ExecuteQuery(string sql);

		/// <summary>
		/// ��ʼ���ݿ�����
		/// </summary>
		void BeginTransaction();

		/// <summary>
		/// �ύ���ݿ�����
		/// </summary>
		void Commit();

		/// <summary>
		/// �ӹ���״̬�ع�����
		/// </summary>
		void Rollback();

		/// <summary>
		/// ִ��û�з��ؽ���Ĵ洢����
		/// </summary>
		/// <param name="storeProcedureName">�洢���̵�����</param>
		/// <returns>��Ӱ�������</returns>
		int ExecuteNonQueryStoreProcedure(string storeProcedureName);

		/// <summary>
		/// ִ�д����ؽ���Ĵ洢����
		/// </summary>
		/// <param name="storeProcedureName">�洢���̵�����</param>
		/// <returns>��ѯ���</returns>
		DataTable ExecuteQueryStoreProcedure(string storeProcedureName);

		/// <summary>
		/// ִ�в�����䲢�ҷ��ص�ǰ����ֵ
		/// </summary>
		/// <param name="insertSql">�������</param>
		/// <param name="tableName">���ݱ�����</param>
		/// <returns>����ֵ</returns>
		int ExecuteInsertReturnPK(string insertSql,string tableName);

		/// <summary>
		/// �õ�ָ���������ֵ
		/// </summary>
		/// <param name="tableName">������</param>
		/// <returns>����ֵ</returns>
		int GetSequence(string tableName);

		/// <summary>
		/// ִ�в�ѯSQL��䲢����DataReader
		/// </summary>
		/// <param name="sql">��ѯSQL���</param>
		/// <returns>DataReader</returns>
		IDataReader ExecuteDataReader(string sql);

		/// <summary>
		/// ִ�в�ѯ�������ز�ѯ�����صĽ�����е�һ�еĵ�һ�С����Զ�����л���
		/// </summary>
		/// <param name="sql">��ѯSQL���</param>
		/// <returns>������е�һ�еĵ�һ�л�����ã���������Ϊ�գ�</returns>
        object ExecuteScalar(string sql);

        /// <summary>
        /// ����Image���͵��ֶ�
        /// </summary>
        /// <param name="tableName">���ݱ�����</param>
        /// <param name="imageFieldName">Image���͵��ֶ�����</param>
        /// <param name="keyFieldName">�ؼ��ֶ�����</param>
        /// <param name="keyFieldValue">�ؼ��ֶε�ȡֵ</param>
        /// <param name="imageContent">ͼƬ���ֽ�����</param>
        /// <returns>ִ��SQL��������ݿ��Ӱ��������1��������0����������</returns>
        int UpdateImageFiled(string tableName, string imageFieldName, string[] keyFieldName,
            string[] keyFieldValue, byte[] imageContent);

		#endregion ��������
	}
}
