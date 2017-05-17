using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HospitalBook.Module;
using Module.Models;
using Module.Utils;
using Senparc.Weixin.MP.Sample.CommonService;
using WeiXinYiShengCollege.Business;

namespace HospitalBookWebSite.handler
{
    /// <summary>
    /// PageHandler 的摘要说明
    /// </summary>
    public class PageHandler : BaseHandler
    {
        public PageHandler()
        {
            //提交审核 理事
            dictAction.Add("SubmitApprove", SubmitApprove);
            //理事修改自己信息
            dictAction.Add("UpdateProfile", UpdateProfile);
            //粉丝信息完善
            dictAction.Add("FansInfoComplete", FansInfoComplete);
            
            //粉丝修改自己信息
            dictAction.Add("UpdateFansInfo", UpdateFansInfo);
            
            //理事兑换积分
            dictAction.Add("Exchange", Exchange);
            //插入排班信息
            dictAction.Add("InsertDoctorWorkSchedule", InsertDoctorWorkSchedule);
            //更新排班信息
            dictAction.Add("UpdateDoctorWorkSchedule", UpdateDoctorWorkSchedule);
            //删除排班信息
            dictAction.Add("DeleteDoctorWorkSchedule", DeleteDoctorWorkSchedule);
            
            //插入医生信息
            dictAction.Add("InsertDoctorInfo", InsertDoctorInfo);
            //修改医生信息
            dictAction.Add("UpdateDoctorInfo", UpdateDoctorInfo);
            //删除医生信息
            dictAction.Add("DeleteDoctorInfo", DeleteDoctorInfo);

        }

        private void DeleteDoctorInfo()
        {
            if (!IsReady())
                return;

            string OpenId = RequestKeeper.GetFormString(Request["OpenId"]);
            int Id = RequestKeeper.GetFormInt(Request["Id"]);

            if (string.IsNullOrEmpty(OpenId) || Id <= 0)
            {
                Response.Write(BaseCommon.ObjectToJson(new ReturnJsonType<string>() { code = -1, m = "失败" }));
                return;
            }
            int obj = DoctorBusiness.DeleteDoctorInfo(OpenId, Id);

            if (Convert.ToInt32(obj) > 0)
            {
                Response.Write(BaseCommon.ObjectToJson(new ReturnJsonType<string>() { code = 1, m = "成功" }));
            }
            else
            {
                Response.Write(BaseCommon.ObjectToJson(new ReturnJsonType<string>() { code = 0, m = "失败" }));
            }
        }

        private void DeleteDoctorWorkSchedule()
        {
            if (!IsReady())
                return;

            string OpenId = RequestKeeper.GetFormString(Request["OpenId"]);
            int Id = RequestKeeper.GetFormInt(Request["Id"]);

            if (string.IsNullOrEmpty(OpenId) || Id<=0)
            {
                Response.Write(BaseCommon.ObjectToJson(new ReturnJsonType<string>() { code = -1, m = "失败" }));
                return;
            }
            int obj =DoctorBusiness.DeleteDoctorWorkSchedule(OpenId, Id);
            
            if (Convert.ToInt32(obj) > 0)
            {
                Response.Write(BaseCommon.ObjectToJson(new ReturnJsonType<string>() { code = 1, m = "成功" }));
            }
            else
            {
                Response.Write(BaseCommon.ObjectToJson(new ReturnJsonType<string>() { code = 0, m = "失败" }));
            }
            
        }

        private void UpdateDoctorInfo()
        {
            //data: { Action: "InsertDoctorInfo", OpenId: OpenId, txtName: txtName, txtRemark: txtRemark, r: Math.random() },
            if (!IsReady())
                return;
            // data: { Action: "InsertDoctorWorkSchedule", OpenId: OpenId, ddlDate: ddlDate, ddlDayTime: ddlDayTime, ddlDoctorId: ddlDoctor, ddlDoctorName: ddlDoctorName, r: Math.random() },
            string OpenId = RequestKeeper.GetFormString(Request["OpenId"]);
            int Id = RequestKeeper.GetFormInt(Request["Id"]);
            string txtName = RequestKeeper.GetFormString(Request["txtName"]);
            string txtRemark = RequestKeeper.GetFormString(Request["txtRemark"]);


            if (string.IsNullOrEmpty(OpenId))
            {
                Response.Write(BaseCommon.ObjectToJson(new ReturnJsonType<string>() { code = -1, m = "失败" }));
                return;
            }
            DoctorInfo doctor = DoctorBusiness.GetDoctorInfo(Id);

            if (null == doctor)
            {
                Response.Write(BaseCommon.ObjectToJson(new ReturnJsonType<string>() { code = -1, m = "失败" }));
                return;
            }

            doctor.DoctorName = txtName;
            doctor.Remark = txtRemark;
            
            int obj = doctor.Update();
            if (Convert.ToInt32(obj) > 0)
            {
                Response.Write(BaseCommon.ObjectToJson(new ReturnJsonType<string>() { code = 1, m = "成功" }));
            }
            else
            {
                Response.Write(BaseCommon.ObjectToJson(new ReturnJsonType<string>() { code = 0, m = "失败" }));
            }
            

        }

        private void InsertDoctorInfo()
        {
            //data: { Action: "InsertDoctorInfo", OpenId: OpenId, txtName: txtName, txtRemark: txtRemark, r: Math.random() },
            if (!IsReady())
                return;
            // data: { Action: "InsertDoctorWorkSchedule", OpenId: OpenId, ddlDate: ddlDate, ddlDayTime: ddlDayTime, ddlDoctorId: ddlDoctor, ddlDoctorName: ddlDoctorName, r: Math.random() },
            string OpenId = RequestKeeper.GetFormString(Request["OpenId"]);
            string txtName = RequestKeeper.GetFormString(Request["txtName"]);
            string txtRemark = RequestKeeper.GetFormString(Request["txtRemark"]);
            

            if (string.IsNullOrEmpty(OpenId))
            {
                Response.Write(BaseCommon.ObjectToJson(new ReturnJsonType<string>() { code = -1, m = "失败" }));
                return;
            }

            DoctorInfo doctor = new DoctorInfo() { 
                 DoctorName=txtName
                 , Remark=txtRemark
                 , CreatorOpenId=OpenId
                 , CreateDatetime=DateTime.Now
             };
            object obj = doctor.Insert();
            if (Convert.ToInt32(obj) > 0)
            {
                Response.Write(BaseCommon.ObjectToJson(new ReturnJsonType<string>() { code = 1, m = "成功" }));
            }
            else
            {
                Response.Write(BaseCommon.ObjectToJson(new ReturnJsonType<string>() { code = 0, m = "失败" }));
            }
            

        }

        private void UpdateDoctorWorkSchedule()
        {

            if (!IsReady())
                return;

            // data: { Action: "InsertDoctorWorkSchedule", OpenId: OpenId, ddlDate: ddlDate, ddlDayTime: ddlDayTime, ddlDoctorId: ddlDoctor, ddlDoctorName: ddlDoctorName, r: Math.random() },
            string OpenId = RequestKeeper.GetFormString(Request["OpenId"]);
            int Id = RequestKeeper.GetFormInt(Request["Id"]);
            string ddlDate = RequestKeeper.GetFormString(Request["ddlDate"]);
            string ddlDayTime = RequestKeeper.GetFormString(Request["ddlDayTime"]);
            string ddlDoctorId = RequestKeeper.GetFormString(Request["ddlDoctorId"]);
            string ddlDoctorName = RequestKeeper.GetFormString(Request["ddlDoctorName"]);

            if (string.IsNullOrEmpty(OpenId))
            {
                Response.Write(BaseCommon.ObjectToJson(new ReturnJsonType<string>() { code = -1, m = "失败" }));
                return;
            }
            DoctorWorkSchedule dws = DoctorBusiness.GetDoctorWorkSchedule(OpenId, Id);

            if (null == dws)
            {
                Response.Write(BaseCommon.ObjectToJson(new ReturnJsonType<string>() { code = -1, m = "失败" }));
                return;
            }
            
                dws.DoctorName = ddlDoctorName;
               
                dws.DayTime = Convert.ToInt32(ddlDayTime);
        
                dws.CreatorOpenId = OpenId;
                
                dws.DoctorId = Convert.ToInt32(ddlDoctorId);
             
                dws.WorkDateTime = Convert.ToDateTime(ddlDate);
             
                dws.WorkShortDate = Convert.ToInt32(Convert.ToDateTime(ddlDate).ToString("yyyyMMdd"));
         
            int obj = dws.Update();
            if (Convert.ToInt32(obj) > 0)
            {
                Response.Write(BaseCommon.ObjectToJson(new ReturnJsonType<string>() { code = 1, m = "成功" }));
            }
            else
            {
                Response.Write(BaseCommon.ObjectToJson(new ReturnJsonType<string>() { code = 0, m = "失败" }));
            }
        }

        private void InsertDoctorWorkSchedule()
        {

            if (!IsReady())
                return;
            // data: { Action: "InsertDoctorWorkSchedule", OpenId: OpenId, ddlDate: ddlDate, ddlDayTime: ddlDayTime, ddlDoctorId: ddlDoctor, ddlDoctorName: ddlDoctorName, r: Math.random() },
            string OpenId = RequestKeeper.GetFormString(Request["OpenId"]);
            string ddlDate = RequestKeeper.GetFormString(Request["ddlDate"]);
            string ddlDayTime = RequestKeeper.GetFormString(Request["ddlDayTime"]);
            string ddlDoctorId = RequestKeeper.GetFormString(Request["ddlDoctorId"]);
            string ddlDoctorName = RequestKeeper.GetFormString(Request["ddlDoctorName"]);
           
            if (string.IsNullOrEmpty(OpenId) )
            {
                Response.Write(BaseCommon.ObjectToJson(new ReturnJsonType<string>() { code = -1, m = "失败" }));
                return;
            }

            DoctorWorkSchedule dws = new DoctorWorkSchedule() { 
                 DoctorName=ddlDoctorName
                 , DayTime=Convert.ToInt32(ddlDayTime)
                 ,  CreatorOpenId=OpenId
                 , CreateDateTime=DateTime.Now
                 ,
                 DoctorId = Convert.ToInt32(ddlDoctorId)
                 , WorkDateTime=Convert.ToDateTime(ddlDate)
                 , WorkShortDate=Convert.ToInt32(Convert.ToDateTime(ddlDate).ToString("yyyyMMdd"))
            };
            object obj = dws.Insert();
            if (Convert.ToInt32(obj) > 0)
            {
                Response.Write(BaseCommon.ObjectToJson(new ReturnJsonType<string>() { code = 1, m = "成功" }));
            }
            else
            {
                Response.Write(BaseCommon.ObjectToJson(new ReturnJsonType<string>() { code = 0, m = "失败" }));
            }
            
        }


        
        private void SubmitApprove()
        {
            if (!IsReady())
                return;

            long mobile = RequestKeeper.GetFormLong(Request["mobile"]);
            string nickname = RequestKeeper.GetFormString(Request["nickname"]);
            string companyname = RequestKeeper.GetFormString(Request["companyname"]);
            string openId = RequestKeeper.GetFormString(Request["OpenId"]);

            Sys_User sUser =  UserBusiness.GetUserInfo(openId);
            if(sUser!=null && sUser.Id>0)
            {
                sUser.Mobile = mobile;
                sUser.NickName = nickname;
                sUser.CompanyName = companyname;
                sUser.ApproveFlag = (int)ApproveFlag.已提交认证申请;
                object obj = sUser.Update();
                if(Convert.ToInt32(obj)>0)
                {
                    Response.Write(BaseCommon.ObjectToJson(new ReturnJsonType<string>() { code = 1, m = "成功" }));
                }else
                {
                    Response.Write(BaseCommon.ObjectToJson(new ReturnJsonType<string>() { code = 0, m = "失败" }));
                }
            }
            else
            {
                Response.Write(BaseCommon.ObjectToJson(new ReturnJsonType<string>() { code = 0, m = "失败" }));
            }


        }


        private void UpdateProfile()
        {
            if (!IsReady())
                return;

            long mobile = RequestKeeper.GetFormLong(Request["mobile"]);
            string nickname = RequestKeeper.GetFormString(Request["nickname"]);
            string companyname = RequestKeeper.GetFormString(Request["companyname"]);
            string openId = RequestKeeper.GetFormString(Request["OpenId"]);

            Sys_User sUser = UserBusiness.GetUserInfo(openId);
            if (sUser != null && sUser.Id > 0)
            {
                sUser.Mobile = mobile;
                sUser.NickName = nickname;
                sUser.CompanyName = companyname;
                //sUser.ApproveFlag = (int)ApproveFlag.已提交认证申请;
                object obj = sUser.Update();
                if (Convert.ToInt32(obj) > 0)
                {
                    Response.Write(BaseCommon.ObjectToJson(new ReturnJsonType<string>() { code = 1, m = "成功" }));
                }
                else
                {
                    Response.Write(BaseCommon.ObjectToJson(new ReturnJsonType<string>() { code = 0, m = "失败" }));
                }
            }
            else
            {
                Response.Write(BaseCommon.ObjectToJson(new ReturnJsonType<string>() { code = 0, m = "失败" }));
            }


        }

        private void FansInfoComplete()
        {
            if (!IsReady())
                return;

            long mobile = RequestKeeper.GetFormLong(Request["mobile"]);
            string nickname = RequestKeeper.GetFormString(Request["nickname"]);
            
            string openId = RequestKeeper.GetFormString(Request["OpenId"]);

            Sys_User sUser = UserBusiness.GetUserInfo(openId);
            if (sUser != null && sUser.Id > 0)
            {
                sUser.Mobile = mobile;
                sUser.NickName = nickname;
                
                
                object obj = sUser.Update();
                if (Convert.ToInt32(obj) > 0)
                {
                    Response.Write(BaseCommon.ObjectToJson(new ReturnJsonType<string>() { code = 1, m = "成功" }));
                }
                else
                {
                    Response.Write(BaseCommon.ObjectToJson(new ReturnJsonType<string>() { code = 0, m = "失败" }));
                }
            }
            else
            {
                Response.Write(BaseCommon.ObjectToJson(new ReturnJsonType<string>() { code = 0, m = "失败" }));
            }


        }

        private void UpdateFansInfo()
        {
            if (!IsReady())
                return;

            long mobile = RequestKeeper.GetFormLong(Request["mobile"]);
            string nickname = RequestKeeper.GetFormString(Request["nickname"]);

            string openId = RequestKeeper.GetFormString(Request["OpenId"]);

            Sys_User sUser = UserBusiness.GetUserInfo(openId);
            if (sUser != null && sUser.Id > 0)
            {
                sUser.Mobile = mobile;
                sUser.NickName = nickname;

                object obj = sUser.Update();
                if (Convert.ToInt32(obj) > 0)
                {
                    Response.Write(BaseCommon.ObjectToJson(new ReturnJsonType<string>() { code = 1, m = "成功" }));
                }
                else
                {
                    Response.Write(BaseCommon.ObjectToJson(new ReturnJsonType<string>() { code = 0, m = "失败" }));
                }
            }
            else
            {
                Response.Write(BaseCommon.ObjectToJson(new ReturnJsonType<string>() { code = 0, m = "失败" }));
            }


        }


        private void Exchange()
        {
            if (!IsReady())
                return;

            string scoreStr = RequestKeeper.GetFormString(Request["score"]);
            string openId = RequestKeeper.GetFormString(Request["OpenId"]);
            decimal score = 0;
            decimal.TryParse(scoreStr, out score);
            if(score<=0)
            {
                Response.Write(BaseCommon.ObjectToJson(new ReturnJsonType<string>() { code = -1, m = "失败" }));
                return;
            }
            score = score * 100;//把前台的元扩大成存储的分的形式
            //申请 插入表 扣除相关的积分
            Sys_User sUser = UserBusiness.GetUserInfo(openId);
            if (sUser != null && sUser.Id > 0)
            {
                decimal validScore = ScoreBusiness.GetValidExchangeScore(sUser.OpenId, sUser.LastScore);
                if (score <= validScore)
                {
                    int obj = ScoreBusiness.UserExchangeScore(sUser, score);
                    if (Convert.ToInt32(obj) > 0)
                    {
                        Response.Write(BaseCommon.ObjectToJson(new ReturnJsonType<string>() { code = 1, m = "成功" }));
                    }
                    else
                    {
                        Response.Write(BaseCommon.ObjectToJson(new ReturnJsonType<string>() { code = 0, m = "失败" }));
                    }
                }else
                {
                    Response.Write(BaseCommon.ObjectToJson(new ReturnJsonType<string>() { code = 0, m = "失败,申请兑换的分数已大于可用分数" }));
                }
            }
            else
            {
                Response.Write(BaseCommon.ObjectToJson(new ReturnJsonType<string>() { code = 0, m = "失败" }));
            }
            
           
        }

        /// <summary>
        /// code值为:
        ///-105: 用户未登录.
        /// </summary>
        /// <returns></returns>
        private bool IsReady()
        {
            if (WeiXinBusiness.IsEnabledWeixinBrowser() && !WeiXinBusiness.IsSideInWeixinBrowser(Request.RequestContext.HttpContext))
            {
                return false;
            }
            return true;
        }

    }
}