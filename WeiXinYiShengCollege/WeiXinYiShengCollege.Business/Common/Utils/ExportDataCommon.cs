using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Module.Utils
{
   public class ExportDataCommon
    {
        public static void ExportWord(GridView gv, string wordName)
        {
            string ExportFileName = wordName;
            ExportFileName += ".doc";
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.BufferOutput = true;
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;//.GetEncoding("GB2312");
            HttpContext.Current.Response.Charset = "UTF-8";
            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpContext.Current.Server.UrlEncode(ExportFileName));
            HttpContext.Current.Response.ContentType = "application/ms-word";
            gv.Page.EnableViewState = false;

            System.IO.StringWriter stringWriter = new System.IO.StringWriter();
            HtmlTextWriter textWriter = new HtmlTextWriter(stringWriter);
            Literal header = new Literal();
            // header.Text = "<h2>文章标题</h2>";
            // Header.Controls.Add(header);
            // Header.RenderControl(textWriter);
            gv.RenderControl(textWriter);
            HttpContext.Current.Response.Write(stringWriter.ToString());
            HttpContext.Current.Response.Write("<html xmlns:v='urn:schemas-microsoft-com:vml' xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:word' xmlns:m='http://schemas.microsoft.com/office/2004/12/omml' xmlns='http://www.w3.org/TR/REC-html40'><head></head><body lang=ZH-CN>" + stringWriter.ToString());
            HttpContext.Current.Response.End();
            HttpContext.Current.Response.Flush();

        }

        /// <summary> 
        /// dtData是要导出为Excel的DataTable,FileName是要导出的Excel文件名(不加.xls) 
        /// </summary> 
        /// <param name="dtData"></param> 
        /// <param name="FileName"></param> 
        public static void ExportToExcel(System.Data.DataTable dtData, String FileName)
        {
            System.Web.UI.WebControls.GridView dgExport = null;
            //当前对话 
            System.Web.HttpContext curContext = System.Web.HttpContext.Current;
            //IO用于导出并返回excel文件 
            System.IO.StringWriter strWriter = null;
            System.Web.UI.HtmlTextWriter htmlWriter = null;

            if (dtData != null)
            {
                //设置编码和附件格式 
                //System.Web.HttpUtility.UrlEncode(FileName, System.Text.Encoding.UTF8)作用是方式中文文件名乱码 
                curContext.Response.AddHeader("content-disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode(FileName, System.Text.Encoding.UTF8) + ".xls");
                curContext.Response.ContentType = "application nd.ms-excel";
                //curContext.Response.ContentEncoding = System.Text.Encoding.UTF8;
                //curContext.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
                //curContext.Response.Charset = "GB2312";
                HttpContext.Current.Response.Charset = "UTF-8";
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;

                //导出Excel文件 
                strWriter = new System.IO.StringWriter();
                htmlWriter = new System.Web.UI.HtmlTextWriter(strWriter);

                //为了解决dgData中可能进行了分页的情况,需要重新定义一个无分页的GridView 
                dgExport = new System.Web.UI.WebControls.GridView();
                dgExport.DataSource = dtData.DefaultView;
                dgExport.AllowPaging = false;
                dgExport.RowDataBound += new GridViewRowEventHandler(dgExport_RowDataBound);
                dgExport.DataBind();
                //下载到客户端 
                dgExport.RenderControl(htmlWriter);
                curContext.Response.Write(strWriter.ToString());
                curContext.Response.End();
            }
        }

        /// <summary>
        /// dtData是要导出为Excel的DataTable,FileName是要导出的Excel文件名(不加.xls)
        /// </summary>
        /// <param name="dtData"></param>
        /// <param name="gatherInfo"></param>
        /// <param name="FileName"></param>
        public static void ExportToExcel(System.Data.DataTable dtData, string gatherInfo, String FileName)
        {
            System.Web.UI.WebControls.GridView dgExport = null;
            //当前对话 
            System.Web.HttpContext curContext = System.Web.HttpContext.Current;
            //IO用于导出并返回excel文件 
            System.IO.StringWriter strWriter = null;
            System.Web.UI.HtmlTextWriter htmlWriter = null;

            if (dtData != null)
            {
                //设置编码和附件格式 
                //System.Web.HttpUtility.UrlEncode(FileName, System.Text.Encoding.UTF8)作用是方式中文文件名乱码 
                curContext.Response.AddHeader("content-disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode(FileName, System.Text.Encoding.UTF8) + ".xls");
                curContext.Response.ContentType = "application nd.ms-excel";
                curContext.Response.ContentEncoding = System.Text.Encoding.UTF8;
                curContext.Response.Charset = "GB2312";

                //导出Excel文件 
                strWriter = new System.IO.StringWriter();
                htmlWriter = new System.Web.UI.HtmlTextWriter(strWriter);

                //为了解决dgData中可能进行了分页的情况,需要重新定义一个无分页的GridView 
                dgExport = new System.Web.UI.WebControls.GridView();
                dgExport.DataSource = dtData.DefaultView;
                dgExport.AllowPaging = false;
                dgExport.RowDataBound += new GridViewRowEventHandler(dgExport_RowDataBound);
                dgExport.DataBind();
                //下载到客户端 
                dgExport.RenderControl(htmlWriter);
                curContext.Response.Write(strWriter.ToString() + gatherInfo);
                curContext.Response.End();
            }
        }

        public static void ExportWord(Page page, GridView gv, string wordName)
        {
            string ExportFileName = wordName;
            ExportFileName += ".doc";
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.BufferOutput = true;
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;//.GetEncoding("GB2312");
            HttpContext.Current.Response.Charset = "UTF-8";
            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpContext.Current.Server.UrlEncode(ExportFileName));
            HttpContext.Current.Response.ContentType = "application/ms-word";
            page.EnableViewState = false;

            System.IO.StringWriter stringWriter = new System.IO.StringWriter();
            HtmlTextWriter textWriter = new HtmlTextWriter(stringWriter);
            Literal header = new Literal();
            // header.Text = "<h2>文章标题</h2>";
            // Header.Controls.Add(header);
            // Header.RenderControl(textWriter);
            gv.RenderControl(textWriter);
            HttpContext.Current.Response.Write(stringWriter.ToString());
            HttpContext.Current.Response.Write("<html xmlns:v='urn:schemas-microsoft-com:vml' xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:word' xmlns:m='http://schemas.microsoft.com/office/2004/12/omml' xmlns='http://www.w3.org/TR/REC-html40'><head></head><body lang=ZH-CN>" + stringWriter.ToString());
            HttpContext.Current.Response.End();
            HttpContext.Current.Response.Flush();

        }

        /// <summary>
        /// 在DataTable导出EXCEL后发现有些格式显示有问题，比如身份证号码等大于11位的数字显示为科学计数法、13681-1等 带中划线的两段数字显示为日期格式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected static void dgExport_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                foreach (TableCell cell in e.Row.Cells)
                {
                    if (Regex.IsMatch(cell.Text.Trim(), @"^\d{12,}$") || Regex.IsMatch(cell.Text.Trim(), @"^\d+[-]\d+$"))
                    {
                        cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@");
                    }
                }
            }
        }


        public static void ExportToExcel(GridView gv, string fileName)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Charset = "GB2312";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");//System.Text.Encoding.UTF8;//注意编码
            HttpContext.Current.Response.AppendHeader("Content-Disposition",
                "attachment;filename=" + HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8).ToString());

            HttpContext.Current.Response.AddHeader(
                "content-disposition", string.Format("attachment; filename={0}", fileName));
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    Table table = new Table();
                    table.GridLines = GridLines.Both;
                    if (gv.HeaderRow != null)
                    {
                        PrepareControlForExport(gv.HeaderRow);
                        table.Rows.Add(gv.HeaderRow);
                    }
                    foreach (GridViewRow row in gv.Rows)
                    {
                        PrepareControlForExport(row);
                        table.Rows.Add(row);
                    }

                    if (gv.FooterRow != null)
                    {
                        PrepareControlForExport(gv.FooterRow);
                        table.Rows.Add(gv.FooterRow);
                    }
                    foreach (TableRow tr in table.Rows)
                    {
                        foreach (TableCell cell in tr.Cells)
                        {
                            if (Regex.IsMatch(cell.Text.Trim(), @"^\d{12,}$") || Regex.IsMatch(cell.Text.Trim(), @"^\d+[-]\d+$"))
                            {
                                cell.Attributes.Add("style", "vnd.ms-excel.numberformat:@");
                            }
                        }
                    }

                    table.RenderControl(htw);
                    HttpContext.Current.Response.Write(sw.ToString());
                    HttpContext.Current.Response.Flush();
                    HttpContext.Current.Response.End();
                }
            }
        }

        private static void PrepareControlForExport(Control control)
        {
            for (int i = 0; i < control.Controls.Count; i++)
            {
                Control current = control.Controls[i];
                if (current is LinkButton)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as LinkButton).Text));
                }
                else if (current is ImageButton)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as ImageButton).AlternateText));
                }
                else if (current is HyperLink)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as HyperLink).Text));
                }
                else if (current is DropDownList)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as DropDownList).SelectedItem.Text));
                }
                else if (current is CheckBox)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as CheckBox).Checked ? "True" : "False"));
                }
                if (current.HasControls())
                {
                    PrepareControlForExport(current);
                }
            }
        }
    }
}
