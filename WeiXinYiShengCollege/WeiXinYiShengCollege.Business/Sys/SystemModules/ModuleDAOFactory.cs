using System;


			/*************************************
			 * 类名称：ModuleDAOFactory
			 *   功能：创建维护系统模块的对象的工厂
			 *     by：Lining
			 *   日期：2004-10-26
			 *   备注：不能继承此类
			 *************************************/


namespace SNS.Library.SystemModules
{
	/// <summary>
	/// 提供创建维护菜单项的对象的方法。不能继承此类。
	/// </summary>
	public sealed class ModuleDAOFactory
	{
		/// <summary>
		/// 创建维护系统模块的对象
		/// </summary>
		/// <returns>维护系统模块的对象</returns>
		public static ModuleDAO CreateObject()
		{ 
			return new ModuleDAOImplSQLServer();
		}
	}
}
