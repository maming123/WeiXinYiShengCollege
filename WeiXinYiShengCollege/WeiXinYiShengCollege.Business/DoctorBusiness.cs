using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Module.Models;

namespace WeiXinYiShengCollege.Business
{
    /// <summary>
    /// 医生出诊情况
    /// </summary>
    public class DoctorBusiness
    {
        /// <summary>
        /// 获得指定理事添加的医生信息
        /// </summary>
        /// <param name="creatorOpenId"></param>
        /// <returns></returns>
        public static List<DoctorInfo> GetDoctorList(string creatorOpenId)
        {
            List<DoctorInfo> list = DoctorInfo.Query("where CreatorOpenId=@0 ", creatorOpenId).ToList();
            return list;
        }

        /// <summary>
        /// 获取指定Id的医生信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static DoctorInfo GetDoctorInfo(int id)
        {
            DoctorInfo dwinfo = DoctorInfo.SingleOrDefault((object)id);
            return dwinfo;
        }

        /// <summary>
        /// 获取指定Id的出诊信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static DoctorWorkSchedule GetDoctorWorkSchedule(string CreatorOpenId, int id)
        {
            DoctorWorkSchedule dws = DoctorWorkSchedule.SingleOrDefault("where Id=@0 and CreatorOpenId=@1", id, CreatorOpenId);
            return dws;
        }


        /// <summary>
        /// 获得指定时间的排版时间
        /// </summary>
        /// <param name="creatorOpenId"></param>
        /// <returns></returns>
        public static List<DoctorWorkSchedule> GetDoctorWorkList(string creatorOpenId, DateTime WorkDateTime)
        {
            List<DoctorWorkSchedule> list = DoctorWorkSchedule.Query("where CreatorOpenId=@0 and WorkDateTime >=@1 order by WorkDateTime asc", creatorOpenId, Convert.ToDateTime(WorkDateTime.ToString("yyyy-MM-dd"))).ToList();
            return list;
        }

        public static int  DeleteDoctorWorkSchedule(string creatorOpenId,int Id)
        {
           int r= DoctorWorkSchedule.Delete("where CreatorOpenId=@0 and Id=@1", creatorOpenId, Id);
           return r;
        }
        public static int DeleteDoctorInfo(string creatorOpenId, int Id)
        {
            int r = DoctorInfo.Delete("where CreatorOpenId=@0 and Id=@1", creatorOpenId, Id);
            return r;
        }

    }
}
