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
        public static PageList<List<User>> GetUserList(long mobile, int pageIndex, int pageSize)
        {
            string strSql = string.Format(@"select * from [User] where 1=1 ");
            if(mobile>0)
            {
                strSql += string.Format(@" and Mobile={0}",mobile);
            }
            
            var db = CoreDB.GetInstance();
            Page<User> pagelist = db.Page<User>(pageIndex, pageSize, strSql);

            PageList<List<User>> pList = new PageList<List<User>>((int)pagelist.CurrentPage, (int)pagelist.ItemsPerPage, (int)pagelist.TotalItems);
            pList.Source = pagelist.Items.ToList();
            return pList;
        }

        public static User GetUserInfo(string openId)
        {
           User u = User.SingleOrDefault(" where OpenId=@0", openId);
           return u;
        }

        public static bool IsExistUser(string openId)
        {
            User u = GetUserInfo(openId);
            if (u != null && u.Id > 0)
                return true;
            else
                return false;
        }
        
    }

    /// <summary>
    /// 理事级别：0：未分配，1：理事 ，2：常务理事，3：荣誉理事 
    /// </summary>
    public enum UserLevel
    {
        /// <summary>
        /// 0：未分配
        /// </summary>
        noassign = 0,
        /// <summary>
        /// 1：理事 
        /// </summary>
        lishi=1,
        /// <summary>
        /// 2：常务理事
        /// </summary>
        changwulishi=2,
        /// <summary>
        /// 3：荣誉理事
        /// </summary>
        rongyulishi=3
    }
    /// <summary>
    /// 用户类型：0：未分配，1：粉丝类型 ,2：理事类型
    /// </summary>
    public enum UserType
    {
        /// <summary>
        /// 0：未分配
        /// </summary>
        noassign=0,
        /// <summary>
        /// 1：粉丝类型
        /// </summary>
        fans=1,
        /// <summary>
        /// 1：理事类型
        /// </summary>
        lishi=2
    }
    /// <summary>
    /// 是否认证 0:未认证 1:已认证 2：提交认证申请 3：认证未通过  目前只针对理事
    /// </summary>
    public enum ApproveFlag
    {
        /// <summary>
        /// 0:未认证
        /// </summary>
        noapprove=0,
        /// <summary>
        /// 1:已认证
        /// </summary>
        approved=1,
        /// <summary>
        /// 2：提交认证申请
        /// </summary>
        approvesubmit=2,
        /// <summary>
        /// 3：认证未通过 
        /// </summary>
        notpassapproved=3
    }
}
