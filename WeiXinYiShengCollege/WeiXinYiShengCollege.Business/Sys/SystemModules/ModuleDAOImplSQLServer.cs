using System;
using SNS.Library.Database;


			/*************************************
			 * 类名称：ModuleDAOImplSQLServer
			 *   功能：管理系统模块的数据访问对象
			 *         的SQLServer版本
			 *     by：Lining
			 *   日期：2004-10-26
			 *   备注：继承抽象类ModuleDAO
			 ************************************/


namespace SNS.Library.SystemModules
{
    /// <summary>
    /// 管理系统模块的数据访问对象的SQLServer版本。此类为内部类。
    /// </summary>
    internal class ModuleDAOImplSQLServer : ModuleDAO
    {
        /// <summary>
        /// 创建管理系统模块的数据访问对象
        /// </summary>
        public ModuleDAOImplSQLServer()
        {
        }


        #region 实现抽象方法

        /// <summary>
        /// 添加模块
        /// </summary>
        /// <param name="module">模块对象</param>
        /// <returns>模块编号</returns>
        public override int Add(Module module)
        {
            string strSql = "INSERT INTO Sys_Module(MODULE_NAME,PARENT_MODULE_ID,IS_DISPLAY,Link_Path," +
                "Link_Type,Link_Target,Create_Table,Template_ID,Business_Table{0},Is_Original_Data,Creator_Area_Code) VALUES('" + module.Name + "'," +
                module.ParentID + "," + (module.Display ? 1 : 0) + ",'" + module.LinkPath +
                "','" + module.LinkType + "' ,'" + module.LinkTarget + "'," + (module.IsCreateTable ? 1 : 0) +
                ",'" + module.TemplateID + "','" + module.BusinessTableName + "'{1}," + (module.IsOriginalData ? "1" : "0") + ",'" + module.CreatorAreaCode + "')";
            if (module.IsSystemFile)
            {
                strSql = string.Format(strSql, ",Is_Sys_File", ",1");
            }
            else
            {
                strSql = string.Format(strSql, "", "");
            }
            return DatabaseFactory.ExecuteInsertReturnPK(strSql, "Sys_Module");
        }

        /// <summary>
        /// 得到所有业务相关的模块
        /// </summary>
        /// <returns>包含模块信息的DataTable</returns>
        public override System.Data.DataTable FindBusinessDataMoudle()
        {
            string strSql = "SELECT Module_ID,Module_Name,T.TableIndex AS Business_Table " +
                "FROM Sys_Module S1,SysObjects S2,TIT_Tables T " +
                "WHERE S1.Module_ID=T.ModuleID AND T.TableIndex=S2.Name AND S1.Is_Display=1";
            return DatabaseFactory.ExecuteQuery(strSql);
        }

        #endregion 结束实现抽象方法
    }
}
