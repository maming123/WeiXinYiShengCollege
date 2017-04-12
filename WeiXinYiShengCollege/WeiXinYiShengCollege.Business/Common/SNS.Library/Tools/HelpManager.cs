using System;
using System.Data;
using SNS.Library.Database;

/****************************
* �����ƣ�HelpManager
*   ���ܣ�ϵͳ����������
*     by��Lining
*   ���ڣ�2006-3-27
****************************/

namespace SNS.Library.Tools
{
    public class HelpManager
    {
        #region ��̬��������

        /// <summary>
        /// ����ģ���ŵõ�����ҳ���Url
        /// </summary>
        /// <param name="moduleID">ģ����</param>
        /// <returns>����ҳ���Url</returns>
        public static string FindHelpUrlByModuleID(string moduleID)
        {
            string strSql = "SELECT I.Type_ID,I.Info_ID FROM Info_Detail I,Sys_Module S WHERE I.Info_Title=S.Module_Name " +
                "AND S.Module_ID=" + moduleID + " AND I.Type_ID<>" + moduleID;
            DataTable dtQueryResult = DatabaseFactory.ExecuteQuery(strSql);

            if (dtQueryResult.Rows.Count > 0)
            {
                return "InfoModule/InforManage/InforListShow.aspx?Type_ID=" + dtQueryResult.Rows[0][0].ToString() +
                    "&Info_ID=" + dtQueryResult.Rows[0][1].ToString();
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// ����ģ���ŵõ��б����ҳ���Url
        /// </summary>
        /// <param name="moduleID">ģ����</param>
        /// <returns>�б����ҳ���Url</returns>
        public static string FindListHelpUrlByModuleID(string moduleID)
        {
            string strSql = "SELECT Module_ID FROM Sys_Module WHERE Link_Type=3 " +
                "AND Module_Name=(SELECT Module_Name FROM Sys_Module WHERE Module_ID=" + moduleID + ")";
            object objModuleID = DatabaseFactory.ExecuteScalar(strSql);

            if (objModuleID != null)
            {
                return "InfoModule/InforManage/InforList.aspx?Type_id=" + objModuleID.ToString() + "&ModuleID=" + objModuleID.ToString();
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// �õ�ģ���������ʾ�ļ��б�
        /// </summary>
        /// <param name="moduleID">ģ����</param>
        /// <returns>������ʾ�ļ��б�</returns>
        public static DataTable FindModuleLiveDemo(string moduleID)
        {
            string strSql = "SELECT ChildModule_Name,Flash_Name FROM Module_LiveDemo WHERE Module_ID=" + moduleID;
            return DatabaseFactory.ExecuteQuery(strSql);
        }

        #endregion ������̬��������
    }
}
