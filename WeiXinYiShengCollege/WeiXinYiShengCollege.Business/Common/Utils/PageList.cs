using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Module.Utils
{
    /// <summary>
    /// 分页对象
    /// </summary>
    public class PageList : PageList<DataTable>
    {
        /// <summary>
        /// 分页对象
        /// </summary>
        /// <param name="pageIndex">页面索引值</param>
        /// <param name="pageSize">每页记录的数量</param>
        /// <param name="totalCount">记录总条数</param>
        public PageList(int pageIndex, int pageSize, int totalCount) :
            base(pageIndex, pageSize, totalCount)
        {
        }
    }


    /// <summary>
    /// 分页对象
    /// </summary>
    public class PageList<T>
    {
        /// <summary>
        /// 页面索引值
        /// </summary>
        public int PageIndex
        {
            get;
            private set;
        }

        /// <summary>
        /// 每页记录的数量
        /// </summary>
        public int PageSize
        {
            get;
            private set;
        }

        /// <summary>
        /// 记录总条数
        /// </summary>
        public int TotalCount
        {
            get;
            private set;
        }

        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPages
        {
            get;
            private set;
        }

        /// <summary>
        /// 是否存在前一页
        /// </summary>
        public bool HasPreviousPage
        {
            get
            {
                return PageIndex > 1;
            }
        }

        /// <summary>
        /// 是否存在下一页
        /// </summary>
        public bool HasNextPage
        {
            get
            {
                return PageIndex < TotalPages;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public List<int> NumberPage
        {
            get;
            set;
        }

        /// <summary>
        /// 单前页数据
        /// </summary>
        public T Source
        {
            get;
            set;
        }

        /// <summary>
        /// 获取指定的数字型页数
        /// </summary>
        /// <param name="count">需要的数量</param>
        /// <returns>返回分页表</returns>
        public List<int> GetNumberPage(int count)
        {
            List<int> result = new List<int>();
            result.Add(PageIndex);

            if (count <= 1)
            {
                return result;
            }

            int preIndex = PageIndex - 1;
            int nextIndex = PageIndex + 1;
            for (int i = 0; i < count - 1; )
            {
                bool hasData = false;

                if (preIndex >= 1)
                {
                    hasData = true;
                    result.Add(preIndex);
                    preIndex = preIndex - 1;
                    i++;
                    if (i >= count - 1)
                    {
                        break;
                    }
                }

                if (nextIndex <= TotalPages)
                {
                    hasData = true;
                    result.Add(nextIndex);
                    nextIndex = nextIndex + 1;
                    i++;
                    if (i >= count - 1)
                    {
                        break;
                    }
                }

                if (!hasData)
                {
                    break;
                }
            }

            result.Sort(Coparison);

            return result;
        }

        /// <summary>
        /// 分页对象
        /// </summary>
        /// <param name="pageIndex">页面索引值</param>
        /// <param name="pageSize">每页记录的数量</param>
        /// <param name="totalCount">记录总条数</param>
        public PageList(int pageIndex, int pageSize, int totalCount)
        {
            if (pageSize <= 0)
            {
                throw new ArgumentOutOfRangeException("pageSize");
            }

            PageSize = pageSize;


            if (totalCount <= 0)
            {
                //没有记录信息
                TotalCount = 0;
                PageIndex = 1;
                TotalPages = 1;
            }
            else
            {
                TotalCount = totalCount;

                //总页数
                TotalPages = (int)Math.Ceiling(TotalCount / (double)pageSize);

                //当前页处理
                if (pageIndex <= 0)
                {
                    PageIndex = 1;
                }
                else
                {
                    if (pageIndex > TotalPages)
                    {
                        PageIndex = TotalPages;
                    }
                    else
                    {
                        PageIndex = pageIndex;
                    }
                }
            }

            NumberPage = GetNumberPage(5);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        private static int Coparison(int i, int j)
        {
            if (i > j)
            {
                return 1;
            }
            else if (i < j)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }
}