using System;


			/******************************
			 * �����ƣ�Log
			 *   ���ܣ���ʾһ��ϵͳ��־
			 *     by��Lining
			 *   ���ڣ�2004-11-30
			 *   ��ע�����ܼ̳д���
			 *****************************/


namespace SNS.Library.Logs
{
	/// <summary>
	/// ��ʾһ��ϵͳ��־�����ܼ̳д��ࡣ
	/// </summary>
	public sealed class Log
	{
		/// <summary>
		/// ����һ��ϵͳ��־
		/// </summary>
		public Log()
		{
		}


		#region �ֶ�

		/// <summary>
		/// ��־���
		/// </summary>
		private int _id = 0;

		/// <summary>
		/// ��¼��־��ʱ��
		/// </summary>
		private string _dateTime = "";

		/// <summary>
		/// ��־������
		/// </summary>
		private string _description = "";

		/// <summary>
		/// ��־���
		/// </summary>
		private LogType _logType = 0;

		/// <summary>
		/// ��־��Ϣ�����һЩ������Ϣ�����û����ƣ�
		/// </summary>
		private string _info = "";

        /// <summary>
        /// ģ������
        /// </summary>
        private string _moduleName = "";

		#endregion �����ֶ�


		#region ����

		/// <summary>
		/// ��ȡ��������־���
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
		/// ��ȡ�����ü�¼��־��ʱ��
		/// </summary>
		public string DateTime
		{
			get
			{
				return this._dateTime;
			}
			set
			{
				this._dateTime = value;
			}
		}

		/// <summary>
		/// ��ȡ��������־����
		/// </summary>
		public string Description
		{
			get
			{
				return this._description;
			}
			set
			{
				this._description = value;
			}
		}

		/// <summary>
		/// ��ȡ��������־�����
		/// </summary>
		public LogType Type
		{
			get
			{
				return this._logType;
			}
			set
			{
				this._logType = value;
			}
		}

		/// <summary>
		/// ��ȡ��������־��Ϣ
		/// </summary>
		public string Info
		{
			get
			{
				return this._info;
			}
			set
			{
				this._info = value;
			}
		}

        /// <summary>
        /// ��ȡ������ģ������
        /// </summary>
        public string ModuleName
        {
            get
            {
                return this._moduleName;
            }
            set
            {
                this._moduleName = value;
            }
        }

		#endregion ��������
	}


    /// <summary>
    /// ϵͳ��־����ö��
    /// </summary>
    public enum LogType
    {
        /// <summary>
        /// ������־
        /// </summary>
        Operation = 9,

        /// <summary>
        /// ������־
        /// </summary>
        Error = 10,

        /// <summary>
        /// ������־
        /// </summary>
        Application = 11,
        
        /// <summary>
        /// ͨѶ��־
        /// </summary>
        Communication = 12,

        /// <summary>
        /// ���ݿ�ͬ����־
        /// </summary>
        DatabaseReplication = 13,

        /// <summary>
        /// ������־
        /// </summary>
        Debug = 300
    }
}
