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
        public static PageList<List<Sys_User>> GetUserList(long mobile, int pageIndex, int pageSize)
        {
            string strSql = string.Format(@"select * from Sys_User where 1=1 ");
            if(mobile>0)
            {
                strSql += string.Format(@" and Mobile={0}",mobile);
            }
            
            var db = CoreDB.GetInstance();
            Page<Sys_User> pagelist = db.Page<Sys_User>(pageIndex, pageSize, strSql);

            PageList<List<Sys_User>> pList = new PageList<List<Sys_User>>((int)pagelist.CurrentPage, (int)pagelist.ItemsPerPage, (int)pagelist.TotalItems);
            pList.Source = pagelist.Items.ToList();
            return pList;
        }

        public static Sys_User GetUserInfo(string openId)
        {
            Sys_User u = Sys_User.SingleOrDefault(" where OpenId=@0", openId);
           return u;
        }



        public static bool IsExistUser(string openId)
        {
            Sys_User u = GetUserInfo(openId);
            if (u != null && u.Id > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 获取理事数量
        /// </summary>
        /// <returns></returns>
        public static int GetLishiUserCount()
        {
            String str =String.Format(@"select count(1) from Sys_User where ApproveFlag=@0 and UserType=@1");
           int count =  CoreDB.GetInstance().ExecuteScalar<int>(str,(int)ApproveFlag.已认证,(int)UserType.理事类型);
           return count;
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
        未分配 = 0,
        /// <summary>
        /// 1：理事 
        /// </summary>
        理事 = 1,
        /// <summary>
        /// 2：常务理事
        /// </summary>
        常务理事 = 2,
        /// <summary>
        /// 3：荣誉理事
        /// </summary>
        荣誉理事 = 3
    }
    /// <summary>
    /// 用户类型：0：未分配，1：粉丝类型 ,2：理事类型
    /// </summary>
    public enum UserType
    {
        /// <summary>
        /// 0：未分配
        /// </summary>
        未分配 = 0,
        /// <summary>
        /// 1：粉丝类型
        /// </summary>
        粉丝类型 = 1,
        /// <summary>
        /// 1：理事类型
        /// </summary>
        理事类型 = 2
    }
    /// <summary>
    /// 是否认证 0:未认证 1:已认证 2：提交认证申请 3：认证未通过  目前只针对理事
    /// </summary>
    public enum ApproveFlag
    {
        /// <summary>
        /// 0:未认证
        /// </summary>
        未认证 = 0,
        /// <summary>
        /// 1:已认证
        /// </summary>
        已认证 = 1,
        /// <summary>
        /// 2：提交认证申请
        /// </summary>
        已提交认证申请 = 2,
        /// <summary>
        /// 3：认证未通过 
        /// </summary>
        认证未通过 = 3
    }
}
