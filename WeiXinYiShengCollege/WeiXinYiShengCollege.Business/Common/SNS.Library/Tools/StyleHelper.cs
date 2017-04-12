using System;
using System.Data;
using System.Web;
using SNS.Library.Database;

                                    /****************************
                                    * 类名称：StyleHelper
                                    *   功能：自定义风格管理类
                                    *     by：Lining
                                    *   日期：2005-12-30
                                    ****************************/

namespace SNS.Library.Tools
{
	/// <summary>
    /// 自定义风格管理类。
	/// </summary>
	public class StyleHelper
    {
        #region 静态公开方法

        /// <summary>
		/// 得到风格路径的字符串
		/// </summary>
		/// <param name="filePath">文件的路径</param>
		/// <param name="myStyleFolder">样式文件的文件夹</param>
		/// <returns>风格路径的字符串</returns>
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
        /// 得到风格路径的字符串
        /// </summary>
        /// <param name="filePath">文件的路径</param>
        /// <returns>风格路径的字符串</returns>
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

        #endregion 结束静态公开方法
    }
}
