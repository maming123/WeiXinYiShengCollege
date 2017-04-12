using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Module.DataAccess
{

        /// <summary>
        /// 新增数据库时，请在FetionActivity数据库中的DataBaseList表中增加记录，并在本类中增加相应的Field
        /// </summary>
        public static partial class ConnectionStrings
        {
            /// <summary>
            /// 从数据库中获取数据库相应连接字符串
            /// </summary>
            /// <param name="dbName"></param>
            /// <returns></returns>
            public static string GetConnectString(string dbKey)
            {
                return ConfigurationManager.ConnectionStrings[dbKey].ConnectionString;
            }

            public static string ProviderName = "System.Data.SqlClient";

            public static readonly string Core = ConfigurationManager.ConnectionStrings["Core"].ConnectionString;

        }
    
}