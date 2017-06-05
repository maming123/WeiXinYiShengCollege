using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Module.Models;
using Module.Utils;
using WeiXinYiShengCollege.Business.Common.Models;

namespace WeiXinYiShengCollege.Business
{
    /// <summary>
    /// 药方逻辑类
    /// </summary>
    public class MedicineBusiness
    {
        /// <summary>
        /// 获取药方目录类别 有缓存5分钟
        /// </summary>
        /// <returns></returns>
        public static List<Sys_Module> GetSysModuleList(int parentId)
        {
            string cacheKey = string.Format(@"GetSysModuleList_{0}", parentId);
            if(BaseCommon.HasCache(cacheKey))
            {
                return BaseCommon.GetCache<List<Sys_Module>>(cacheKey);
            }

            List<Sys_Module> list = Sys_Module.Query("where parent_module_id=@0 order by ORDER_ID  asc",parentId).ToList();
            if(null!=list)
            {
                BaseCommon.CacheInsert(cacheKey, list, DateTime.Now.AddMinutes(5));
                return list;
            }
            return null;
        }

        /// <summary>
        /// 根据节点ID获得对应的药方内容
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        public static Sys_Point GetSysPoint(int moduleId)
        {
            string cacheKey = string.Format(@"GetMedicine_{0}", moduleId);
            if (BaseCommon.HasCache(cacheKey))
            {
                return BaseCommon.GetCache<Sys_Point>(cacheKey);
            }

            Sys_Point point = Sys_Point.SingleOrDefault("where ModuleId=@0", moduleId);

            if (null != point)
            {
                BaseCommon.CacheInsert(cacheKey, point, DateTime.Now.AddMinutes(5));
                return point;
            }
            return new Sys_Point();

        }
        public static Medicine GetMedicineFromContent(string content)
        {
            try
            {
                Medicine m = BaseCommon.JsonToObject<Medicine>(content);
                return m;
            }
            catch
            {
                return new Medicine();
            }
        }

        /// <summary>
        /// 是否有同意免责声明 缓存30分钟
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static bool IsHaveAgreeExceptions(int userid,ExceptionsType et)
        {
            string cacheKey = string.Format(@"IsHaveAgreeExceptions_{0}_{1}", userid,et.ToString());
            if (BaseCommon.HasCache(cacheKey))
            {
                return BaseCommon.GetCache<bool>(cacheKey);
            }
            UserExceptionsRecord ue = UserExceptionsRecord.SingleOrDefault(@"where UserId=@0 and ExceptionsType=@1", userid, (int)et);
            if (null != ue && ue.Id > 0)
            {
                BaseCommon.CacheInsert(cacheKey, true, DateTime.Now.AddMinutes(30));
                return true;
            }
            else
                return false;
        }
    }
}
