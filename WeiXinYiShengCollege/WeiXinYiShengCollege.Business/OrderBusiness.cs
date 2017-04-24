using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Module.Models;

namespace WeiXinYiShengCollege.Business
{
   public class OrderBusiness
    {
       public static List<OrderInfo> GetOrderInfoListFromDB(int status,DateTime dtbegin,DateTime dtend)
       {
           string strSql = String.Format(@"where OrderStatus=@0 and OrderCreateDateTime between @1 and @2");
           List<OrderInfo> list = OrderInfo.Query(strSql, status, dtbegin, dtend).ToList();
           return list;
       }
       public static void InsertIntoDB(List<OrderInfo> list)
       {
           foreach(OrderInfo info in list)
           {
               OrderInfo orderInfoFromDB = OrderInfo.SingleOrDefault("where OrderId =@0", info.OrderId);
               if(orderInfoFromDB!=null)
               {
                   //赋值然后更新
                   orderInfoFromDB.OrderStatus = info.OrderStatus;
                   orderInfoFromDB.Update();
               }else
               {
                   //插入
                   info.Insert();
               }
           }
       }

       /// <summary>
       /// 获取积分日志记录
       /// </summary>
       /// <param name="openId"></param>
       /// <param name="orderid"></param>
       /// <returns></returns>
       public static AddScoreLog GetScoreLogItem(string openId,string orderid)
       {
           string strSql = string.Format(@"where OpenId=@0 and OrderId=@1");
           AddScoreLog item = AddScoreLog.SingleOrDefault(strSql, openId, orderid);
           if (null != item)
               return item;
           else
               return null;
       }
    }
}

