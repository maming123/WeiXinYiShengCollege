using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;

namespace Module.Utils
{
    public class TypeConvert
    {
        /// <summary>
        /// 匿名类型转换
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="anonymous"></param>
        /// <param name="anonymousType"></param>
        /// <returns></returns>
        public static T CastAnonymous<T>(object anonymous, T anonymousType)
        {
            return (T)anonymous;
        }

        /// <summary>
        /// DataTable.Rows[0] 转换为 Model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static T ToModel<T>(DataTable dt) where T : class, new()
        {
            if (dt == null || dt.Rows.Count <= 0)
            {
                return null;
            }

            //创建一个属性的列表
            List<PropertyInfo> prlist = new List<PropertyInfo>();
            //获得T 的所有的Public 属性 并找出T属性和DataTable的列名称相同的属性(PropertyInfo) 并加入到属性列表 
            Array.ForEach<PropertyInfo>(GetProperties<T>(), (p) =>
            {
                if (dt.Columns.IndexOf(p.Name) != -1)
                    prlist.Add(p);
            });

            //创建T的实例
            T ob = new T();
            foreach (DataRow row in dt.Rows)
            {
                //找到对应的数据并赋值
                prlist.ForEach((p) =>
                {
                    if (row[p.Name] != DBNull.Value)
                        p.SetValue(ob, row[p.Name], null);
                });
                break;
            }
            return ob;
        }

        /// <summary>
        /// DataTable 转换为List 集合
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="dt">DataTable</param>
        /// <returns></returns>
        public static List<T> ToList<T>(DataTable dt) where T : class, new()
        {
            List<PropertyInfo> prlist = new List<PropertyInfo>();
            Array.ForEach<PropertyInfo>(GetProperties<T>(), (p) =>
            {
                if (dt.Columns.IndexOf(p.Name) != -1)
                    prlist.Add(p);
            });
            List<T> oblist = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T ob = new T();
                prlist.ForEach((p) =>
                {
                    if (row[p.Name] != DBNull.Value)
                        p.SetValue(ob, row[p.Name], null);
                });
                oblist.Add(ob);
            }
            return oblist;
        }

        /// <summary>
        /// 缓存类型属性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private static PropertyInfo[] GetProperties<T>() where T : class, new()
        {
            Type t = typeof(T);

            string key = t.FullName;

            PropertyInfo[] pt = BaseCommon.GetCache<PropertyInfo[]>(key);

            if (pt == null)
            {
                pt = t.GetProperties();
                BaseCommon.CacheInsert(key, pt, TimeSpan.FromHours(1));
            }

            return pt;
        }
    }
}