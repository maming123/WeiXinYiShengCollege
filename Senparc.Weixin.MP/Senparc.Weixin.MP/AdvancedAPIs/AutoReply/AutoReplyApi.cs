﻿#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2017 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file
except in compliance with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the
License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
either express or implied. See the License for the specific language governing permissions
and limitations under the License.

Detail: https://github.com/JeffreySu/WeiXinMPSDK/blob/master/license.md

----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2017 Senparc
    
    文件名：AutoReplyApi.cs
    文件功能描述：获取自动回复规则接口
    
    
    创建标识：Senparc - 20150907

    修改标识：Senparc - 20160718
    修改描述：将其接口增加了异步方法
----------------------------------------------------------------*/

/*
    Api地址：http://mp.weixin.qq.com/wiki/7/7b5789bb1262fb866d01b4b40b0efecb.html
 */



using System.Threading.Tasks;
using Senparc.Weixin.MP.AdvancedAPIs.AutoReply;
using Senparc.Weixin.MP.CommonAPIs;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 获取自动回复规则
    /// </summary>
    public static class AutoReplyApi
    {
        #region 同步请求

        /// <summary>
        /// 获取自动回复规则
        /// </summary>
        /// <param name="accessTokenOrAppId">调用接口凭证</param>
        /// <returns></returns>
        public static GetCurrentAutoreplyInfoResult GetCurrentAutoreplyInfo(string accessTokenOrAppId)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = "https://api.weixin.qq.com/cgi-bin/get_current_autoreply_info?access_token={0}";

                return CommonJsonSend.Send<GetCurrentAutoreplyInfoResult>(accessToken, urlFormat, null, CommonJsonSendType.GET);

            }, accessTokenOrAppId);
        }
        #endregion

    }
}