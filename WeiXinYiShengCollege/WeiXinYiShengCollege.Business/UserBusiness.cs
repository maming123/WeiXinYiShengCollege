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
        public static PageList<List<dynamic>> GetUserList(long mobile,int userType,int userLevel,int customerManagerId, int province,int city,int pageIndex, int pageSize)
        {
            string strSql = string.Format(@"
SELECT  s1.* ,
        s2.nickname AS 'ParentNickName',
        (SELECT TOP 1 AreaName FROM Area WHERE Id=s1.Province) AS 'ProvinceStr',
        (SELECT TOP 1 AreaName FROM Area WHERE Id=s1.City) AS 'CityStr'
FROM    Sys_User s1
        LEFT JOIN Sys_User s2 ON s1.parentid = s2.id
WHERE   s1.IsDelete = 0
");
            if(mobile>0)
            {
                strSql += string.Format(@" and s1.Mobile={0}",mobile);
            }
            if (userType >= 0)
            {
                strSql += string.Format(@" and s1.UserType={0}", userType);
            }
            if (userLevel >= 0)
            {
                strSql += string.Format(@" and s1.UserLevel={0}", userLevel);
            }
            if (customerManagerId > 0)
            {
                strSql += string.Format(@" and s1.CustomerManagerId={0}", customerManagerId);
            }
            if (province > 0)
            {
                strSql += string.Format(@" and s1.Province={0}", province);
            }
            if (city > 0)
            {
                strSql += string.Format(@" and s1.City={0}", city);
            }
            
            var db = CoreDB.GetInstance();
            Page<dynamic> pagelist = db.Page<dynamic>(pageIndex, pageSize, strSql);

            PageList<List<dynamic>> pList = new PageList<List<dynamic>>((int)pagelist.CurrentPage, (int)pagelist.ItemsPerPage, (int)pagelist.TotalItems);
            pList.Source = pagelist.Items.ToList();
            return pList;
        }

        public static Sys_User GetUserInfo(string openId)
        {
            Sys_User u = Sys_User.SingleOrDefault(" where OpenId=@0", openId);
           return u;
        }
        public static Sys_User GetUserInfoById(int id)
        {
            Sys_User u = Sys_User.SingleOrDefault((object)id);
            return u;
        }

        /// <summary>
        /// 更新积分
        /// </summary>
        /// <param name="openId"></param>
        /// <param name="score"></param>
        public static void UpdateScore(string openId,int score)
        {
            Sys_User u = GetUserInfo(openId);
            u.Score = score;
            u.Update();
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
            String str = String.Format(@"select count(1) from Sys_User where  IsDelete=0 and ApproveFlag=@0 and UserType=@1");
           int count =  CoreDB.GetInstance().ExecuteScalar<int>(str,(int)ApproveFlag.已认证,(int)UserType.理事类型);
           return count;
        }

        #region 客服经理
        /// <summary>
        /// 获取客服经理列表
        /// </summary>
        /// <returns></returns>
        public static List<CustomerManager> GetCustomerManagerList()
        {
            
            List<CustomerManager> list = CustomerManager.Query("").ToList();
            return list;
        }

        /// <summary>
        /// 获取客服经理信息
        /// </summary>
        /// <returns></returns>
        public static CustomerManager GetCustomerManagerInfo(int CustomerManagerId)
        {

            CustomerManager cm = CustomerManager.SingleOrDefault((object)CustomerManagerId);
            return cm;
        }
        #endregion

        #region 粉丝
        /// <summary>
        /// 获取我的粉丝列表
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static PageList<List<Sys_User>> GetMyFansList(int parentId, int pageIndex, int pageSize)
        {
            string strSql = string.Format(@"SELECT  * 
                                            FROM    Sys_User WHERE IsDelete=0  ");
            if (parentId > 0)
            {
                strSql += string.Format(@" and ParentId={0}", parentId);
            }else
            {
                return null;
            }

            var db = CoreDB.GetInstance();
            Page<Sys_User> pagelist = db.Page<Sys_User>(pageIndex, pageSize, strSql);

            PageList<List<Sys_User>> pList = new PageList<List<Sys_User>>((int)pagelist.CurrentPage, (int)pagelist.ItemsPerPage, (int)pagelist.TotalItems);
            pList.Source = pagelist.Items.ToList();
            return pList;
        }
        #endregion


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
