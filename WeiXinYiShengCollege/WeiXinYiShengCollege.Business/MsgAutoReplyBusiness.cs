using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Module.Models;

namespace WeiXinYiShengCollege.Business
{
    /// <summary>
    /// 消息上行自动回复
    /// </summary>
   public class MsgAutoReplyBusiness
    {
       /// <summary>
       /// 获取未被删除的回复内容列表
       /// </summary>
       /// <returns></returns>
       public static List<AutoReplyContent> GetReplyContentList()
       {
           List<AutoReplyContent> list = GetReplyContentList("");
           return list;
       }
       /// <summary>
       /// 获取未被删除的回复内容列表 可以模糊查询
       /// </summary>
       /// <returns></returns>
       public static List<AutoReplyContent> GetReplyContentList(string upkey)
       {
           String strSql = string.Format(@"select * from AutoReplyContent where IsDelete=0 ");
           if (!string.IsNullOrEmpty(upkey))
           {
               strSql += string.Format(@" and UpKey like'%{0}%'", upkey);
           }
           List<AutoReplyContent> list = AutoReplyContent.Query(strSql).ToList();
           return list;
       }

       /// <summary>
       /// 获取未被删除的回复内容列表
       /// </summary>
       /// <returns></returns>
       public static AutoReplyContent GetReplyContent(String upKey)
       {
           AutoReplyContent arc = AutoReplyContent.SingleOrDefault("where IsDelete=@0 and UpKey=@1", 0,upKey);
           return arc;
       }
       /// <summary>
       /// 获取未被删除的回复内容列表
       /// </summary>
       /// <returns></returns>
       public static AutoReplyContent GetReplyContent(int id)
       {
           AutoReplyContent arc = AutoReplyContent.SingleOrDefault("where IsDelete=@0 and Id=@1", 0, id);
           return arc;
       }

    }

}
