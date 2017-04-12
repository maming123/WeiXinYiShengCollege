using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Module.Utils
{
    /// <summary>
    /// MessageBox 的摘要说明
    /// </summary>
    public class MessageBox
    {
        public MessageBox()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        /// <summary>
        /// 显示消息提示对话框
        /// </summary>
        /// <param name="page">当前页面指针，一般为this</param>
        /// <param name="msg">提示信息</param>
        public static void Show(System.Web.UI.Page page, string msg)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javascript' defer>alert('" + msg.ToString() + "');</script>");
        }

        /// <summary>
        /// 控件点击 消息确认提示框
        /// </summary>
        /// <param name="page">当前页面指针，一般为this</param>
        /// <param name="msg">提示信息</param>
        public static void ShowConfirm(System.Web.UI.WebControls.WebControl Control, string msg)
        {
            //Control.Attributes.Add("onClick","if (!window.confirm('"+msg+"')){return false;}");
            Control.Attributes.Add("onclick", "return confirm('" + msg + "');");
        }

        /// <summary>
        /// 显示消息提示对话框，并进行页面跳转
        /// </summary>
        /// <param name="page">当前页面指针，一般为this</param>
        /// <param name="msg">提示信息</param>
        /// <param name="url">跳转的目标URL</param>
        public static void ShowAndRedirect(System.Web.UI.Page page, string msg, string url)
        {
            //Response.Write("<script>alert('帐户审核通过！现在去为企业充值。');window.location=\"" + pageurl + "\"</script>");
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javascript' defer>alert('" + msg + "');window.location=\"" + url + "\"</script>");


        }
        /// <summary>
        /// 显示消息提示对话框，并进行页面跳转
        /// </summary>
        /// <param name="page">当前页面指针，一般为this</param>
        /// <param name="msg">提示信息</param>
        /// <param name="url">跳转的目标URL</param>
        public static void ShowAndRedirects(System.Web.UI.Page page, string msg, string url)
        {
            StringBuilder Builder = new StringBuilder();
            Builder.Append("<script language='javascript' defer>");
            Builder.AppendFormat("alert('{0}');", msg);
            Builder.AppendFormat("top.location.href='{0}'", url);
            Builder.Append("</script>");
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", Builder.ToString());

        }

        /// <summary>
        /// 输出自定义脚本信息
        /// </summary>
        /// <param name="page">当前页面指针，一般为this</param>
        /// <param name="script">输出脚本</param>
        public static void ResponseScript(System.Web.UI.Page page, string script)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javascript' defer>" + script + "</script>");

        }

        /// <summary>
        /// 客户端提示框
        /// </summary>
        /// <param name="message">提示信息</param>
        /// <param name="page">当前页Page对象</param>
        /// <param name="clientkey">客户端注册ID</param>
        public static void Show(string message, System.Web.UI.Page page, string clientkey)
        {
            string clientscript = "<script>";
            clientscript += "alert(\"" + message + "\");";
            clientscript += "</script>";
            page.ClientScript.RegisterStartupScript(Type.GetType("System.String"), clientkey, clientscript);
        }

        /// <summary>
        /// 客户端提示框
        /// </summary>
        /// <param name="message">信息框内容</param>
        /// <param name="behindcontent">在弹出信息框后的脚本处理内容</param>
        /// <param name="page">当前页Page对象</param>
        /// <param name="clientkey">客户端注册ID</param>
        public static void Show(string message, string behindcontent, System.Web.UI.Page page, string clientkey)
        {
            string clientscript = "<script>";
            clientscript += "alert('" + message + "');";
            clientscript += behindcontent;
            clientscript += "</script>";
            page.ClientScript.RegisterStartupScript(Type.GetType("System.String"), clientkey, clientscript);
        }


    }
}
