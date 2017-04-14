using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Module.Models;
using Module.Utils;
using PetaPoco;

namespace WeiXinYiShengCollege.Business
{
    public  class UserBusiness:BaseBusiness
    {
        public static PageList<List<User>> GetUserList(long mobile,int bookId, int pageIndex, int pageSize)
        {
            string strSql = string.Format(@"select * from [User] where 1=1 ");
            if(mobile>0)
            {
                strSql += string.Format(@" and Mobile={0}",mobile);
            }
            if (bookId > 0)
            {
                strSql += string.Format(@" and BookId={0}", bookId);
            }
            var db = CoreDB.GetInstance();
            Page<User> pagelist = db.Page<User>(pageIndex, pageSize, strSql);

            PageList<List<User>> pList = new PageList<List<User>>((int)pagelist.CurrentPage, (int)pagelist.ItemsPerPage, (int)pagelist.TotalItems);
            pList.Source = pagelist.Items.ToList();
            return pList;
        }
        
    }
}
