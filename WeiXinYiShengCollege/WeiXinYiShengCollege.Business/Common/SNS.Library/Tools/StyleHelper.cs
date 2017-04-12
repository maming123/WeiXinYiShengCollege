using System;
using System.Data;
using System.Web;
using SNS.Library.Database;

                                    /****************************
                                    * �����ƣ�StyleHelper
                                    *   ���ܣ��Զ����������
                                    *     by��Lining
                                    *   ���ڣ�2005-12-30
                                    ****************************/

namespace SNS.Library.Tools
{
	/// <summary>
    /// �Զ���������ࡣ
	/// </summary>
	public class StyleHelper
    {
        #region ��̬��������

        /// <summary>
		/// �õ����·�����ַ���
		/// </summary>
		/// <param name="filePath">�ļ���·��</param>
		/// <param name="myStyleFolder">��ʽ�ļ����ļ���</param>
		/// <returns>���·�����ַ���</returns>
		public static string GetStyle(string filePath,string myStyleFolder)
		{
			int iIndex = filePath.ToUpper().IndexOf("IMAGES");
			if(iIndex > -1)
			{
                return filePath.ToLower().Replace("images/", "style/" + myStyleFolder + "/images/");
			}
			else
			{
				iIndex = filePath.ToUpper().IndexOf("CSS");
				if(iIndex > -1)
				{
                    return filePath.ToLower().Replace("css/", "style/" + myStyleFolder + "/css/");
				}
				else
				{
					return filePath;
				}
			}
        }

        /// <summary>
        /// �õ����·�����ַ���
        /// </summary>
        /// <param name="filePath">�ļ���·��</param>
        /// <returns>���·�����ַ���</returns>
        public static string GetStyle(string filePath)
        {
            string strPath = HttpContext.Current.Request.ApplicationPath;
            if (strPath.EndsWith("/"))
            {
                strPath = strPath.Substring(0, strPath.Length - 1);
            }

            int iIndex = filePath.ToUpper().IndexOf("CSS");
            if (iIndex > -1)
            {
                strPath += "/css/" +
                        filePath;

            }
            else
            {
                strPath += "/images/" +
                    filePath;
            }
            return strPath;
        }

        #endregion ������̬��������
    }
}
