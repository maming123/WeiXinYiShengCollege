﻿using System;
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
            //插入免责声明表
            dictAction.Add("InsertUserExceptions", InsertUserExceptions);
            //点赞
            dictAction.Add("InsertUserOpLogZan", InsertUserOpLogZan);
            //点收藏
            dictAction.Add("InsertMyCollectMedicine", InsertMyCollectMedicine);
            //提交问卷调查
            dictAction.Add("SubmitQuestion", SubmitQuestion);
            
        }

        private void SubmitQuestion()
        {
            string name = RequestKeeper.GetFormString(Request["Name"]);
            string sex = RequestKeeper.GetFormString(Request["Sex"]);
            string birthday = RequestKeeper.GetFormString(Request["Birthday"]);
            string profession = RequestKeeper.GetFormString(Request["Profession"]);
            string mobile = RequestKeeper.GetFormString(Request["Mobile"]);
            string sickness = RequestKeeper.GetFormString(Request["Sickness"]);

            if (String.IsNullOrEmpty(name))
            {
                Response.Write(BaseCommon.ObjectToJson(new ReturnJsonType<string>() { code = -1, m = "姓名不能为空" }));
                return;
            }
            if (String.IsNullOrEmpty(mobile))
            {
                Response.Write(BaseCommon.ObjectToJson(new ReturnJsonType<string>() { code = -1, m = "手机号不能为空" }));
                return;
            }
            if (String.IsNullOrEmpty(sickness))
            {
                Response.Write(BaseCommon.ObjectToJson(new ReturnJsonType<string>() { code = -1, m = "病症不能为空" }));
                return;
            }
            
            //查看是否用户已经注册，如果未注册那么在数据库中添加一条信息并设置parentId=尉迟的号
            string OpenId = UserBusiness.GetCookieOpenId();
            Sys_User tmpUser = UserBusiness.GetUserInfoForIsNotDelete(OpenId);

            DateTime dt = Convert.ToDateTime(birthday + " 00:00:00");
            Question qExist = QuestionBusiness.GetQuestion(OpenId);
            if(null!=qExist && qExist.Id>0)
            {
                Response.Write(BaseCommon.ObjectToJson(new ReturnJsonType<string>() { code = -900, m = "您已提交过问卷，请不要重复提交" }));
                return;
            }

            if (null == tmpUser)
            {
                //不存在该用户，并且OpenId长度》0则新创建该用户
                if (string.IsNullOrEmpty(OpenId))
                {
                    SNS.Library.Logs.LogDAOFactory.Write("OpenId为空", "", Convert.ToString((int)ErrorCode.NotFindOpenId), SNS.Library.Logs.LogType.Error);
                    
                    Response.Write(BaseCommon.ObjectToJson(new ReturnJsonType<string>() { code = -901, m = "OpenId为空,错误码:" + Convert.ToString((int)ErrorCode.NotFindOpenId) }));
                    return;

                }
                else
                {
                    //重新执行订阅逻辑（不知什么原因有可能在订阅时未将用户信息插入）
                    WeiXinBusiness.Subscribe("", OpenId);
                    tmpUser = UserBusiness.GetUserInfo(OpenId);
                    //然后更新手机号码和用户名
                    tmpUser.Mobile = Convert.ToInt64(mobile);
                    tmpUser.NickName = name;
                    tmpUser.UserType = (int)UserType.粉丝类型;
                    tmpUser.ParentId = 17;//17是数据库里边的尉迟
                    tmpUser.Update();
                }
            }

            bool bOk = QuestionBusiness.Add(new Question() { 
                OpenId=OpenId,
                 Name=name,
                  Birthday=dt,
                   Mobile=mobile,
                    Profession= profession,
                     Sex=sex,
                      Sickness=sickness,
                      CreateDateTime=DateTime.Now
            });

           if (bOk)
            {
                Response.Write(BaseCommon.ObjectToJson(new ReturnJsonType<string>() { code = 1, m = "成功" }));
            }
            else
            {
                Response.Write(BaseCommon.ObjectToJson(new ReturnJsonType<string>() { code = 0, m = "失败" }));
            }
        }

        private void InsertMyCollectMedicine()
        {
            if (!IsReady())
                return;
            int pointid = RequestKeeper.GetFormInt(Request["pointid"]);
            if (pointid <= 0)
            {
                Response.Write(BaseCommon.ObjectToJson(new ReturnJsonType<string>() { code = -1, m = "参数非法" }));
                return;
            }
            int userId = UserBusiness.GetCookieUserId();
            MyCollectMedicine userOp = new MyCollectMedicine()
            {
                CreateDateTime = DateTime.Now
                ,
                PointId = pointid
                ,
                UserId = userId
            };

            object obj = userOp.Insert();
            Sys_Point sPoint = Sys_Point.SingleOrDefault((int)pointid);
            sPoint.CollectCount++;
            sPoint.Update(new String[] { "CollectCount" });
            string cacheKey = string.Format(@"GetMedicine_{0}", sPoint.ModuleId);
            BaseCommon.CacheRemove(cacheKey);
            if (Convert.ToInt32(obj) > 0)
            {
                Response.Write(BaseCommon.ObjectToJson(new ReturnJsonType<string>() { code = 1, m = "成功" }));
            }
            else
            {
                Response.Write(BaseCommon.ObjectToJson(new ReturnJsonType<string>() { code = 0, m = "失败" }));
            }
        }

        private void InsertUserOpLogZan()
        {
            if (!IsReady())
                return;
            int pointid = RequestKeeper.GetFormInt(Request["pointid"]);
            if(pointid<=0)
            {
                Response.Write(BaseCommon.ObjectToJson(new ReturnJsonType<string>() { code = -1, m = "参数非法" }));
                return;
            }
            int userId = UserBusiness.GetCookieUserId();
            UserOpLog userOp = new UserOpLog() { 
             CreateDateTime=DateTime.Now
             , OptionType= (int)OptionType.zan
             , PointId=pointid
             ,
             UserId = userId
            };
            
            object obj = userOp.Insert();
            Sys_Point sPoint = Sys_Point.SingleOrDefault((int)pointid);
            sPoint.ZanCount++;
            sPoint.Update(new String[] { "ZanCount" });
            string cacheKey = string.Format(@"GetMedicine_{0}", sPoint.ModuleId);
            BaseCommon.CacheRemove(cacheKey);
            if (Convert.ToInt32(obj) > 0)
            {
                Response.Write(BaseCommon.ObjectToJson(new ReturnJsonType<string>() { code = 1, m = "成功" }));
            }
            else
            {
                Response.Write(BaseCommon.ObjectToJson(new ReturnJsonType<string>() { code = 0, m = "失败" }));
            }
        }

        private void InsertUserExceptions()
        {
            if (!IsReady())
                return;
            int moduleId = RequestKeeper.GetFormInt(Request["moduleId"]);
            int linkType = RequestKeeper.GetFormInt(Request["linkType"]);
            if (moduleId <= 0 || linkType<=0)
            {
                Response.Write(BaseCommon.ObjectToJson(new ReturnJsonType<string>() { code = -1, m = "参数非法" }));
                return;
            }
            int userId = UserBusiness.GetCookieUserId();
            ExceptionsType et = ExceptionsType.经典方剂;
            if (linkType == (int)SysModuleLinkType.临证参考)
            {
                et = ExceptionsType.临证参考;
            }
            else if (linkType == (int)SysModuleLinkType.经典方剂)
            {
                et = ExceptionsType.经典方剂;
            }

            UserExceptionsRecord uer = new UserExceptionsRecord()
            {
                CreateDateTime = DateTime.Now
                 ,
                ExceptionsType = (int)et
                 ,
                UserId = userId
            };
            object obj =uer.Insert();
            if (Convert.ToInt32(obj) > 0)
            {
                Response.Write(BaseCommon.ObjectToJson(new ReturnJsonType<string>() { code = 1, m = "成功" }));
            }
            else
            {
                Response.Write(BaseCommon.ObjectToJson(new ReturnJsonType<string>() { code = 0, m = "失败" }));
            }

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
            if (WeiXinBusiness.IsEnabledWeixinBrowser() 
                && !WeiXinBusiness.IsSideInWeixinBrowser(Request.RequestContext.HttpContext)
                && UserBusiness.GetCookieUserId()>0)
            {
                return false;
            }
            return true;
        }

    }
}