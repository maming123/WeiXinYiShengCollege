using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Module.DataAccess;

namespace Module.Utils
{
    public class BaseBusiness
    {
        private const int PagingType = 0;

        /// <summary>
        /// 简单分页，适合小数据量
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="pkCol"></param>
        /// <param name="cols"></param>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="recordCount"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public static DataTable GetDatasForPaging(string tableName, string pkCol, string cols, string where, string orderBy, int pageIndex, int pageSize, out int recordCount, out int pageCount, string connectionStrings)
        {
            int t = PagingType;
            if (t == 1)
            {
                return
                    GetDatasForPagingByTop(tableName, pkCol, cols, where, orderBy, pageIndex, pageSize, out recordCount, out pageCount, connectionStrings);
            }
            else
            {
                return
                    GetDatasForPagingByRowNum(tableName, pkCol, cols, where, orderBy, pageIndex, pageSize, out recordCount, out pageCount, connectionStrings);
            }
        }

        /// <summary>
        /// Top分页
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="pkCol"></param>
        /// <param name="cols"></param>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="recordCount"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public static DataTable GetDatasForPagingByTop(string tableName, string pkCol, string cols, string where, string orderBy, int pageIndex, int pageSize, out int recordCount, out int pageCount, string connectionStrings)
        {
            recordCount = 0;
            pageCount = 0;

            if (String.IsNullOrEmpty(where))
            {
                where = "1=1";
            }

            string sql = String.Format(
                "select count(*) from {0} AS T where {1}",
                tableName,
                where);

            recordCount = Convert.ToInt32(SqlHelper.GetObject(sql, null, connectionStrings));

            if (recordCount > 0)
            {
                if (recordCount <= pageSize)
                {
                    pageCount = 1;
                }
                else
                {
                    int y = recordCount % pageSize;
                    pageCount = recordCount / pageSize;
                    pageCount = y > 0 ? pageCount + 1 : pageCount;
                }
            }

            if (pageIndex <= 1)
            {
                pageIndex = 1;
            }
            else if (pageIndex > pageCount)
            {
                pageIndex = pageCount;
            }

            if (pageIndex <= 0)
            {
                pageIndex = 1;
            }

            if (pageIndex == 1)
            {
                sql = String.Format(
                    "select top {0} {1} from {2} AS T where {3} order by {4}",
                    pageSize.ToString(),
                    cols,
                    tableName,
                    where,
                    orderBy);
            }
            else
            {
                int count = (pageIndex - 1) * pageSize;
                sql = String.Format(
                    "select top {0} {1} from {2} AS T1 where {3} not in (select top {4} {3} from {2} AS T where {5} order by {6}) and {5}  order by {6}",
                    pageSize.ToString(),
                    cols,
                    tableName,
                    pkCol,
                    count.ToString(),
                    where,
                    orderBy);
            }

            DataTable dt = SqlHelper.GetData(sql, null, connectionStrings);

            return dt;
        }

        /// <summary>
        /// RowNumber 分页
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="pkCol"></param>
        /// <param name="cols"></param>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="recordCount"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public static DataTable GetDatasForPagingByRowNum(string tableName, string pkCol, string cols, string where, string orderBy, int pageIndex, int pageSize, out int recordCount, out int pageCount, string connectionStrings)
        {
            recordCount = 0;
            pageCount = 0;

            if (String.IsNullOrEmpty(where))
            {
                where = "1=1";
            }

            string sql = String.Format(
                "select count(*) from {0} AS T where {1}",
                tableName,
                where);

            recordCount = Convert.ToInt32(SqlHelper.GetObject(sql, null, connectionStrings));

            if (recordCount > 0)
            {
                if (recordCount <= pageSize)
                {
                    pageCount = 1;
                }
                else
                {
                    int y = recordCount % pageSize;
                    pageCount = recordCount / pageSize;
                    pageCount = y > 0 ? pageCount + 1 : pageCount;
                }
            }

            if (pageIndex <= 1)
            {
                pageIndex = 1;
            }
            else if (pageIndex > pageCount)
            {
                pageIndex = pageCount;
            }

            if (pageIndex <= 0)
            {
                pageIndex = 1;
            }

            //if (pageIndex == 1)
            //{
            //    sql = String.Format(
            //        "select top {0} {1} from {2} AS T where {3} order by {4}",
            //        pageSize.ToString(),
            //        cols,
            //        tableName,
            //        where,
            //        orderBy);
            //}
            //else
            //{
            int beginNumber = (pageIndex - 1) * pageSize + 1;
            int endNumber = beginNumber + pageSize - 1;

            sql = String.Format(
                @"
                    SELECT {6} 
                    FROM 
                    (
                        SELECT {0} as PageRowId ,
                           RowID = ROW_NUMBER () OVER (ORDER BY {2})
                        FROM {1} AS TX where {3}
                    ) AS PageTableList
                    JOIN {1} AS T ON T.{0} = PageTableList.PageRowId 
                    WHERE RowID BETWEEN {4} AND {5} 
                    ORDER BY {2}", pkCol, tableName, orderBy, where, beginNumber.ToString(), endNumber.ToString(), cols);
            //}

            DataTable dt = SqlHelper.GetData(sql, null, connectionStrings);

            return dt;
        }

        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <param name="data">数据源</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="pageSize">每页数量</param>
        /// <param name="pageCount">页数</param>
        /// <returns></returns>
        public static DataTable GetDatasForPaging(DataTable data, int pageIndex, int pageSize, out int pageCount)
        {
            pageCount = 0;
            if (data == null || data.Rows.Count <= 1)
            {
                return data;
            }



            //总页数
            int totalPages = (int)Math.Ceiling(data.Rows.Count / (double)pageSize);
            pageCount = totalPages;
            //当前页处理
            if (pageIndex <= 0)
            {
                pageIndex = 1;
            }
            else
            {
                if (pageIndex > totalPages)
                {
                    pageIndex = totalPages;
                }
            }

            DataTable newDt = data.Copy();
            newDt.Clear();
            int beginIndex = (pageIndex - 1) * pageSize;
            for (int i = 0; beginIndex < data.Rows.Count && i < pageSize; i++, beginIndex++)
            {
                DataRow newDr = newDt.NewRow();
                foreach (DataColumn column in data.Columns)
                {
                    newDr[column.ColumnName] = data.Rows[beginIndex][column.ColumnName];
                }
                newDt.Rows.Add(newDr);
            }

            return newDt;
        }

        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <param name="data">数据源</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="pageSize">每页数量</param>
        /// <param name="pageCount">页数</param>
        /// <returns></returns>
        public static List<T> GetDatasForPaging<T>(List<T> data, int pageIndex, int pageSize, out int pageCount)
        {
            pageCount = 0;
            if (data == null || data.Count <= 1)
            {
                return data;
            }

            //总页数
            int totalPages = (int)Math.Ceiling(data.Count / (double)pageSize);
            pageCount = totalPages;
            //当前页处理
            if (pageIndex <= 0)
            {
                pageIndex = 1;
            }
            else
            {
                if (pageIndex > totalPages)
                {
                    pageIndex = totalPages;
                }
            }

            List<T> newDt = new List<T>();
            newDt.Clear();
            int beginIndex = (pageIndex - 1) * pageSize;
            for (int i = 0; beginIndex < data.Count && i < pageSize; i++, beginIndex++)
            {
                newDt.Add(data[beginIndex]);
            }

            return newDt;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="procName"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="where"></param>
        /// <param name="connectionStrings"></param>
        /// <returns></returns>
        public static PageList ExecutePageByTopWithProc(string tableName, string procName, int pageIndex, int pageSize, string where, string connectionStrings)
        {
            string sql = String.Format("select count(*) from {0} with(nolock) where {1}", tableName, where);

            int recordCount = Convert.ToInt32(SqlHelper.GetObject(sql, null, connectionStrings));
            PageList pager = new PageList(pageIndex, pageSize, recordCount);

            if (pageIndex <= 0)
                pageIndex = 1;

            var parms = new[] { new SqlParameter("@PageSize", pageSize), new SqlParameter("@PageIndex", pageIndex) };

            pager.Source = SqlHelper.GetDataByPro(procName, parms, connectionStrings);

            return pager;

        }

        /// <summary>
        /// 通过Top方法分页查询数据
        /// </summary>
        /// <param name="tableName">表名称</param>
        /// <param name="primaryKeyColumn">主键名称</param>
        /// <param name="columns">需要的结果列</param>
        /// <param name="where">where子句</param>
        /// <param name="orderBy">orderBy子句</param>
        /// <param name="pageIndex">页面Index</param>
        /// <param name="pageSize">每页数量</param>
        /// <returns>返回数据内容</returns>
        public static PageList ExecutePageByTop(string tableName, string primaryKeyColumn, string columns, string where, string orderBy, int pageIndex, int pageSize, string connectionStrings)
        {
            if (String.IsNullOrEmpty(where))
            {
                where = "1=1";
            }

            string sql = String.Format(
                "select count(*) from {0} where {1}",
                tableName,
                where);

            int recordCount = Convert.ToInt32(SqlHelper.GetObject(sql, null, connectionStrings));
            PageList pager = new PageList(pageIndex, pageSize, recordCount);
            if (pager.PageIndex == 1)
            {
                sql = String.Format(
                    "select top {0} {1} from {2} where {3} order by {4}",
                    pageSize.ToString(),
                    columns,
                    tableName,
                    where,
                    orderBy);
            }
            else
            {
                int count = (pager.PageIndex - 1) * pager.PageSize;
                sql = String.Format(
                    "select top {0} {1} from {2} where {3} not in (select top {4} {3} from {2} where {5} order by {6}) and {5}  order by {6}",
                    pageSize.ToString(),
                    columns,
                    tableName,
                    primaryKeyColumn,
                    count.ToString(),
                    where,
                    orderBy);
            }

            pager.Source = SqlHelper.GetData(sql, null, connectionStrings);
            return pager;
        }

        /// <summary>
        /// 通过RowNumber方法分页查询数据
        /// </summary>
        /// <param name="tableName">表名称</param>
        /// <param name="primaryKeyColumn">主键名称</param>
        /// <param name="columns">需要的结果列</param>
        /// <param name="where">where子句</param>
        /// <param name="orderBy">orderBy子句</param>
        /// <param name="pageIndex">页面Index</param>
        /// <param name="pageSize">每页数量</param>
        /// <returns>返回数据内容</returns>
        public static PageList ExecutePageByRowNum(string tableName, string primaryKeyColumn, string columns, string where, string orderBy, int pageIndex, int pageSize, string connectionStrings)
        {
            if (String.IsNullOrEmpty(where))
            {
                where = "1=1";
            }

            string sql = String.Format(
                "select count(*) from {0} where {1}",
                tableName,
                where);

            int recordCount = Convert.ToInt32(SqlHelper.GetObject(sql, null, connectionStrings));
            PageList pager = new PageList(pageIndex, pageSize, recordCount);

            if (pager.PageIndex == 1)
            {
                sql = String.Format(
                    "select top {0} {1} from {2} where {3} order by {4}",
                    pageSize.ToString(),
                    columns,
                    tableName,
                    where,
                    orderBy);
            }
            else
            {
                int beginNumber = (pager.PageIndex - 1) * pager.PageSize + 1;
                int endNumber = beginNumber + pager.PageSize - 1;

                sql = String.Format(
                    @"
                    SELECT {6} 
                    FROM 
                    (
                        SELECT {0} as PageRowId ,
                           RowID = ROW_NUMBER () OVER (ORDER BY {2})
                        FROM {1} where {3}
                    ) AS PageTableList
                    JOIN {1} AS T ON T.{0} = PageTableList.PageRowId 
                    WHERE RowID BETWEEN {4} AND {5} 
                    ORDER BY {2}", primaryKeyColumn, tableName, orderBy, where, beginNumber.ToString(), endNumber.ToString(), columns);
            }

            pager.Source = SqlHelper.GetData(sql, null, connectionStrings);

            return pager;
        }

        /// <summary>
        /// 通过RowNumber方法分页查询数据
        /// </summary>
        /// <param name="tableName">表名称</param>
        /// <param name="primaryKeyColumn">主键名称</param>
        /// <param name="columns">需要的结果列</param>
        /// <param name="where">where子句</param>
        /// <param name="orderBy">orderBy子句</param>
        /// <param name="pageIndex">页面Index</param>
        /// <param name="pageSize">每页数量</param>
        /// <returns></returns>
        public static PageList GetDatasForPaging(string tableName, string primaryKeyColumn, string columns, string where, string orderBy, int pageIndex, int pageSize, string connectionStrings)
        {
            int t = PagingType;

            if (t == 1)
            {
                return ExecutePageByTop(tableName, primaryKeyColumn, columns, where, orderBy, pageIndex, pageSize, connectionStrings);
            }
            else
            {
                return ExecutePageByRowNum(tableName, primaryKeyColumn, columns, where, orderBy, pageIndex, pageSize, connectionStrings);
            }
        }

        public static PageList GetDataForTopPageing(string tableName, string primaryKeyColumn, string columns, string where, string orderBy, int pageIndex, int pageSize, string connectionStrings)
        {
            return ExecutePageByTop(tableName, primaryKeyColumn, columns, where, orderBy, pageIndex, pageSize, connectionStrings);
        }

        public static PageList GetDataForPageingWithProc(string tableName, string procName, int pageIndex, int pageSize, string where, string connectionStrings)
        {
            return ExecutePageByTopWithProc(tableName, procName, pageIndex, pageSize, where, connectionStrings);
        }

        /// <summary>
        /// 通过Top方法分页查询数据
        /// </summary>
        /// <param name="tableName">表名称</param>
        /// <param name="primaryKeyColumn">主键名称</param>
        /// <param name="columns">需要的结果列</param>
        /// <param name="where">where子句</param>
        /// <param name="orderBy">orderBy子句</param>
        /// <param name="pageIndex">页面Index</param>
        /// <param name="pageSize">每页数量</param>
        /// <returns>返回数据内容</returns>
        public static PageList<List<T>> ExecutePageByTop<T>(string tableName, string primaryKeyColumn, string columns, string where, string orderBy, int pageIndex, int pageSize, string connectionStrings)
             where T : class, new()
        {
            if (String.IsNullOrEmpty(where))
            {
                where = "1=1";
            }

            string sql = String.Format(
                "select count(*) from {0} where {1}",
                tableName,
                where);

            int recordCount = Convert.ToInt32(SqlHelper.GetObject(sql, null, connectionStrings));
            PageList<List<T>> pager = new PageList<List<T>>(pageIndex, pageSize, recordCount);
            if (pager.PageIndex == 1)
            {
                sql = String.Format(
                    "select top {0} {1} from {2} where {3} order by {4}",
                    pageSize.ToString(),
                    columns,
                    tableName,
                    where,
                    orderBy);
            }
            else
            {
                int count = (pager.PageIndex - 1) * pager.PageSize;
                sql = String.Format(
                    "select top {0} {1} from {2} where {3} not in (select top {4} {3} from {2} where {5} order by {6}) and {5}  order by {6}",
                    pageSize.ToString(),
                    columns,
                    tableName,
                    primaryKeyColumn,
                    count.ToString(),
                    where,
                    orderBy);
            }

            pager.Source = TypeConvert.ToList<T>(SqlHelper.GetData(sql, null, connectionStrings));
            return pager;
        }

        /// <summary>
        /// 通过RowNumber方法分页查询数据
        /// </summary>
        /// <param name="tableName">表名称</param>
        /// <param name="primaryKeyColumn">主键名称</param>
        /// <param name="columns">需要的结果列</param>
        /// <param name="where">where子句</param>
        /// <param name="orderBy">orderBy子句</param>
        /// <param name="pageIndex">页面Index</param>
        /// <param name="pageSize">每页数量</param>
        /// <returns>返回数据内容</returns>
        public static PageList<List<T>> ExecutePageByRowNum<T>(string tableName, string primaryKeyColumn, string columns, string where, string orderBy, int pageIndex, int pageSize, string connectionStrings)
              where T : class, new()
        {
            if (String.IsNullOrEmpty(where))
            {
                where = "1=1";
            }

            string sql = String.Format(
                "select count(*) from {0} where {1}",
                tableName,
                where);

            int recordCount = Convert.ToInt32(SqlHelper.GetObject(sql, null, connectionStrings));
            PageList<List<T>> pager = new PageList<List<T>>(pageIndex, pageSize, recordCount);

            if (pager.PageIndex == 1)
            {
                sql = String.Format(
                    "select top {0} {1} from {2} where {3} order by {4}",
                    pageSize.ToString(),
                    columns,
                    tableName,
                    where,
                    orderBy);
            }
            else
            {
                int beginNumber = (pager.PageIndex - 1) * pager.PageSize + 1;
                int endNumber = beginNumber + pager.PageSize - 1;

                sql = String.Format(
                    @"
                    SELECT {6} 
                    FROM 
                    (
                        SELECT {0} as PageRowId ,
                           RowID = ROW_NUMBER () OVER (ORDER BY {2})
                        FROM {1} where {3}
                    ) AS PageTableList
                    JOIN {1} AS T ON T.{0} = PageTableList.PageRowId 
                    WHERE RowID BETWEEN {4} AND {5} 
                    ORDER BY {2}", primaryKeyColumn, tableName, orderBy, where, beginNumber.ToString(), endNumber.ToString(), columns);
            }

            pager.Source = TypeConvert.ToList<T>(SqlHelper.GetData(sql, null, connectionStrings));
            return pager;
        }

        /// <summary>
        /// 通过RowNumber方法分页查询数据
        /// </summary>
        /// <param name="tableName">表名称</param>
        /// <param name="primaryKeyColumn">主键名称</param>
        /// <param name="columns">需要的结果列</param>
        /// <param name="where">where子句</param>
        /// <param name="orderBy">orderBy子句</param>
        /// <param name="pageIndex">页面Index</param>
        /// <param name="pageSize">每页数量</param>
        /// <returns></returns>
        public static PageList<List<T>> GetDatasForPaging<T>(string tableName, string primaryKeyColumn, string columns, string where, string orderBy, int pageIndex, int pageSize, string connectionStrings)
            where T : class, new()
        {
            int t = PagingType;

            if (t == 1)
            {
                return ExecutePageByTop<T>(tableName, primaryKeyColumn, columns, where, orderBy, pageIndex, pageSize, connectionStrings);
            }
            else
            {
                return ExecutePageByRowNum<T>(tableName, primaryKeyColumn, columns, where, orderBy, pageIndex, pageSize, connectionStrings);
            }
        }


        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <param name="data">数据源</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="pageSize">每页数量</param>
        /// <returns></returns>
        public static PageList GetDatasForPaging(DataTable data, int pageIndex, int pageSize)
        {
            if (data == null)
            {
                return new PageList(pageIndex, pageSize, 0);
            }

            PageList list = new PageList(pageIndex, pageSize, data.Rows.Count);

            DataTable newDt = data.Copy();
            newDt.Clear();
            int beginIndex = (list.PageIndex - 1) * list.PageSize;
            for (int i = 0; beginIndex < data.Rows.Count && i < pageSize; i++, beginIndex++)
            {
                DataRow newDr = newDt.NewRow();
                foreach (DataColumn column in data.Columns)
                {
                    newDr[column.ColumnName] = data.Rows[beginIndex][column.ColumnName];
                }
                newDt.Rows.Add(newDr);
            }

            list.Source = newDt;

            return list;
        }

        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <param name="data">数据源</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="pageSize">每页数量</param>
        /// <returns></returns>
        public static PageList<List<T>> GetDatasForPaging<T>(List<T> data, int pageIndex, int pageSize)
        {
            if (data == null)
            {
                return new PageList<List<T>>(pageIndex, pageSize, 0);
            }

            PageList<List<T>> list = new PageList<List<T>>(pageIndex, pageSize, data.Count);
            List<T> newDt = new List<T>();
            newDt.Clear();
            int beginIndex = (list.PageIndex - 1) * list.PageSize;
            for (int i = 0; beginIndex < data.Count && i < pageSize; i++, beginIndex++)
            {
                newDt.Add(data[beginIndex]);
            }

            list.Source = newDt;
            return list;
        }
    }
}