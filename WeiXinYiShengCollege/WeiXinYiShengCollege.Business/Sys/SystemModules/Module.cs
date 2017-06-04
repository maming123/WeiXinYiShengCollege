using System;


			/*******************************
			 * 类名称：Module
			 *   功能：表示一个系统模块
			 *     by：Lining
			 *   日期：2004-10-25
			 *******************************/


namespace SNS.Library.SystemModules
{
	/// <summary>
	/// 表示一个系统模块。不能继承此类。
	/// </summary>
	[Serializable]
	public sealed class Module
	{
		/// <summary>
		/// 创建一个系统模块
		/// </summary>
		public Module()
		{
		}


		#region 字段

		/// <summary>
		/// 系统模块编号
		/// </summary>
		private int _id = 0;

		/// <summary>
		/// 系统模块名称
		/// </summary>
		private string _name = "";

		/// <summary>
		/// 父项编号（0为主菜单项）
		/// </summary>
		private int _parentID = 0;

		/// <summary>
		/// 指向的链接对象的路径
		/// </summary>
		private string _linkPath = "";

        /// <summary>
        /// 
        /// </summary>
        private string _linkType = "";

		/// <summary>
		/// 是否现在在系统主界面中
		/// </summary>
		private bool _isDisplay = true;

		/// <summary>
		/// 是否是功能模块
		/// </summary>
		private bool _isFunctionModule = false;

		/// <summary>
		/// 模块的级别（默认为根模块，级别为1）
		/// </summary>
		private int _level = 1;

        /// <summary>
        /// 作为Banner上一个Button的宽度
        /// </summary>
        /// <remarks>每个根模块可以作为Banner上的一个Button</remarks>
        private int _bannerButtonWidth = 0;

        /// <summary>
        /// 模块链接的Target
        /// </summary>
        private string _linkTarget = "";

        /// <summary>
        /// 是否为业务模块创建数据表
        /// </summary>
        private bool _isCreateTable = false;

        /// <summary>
        /// 模板编号
        /// </summary>
        private string _templateID = "";

        /// <summary>
        /// 对应的业务数据表
        /// </summary>
        private string _businessTableName = "";

        /// <summary>
        /// 是否是系统文件
        /// </summary>
        private bool _isSystemFile = false;

        /// <summary>
        /// 是否是原始数据
        /// </summary>
        private bool _isOriginalData = false;

        /// <summary>
        /// 模块创建者所属的地址码
        /// </summary>
        private string _creatorAreaCode = "";

		#endregion 结束私有成员


		#region 属性

		/// <summary>
		/// 获取或设置系统模块的编号
		/// </summary>
		public int ID
		{
			get
			{
				return this._id;
			}
			set
			{
				this._id = value;
			}
		}

		/// <summary>
		/// 获取或设置系统模块名称
		/// </summary>
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		/// <summary>
		/// 获取或设置系统模块的父级编号
		/// </summary>
		public int ParentID
		{
			get
			{
				return this._parentID;
			}
			set
			{
				this._parentID = value;
			}
		}

		/// <summary>
		/// 获取或设置指向的链接对象的路径
		/// </summary>
		public string LinkPath
		{
			get
			{
				return this._linkPath;
			}
			set
			{
				this._linkPath = value;
			}
		}

		/// <summary>
		/// 获取或设置一个值，表示该模块是否在程序主界面中显示
		/// </summary>
		public bool Display
		{
			get
			{
				return this._isDisplay;
			}
			set
			{
				this._isDisplay = value;
			}
		}

		/// <summary>
		/// 获取或设置一个值，表示该模块是否是功能模块
		/// </summary>
		public bool FunctionModule
		{
			get
			{
				return this._isFunctionModule;
			}
			set
			{
				this._isFunctionModule = value;
			}
		}

		/// <summary>
		/// 获取或设置模块的级别（默认为根模块，级别为1）
		/// </summary>
		public int Level
		{
			get
			{
				return this._level;
			}
			set
			{
				this._level = value;
			}
		}

        /// <summary>
        /// 获取或设置作为Banner上一个Button的宽度
        /// </summary>
        /// <remarks>每个根模块可以作为Banner上的一个Button</remarks>
        public int BannerButtonWidth
        {
            get
            {
                return this._bannerButtonWidth;
            }
            set
            {
                this._bannerButtonWidth = value;
            }
        }

        /// <summary>
        /// 获取或设置模块链接的Target
        /// </summary>
        public string LinkTarget
        {
            get
            {
                return this._linkTarget;
            }
            set
            {
                this._linkTarget = value;
            }
        }

        /// <summary>
        /// 获取或设置链接类型
        /// </summary>
        public string LinkType
        {
            get
            {
                return this._linkType;
            }
            set
            {
                this._linkType = value;
            }
        }

        /// <summary>
        /// 是否为业务模块创建数据表
        /// </summary>
        public bool IsCreateTable
        {
            get
            {
                return this._isCreateTable;
            }
            set
            {
                this._isCreateTable = value;
            }
        }

        /// <summary>
        /// 获取或设置模板编号
        /// </summary>
        public string TemplateID
        {
            get
            {
                return this._templateID;
            }
            set
            {
                this._templateID = value;
            }
        }

        /// <summary>
        /// 对应的业务数据表
        /// </summary>
        public string BusinessTableName
        {
            get
            {
                return this._businessTableName;
            }
            set
            {
                this._businessTableName = value;
            }
        }

        /// <summary>
        /// 获取或设置模块是否是系统文件
        /// </summary>
        public bool IsSystemFile
        {
            get
            {
                return this._isSystemFile;
            }
            set
            {
                this._isSystemFile = value;
            }
        }

        /// <summary>
        /// 获取或设置是否是原始数据
        /// </summary>
        public bool IsOriginalData
        {
            get
            {
                return this._isOriginalData;
            }
            set
            {
                this._isOriginalData = value;
            }
        }

        /// <summary>
        /// 获取或设置模块创建者的地址码
        /// </summary>
        public string CreatorAreaCode
        {
            get
            {
                return this._creatorAreaCode;
            }
            set
            {
                this._creatorAreaCode = value;
            }
        }

		#endregion 结束属性
	}
}
