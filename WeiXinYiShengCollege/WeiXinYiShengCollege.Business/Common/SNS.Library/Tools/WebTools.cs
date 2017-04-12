using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using SNS.Library.Logs;
using System.Configuration;
using System.Data;
using System.Xml;
using System.IO;
using SNS.Library.Database;

namespace SNS.Library.Tools
{
    /****************************
    * 类名称：WebTools
    *   功能：自定义风格管理类
    *     by：wuling
    *   日期：2007-8-1
    ****************************/
    /// <summary>
    /// WEB 开发常用工具类
    /// </summary>
    public class WebTools
    {

        #region 写日志
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="description">操作日志</param>
        public static void WriteLog(string description)
        {
            WriteLog(description, LogType.Operation);
        }

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="theException">异常</param>
        public static void WriteLog(Exception theException)
        {
			WriteLog(theException.Message.Replace('\'', '\"') + theException.Source.Replace('\'', '\"') + theException.StackTrace.Replace('\'', '\"'), LogType.Error);
        }

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="description">日志描述</param>
        /// <param name="type">日志类型</param>
        public static void WriteLog(string description, LogType type)
        {
            string strUserName = "";
            string strModuleName = "";
            try
            {
                strUserName = GetSession("LoginName");

                if (GetSession("ModuleName") != null)
                {
                    strModuleName = GetSession("ModuleName");
                }
            }
            catch { }

            LogDAOFactory.Write(description, strModuleName, strUserName, type);
        }

        #endregion 写日志

        public static string GetSession(string stringKeyName)
        {
            return GetSession(stringKeyName, "");
        }
        public static string GetSession(string stringKeyName,string strDefaultValue)
        {
            string strValue = strDefaultValue;
            try
            {
                strValue = HttpContext.Current.Session[stringKeyName].ToString();
            }
            catch { }

            return strValue;
        }

        #region 读写配置项
        /// <summary>
        /// 写配置参数到配置文件
        /// </summary>
        /// <param name="strKey">配置项名称</param>
        /// <param name="strValue">配置项的值</param>
        static public void WriteConfigParameter(string strKey, string strValue)
        {
            Configuration cfg = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            System.Configuration.AppSettingsSection s = (AppSettingsSection)cfg.GetSection("appSettings");
            if (s.Settings[strKey] != null)
                s.Settings[strKey].Value = strValue;
            else
                s.Settings.Add(strKey,strValue);

            s.SectionInformation.ForceSave = true;
            cfg.Save(ConfigurationSaveMode.Minimal);
        }

        /// <summary>
        /// 读取配置参数
        /// </summary>
        /// <param name="ParamaterName">配置项名称</param>
        /// <param name="DefaultValue">配置项缺省值</param>
        /// <returns>整数值</returns>
        public static int GetConfigParameter(string ParamaterName, int DefaultValue)
        {
            int iResult = DefaultValue;//缺省初值

            try
            {
                iResult = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings[ParamaterName]);
            }
            catch (Exception ex)
            {
                WriteLog(ex);
            }
            return iResult;
        }

        /// <summary>
        /// 读取配置参数
        /// </summary>
        /// <param name="ParamaterName">配置项名称</param>
        /// <param name="DefaultValue">配置项缺省值</param>
        /// <returns>字符串值</returns>
        public static string GetConfigParameter(string ParamaterName, string DefaultValue)
        {
            string iResult = DefaultValue;//缺省初值

            try
            {
                System.Configuration.ConfigurationManager.RefreshSection("appSettings");
                iResult = System.Configuration.ConfigurationManager.AppSettings[ParamaterName];
            }
            catch (Exception ex)
            {
                WriteLog(ex);
            }
            return iResult;
        }
        #endregion 读写配置项

        #region 读指定文件名的XML文件配置节
        /// <summary>
        /// 读指定文件名的XML文件配置节点某一属性列表
        /// </summary>
        /// <param name="XmlFullPath">XML文件全路径 e.g:d:\nsmis\web\appdata\xml.xml</param>
        /// <param name="xmlXpath">"//Pro[@pro="黑龙江"]"</param>
        /// <param name="xmlAttributes">属性名称</param>
        /// <returns></returns>
        public static string[] GetXmlNodeAttribute(string XmlFullPath, string xmlXpath,string xmlAttributes)
        {
            XmlDocument doc = new XmlDocument();
            string[] strReturnArray = null;
            if (File.Exists(XmlFullPath))
            {
                doc.Load(XmlFullPath);
                XmlNodeList xnl = doc.SelectNodes(xmlXpath);
                if (xnl.Count > 0)
                {
                    strReturnArray = new string[xnl.Count];
                    for (int i = 0; i < xnl.Count; i++)
                    {
                        strReturnArray[i] = xnl[i].Attributes[""].Value;
                    }
                }
            }
            return strReturnArray;
        }

        /// <summary>
        /// 读指定文件名的XML文件配置节点类表
        /// </summary>
        /// <param name="XmlFullPath">XML文件全路径 e.g:d:\nsmis\web\appdata\xml.xml</param>
        /// <param name="xmlXpath">"//Pro[@pro="黑龙江"]"</param>
        /// <returns></returns>
        public static XmlNodeList GetXmlNodeList(string XmlFullPath, string xmlXpath)
        {
            XmlDocument doc = new XmlDocument();
            XmlNodeList xmlNodeList = null;
            if (File.Exists(XmlFullPath))
            {
                doc.Load(XmlFullPath);
                xmlNodeList = doc.SelectNodes(xmlXpath);
            }
            return xmlNodeList;
        }
        #endregion

      
        /// <summary>
        /// 根据请求的页面得到根目录需要的../的字符串
        /// </summary>
        /// <returns></returns>
        public static string GetRootDes()
        {
            string str = System.Web.HttpContext.Current.Request.PhysicalApplicationPath.ToLower();
            string strAll = System.Web.HttpContext.Current.Request.PhysicalPath.ToLower();
            string[] matchCol = strAll.Replace(str, "").Split(new string[] { "\\" }, StringSplitOptions.None);
            string strReturn = "";
            for (int i = 0; i < matchCol.Length - 1; i++)
            {
                strReturn += @"../";
            }
            return strReturn;
        }

    }
}