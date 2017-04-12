using System;
using System.Web;
using System.Web.UI;
using System.IO;

								/****************************
								* 类名称：ExportPage
								*   功能：导出Web页面
								*     by：Lining
								*   日期：2005-1-7
								****************************/


namespace SNS.Library.Tools
{
	/// <summary>
	/// 导出Web页面。
	/// </summary>
	public class ExportPage
	{
		#region 公开方法

		/// <summary>
		/// 导出页面内容到Excel中
		/// </summary>
		/// <param name="page">Page对象</param>
		/// <param name="includeStyle">是否包含页面样式</param>
		public static void ExportToExcel(System.Web.UI.Page page,bool includeStyle)
		{
            page.EnableViewState = false;

            System.Web.HttpResponse response = page.Response;
            response.Clear();
            response.Buffer = true;
            response.Charset = "gb2312";
            response.AddHeader("Content-Disposition", "inline;filename=" + HttpUtility.UrlEncode("excel") + ".xls");	//设置输出文件类型为excel文件。		
            response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");
            response.ContentType = "application/ms-excel";
            System.Globalization.CultureInfo myCItrad = new System.Globalization.CultureInfo("ZH-CN", true);
            StringWriter strWriter = new StringWriter();
            HtmlTextWriter htmlWriter = new HtmlTextWriter(strWriter);
            page.RenderControl(htmlWriter);

            //得到样式表文件在HTML中的位置
            string strValue = strWriter.ToString();
            int iLinkStart = strValue.ToLower().IndexOf("<link");

            if (iLinkStart > -1)
            {
                int iLinkEnd = strValue.IndexOf(">", iLinkStart, 100);

                //去掉样式表文件的<link ...>行
                strValue = strValue.Remove(iLinkStart, iLinkEnd - iLinkStart + 1);

                if (includeStyle)
                {
                    //将样式表文件的内容添加到页面中
                    //先得到样式表文件的名称
                    int iLinkHrefEnd = strValue.ToLower().IndexOf(".css");
                    string strCssFileName = GetCssFileName(strValue.Substring(0, iLinkHrefEnd));

                    //将样式表文件的内容添加到原来样式表文件的位置
                    if (strCssFileName != "")
                    {
                        string strCssFilePath = page.Server.MapPath(strCssFileName);

                        StreamReader reader = new StreamReader(strCssFilePath, System.Text.Encoding.Default);
                        string strCssContent = reader.ReadToEnd();
                        reader.Close();

                        strCssContent = " <style>" + strCssContent + "</style> ";
                        strValue = strValue.Insert(iLinkStart, strCssContent);
                    }
                }
            }

            strValue = RemoveTagFromFileContent(strValue, "<img", ">");
            strValue = RemoveTagFromFileContent(strValue, "<a", ">");
            strValue = RemoveTagFromFileContent(strValue, "</a", ">");
            HttpContext.Current.Response.Write(strValue);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
		}

		#endregion 结束公开方法


		#region 私有方法

		/// <summary>
		/// 得到样式表文件的文件名称
		/// </summary>
		/// <param name="html">HTML代码</param>
		/// <returns>样式表文件的名称</returns>
		private static string GetCssFileName(string html)
		{
			string strCssFileName = "";
			for(int i = 0; i < html.Length; i++)
			{
				string strChar = html.Substring(html.Length - i - 1,1);
				if(strChar != "\"")
				{
					strCssFileName = strChar + strCssFileName;
				}
				else
				{
					if(strCssFileName == "")
					{
						return "";
					}
					else
					{
						return strCssFileName + ".css";
					}
				}
			}
			if(strCssFileName == "")
			{
				return "";
			}
			else
			{
				return strCssFileName + ".css";
			}
		}

        /// <summary>
        /// 从文件内容中删除指定的标签
        /// </summary>
        /// <param name="fileContent">文件内容</param>
        /// <param name="beginTag">开始标签</param>
        /// <param name="endTag">结束标签</param>
        /// <returns>删除指定标签后的文件内容</returns>
        private static string RemoveTagFromFileContent(string fileContent, string beginTag, string endTag)
        {
            int iImageIndex = fileContent.IndexOf(beginTag);
            if (iImageIndex > 0)
            {
                int iCount = 0;
                for (int i = iImageIndex; i < fileContent.Length; i++)
                {
                    if (fileContent.Substring(i, 1) != endTag)
                    {
                        iCount++;
                    }
                    else
                    {
                        break;
                    }
                }
                fileContent = fileContent.Remove(iImageIndex, iCount + 1);
                return RemoveTagFromFileContent(fileContent, beginTag, endTag);
            }
            else
            {
                return fileContent;
            }
        }

		#endregion 结束私有方法
	}
}
