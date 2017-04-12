using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace Module.Utils
{
    /// <summary>
    /// UnitCommon 的摘要说明
    /// </summary>
    public class UnitCommon
    {
        /// <summary>
        /// 合并行(普通列)
        /// </summary>
        /// <param name=“gv”>所对应的GridView对象</param>
        /// <param name=“columnIndex”>所对应要合并的列的索引，从0开始</param>
        public static void UnitCell(GridView gv, int columnIndex, int columnIndex费用小计, int columnIndex费用合计)
        {
            int i;
            string lastType;
            int lastCell;
            decimal lastCellValue费用小计 = 0;
            if (gv.Rows.Count > 0)
            {
                lastType = gv.Rows[0].Cells[columnIndex].Text;
                gv.Rows[0].Cells[columnIndex].RowSpan = 1;
                gv.Rows[0].Cells[columnIndex费用合计].RowSpan = 1;
                lastCell = 0;
                Label lblPriceSum = ((Label)gv.Rows[0].Cells[columnIndex费用小计].FindControl("lblPriceSum"));
                if (lblPriceSum != null)
                {
                    lastCellValue费用小计 = Convert.ToDecimal(lblPriceSum.Text);
                }
                else
                {
                    lastCellValue费用小计 = Convert.ToDecimal(gv.Rows[0].Cells[columnIndex费用小计].Text);
                }
                gv.Rows[lastCell].Cells[columnIndex费用合计].Text = string.Format("{0:N0}", lastCellValue费用小计);
                for (i = 1; i < gv.Rows.Count; i++)
                {
                    if (gv.Rows[i].Cells[columnIndex].Text == lastType)
                    {
                        gv.Rows[i].Cells[columnIndex].Visible = false;
                        gv.Rows[lastCell].Cells[columnIndex].RowSpan++;

                        gv.Rows[i].Cells[columnIndex费用合计].Visible = false;
                        gv.Rows[lastCell].Cells[columnIndex费用合计].RowSpan++;
                        Label lblPriceSum3 = ((Label)gv.Rows[i].Cells[columnIndex费用小计].FindControl("lblPriceSum"));
                        if (lblPriceSum3 != null)
                        {

                            lastCellValue费用小计 += Convert.ToDecimal(lblPriceSum3.Text);
                        }
                        else
                        {
                            lastCellValue费用小计 += Convert.ToDecimal(gv.Rows[i].Cells[columnIndex费用小计].Text);
                        }
                        //lastCellValue费用小计 += Convert.ToDecimal(gv.Rows[i].Cells[columnIndex费用小计].Text);
                        gv.Rows[lastCell].Cells[columnIndex费用合计].Text = string.Format("{0:N0}", lastCellValue费用小计);
                    }
                    else
                    {
                        lastType = gv.Rows[i].Cells[columnIndex].Text;
                        lastCell = i;
                        gv.Rows[i].Cells[columnIndex].RowSpan = 1;
                        gv.Rows[i].Cells[columnIndex费用合计].RowSpan = 1;
                        lastCellValue费用小计 = 0;
                        Label lblPriceSum2 = ((Label)gv.Rows[i].Cells[columnIndex费用小计].FindControl("lblPriceSum"));
                        if (lblPriceSum2 != null)
                        {

                            lastCellValue费用小计 = Convert.ToDecimal(lblPriceSum2.Text);
                        }
                        else
                        {
                            lastCellValue费用小计 = Convert.ToDecimal(gv.Rows[i].Cells[columnIndex费用小计].Text);
                        }
                        //lastCellValue费用小计 = Convert.ToDecimal(gv.Rows[i].Cells[columnIndex费用小计].Text);
                        gv.Rows[lastCell].Cells[columnIndex费用合计].Text = string.Format("{0:N0}", lastCellValue费用小计);

                    }


                }
            }
        }
        /// <summary>
        /// 合并行(普通列)
        /// </summary>
        /// <param name=“gv”>所对应的GridView对象</param>
        /// <param name=“columnIndex”>所对应要合并的列的索引，从0开始</param>
        public static void UnitCell(GridView gv, int columnIndex)
        {
            int i;
            string lastType;
            int lastCell;
            if (gv.Rows.Count > 0)
            {
                lastType = gv.Rows[0].Cells[columnIndex].Text;
                gv.Rows[0].Cells[columnIndex].RowSpan = 1;
                lastCell = 0;
                for (i = 1; i < gv.Rows.Count; i++)
                {
                    if (gv.Rows[i].Cells[columnIndex].Text == lastType)
                    {
                        gv.Rows[i].Cells[columnIndex].Visible = false;
                        gv.Rows[lastCell].Cells[columnIndex].RowSpan++;
                    }
                    else
                    {
                        lastType = gv.Rows[i].Cells[columnIndex].Text;
                        lastCell = i;
                        gv.Rows[i].Cells[columnIndex].RowSpan = 1;
                    }
                }
            }
        }

        /// <summary>
        /// 合并行(模板列)
        /// </summary>
        /// <param name=“gv”>所对应的GridView对象</param>
        /// <param name=“columnIndex”>所对应要合并的列的索引，从0开始</param> 
        /// <param name=“lblName”>模板列里面Lable的Id</param>
        public static void UnitCell(GridView gv, int columnIndex, string lblName)
        {
            int i;
            string lastType;
            int lastCell;
            if (gv.Rows.Count > 0)
            {
                lastType = (gv.Rows[0].Cells[columnIndex].FindControl(lblName) as Label).Text;
                gv.Rows[0].Cells[columnIndex].RowSpan = 1;
                lastCell = 0;
                for (i = 1; i < gv.Rows.Count; i++)
                {
                    if ((gv.Rows[i].Cells[columnIndex].FindControl(lblName) as Label).Text == lastType)
                    {
                        gv.Rows[i].Cells[columnIndex].Visible = false;
                        gv.Rows[lastCell].Cells[columnIndex].RowSpan++;
                    }
                    else
                    {
                        lastType = (gv.Rows[i].Cells[columnIndex].FindControl(lblName) as Label).Text;
                        lastCell = i;
                        gv.Rows[i].Cells[columnIndex].RowSpan = 1;
                    }
                }
            }
        }
        /// <summary>
        /// 合并行(模板列)((DataBoundLiteralControl)row.Cells[index].Controls[0])
        /// </summary>
        /// <param name=“gv”>所对应的GridView对象</param>
        /// <param name=“columnIndex”>所对应要合并的列的索引，从0开始</param> 
        /// <param name=“lblName”>模板列里面Lable的Id</param>
        public static void UnitCellLiteralControl(GridView gv, int columnIndex)
        {
            int i;
            string lastType;
            int lastCell;
            if (gv.Rows.Count > 0)
            {
                lastType = (gv.Rows[0].Cells[columnIndex].Controls[0] as DataBoundLiteralControl).Text;
                gv.Rows[0].Cells[columnIndex].RowSpan = 1;
                lastCell = 0;
                for (i = 1; i < gv.Rows.Count; i++)
                {
                    if ((gv.Rows[i].Cells[columnIndex].Controls[0] as DataBoundLiteralControl).Text == lastType)
                    {
                        gv.Rows[i].Cells[columnIndex].Visible = false;
                        gv.Rows[lastCell].Cells[columnIndex].RowSpan++;
                    }
                    else
                    {
                        lastType = (gv.Rows[i].Cells[columnIndex].Controls[0] as DataBoundLiteralControl).Text;
                        lastCell = i;
                        gv.Rows[i].Cells[columnIndex].RowSpan = 1;
                    }
                }
            }
        }


        /// <summary>
        /// 合并行(普通列 需要列的索引)索引列是要合并的列
        /// </summary>
        /// <param name="gv">所对应的GridView对象</param>
        /// <param name="columnIndex">所对应要合并的列的索引，从0开始</param>
        /// <param name="currentRowIndex">当前行的唯一索引 为了防止出现第一组和第二组重复被覆盖的问题</param>
        /// <param name="currentRowForlblName">索引列为label标签模版(索引列是要合并的列)</param>
        public static void UnitCell(GridView gv, int columnIndex, int currentRowIndex, string currentRowForlblName)
        {
            int i;
            string lastType;
            int lastCell;
            string lastTypeRowIndex;
            if (gv.Rows.Count > 0)
            {
                lastTypeRowIndex = (gv.Rows[0].Cells[currentRowIndex].FindControl(currentRowForlblName) as Label).Text;
                lastType = gv.Rows[0].Cells[columnIndex].Text;
                gv.Rows[0].Cells[columnIndex].RowSpan = 1;
                lastCell = 0;
                for (i = 1; i < gv.Rows.Count; i++)
                {
                    if (gv.Rows[i].Cells[columnIndex].Text == lastType
                        && (gv.Rows[i].Cells[currentRowIndex].FindControl(currentRowForlblName) as Label).Text == lastTypeRowIndex)
                    {
                        gv.Rows[i].Cells[columnIndex].Visible = false;
                        gv.Rows[lastCell].Cells[columnIndex].RowSpan++;
                    }
                    else
                    {
                        lastType = gv.Rows[i].Cells[columnIndex].Text;
                        lastTypeRowIndex = (gv.Rows[i].Cells[currentRowIndex].FindControl(currentRowForlblName) as Label).Text;
                        lastCell = i;
                        gv.Rows[i].Cells[columnIndex].RowSpan = 1;
                    }
                }
            }
        }

        /// <summary>
        /// 合并行(普通列,需要列的索引)
        /// </summary>
        /// <param name=“gv”>所对应的GridView对象</param>
        /// <param name=“columnIndex”>所对应要合并的列的索引，从0开始</param>
        /// <param name="currentRowIndex">当前行的唯一索引 为了防止出现第一组和第二组重复被覆盖的问题</param>
        public static void UnitCell(GridView gv, int columnIndex, int currentRowIndex)
        {
            int i;
            string lastType;
            int lastCell;
            string lastTypeRowIndex;
            if (gv.Rows.Count > 0)
            {
                lastTypeRowIndex = gv.Rows[0].Cells[currentRowIndex].Text;
                lastType = gv.Rows[0].Cells[columnIndex].Text;
                gv.Rows[0].Cells[columnIndex].RowSpan = 1;
                lastCell = 0;
                for (i = 1; i < gv.Rows.Count; i++)
                {
                    if (gv.Rows[i].Cells[columnIndex].Text == lastType
                        && gv.Rows[i].Cells[currentRowIndex].Text == lastTypeRowIndex)
                    {
                        gv.Rows[i].Cells[columnIndex].Visible = false;
                        gv.Rows[lastCell].Cells[columnIndex].RowSpan++;
                    }
                    else
                    {
                        lastType = gv.Rows[i].Cells[columnIndex].Text;
                        lastTypeRowIndex = gv.Rows[i].Cells[currentRowIndex].Text;
                        lastCell = i;
                        gv.Rows[i].Cells[columnIndex].RowSpan = 1;
                    }
                }
            }
        }
    }
}