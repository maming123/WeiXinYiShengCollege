using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Module.DataAccess
{
    public class SqlHelper
    {
        #region sql语句执行
        /// <summary>
        /// 获取第一行的所有元素
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="OPS"></param>
        /// <param name="connStr"></param>
        /// <returns></returns>
        public static object[] GetReader(string sql, SqlParameter[] OPS, string connStr)
        {
            object[] obj = null;

            #region sql语句处理
            if (sql.Substring(sql.Length - 1) != ";")
                sql += ";";
            sql = "begin " + sql + " end;";
            #endregion

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    if (OPS != null)
                    {
                        foreach (SqlParameter OP in OPS)
                        {
                            cmd.Parameters.Add(((ICloneable)OP).Clone());
                        }
                    }
                    SqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        obj = new object[sdr.FieldCount];
                        for (int i = 0; i < obj.Length; i++)
                        {
                            obj[i] = sdr[i];
                        }
                    }
                    sdr.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                    //SqlConnection.ClearPool(conn);
                }
            }
            return obj;
        }

        /// <summary>
        /// 返回指定查询的结果
        /// </summary>
        /// <param name="sql">输入的sql语句</param>
        /// <param name="OPS">参数</param>
        /// <returns>返回的结果</returns>
        public static object GetObject(string sql, SqlParameter[] OPS, string connStr)
        {
            object obj = null;
            if (sql == "")
                return obj;

            #region sql语句处理
            if (sql.Substring(sql.Length - 1) != ";")
                sql += ";";
            sql = "begin " + sql + " end;";
            #endregion

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlTransaction OT = conn.BeginTransaction();
                try
                {

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    if (OPS != null)
                    {
                        foreach (SqlParameter OP in OPS)
                        {
                            cmd.Parameters.Add(((ICloneable)OP).Clone());
                        }
                    }
                    cmd.Transaction = OT;
                    obj = cmd.ExecuteScalar();
                    OT.Commit();
                }
                catch (Exception ex)
                {
                    OT.Rollback();
                    throw ex;
                }
                finally
                {
                    conn.Close();
                    //SqlConnection.ClearPool(conn);
                }
            }
            return obj;
        }

        /// <summary>
        /// 带参数的sql执行方式
        /// </summary>
        /// <param name="sql">执行的sql语句</param>
        /// <param name="OPS">参数</param>
        public static int ExecuteSql(string sql, SqlParameter[] OPS, string connStr)
        {


            int i = -1;
            if (sql == "")
                return i;

            #region sql语句处理
            if (sql.Substring(sql.Length - 1) != ";")
                sql += ";";
            sql = "begin " + sql + " end;";
            #endregion

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlTransaction OT = conn.BeginTransaction();
                try
                {

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    if (OPS != null)
                    {
                        foreach (SqlParameter OP in OPS)
                        {
                            cmd.Parameters.Add(((ICloneable)OP).Clone());
                        }
                    }
                    cmd.Transaction = OT;
                    i = cmd.ExecuteNonQuery();
                    OT.Commit();
                }
                catch (Exception ex)
                {
                    OT.Rollback();
                    throw ex;
                }
                finally
                {
                    conn.Close();
                    //SqlConnection.ClearPool(conn);
                }
            }
            return i;
        }

        /// <summary>
        /// 带参数的sql执行方式
        /// </summary>
        /// <param name="sql">执行的sql语句</param>
        /// <param name="OPS">参数</param>
        public static DataTable GetData(string sql, SqlParameter[] OPS, string connStr)
        {
            #region sql语句处理
            if (sql.Substring(sql.Length - 1) == ";")
                sql = sql.Substring(0, sql.Length - 1);
            #endregion

            DataTable data = new DataTable();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                try
                {

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    if (OPS != null)
                    {
                        foreach (SqlParameter OP in OPS)
                        {
                            cmd.Parameters.Add(((ICloneable)OP).Clone());
                        }
                    }
                    SqlDataAdapter oda = new SqlDataAdapter(cmd);
                    oda.Fill(data);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                    //SqlConnection.ClearPool(conn);
                }
            }
            return data;
        }

        /// <summary>
        /// 带参数的sql执行方式
        /// </summary>
        /// <param name="sql">执行的sql语句</param>
        /// <param name="OPS">参数</param>
        public static DataSet GetDataSet(string sql, SqlParameter[] OPS, string connStr)
        {
            #region sql语句处理
            if (sql.Substring(sql.Length - 1) == ";")
                sql = sql.Substring(0, sql.Length - 1);
            #endregion

            DataSet data = new DataSet();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                try
                {

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    if (OPS != null)
                    {
                        foreach (SqlParameter OP in OPS)
                        {
                            cmd.Parameters.Add(((ICloneable)OP).Clone());
                        }
                    }
                    SqlDataAdapter oda = new SqlDataAdapter(cmd);
                    oda.Fill(data);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                    //SqlConnection.ClearPool(conn);
                }
            }
            return data;
        }

        public static void ExcuteSqlWithOutPut(string sql, SqlParameter[] paras, string connStr, string outputName, out object outputObject)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = sql;
                    cmd.Connection = conn;

                    if (paras != null)
                    {
                        foreach (SqlParameter OP in paras)
                        {
                            cmd.Parameters.Add(((ICloneable)OP).Clone());
                        }
                    }
                    cmd.ExecuteNonQuery();

                    outputObject = cmd.Parameters[outputName].Value;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        #endregion

        #region 存储过程调用的方法

        /// <summary>
        /// 返回指定查询的结果
        /// </summary>
        /// <param name="sql">输入的sql语句</param>
        /// <param name="OPS">参数</param>
        /// <returns>返回的结果</returns>
        public static object GetObjectByPro(string sql, SqlParameter[] OPS, string connStr)
        {

            object obj = null;
            if (sql == "")
                return obj;


            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlTransaction OT = conn.BeginTransaction();
                try
                {

                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = sql;
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (OPS != null)
                    {
                        foreach (SqlParameter OP in OPS)
                        {
                            cmd.Parameters.Add(((ICloneable)OP).Clone());
                        }
                    }
                    cmd.Transaction = OT;
                    obj = cmd.ExecuteScalar();
                    OT.Commit();
                }
                catch (Exception ex)
                {
                    OT.Rollback();
                    throw ex;
                }
                finally
                {
                    conn.Close();
                    //SqlConnection.ClearPool(conn);
                }
            }
            return obj;
        }


        /// <summary>
        /// 带参数的sql执行方式
        /// </summary>
        /// <param name="sql">执行的sql语句</param>
        /// <param name="OPS">参数</param>
        public static int ExecuteSqlByPro(string sql, SqlParameter[] OPS, string connStr)
        {


            int i = -1;
            if (sql == "")
                return i;


            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlTransaction OT = conn.BeginTransaction();
                try
                {

                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = sql;
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (OPS != null)
                    {
                        foreach (SqlParameter OP in OPS)
                        {
                            cmd.Parameters.Add(((ICloneable)OP).Clone());
                        }
                    }
                    cmd.Transaction = OT;
                    i = cmd.ExecuteNonQuery();
                    OT.Commit();
                }
                catch (Exception ex)
                {
                    OT.Rollback();
                    throw ex;
                }
                finally
                {
                    conn.Close();
                    //SqlConnection.ClearPool(conn);
                }
            }
            return i;
        }


        /// <summary>
        /// 带参数的sql执行方式
        /// </summary>
        /// <param name="sql">执行的sql语句</param>
        /// <param name="OPS">参数</param>
        public static int ExecuteProNotLimitTimeNotTransaction(string sql, SqlParameter[] OPS, string connStr)
        {
            int i = -1;
            if (sql == "")
                return i;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                try
                {

                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandTimeout = 0;
                    cmd.CommandText = sql;
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (OPS != null)
                    {
                        foreach (SqlParameter OP in OPS)
                        {
                            cmd.Parameters.Add(((ICloneable)OP).Clone());
                        }
                    }
                    i = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                    //SqlConnection.ClearPool(conn);
                }
            }
            return i;
        }

        /// <summary>
        /// 带参数的sql执行方式
        /// </summary>
        /// <param name="sql">执行的sql语句</param>
        /// <param name="OPS">参数</param>
        public static DataTable GetDataByPro(string sql, SqlParameter[] OPS, string connStr)
        {
            DataTable data = new DataTable();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                try
                {

                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = sql;
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (OPS != null)
                    {
                        foreach (SqlParameter OP in OPS)
                        {
                            cmd.Parameters.Add(((ICloneable)OP).Clone());
                        }
                    }
                    SqlDataAdapter oda = new SqlDataAdapter(cmd);
                    oda.Fill(data);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                    //SqlConnection.ClearPool(conn);
                }
            }
            return data;
        }

        /// <summary>
        /// 带参数的sql执行方式
        /// </summary>
        /// <param name="sql">执行的sql语句</param>
        /// <param name="OPS">参数</param>
        public static DataSet GetDataSetByPro(string sql, SqlParameter[] OPS, string connStr)
        {
            DataSet data = new DataSet();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                try
                {

                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = sql;
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (OPS != null)
                    {
                        foreach (SqlParameter OP in OPS)
                        {
                            cmd.Parameters.Add(((ICloneable)OP).Clone());
                        }
                    }
                    SqlDataAdapter oda = new SqlDataAdapter(cmd);
                    oda.Fill(data);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                    //SqlConnection.ClearPool(conn);
                }
            }
            return data;
        }


        /// <summary>
        /// 根据输入的字符串获取类型
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static SqlDbType GetDBType(string t)
        {
            SqlDbType dbt = SqlDbType.Variant;
            switch (t.ToLower())
            {
                case "int":
                    dbt = SqlDbType.Int;
                    break;
                case "string":
                    dbt = SqlDbType.VarChar;
                    break;
            }
            return dbt;
        }

        /// <summary>
        /// 使用存储过程插入数据
        /// </summary>
        /// <param name="sql">执行的sql语句</param>
        /// <param name="OPS">参数</param>
        /// <param name="connStr">数据库连接字符串</param>
        public static int InsertDataByPro(string sql, SqlParameter[] OPS, string connStr)
        {
            int i = -1;
            if (sql == "")
                return i;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlTransaction OT = conn.BeginTransaction();
                try
                {

                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = sql;
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (OPS != null)
                    {
                        foreach (SqlParameter OP in OPS)
                        {
                            cmd.Parameters.Add(((ICloneable)OP).Clone());
                        }
                    }
                    cmd.Transaction = OT;
                    Object o = cmd.ExecuteScalar();
                    i = decimal.ToInt32((decimal)o);
                    OT.Commit();
                }
                catch (Exception ex)
                {
                    OT.Rollback();
                    throw ex;
                }
                finally
                {
                    conn.Close();
                    //SqlConnection.ClearPool(conn);
                }
            }
            return i;
        }

        #endregion


        /// <summary>
        /// 带参数的sql执行方式
        /// </summary>
        /// <param name="sql">执行的sql语句</param>
        /// <param name="OPS">参数</param>
        public static int AlterDataBase(string sql, string connStr)
        {
            int i = -1;
            if (sql == "")
                return i;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                try
                {
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    i = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                    //SqlConnection.ClearPool(conn);
                }
            }
            return i;
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="OPS"></param>
        /// <returns></returns>
        public static DataSet fn_GetPaginationData(string strSql, SqlParameter[] OPS, string connStr)
        {
            DataSet data = new DataSet();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                try
                {

                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = strSql;
                    cmd.Connection = conn;

                    if (OPS != null)
                    {
                        foreach (SqlParameter OP in OPS)
                        {
                            cmd.Parameters.Add(((ICloneable)OP).Clone());
                        }
                    }
                    SqlDataAdapter oda = new SqlDataAdapter(cmd);
                    oda.Fill(data);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                    //SqlConnection.ClearPool(conn);
                }
            }
            return data;
        }


        /// <summary>
        /// 执行存储过程，并根据输出参数进行返回
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="OPS"></param>
        /// <param name="connStr"></param>
        /// <param name="outPutParas"></param>
        /// <returns></returns>
        public static string[] ExecuteProWithOutPutParas(string sql, SqlParameter[] OPS, string connStr, string[] outPutParas)
        {


            string[] results = null;
            if (sql == "")
                return results;


            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlTransaction OT = conn.BeginTransaction();
                try
                {

                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = sql;
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (OPS != null)
                    {
                        foreach (SqlParameter OP in OPS)
                        {
                            cmd.Parameters.Add(((ICloneable)OP).Clone());
                        }
                    }
                    cmd.Transaction = OT;
                    cmd.ExecuteNonQuery();
                    if (outPutParas != null)
                    {
                        if (outPutParas.Length != 0)
                        {
                            results = new string[outPutParas.Length];
                            for (int i = 0; i < outPutParas.Length; i++)
                            {
                                results[i] = cmd.Parameters[outPutParas[i]].Value.ToString();
                            }
                        }
                    }
                    OT.Commit();
                }
                catch (Exception ex)
                {
                    OT.Rollback();
                    throw ex;
                }
                finally
                {
                    conn.Close();
                    //SqlConnection.ClearPool(conn);
                }
            }
            return results;
        }

        /// <summary>
        /// 通过存储过程获取DataTable并返回一个Output
        /// </summary>
        /// <param name="sql">存储过程名</param>
        /// <param name="OPS">参数</param>
        /// <param name="connStr">连接字符串</param>
        /// <param name="outputName">输出参数名</param>
        /// <param name="outputObject">输出对象引用</param>
        /// <returns></returns>
        public static DataTable GetDataByProAndOutput(string sql, SqlParameter[] OPS, string connStr, string outputName, out object outputObject)
        {
            DataTable data = new DataTable();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = sql;
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (OPS != null)
                    {
                        foreach (SqlParameter OP in OPS)
                        {
                            cmd.Parameters.Add(((ICloneable)OP).Clone());
                        }
                    }
                    SqlDataAdapter oda = new SqlDataAdapter(cmd);
                    oda.Fill(data);

                    outputObject = cmd.Parameters[outputName].Value;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }
            return data;
        }


        /// <summary>
        /// 通过存储过程获取DataTable并返回一个Output
        /// </summary>
        /// <param name="sql">存储过程名</param>
        /// <param name="OPS">参数</param>
        /// <param name="connStr">连接字符串</param>
        /// <param name="outputName">输出参数名</param>
        /// <param name="outputObject">输出对象引用</param>
        /// <returns></returns>
        public static DataSet GetDataSetByProAndOutput(string sql, SqlParameter[] OPS, string connStr, string outputName, out object outputObject)
        {
            DataSet data = new DataSet();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = sql;
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (OPS != null)
                    {
                        foreach (SqlParameter OP in OPS)
                        {
                            cmd.Parameters.Add(((ICloneable)OP).Clone());
                        }
                    }
                    SqlDataAdapter oda = new SqlDataAdapter(cmd);
                    oda.Fill(data);

                    outputObject = cmd.Parameters[outputName].Value;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }
            return data;
        }
    }
}