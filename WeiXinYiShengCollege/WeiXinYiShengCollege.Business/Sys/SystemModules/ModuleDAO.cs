using System;
using System.Data;
using System.Collections;
using SNS.Library.Database;


			/************************************
			 * �����ƣ�ModuleDAO
			 *   ���ܣ�����ϵͳģ������ݷ��ʶ���
			 *     by��Lining
			 *   ���ڣ�2004-10-26
			 ***********************************/


namespace SNS.Library.SystemModules
{
	/// <summary>
	/// �ṩ����ϵͳģ��ķ���������Ϊ�����ࡣ
	/// </summary>
	public abstract class ModuleDAO
	{
		#region ˽�з���

		/// <summary>
		/// ��ѯָ��ģ���������ģ��
		/// </summary>
		/// <param name="parentModuleID">��ģ����</param>
		/// <param name="modules">ģ�鼯��</param>
		/// <param name="parentLevel">����ģ��ļ���</param>
		private void FindAllChildModules(int parentModuleID,ArrayList modules,int parentLevel)
		{
			string strSql = "SELECT M.MODULE_ID,M.MODULE_NAME,M.LINK_ID,M.IS_DISPLAY,L.APP_PATH FROM Sys_Module M,"+
				"SYS_LINK_OBJECT L WHERE M.PARENT_MODULE_ID=" + parentModuleID + " AND M.LINK_ID=L.LINK_ID ORDER BY M.ORDER_ID";
			DataTable dtChildModules = DatabaseFactory.ExecuteQuery(strSql);
			if(dtChildModules.Rows.Count > 0)
			{
				foreach(DataRow drChildModule in dtChildModules.Rows)
				{
					Module module = new Module();
					module.ID = int.Parse(drChildModule[0].ToString());
					module.Name = drChildModule[1].ToString();
					module.Level = parentLevel + 1;
					module.LinkPath = drChildModule["Link_Path"].ToString();
					module.Display = Convert.ToInt32(drChildModule["IS_DISPLAY"]) == 1 ? true : false; 
					modules.Add(module);
					FindAllChildModules(module.ID,modules,parentLevel + 1);
				}
			}
		}

		/// <summary>
		/// ��������Ų�ѯ
		/// </summary>
		/// <param name="parentID">�������</param>
		/// <returns>ģ�鼯��</returns>
		private ArrayList FindByParentID(int parentID)
		{
			string strSql = "SELECT M.MODULE_ID,M.MODULE_NAME,M.LINK_ID,M.IS_DISPLAY,L.APP_PATH FROM Sys_Module M,"+
				"SYS_LINK_OBJECT L WHERE M.LINK_ID=L.LINK_ID AND PARENT_MODULE_ID=" + parentID + " ORDER BY M.ORDER_ID";
			DataTable dtModules = DatabaseFactory.ExecuteQuery(strSql);

			if(dtModules.Rows.Count > 0)
			{
				ArrayList modules = new ArrayList(dtModules.Rows.Count);
				foreach(DataRow drModule in dtModules.Rows)
				{
					Module module = new Module();
					module.ID = Convert.ToInt32(drModule["MODULE_ID"]);
					module.ParentID = parentID;
					module.Name = drModule["MODULE_NAME"].ToString();
					module.LinkPath = drModule["APP_PATH"].ToString();
					module.Display = Convert.ToInt32(drModule["IS_DISPLAY"]) == 1 ? true : false; 
					modules.Add(module);
				}
				return modules;
			}
			else
			{
				return null;
			}
		}

		#endregion ����˽�з���


		#region ��������

		/// <summary>
		/// ��ѯ����ϵͳģ��
		/// </summary>
		/// <returns>ģ�鼯��</returns>
		public ArrayList FindAll()
		{
			ArrayList rootModules = FindRootModules();
			if(rootModules != null)
			{
				ArrayList modules = new ArrayList();
				foreach(object objModule in rootModules)
				{
					Module module = (Module)objModule;
					modules.Add(module);
					FindAllChildModules(module.ID,modules,1);
				}
				return modules;
			}
			else
			{
				return null;
			}
		}

        /// <summary>
        /// ���ո���ģ���Ų�ѯ
        /// </summary>
        /// <param name="parentModuleID">����ģ����</param>
        /// <param name="isFindNotDisplayModule">�Ƿ��������ʾ��ģ��</param>
        /// <param name="loginUserID">�û����</param>
        /// <returns>��ģ�鼯��</returns>
        public ArrayList FindByParentModuleID(int parentModuleID, bool isFindNotDisplayModule, string loginUserID)
        {
            using (IDatabase database = DatabaseFactory.CreateObject())
            {
                string strSql = "SELECT Module_ID,Module_Name,ISNULL(Link_Path,'') AS Link_Path," +
                    "Link_Type,ISNULL(Link_Target,'') AS Link_Target,ISNULL(Template_ID,'') AS Template_ID,Business_Table,ISNULL(Is_Sys_File,'FALSE') AS Is_Sys_File " +
                    "FROM Sys_Module WHERE Parent_Module_ID=" + parentModuleID;
                if (!isFindNotDisplayModule)
                {
                    strSql += " AND Is_Display=1";
                }
                if (loginUserID != null && loginUserID.Trim() != "")
                {
                    strSql += " AND EXISTS(SELECT U2.User_ID FROM FU_User_Right U1,FU_Group_User U2 " +
                        "WHERE U1.User_ID=U2.Group_ID AND U2.User_ID=" + loginUserID + " AND U1.Module_ID=Sys_Module.Module_ID)";

                    //�õ��û���ַ�룬����ƥ�䱾�����ܿ�����ģ��
                    string strTempSql = "SELECT A.Area_Code FROM Area A,U_User U WHERE A.Area_ID=U.Org_ID AND U.User_ID=" + loginUserID;
                    object objAreaCode = database.ExecuteScalar(strTempSql);
                    if (!Convert.IsDBNull(objAreaCode) && objAreaCode != null)
                    {
                        strSql += " AND CAST(Creator_Area_Code AS int)<=" + objAreaCode.ToString();
                        strSql += " AND (LEFT(Creator_Area_Code,2)='" + objAreaCode.ToString().Substring(0, 2) + "' OR CAST(Creator_Area_Code AS int)=0)";
                    }
                }

                strSql += " ORDER BY Order_ID";
                DataTable dtModules = database.ExecuteQuery(strSql);

                if (dtModules.Rows.Count > 0)
                {
                    ArrayList modules = new ArrayList();
                    foreach (DataRow drModule in dtModules.Rows)
                    {
                        Module module = new Module();
                        module.ID = Convert.ToInt32(drModule["MODULE_ID"]);
                        module.Name = drModule["MODULE_NAME"].ToString();
                        module.LinkType = drModule["Link_Type"].ToString();
                        module.LinkPath = drModule["Link_Path"].ToString();
                        module.LinkTarget = drModule["Link_Target"].ToString();
                        module.TemplateID = drModule["Template_ID"].ToString();
                        if (!Convert.IsDBNull(drModule["Business_Table"]))
                        {
                            module.BusinessTableName = drModule["Business_Table"].ToString();
                        }
                        module.IsSystemFile = Convert.ToBoolean(drModule["Is_Sys_File"]);
                        modules.Add(module);
                    }
                    return modules;
                }
                else
                {
                    return null;
                }
            }
        }

		/// <summary>
		/// ���ո���ģ���Ų�ѯ
		/// </summary>
		/// <param name="parentModuleID">����ģ����</param>
        /// <param name="isFindNotDisplayModule">�Ƿ��������ʾ��ģ��</param>
		/// <returns>��ģ�鼯��</returns>
		public ArrayList FindByParentModuleID(int parentModuleID,bool isFindNotDisplayModule)
		{
            return this.FindByParentModuleID(parentModuleID, isFindNotDisplayModule, "");
		}

        		/// <summary>
		/// ���ո���ģ���Ų�ѯ
		/// </summary>
		/// <param name="parentModuleID">����ģ����</param>
		/// <returns>��ģ�鼯��</returns>
        public ArrayList FindByParentModuleID(int parentModuleID)
        {
            //maming -Edit 2006-5-25,true----->false ��ֻ��ʾ���ݿ���IsDisplay==1�Ľڵ�
            return this.FindByParentModuleID(parentModuleID, true);
        }

		/// <summary>
		/// �õ����п�����ʾ����ģ��
		/// </summary>
		/// <returns>��ģ���б�</returns>
		public ArrayList FindAllDisplayChildModules()
		{
			string strSql = "SELECT * FROM (SELECT M.Module_ID,M.Module_Name,M.Parent_Module_ID," +
				"M.Is_Display,L.App_Path,M.Order_ID FROM Sys_Module M " +
				"LEFT JOIN Sys_Link_Object L ON M.Link_ID=L.Link_ID) A WHERE Is_Display=1 ORDER BY Order_ID";
			 DataTable dtModules = DatabaseFactory.ExecuteQuery(strSql);
			
			if(dtModules.Rows.Count > 0)
			{
				ArrayList childModules = new ArrayList();
				foreach(DataRow drModule in dtModules.Rows)
				{
					Module childModule = new Module();
					childModule.ID = Convert.ToInt32(drModule["MODULE_ID"]);
					childModule.Name = drModule["MODULE_NAME"].ToString();
					childModule.ParentID = Convert.ToInt32(drModule["PARENT_MODULE_ID"].ToString());
					childModule.LinkPath = drModule["App_Path"].ToString();
					childModules.Add(childModule);
				}
				return childModules;
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// ��ѯָ��ģ���������ģ��
		/// </summary>
		/// <param name="rootModuleID">��ģ����</param>
		/// <returns>��ģ�鼯��</returns>
		public ArrayList FindAllChildModules(int rootModuleID)
		{
			ArrayList childModules = this.FindByParentID(rootModuleID);
			if(childModules != null)
			{
				ArrayList modules = new ArrayList();
				foreach(object objModule in childModules)
				{
					Module module = (Module)objModule;
					module.Level = 2;
					modules.Add(module);
					FindAllChildModules(module.ID,modules,2);
				}
				return modules;
			}
			return null;
		}

		/// <summary>
		/// ��ѯ��ģ��
		/// </summary>
        /// <param name="isFineAllModule">�Ƿ��������ģ��</param>
		/// <returns>ϵͳģ��ļ���</returns>
		public ArrayList FindRootModules(bool isFineAllModule)
		{
            string strSql = "SELECT MODULE_ID,MODULE_NAME,IS_DISPLAY,Link_Path," +
                "ISNULL(Banner_Button_Width,0) AS Banner_Button_Width,ISNULL(Link_Target,'') AS LinkTarget,Link_Type " +
                "FROM Sys_Module WHERE PARENT_MODULE_ID=0";
            if (!isFineAllModule)
            {
                strSql += " AND Link_Type=1 AND Is_Display=1 ";
            }
            strSql += " ORDER BY ORDER_ID";
			DataTable dtModules = DatabaseFactory.ExecuteQuery(strSql);

			if(dtModules.Rows.Count > 0)
			{
				ArrayList modules = new ArrayList(dtModules.Rows.Count);
				foreach(DataRow drModule in dtModules.Rows)
				{
					Module module = new Module();
					module.ID = Convert.ToInt32(drModule["MODULE_ID"]);
					module.Name = drModule["MODULE_NAME"].ToString();
					module.LinkPath = drModule["Link_Path"].ToString();
					module.Display = Convert.ToInt32(drModule["IS_DISPLAY"]) == 1 ? true : false;
                    module.BannerButtonWidth = Convert.ToInt32(drModule["Banner_Button_Width"]);
                    module.LinkTarget = drModule["LinkTarget"].ToString();
                    module.LinkType = drModule["Link_Type"].ToString();
					modules.Add(module);
				}
				return modules;
			}
			else
			{
				return null;
			}
		}

        		/// <summary>
		/// ��ѯ��ģ��
		/// </summary>
		/// <returns>ϵͳģ��ļ���</returns>
        public ArrayList FindRootModules()
        {
            return this.FindRootModules(true);
        }

		/// <summary>
		/// ��ѯ������ʾ�ڽ����ϵĸ�ģ��
		/// </summary>
		/// <param name="userLoginName">�û��ĵ�¼��</param>
		/// <returns>��ģ�鼯��</returns>
		public ArrayList FindDisplayRootModules(string userLoginName)
		{
            string strSql = "SELECT M.MODULE_ID,M.MODULE_NAME,M.LINK_ID,M.IS_DISPLAY,L.APP_PATH," +
                "ISNULL(M.Banner_Button_Width,0) AS Banner_Button_Width,ISNULL(L.Open_Type,'') AS LinkTarget FROM Sys_Module M " +
				"LEFT JOIN SYS_LINK_OBJECT L ON M.LINK_ID=L.LINK_ID WHERE M.PARENT_MODULE_ID=0 " + 
				"AND EXISTS(SELECT R.USER_ID FROM FU_User_Right R WHERE R.MODULE_ID=M.MODULE_ID " +
				"AND EXISTS(SELECT G.GROUP_ID FROM FU_Group_USER G,U_USER U WHERE G.USER_ID=U.USER_ID AND " +
				"G.GROUP_ID=R.USER_ID AND U.LOGIN_NAME='" + userLoginName + "')) AND M.IS_DISPLAY=1 ORDER BY M.ORDER_ID";
			DataTable dtModules = DatabaseFactory.ExecuteQuery(strSql);

			if(dtModules.Rows.Count > 0)
			{
				ArrayList modules = new ArrayList(dtModules.Rows.Count);
				foreach(DataRow drModule in dtModules.Rows)
				{
					Module module = new Module();
					module.ID = Convert.ToInt32(drModule["MODULE_ID"]);
					module.Name = drModule["MODULE_NAME"].ToString();
					module.LinkPath = drModule["APP_PATH"].ToString();
                    module.BannerButtonWidth = Convert.ToInt32(drModule["Banner_Button_Width"]);
                    module.LinkTarget = drModule["LinkTarget"].ToString();
					modules.Add(module);
				}
				return modules;
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// ��ѯ���и�ģ��
		/// </summary>
		/// <param name="userLoginName">�û��ĵ�¼��</param>
		/// <returns>��ģ�鼯��</returns>
		public ArrayList FindRootModules(string userLoginName)
		{
			string strSql = "SELECT M.MODULE_ID,M.MODULE_NAME,M.LINK_ID,M.IS_DISPLAY,L.APP_PATH," +
                "ISNULL(M.Banner_Button_Width,0) AS Banner_Button_Width,ISNULL(L.Open_Type,'') AS LinkTarget FROM Sys_Module M " +
				"LEFT JOIN SYS_LINK_OBJECT L ON M.LINK_ID=L.LINK_ID WHERE M.PARENT_MODULE_ID=0 " + 
				"AND EXISTS(SELECT R.USER_ID FROM FU_User_Right R WHERE R.MODULE_ID=M.MODULE_ID " +
				"AND EXISTS(SELECT G.GROUP_ID FROM FU_Group_USER G,U_USER U WHERE G.USER_ID=U.USER_ID AND " +
				"G.GROUP_ID=R.USER_ID AND U.LOGIN_NAME='" + userLoginName + "')) ORDER BY M.ORDER_ID";
			DataTable dtModules = DatabaseFactory.ExecuteQuery(strSql);

			if(dtModules.Rows.Count > 0)
			{
				ArrayList modules = new ArrayList(dtModules.Rows.Count);
				foreach(DataRow drModule in dtModules.Rows)
				{
					Module module = new Module();
					module.ID = Convert.ToInt32(drModule["MODULE_ID"]);
					module.Name = drModule["MODULE_NAME"].ToString();
					module.LinkPath = drModule["APP_PATH"].ToString();
					module.Display = Convert.ToInt32(drModule["IS_DISPLAY"]) == 1 ? true : false;
                    module.BannerButtonWidth = Convert.ToInt32(drModule["Banner_Button_Width"]);
                    module.LinkTarget = drModule["LinkTarget"].ToString();
					modules.Add(module);
				}
				return modules;
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// �õ�ָ����ģ�����ģ��
		/// </summary>
		/// <param name="parentID">����ģ����</param>
		/// <param name="userLoginName">�û��ĵ�¼��</param>
		/// <returns>��ģ�鼯��</returns>
		public ArrayList FindChildModules(int parentID,string userLoginName)
		{
			string strSql = "SELECT M.MODULE_ID,M.MODULE_NAME,M.LINK_ID,M.IS_DISPLAY,L.APP_PATH,M.PARENT_MODULE_ID FROM Sys_Module M "+
				"LEFT JOIN SYS_LINK_OBJECT L ON M.LINK_ID=L.LINK_ID WHERE M.PARENT_MODULE_ID=" + parentID + 
				" AND EXISTS(SELECT R.USER_ID FROM FU_User_Right R WHERE R.MODULE_ID=M.MODULE_ID " +
				"AND EXISTS(SELECT G.GROUP_ID FROM FU_Group_USER G,U_USER U WHERE G.USER_ID=U.USER_ID AND " +
				"G.GROUP_ID=R.USER_ID AND U.LOGIN_NAME='" + userLoginName + "')) ORDER BY M.ORDER_ID";
			DataTable dtModules = DatabaseFactory.ExecuteQuery(strSql);

			if(dtModules.Rows.Count > 0)
			{
				ArrayList modules = new ArrayList(dtModules.Rows.Count);
				foreach(DataRow drModule in dtModules.Rows)
				{
					Module module = new Module();
					module.ID = Convert.ToInt32(drModule["MODULE_ID"]);
					module.Name = drModule["MODULE_NAME"].ToString();
					module.LinkPath = drModule["APP_PATH"].ToString();
					module.ParentID = int.Parse(drModule["PARENT_MODULE_ID"].ToString());
					module.Display = Convert.ToInt32(drModule["IS_DISPLAY"]) == 1 ? true : false;
					modules.Add(module);
				}
				return modules;
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// ����ģ���Ų�ѯ
		/// </summary>
		/// <param name="moduleID">ģ����</param>
		/// <returns>ģ�����</returns>
		public Module FindByID(int moduleID)
		{
            string strSql = "SELECT Module_ID,Parent_Module_ID,Module_Name,Link_Path,Is_Display,Link_Type," +
                "Link_Target,ISNULL(Create_Table,'0') AS Create_Table,ISNULL(Template_ID,'') AS Template_ID,Business_Table,Is_Sys_File,Is_Original_Data " +
                "FROM Sys_Module WHERE Module_ID=" + moduleID;
			DataTable dtModule = DatabaseFactory.ExecuteQuery(strSql);

			if(dtModule.Rows.Count > 0)
			{
				Module module = new Module();
				module.ID = Convert.ToInt32(dtModule.Rows[0]["Module_ID"]);
				module.ParentID = Convert.ToInt32(dtModule.Rows[0]["Parent_Module_ID"]);
				module.Name = dtModule.Rows[0]["Module_Name"].ToString();
				module.LinkPath = dtModule.Rows[0]["Link_Path"].ToString();
				module.Display = Convert.ToInt32(dtModule.Rows[0]["IS_DISPLAY"]) == 1 ? true : false;
                module.LinkType = dtModule.Rows[0]["Link_Type"].ToString();
                if (!Convert.IsDBNull(dtModule.Rows[0]["Link_Target"]))
                {
                    module.LinkTarget = dtModule.Rows[0]["Link_Target"].ToString();
                }
                module.IsCreateTable = (dtModule.Rows[0]["Create_Table"].ToString() == "1");
                module.TemplateID = dtModule.Rows[0]["Template_ID"].ToString();

                if (!Convert.IsDBNull(dtModule.Rows[0]["Business_Table"]))
                {
                    module.BusinessTableName = dtModule.Rows[0]["Business_Table"].ToString();
                }
                if (!Convert.IsDBNull(dtModule.Rows[0]["Is_Sys_File"]) && Convert.ToBoolean(dtModule.Rows[0]["Is_Sys_File"]))
                {
                    module.IsSystemFile = true;
                }
                if (!Convert.IsDBNull(dtModule.Rows[0]["Is_Original_Data"]) && Convert.ToBoolean(dtModule.Rows[0]["Is_Original_Data"]))
                {
                    module.IsOriginalData = true;
                }

				return module;
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// ����ģ����Ϣ
		/// </summary>
		/// <param name="module">ģ�����</param>
        public void Update(Module module)
        {
            string strSql = "UPDATE Sys_Module SET MODULE_NAME='" + module.Name +
                "',PARENT_MODULE_ID=" + module.ParentID + ",Link_Path='" + module.LinkPath +
                "',IS_DISPLAY=" + (module.Display ? 1 : 0) + ",Link_Type='" + module.LinkType +
                "',Link_Target='" + module.LinkTarget + "',Create_Table=" + (module.IsCreateTable ? 1 : 0) +
                ",Template_ID=" + (module.TemplateID != "" ? module.TemplateID : "NULL") +
                ",Business_Table='" + module.BusinessTableName + "',Is_Sys_File=" + (module.IsSystemFile ? "1" : "0") +
                ",Is_Original_Data=" + (module.IsOriginalData ? "1" : "0") + ",Creator_Area_Code='" + module.CreatorAreaCode + "'" +
                " WHERE MODULE_ID=" + module.ID;
            DatabaseFactory.ExecuteNonQuery(strSql);
        }

        /// <summary>
        /// ���ģ��
        /// </summary>
        /// <param name="module">ģ�����</param>
        /// <returns>ģ����</returns>
        public abstract int Add(Module module);

		/// <summary>
		/// �Ƴ�ģ��
		/// </summary>
		/// <param name="moduleID">ģ����</param>
		public void Delete(int moduleID)
		{
			//ɾ����ǰģ��
			string strSql = "DELETE FROM Sys_Module WHERE MODULE_ID=" + moduleID;
			DatabaseFactory.ExecuteNonQuery(strSql);
		}

		/// <summary>
		/// �õ��û���ģ��Ȩ���б�
		/// </summary>
		/// <param name="parentModuleID">��ģ����</param>
		/// <param name="userID">�û����</param>
		/// <returns>�û���ģ��Ȩ�޼���</returns>
		/// <remarks>2005-11-22  by:Lining</remarks>
		public DataTable FindModuleRightList(int parentModuleID,string userID)
		{
			//�ȵõ��û�����������
			string strSql = "SELECT Group_ID FROM FU_Group_User WHERE User_ID='" + userID + "'";
			DataTable dtGroupInfo = DatabaseFactory.ExecuteQuery(strSql);

			if(dtGroupInfo.Rows.Count > 0)
			{
                strSql = "SELECT M.Module_ID,M.Module_Name,ISNULL(M.Link_Path,'') AS Link_Path,ISNULL(M.Link_Type,'') AS Link_Type," +
					"ISNULL((SELECT MAX(Module_ID) FROM FU_User_Right U WHERE U.User_ID IN(";
				foreach(DataRow drGroupInfo in dtGroupInfo.Rows)
				{
					strSql += "'" + drGroupInfo["Group_ID"].ToString() + "',";
				}
				strSql = strSql.Substring(0,strSql.Length - 1) +
					") AND U.Module_ID=M.Module_ID),0) AS Module_Right " +
					"FROM Sys_Module M WHERE M.Parent_Module_ID=" + parentModuleID + " AND M.Is_Display=1 " +
                    //"AND Link_Type<>4 AND Link_Type<>5 " +
					"ORDER BY M.Order_ID";
				return DatabaseFactory.ExecuteQuery(strSql);
			}
			else
			{
				return new DataTable();
			}
		}

		/// <summary>
		/// �õ��û���ָ��ģ���Ȩ�޿���
		/// </summary>
		/// <param name="moduleID">ģ����</param>
		/// <param name="userID">�û����</param>
		/// <returns>�û��Ը�ģ��Ŀ�����Ϣ</returns>
		/// <remarks>2005-11-22  by:Lining</remarks>
		public DataTable FindModuleRight(int moduleID,string userID)
		{
			//�ȵõ��û�����������
			string strSql = "SELECT Group_ID FROM FU_Group_User WHERE User_ID='" + userID + "'";
			DataTable dtGroupInfo = DatabaseFactory.ExecuteQuery(strSql);

			if(dtGroupInfo.Rows.Count > 0)
			{
				strSql = "SELECT M.Module_ID,M.Module_Name,ISNULL(L.App_Path,'') AS Link_Path," +
					"ISNULL((SELECT MAX(Module_ID) FROM FU_User_Right U WHERE U.User_ID IN(";
				foreach(DataRow drGroupInfo in dtGroupInfo.Rows)
				{
					strSql += "'" + drGroupInfo["Group_ID"].ToString() + "',";
				}
				strSql = strSql.Substring(0,strSql.Length - 1) +
					") AND U.Module_ID=M.Module_ID),0) AS Module_Right " +
					"FROM Sys_Module M LEFT JOIN Sys_Link_Object L " +
					"ON M.Link_ID=L.Link_ID WHERE M.Module_ID=" + moduleID + " AND M.Is_Display=1";
				return DatabaseFactory.ExecuteQuery(strSql);
			}
			else
			{
				return new DataTable();
			}
		}

		/// <summary>
		/// �õ�ָ����ģ������п���ʾ�ĸ�ģ�鼯��
		/// </summary>
		/// <param name="childModuleID">��ģ����</param>
		/// <param name="moduleList">ģ�鼯��</param>
		public void FindParentModules(int childModuleID,ArrayList moduleList)
		{
			Module module = this.FindByID(childModuleID);

			if(module != null)
			{
				moduleList.Insert(0,module);
				if(module.ParentID != childModuleID)
				{
					this.FindParentModules(module.ParentID,moduleList);
				}
			}
		}

        /// <summary>
        /// �õ���������
        /// </summary>
        /// <returns>��������</returns>
        public DataTable FindAllLink()
        {
            string strSql = "SELECT Module_Name,Link_Path FROM Sys_Module WHERE Link_Path " +
                "IS NOT NULL";
            return DatabaseFactory.ExecuteQuery(strSql);
        }

        /// <summary>
        /// ���չؼ��ֲ�ѯ
        /// </summary>
        /// <param name="keyName">�ؼ������ƣ��ֶ����ƣ�</param>
        /// <param name="keyValue">�ؼ��ֶ�ȡֵ</param>
        /// <returns>����������ģ�鼯��</returns>
        public DataTable Find(string[] keyName, string[] keyValue)
        {
            if(keyName == null || keyValue == null)
            {
                return new DataTable();
            }
            if (keyName.Length != keyValue.Length)
            {
                return new DataTable();
            }

            string strSql = "SELECT Module_Name,Link_Path FROM Sys_Module WHERE Link_Path IS NOT NULL";
            for (int i = 0; i < keyName.Length; i++)
            {
                if (keyValue[i].Trim() != "")
                {
                    strSql += " AND " + keyName[i] + " LIKE '%" + keyValue[i] + "%'";
                }
            }

            return DatabaseFactory.ExecuteQuery(strSql);
        }

        /// <summary>
        /// �õ�ָ���ĸ�ģ���Ƿ������ģ��
        /// </summary>
        /// <param name="parentModuleID">��ģ����</param>
        /// <returns>��ģ���Ƿ������ģ��</returns>
        public bool IsContainChildModule(string parentModuleID)
        {
            string strSql = "SELECT COUNT(*) FROM Sys_Module WHERE Parent_Module_ID=" + parentModuleID;
            object objRecordCount = DatabaseFactory.ExecuteScalar(strSql);

            if (objRecordCount == null || Convert.IsDBNull(objRecordCount))
            {
                return false;
            }
            else
            {
                return (Convert.ToInt32(objRecordCount) > 0);
            }
        }

        /// <summary>
        /// ��ѯģ����Ϣ
        /// </summary>
        /// <returns>ģ����Ϣ</returns>
        public DataTable FindTemplate()
        {
            string strSql = "SELECT Info_ID,Info_Title FROM File_DETAIL WHERE Template_Status=1";
            return DatabaseFactory.ExecuteQuery(strSql);
        }

        /// <summary>
        /// ����ģ���˳��λ�õ���β
        /// </summary>
        /// <param name="moduleID">ģ����</param>
        public void SetOrderToEnd(string moduleID)
        {
            string strSql = "UPDATE Sys_Module SET Order_ID=(SELECT MAX(Order_ID) + 1 FROM Sys_Module) " +
                "WHERE Module_ID=" + moduleID;
            DatabaseFactory.ExecuteNonQuery(strSql);
        }

        #region New Method By Maming 2006-3-8

        /// <summary>
        /// �жϸýڵ����Ƿ�����ӽڵ�
        /// </summary>
        /// <param name="moduleID"></param>
        /// <returns></returns>
        public bool IsHaveChildNode(string moduleID)
        {
            string strSql = "select count(*) from Sys_Module where PARENT_MODULE_ID="+moduleID;
            object o = DatabaseFactory.ExecuteScalar(strSql);
            if (Convert.ToInt32(o) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// �ж��Ƕ����ڵ㲢�����������ӽڵ�
        /// </summary>
        /// <param name="moduleID">ģ��ID</param>
        /// <returns></returns>
        public bool IsSecondChildNodeAndHaveChildNode(string moduleID)
        {
            string strSql = " select Parent_Module_ID from Sys_Module where  "
                           + " Module_ID =( select Parent_Module_ID from Sys_Module where Module_ID=" + moduleID + ")";
            
            object o = DatabaseFactory.ExecuteScalar(strSql);
            if ( Convert.ToInt32(o) == 0 && IsHaveChildNode(moduleID) )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        /// <summary>
        /// �õ�ģ������
        /// </summary>
        /// <param name="moduleID">ģ����</param>
        /// <returns>ģ������</returns>
        public string FindDescription(string moduleID)
        {
            string strSql = "SELECT ISNULL(Description,'') FROM Sys_Module WHERE Module_ID=" + moduleID;
            return DatabaseFactory.ExecuteScalar(strSql).ToString();
        }

        /// <summary>
        /// ��ѯ����ҵ�����ҵ��¼��ģ������Ӧ�����ݱ�
        /// </summary>
        /// <returns>��������ҵ����DataTable</returns>
        public DataTable FindBusinessTable()
        {
            string strSql = "SELECT TableIndex,TableName FROM TIT_Tables";
            return DatabaseFactory.ExecuteQuery(strSql);
        }

        /// <summary>
        /// ��ѯҵ�����Ϣ
        /// </summary>
        /// <param name="moduleID">ģ����</param>
        /// <returns>����ҵ�����Ϣ��DataTable</returns>
        public DataTable FindBusinessTable(string moduleID)
        {
            using (IDatabase database = DatabaseFactory.CreateObject())
            {
                string strSql = "SELECT T.ID,T.TableIndex,T.TableName,T.Report_Period,T.Begin_Year " +
                    "FROM TIT_Tables T,Sys_Module S " +
                    "WHERE T.TableIndex=S.Business_Table AND S.Module_ID=" + moduleID;
                DataTable dtQueryResult = database.ExecuteQuery(strSql);
                if (dtQueryResult == null || dtQueryResult.Rows.Count == 0)
                {
                    strSql = "SELECT ID,TableIndex,TableName,Report_Period,Begin_Year FROM TIT_Tables WHERE ModuleID=" + moduleID;
                    dtQueryResult = database.ExecuteQuery(strSql);
                }

                return dtQueryResult;
            }
        }

        /// <summary>
        /// �õ�ҵ��¼��ģ������Ӧ��ҵ�����ݱ�����
        /// </summary>
        /// <param name="moduleID">ģ����</param>
        /// <returns>ҵ�����ݱ�����</returns>
        /// <remarks>
        /// Author��        Lining  
        /// Create Date��   2006-8-29
        /// </remarks>
        public string FindTableNameOfBusinessInputModule(string moduleID)
        {
            string strSql = "SELECT TableIndex FROM TIT_Tables WHERE ModuleID=" + moduleID;
            object objTableName = DatabaseFactory.ExecuteScalar(strSql);

            return (objTableName != null ? objTableName.ToString() : "");
        }

        /// <summary>
        /// ����ҵ�����ݱ�����Ƶõ�ģ����
        /// </summary>
        /// <param name="businessDataTableName">ҵ�����ݱ������</param>
        /// <returns>ģ����</returns>
        public string FindModuleIDByBueinessDataTableName(string businessDataTableName)
        {
            string strSql = "SELECT ModuleID FROM TIT_Tables WHERE TableIndex='" + businessDataTableName + "'";
            object objModuleID = DatabaseFactory.ExecuteScalar(strSql);

            return (objModuleID != null ? objModuleID.ToString() : "");
        }

        /// <summary>
        /// ��ָ��ģ�����ʾ˳��������һ��
        /// </summary>
        /// <param name="moduleID">ģ����</param>
        /// <param name="direction">�ƶ�����1-�����ƶ���2-�����ƶ���</param>
        private void MoveOrder(string moduleID,int direction)
        {
            /**********************************************************
             * ����ģ���˳�����п�����Null�����Բ��ܼ򵥵Ľ�ָ��
             * ģ���˳��������1������1��
             * ��ȷ�Ĳ������ȵõ���ģ�����һ��ģ���ţ�Ȼ���յ�ǰ
             * ��˳�����¸��º͸�ģ��ͬ��������ģ���˳���š�
             * by Lining
             * 2006-10-26
             * *******************************************************/
            
            //�Ȱ��յ�ǰ������õ��͵�ǰģ��ͬ��������ģ��
            using (IDatabase database = DatabaseFactory.CreateObject())
            {
                try
                {
                    string strSql = "SELECT Module_ID,Order_ID FROM Sys_Module WHERE Parent_Module_ID=(" +
                        "SELECT Parent_Module_ID FROM Sys_Module WHERE Module_ID=" + moduleID + ") ORDER BY Order_ID";
                    DataTable dtModule = database.ExecuteQuery(strSql);

                    int iNearModuleID = 0;
                    strSql = "UPDATE Sys_Module SET Order_ID={0} WHERE Module_ID={1}";
                    database.BeginTransaction();
                    for(int i = 0; i < dtModule.Rows.Count; i++)
                    {
                        DataRow drModule = dtModule.Rows[i];

                        //������������ģ���˳����
                        database.ExecuteNonQuery(string.Format(strSql, (i+1), drModule["Module_ID"]));

                        //�õ���������˳���ǰģ���˳����
                        if (drModule["Module_ID"].ToString().Trim() == moduleID.Trim())
                        {
                            //�õ�����ģ��ı��
                            if (direction == 1)
                            {
                                iNearModuleID = Convert.ToInt32(dtModule.Rows[i - 1]["Module_ID"]);
                            }
                            else
                            {
                                iNearModuleID = Convert.ToInt32(dtModule.Rows[i + 1]["Module_ID"]);
                            }
                        }
                    }

                    //��������ģ���˳��
                    if (direction == 1)
                    {
                        strSql = "UPDATE Sys_Module SET Order_ID=Order_ID+1 WHERE Module_ID=" + iNearModuleID;
                    }
                    else
                    {
                        strSql = "UPDATE Sys_Module SET Order_ID=Order_ID-1 WHERE Module_ID=" + iNearModuleID;
                    }
                    database.ExecuteNonQuery(strSql);

                    //������ǰģ���˳��
                    if (direction == 1)
                    {
                        strSql = "UPDATE Sys_Module SET Order_ID=Order_ID-1 WHERE Module_ID=" + moduleID;
                    }
                    else
                    {
                        strSql = "UPDATE Sys_Module SET Order_ID=Order_ID+1 WHERE Module_ID=" + moduleID;
                    }
                    database.ExecuteNonQuery(strSql);

                    database.Commit();
                }
                catch (Exception exc)
                {
                    database.Rollback();
                    throw exc;
                }
            }
        }

        /// <summary>
        /// �����ƶ�ģ���˳��
        /// </summary>
        /// <param name="moduleID">ģ����</param>
        public void MoveOrderToUp(string moduleID)
        {
            this.MoveOrder(moduleID,1);
        }

        /// <summary>
        /// �����ƶ�ģ���˳��
        /// </summary>
        /// <param name="moduleID">ģ����</param>
        public void MoveOrderToDown(string moduleID)
        {
            this.MoveOrder(moduleID, 2);
        }

        /// <summary>
        /// �õ�����ҵ����ص�ģ��
        /// </summary>
        /// <returns>����ģ����Ϣ��DataTable</returns>
        public abstract DataTable FindBusinessDataMoudle();

        #endregion ������������
    }
}
