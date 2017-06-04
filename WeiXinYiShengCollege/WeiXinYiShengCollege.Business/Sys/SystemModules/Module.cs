using System;


			/*******************************
			 * �����ƣ�Module
			 *   ���ܣ���ʾһ��ϵͳģ��
			 *     by��Lining
			 *   ���ڣ�2004-10-25
			 *******************************/


namespace SNS.Library.SystemModules
{
	/// <summary>
	/// ��ʾһ��ϵͳģ�顣���ܼ̳д��ࡣ
	/// </summary>
	[Serializable]
	public sealed class Module
	{
		/// <summary>
		/// ����һ��ϵͳģ��
		/// </summary>
		public Module()
		{
		}


		#region �ֶ�

		/// <summary>
		/// ϵͳģ����
		/// </summary>
		private int _id = 0;

		/// <summary>
		/// ϵͳģ������
		/// </summary>
		private string _name = "";

		/// <summary>
		/// �����ţ�0Ϊ���˵��
		/// </summary>
		private int _parentID = 0;

		/// <summary>
		/// ָ������Ӷ����·��
		/// </summary>
		private string _linkPath = "";

        /// <summary>
        /// 
        /// </summary>
        private string _linkType = "";

		/// <summary>
		/// �Ƿ�������ϵͳ��������
		/// </summary>
		private bool _isDisplay = true;

		/// <summary>
		/// �Ƿ��ǹ���ģ��
		/// </summary>
		private bool _isFunctionModule = false;

		/// <summary>
		/// ģ��ļ���Ĭ��Ϊ��ģ�飬����Ϊ1��
		/// </summary>
		private int _level = 1;

        /// <summary>
        /// ��ΪBanner��һ��Button�Ŀ��
        /// </summary>
        /// <remarks>ÿ����ģ�������ΪBanner�ϵ�һ��Button</remarks>
        private int _bannerButtonWidth = 0;

        /// <summary>
        /// ģ�����ӵ�Target
        /// </summary>
        private string _linkTarget = "";

        /// <summary>
        /// �Ƿ�Ϊҵ��ģ�鴴�����ݱ�
        /// </summary>
        private bool _isCreateTable = false;

        /// <summary>
        /// ģ����
        /// </summary>
        private string _templateID = "";

        /// <summary>
        /// ��Ӧ��ҵ�����ݱ�
        /// </summary>
        private string _businessTableName = "";

        /// <summary>
        /// �Ƿ���ϵͳ�ļ�
        /// </summary>
        private bool _isSystemFile = false;

        /// <summary>
        /// �Ƿ���ԭʼ����
        /// </summary>
        private bool _isOriginalData = false;

        /// <summary>
        /// ģ�鴴���������ĵ�ַ��
        /// </summary>
        private string _creatorAreaCode = "";

		#endregion ����˽�г�Ա


		#region ����

		/// <summary>
		/// ��ȡ������ϵͳģ��ı��
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
		/// ��ȡ������ϵͳģ������
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
		/// ��ȡ������ϵͳģ��ĸ������
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
		/// ��ȡ������ָ������Ӷ����·��
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
		/// ��ȡ������һ��ֵ����ʾ��ģ���Ƿ��ڳ�������������ʾ
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
		/// ��ȡ������һ��ֵ����ʾ��ģ���Ƿ��ǹ���ģ��
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
		/// ��ȡ������ģ��ļ���Ĭ��Ϊ��ģ�飬����Ϊ1��
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
        /// ��ȡ��������ΪBanner��һ��Button�Ŀ��
        /// </summary>
        /// <remarks>ÿ����ģ�������ΪBanner�ϵ�һ��Button</remarks>
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
        /// ��ȡ������ģ�����ӵ�Target
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
        /// ��ȡ��������������
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
        /// �Ƿ�Ϊҵ��ģ�鴴�����ݱ�
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
        /// ��ȡ������ģ����
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
        /// ��Ӧ��ҵ�����ݱ�
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
        /// ��ȡ������ģ���Ƿ���ϵͳ�ļ�
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
        /// ��ȡ�������Ƿ���ԭʼ����
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
        /// ��ȡ������ģ�鴴���ߵĵ�ַ��
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

		#endregion ��������
	}
}
