using System;
using System.Data;
using SNS.Library.Database;

/*********************************
* �����ƣ�DBImageManager
*   ���ܣ����ݿ�ͼƬ������
*     by��Lining
*   ���ڣ�2006-3-31
 *  ��ע���������ݿ��д�ŵ�ͼƬ
*********************************/

namespace SNS.Library.Tools
{
    public class DBImageManager
    {
        #region ��̬��������

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
            return DatabaseFactory.UpdateImageFiled(tableName, imageFieldName, keyFieldName, keyFieldValue, imageContent);
        }

        /// <summary>
        /// �õ�Image�ֶε��ֽ�����
        /// </summary>
        /// <param name="tableName">���ݱ�����</param>
        /// <param name="imageFieldName">Image���͵��ֶ�����</param>
        /// <param name="keyFieldName">�ؼ��ֶ�����</param>
        /// <param name="keyFieldValue">�ؼ��ֶε�ȡֵ</param>
        /// <returns>ͼƬ���ֽ�����</returns>
        public static byte[] FindImageContent(string tableName, string imageFieldName, string[] keyFieldName,
            string[] keyFieldValue)
        {
            if (keyFieldName == null || keyFieldValue == null)
            {
                throw new Exception("�ؼ��ֶε����ƻ�ȡֵ����Ϊnull��");
            }
            if (keyFieldName.Length != keyFieldValue.Length)
            {
                throw new Exception("�ؼ��ֶε����ƺ�ȡֵ������������");
            }

            string strSql = "SELECT " + imageFieldName + " FROM " + tableName + " WHERE";
            for (int i = 0; i < keyFieldName.Length; i++)
            {
                strSql += " " + keyFieldName[i] + "='" + keyFieldValue[i] + "' AND";
            }
            strSql = strSql.Substring(0, strSql.Length - 4);

            DataTable dtImage = DatabaseFactory.ExecuteQuery(strSql);
            if (dtImage.Rows.Count > 0)
            {
                return (byte[])dtImage.Rows[0][0];
            }
            else
            {
                return null;
            }
        }

        #endregion ������̬��������
    }
}
