using System;


			/*************************************
			 * �����ƣ�ModuleDAOFactory
			 *   ���ܣ�����ά��ϵͳģ��Ķ���Ĺ���
			 *     by��Lining
			 *   ���ڣ�2004-10-26
			 *   ��ע�����ܼ̳д���
			 *************************************/


namespace SNS.Library.SystemModules
{
	/// <summary>
	/// �ṩ����ά���˵���Ķ���ķ��������ܼ̳д��ࡣ
	/// </summary>
	public sealed class ModuleDAOFactory
	{
		/// <summary>
		/// ����ά��ϵͳģ��Ķ���
		/// </summary>
		/// <returns>ά��ϵͳģ��Ķ���</returns>
		public static ModuleDAO CreateObject()
		{ 
			return new ModuleDAOImplSQLServer();
		}
	}
}
