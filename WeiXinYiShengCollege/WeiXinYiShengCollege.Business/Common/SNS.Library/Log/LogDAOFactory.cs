using System;


			/*************************************
			 * �����ƣ�LogDAOFactory
			 *   ���ܣ���������ϵͳ��־�����ݷ���
			 *		   ����Ĺ���
			 *     by��Lining
			 *   ���ڣ�2004-11-30
			 *   ��ע�����ܼ̳д���
			 *************************************/


namespace SNS.Library.Logs
{
	/// <summary>
	/// ��������ϵͳ��־�����ݷ��ʶ���Ĺ�����
	/// </summary>
	public sealed class LogDAOFactory
    {
        #region ������̬����

        /// <summary>
		/// ��������ϵͳ��־�����ݷ��ʶ���
		/// </summary>
		/// <returns>����ϵͳ��־�����ݷ��ʶ���</returns>
		public static LogDAO CreateObject()
		{
			return new LogDAOImplSQLServer();
		}

        /// <summary>
        /// д��־
        /// </summary>
        /// <param name="description">��־����</param>
        public static void Write(string description)
        {
            Write(description, LogType.Operation);
        }

        /// <summary>
        /// д��־
        /// </summary>
        /// <param name="description">��־����</param>
        /// <param name="type">��־����</param>
        public static void Write(string description,LogType type)
        {
            string strUserName = "";

            if (System.Web.HttpContext.Current.Session["SStaffNumber"] != null)
            {
                strUserName = System.Web.HttpContext.Current.Session["SStaffNumber"].ToString();
            }

            string strModuleName = "";
            if (System.Web.HttpContext.Current.Session["ModuleName"] != null)
            {
                strModuleName = System.Web.HttpContext.Current.Session["ModuleName"].ToString();
            }

            Write(description, strModuleName, strUserName, type);
        }

        /// <summary>
        /// д��־
        /// </summary>
        /// <param name="description">��־����</param>
        /// <param name="moduleName">ģ������</param>
        /// <param name="userName">�û�����</param>
        /// <param name="type">��־����</param>
        public static void Write(string description, string moduleName, string userName, LogType type)
        {
            Log log = new Log();
            log.Description = description;
            log.DateTime = DateTime.Now.ToShortTimeString();
            log.ModuleName = moduleName;
            log.Info = userName;
            log.Type = type;

            CreateObject().Write(log);
        }

        #endregion ������̬��������
    }
}
