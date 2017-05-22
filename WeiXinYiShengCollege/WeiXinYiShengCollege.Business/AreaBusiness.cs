using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Module.Models;

namespace WeiXinYiShengCollege.Business
{
   public class AreaBusiness
    {

       public static List<Area>  GetCityList(int provinceId)
       {
           List<Area> list = Area.Query("where ParentId=@0 and ParentId>0", provinceId).ToList();
           return list;
       }
       public static List<Area> GetProvinceList()
       {
           List<Area> list = Area.Query("where ParentId=@0", 0).ToList();
           return list;
       }

       public static void Insert(string province,string city,out Area provinceArea,out Area cityArea)
       {
           provinceArea = new Area();
           cityArea = new Area();
           try
           {
               //判断是否存在province 不存在则插入，存在则获取province
               //判断是否存在city 不存在则再指定的province下插入，存在则返回city实体
               Area pArea = Area.SingleOrDefault(@"where AreaName=@0 and ParentId=0", province);
               if (pArea != null && pArea.Id > 0)
               {
                   provinceArea = pArea;
                   Area cArea = Area.SingleOrDefault(@"where AreaName=@0 and ParentId=@1", city, pArea.Id);
                   if (cArea != null && cArea.Id > 0)
                   {
                       cityArea = cArea;
                   }
                   else
                   {
                       //插入新city
                       Area cNewArea = new Area() { AreaName = city, AreaLevel = 2, ParentId = pArea.Id };
                       cNewArea.Insert();
                       cityArea = cNewArea;

                   }
               }
               else
               {
                   //插入新province
                   Area pNewArea = new Area() { AreaName = province, AreaLevel = 1, ParentId = 0 };
                   object newPId = pNewArea.Insert();
                   //pNewArea.Id = Convert.ToInt32(newPId);
                   provinceArea = pNewArea;
                   //插入新city
                   Area cNewArea = new Area() { AreaName = city, AreaLevel = 2, ParentId = pNewArea.Id };
                   object newCId = cNewArea.Insert();
                   //cNewArea.Id = Convert.ToInt32(newCId);
                   //返回对象已自动把Id赋值
                   cityArea = cNewArea;

               }
           }catch(Exception ex)
           {
               SNS.Library.Logs.LogDAOFactory.Write(
                   string.Format(@"province:{0},city:{1}:ex:{2}", province, city, ex.Message + ex.Source + ex.StackTrace), "", Convert.ToString((int)ErrorCode.InsertArea), SNS.Library.Logs.LogType.Error);
           }
       }
    }
}
