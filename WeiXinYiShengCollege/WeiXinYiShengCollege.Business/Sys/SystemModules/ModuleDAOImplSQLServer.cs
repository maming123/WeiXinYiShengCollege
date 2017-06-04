using System;
using SNS.Library.Database;


			/*************************************
			 * �����ƣ�ModuleDAOImplSQLServer
			 *   ���ܣ�����ϵͳģ������ݷ��ʶ���
			 *         ��SQLServer�汾
			 *     by��Lining
			 *   ���ڣ�2004-10-26
			 *   ��ע���̳г�����ModuleDAO
			 ************************************/


namespace SNS.Library.SystemModules
{
    /// <summary>
    /// ����ϵͳģ������ݷ��ʶ����SQLServer�汾������Ϊ�ڲ��ࡣ
    /// </summary>
    internal class ModuleDAOImplSQLServer : ModuleDAO
    {
        /// <summary>
        /// ��������ϵͳģ������ݷ��ʶ���
        /// </summary>
        public ModuleDAOImplSQLServer()
        {
        }


        #region ʵ�ֳ��󷽷�

        /// <summary>
        /// ���ģ��
        /// </summary>
        /// <param name="module">ģ�����</param>
        /// <returns>ģ����</returns>
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
        /// �õ�����ҵ����ص�ģ��
        /// </summary>
        /// <returns>����ģ����Ϣ��DataTable</returns>
        public override System.Data.DataTable FindBusinessDataMoudle()
        {
            string strSql = "SELECT Module_ID,Module_Name,T.TableIndex AS Business_Table " +
                "FROM Sys_Module S1,SysObjects S2,TIT_Tables T " +
                "WHERE S1.Module_ID=T.ModuleID AND T.TableIndex=S2.Name AND S1.Is_Display=1";
            return DatabaseFactory.ExecuteQuery(strSql);
        }

        #endregion ����ʵ�ֳ��󷽷�
    }
}
