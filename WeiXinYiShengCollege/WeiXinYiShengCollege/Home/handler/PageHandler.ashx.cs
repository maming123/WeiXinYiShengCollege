using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HospitalBook.Module;
using Module.Models;
using Module.Utils;
using WeiXinYiShengCollege.Business;

namespace HospitalBookWebSite.Home.handler
{
    /// <summary>
    /// PageHandler 的摘要说明
    /// </summary>
    public class PageHandler : BaseHandler
    {
        public PageHandler()
        {
            dictAction.Add("GetUserList", GetUserList);
            
            
        }
        /// <summary>
        /// code值为:
        ///-105: 用户未登录.
        /// </summary>
        /// <returns></returns>
        private bool IsReady()
        {
            if (LoginAdminUser.GetCurrentUser() == null)
            {
                Response.Write(BaseCommon.ObjectToJson(new { code = ExceptionType.NotLogin, m = "请先登录" }));
                return false;
            }
            return true;
        }

        private void GetUserList()
        {
            if (!IsReady())
                return;

            int bookId = RequestKeeper.GetFormInt(Request["bookId"]);
            long mobile = RequestKeeper.GetFormLong(Request["mobile"]);
            int pageIndex = RequestKeeper.GetFormInt(Request["PageIndex"]);
            int pageSize = 12;// RequestKeeper.GetFormInt(Request["PageSize"]);

            PageList<List<User>> pList = UserBusiness.GetUserList(mobile, pageIndex, pageSize);


            Response.Write(BaseCommon.ObjectToJson(new ReturnJsonType<PageList<List<User>>>() { code = 1, m = pList }));

        }
        
    }
}