using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Module.Models;
using Module.Utils;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Sample.CommonService.Download;
using WeiXinYiShengCollege.Business;

namespace Senparc.Weixin.MP.Sample.CommonService.MessageHandlers.YSMsgHandler
{
    public partial class YSMsgHandler
    {

        private string GetWelcomeInfo()
        {
            return string.Format(@"欢迎关注易生大健康，账号正在建设中。");
        }

        public string GetDownloadInfo(CodeRecord codeRecord)
        {
            return string.Format(@"您已通过二维码验证，
当前选择的版本：v{0}", codeRecord.Version);
        }

        /// <summary>
        /// 通过二维码扫描关注扫描事件
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnEvent_ScanRequest(RequestMessageEvent_Scan requestMessage)
        {
            //通过扫描关注
            var responseMessage = CreateResponseMessage<ResponseMessageText>();

            //下载文档
            if (!string.IsNullOrEmpty(requestMessage.EventKey))
            {
                var sceneId = long.Parse(requestMessage.EventKey.Replace("qrscene_", ""));
                //var configHelper = new ConfigHelper(new HttpContextWrapper(HttpContext.Current));
                var codeRecord =
                    ConfigHelper.CodeCollection.Values.FirstOrDefault(z => z.QrCodeTicket != null && z.QrCodeId == sceneId);


                if (codeRecord != null)
                {
                    //确认可以下载
                    codeRecord.AllowDownload = true;
                    responseMessage.Content = GetDownloadInfo(codeRecord);
                }
            }

            responseMessage.Content = responseMessage.Content ?? string.Format("通过扫描二维码进入，场景值：{0}", requestMessage.EventKey);

            return responseMessage;
        }

        /// <summary>
        /// 订阅（关注）事件
        /// </summary>
        /// <returns></returns>
        public override IResponseMessageBase OnEvent_SubscribeRequest(RequestMessageEvent_Subscribe requestMessage)
        {
            var responseMessage = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageText>(requestMessage);
            responseMessage.Content = GetWelcomeInfo();
            //订阅，需要插入到数据库 新创建用户
           bool bIsExists = UserBusiness.IsExistUser(requestMessage.FromUserName);
           if (!bIsExists)
           {
               Sys_User newUser = new Sys_User()
               {
                   ApproveFlag = Convert.ToInt32(ApproveFlag.未认证),
                   City = "",
                   CompanyName = "",
                   CreateDateTime = DateTime.Now,
                   CustomerManagerId = 0,
                   Email = "",
                   IsDelete = 0,
                   Mobile = 0,
                   NickName = "",
                   OpenId = requestMessage.FromUserName,
                   ParentId = 0,
                   PassWord = "",
                   Province = "",
                   QrCodeScene_id = 0,
                   Remark = "",
                   Score = 0,
                   UserInfoJson = "",
                   UserLevel = Convert.ToInt32(UserLevel.未分配),
                   UserType = Convert.ToInt32(UserType.未分配)
               };
               newUser.Insert();
           }else
           {
               //存在 那么看是否携带场景值，如果携带 证明是谁的粉丝 那么更新parentid 为QrCodeScene_id的ID
               if (!string.IsNullOrEmpty(requestMessage.EventKey))
               {
                   responseMessage.Content += "\r\n============\r\n场景值：" + requestMessage.EventKey;
               }

           }
            
            
            //推送消息
            //下载文档
            if (requestMessage.EventKey.StartsWith("qrscene_"))
            {
                var sceneId = long.Parse(requestMessage.EventKey.Replace("qrscene_", ""));
                //var configHelper = new ConfigHelper(new HttpContextWrapper(HttpContext.Current));
                var codeRecord =
                    ConfigHelper.CodeCollection.Values.FirstOrDefault(z => z.QrCodeTicket != null && z.QrCodeId == sceneId);

                if (codeRecord != null)
                {
                    //确认可以下载
                    codeRecord.AllowDownload = true;
                    AdvancedAPIs.CustomApi.SendText(null, WeixinOpenId, GetDownloadInfo(codeRecord));
                }
            }


            
            return responseMessage;
        }
    }
}
