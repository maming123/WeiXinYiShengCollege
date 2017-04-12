using System;
using System.Data;
using SNS.Library.Database;

/*********************************
* 类名称：DBImageManager
*   功能：数据库图片管理类
*     by：Lining
*   日期：2006-3-31
 *  备注：管理数据库中存放的图片
*********************************/

namespace SNS.Library.Tools
{
    public class DBImageManager
    {
        #region 静态公开方法

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
            return DatabaseFactory.UpdateImageFiled(tableName, imageFieldName, keyFieldName, keyFieldValue, imageContent);
        }

        /// <summary>
        /// 得到Image字段的字节数组
        /// </summary>
        /// <param name="tableName">数据表名称</param>
        /// <param name="imageFieldName">Image类型的字段名称</param>
        /// <param name="keyFieldName">关键字段名称</param>
        /// <param name="keyFieldValue">关键字段的取值</param>
        /// <returns>图片的字节数组</returns>
        public static byte[] FindImageContent(string tableName, string imageFieldName, string[] keyFieldName,
            string[] keyFieldValue)
        {
            if (keyFieldName == null || keyFieldValue == null)
            {
                throw new Exception("关键字段的名称或取值不能为null！");
            }
            if (keyFieldName.Length != keyFieldValue.Length)
            {
                throw new Exception("关键字段的名称和取值的数量不符！");
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

        #endregion 结束静态公开方法
    }
}
