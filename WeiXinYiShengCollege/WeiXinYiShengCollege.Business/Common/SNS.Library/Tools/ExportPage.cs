using System;
using System.Web;
using System.Web.UI;
using System.IO;

								/****************************
								* �����ƣ�ExportPage
								*   ���ܣ�����Webҳ��
								*     by��Lining
								*   ���ڣ�2005-1-7
								****************************/


namespace SNS.Library.Tools
{
	/// <summary>
	/// ����Webҳ�档
	/// </summary>
	public class ExportPage
	{
		#region ��������

		/// <summary>
		/// ����ҳ�����ݵ�Excel��
		/// </summary>
		/// <param name="page">Page����</param>
		/// <param name="includeStyle">�Ƿ����ҳ����ʽ</param>
		public static void ExportToExcel(System.Web.UI.Page page,bool includeStyle)
		{
            page.EnableViewState = false;

            System.Web.HttpResponse response = page.Response;
            response.Clear();
            response.Buffer = true;
            response.Charset = "gb2312";
            response.AddHeader("Content-Disposition", "inline;filename=" + HttpUtility.UrlEncode("excel") + ".xls");	//��������ļ�����Ϊexcel�ļ���		
            response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");
            response.ContentType = "application/ms-excel";
            System.Globalization.CultureInfo myCItrad = new System.Globalization.CultureInfo("ZH-CN", true);
            StringWriter strWriter = new StringWriter();
            HtmlTextWriter htmlWriter = new HtmlTextWriter(strWriter);
            page.RenderControl(htmlWriter);

            //�õ���ʽ���ļ���HTML�е�λ��
            string strValue = strWriter.ToString();
            int iLinkStart = strValue.ToLower().IndexOf("<link");

            if (iLinkStart > -1)
            {
                int iLinkEnd = strValue.IndexOf(">", iLinkStart, 100);

                //ȥ����ʽ���ļ���<link ...>��
                strValue = strValue.Remove(iLinkStart, iLinkEnd - iLinkStart + 1);

                if (includeStyle)
                {
                    //����ʽ���ļ���������ӵ�ҳ����
                    //�ȵõ���ʽ���ļ�������
                    int iLinkHrefEnd = strValue.ToLower().IndexOf(".css");
                    string strCssFileName = GetCssFileName(strValue.Substring(0, iLinkHrefEnd));

                    //����ʽ���ļ���������ӵ�ԭ����ʽ���ļ���λ��
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

		#endregion ������������


		#region ˽�з���

		/// <summary>
		/// �õ���ʽ���ļ����ļ�����
		/// </summary>
		/// <param name="html">HTML����</param>
		/// <returns>��ʽ���ļ�������</returns>
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
        /// ���ļ�������ɾ��ָ���ı�ǩ
        /// </summary>
        /// <param name="fileContent">�ļ�����</param>
        /// <param name="beginTag">��ʼ��ǩ</param>
        /// <param name="endTag">������ǩ</param>
        /// <returns>ɾ��ָ����ǩ����ļ�����</returns>
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

		#endregion ����˽�з���
	}
}
