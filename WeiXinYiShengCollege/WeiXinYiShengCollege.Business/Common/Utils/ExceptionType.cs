using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Module.Utils
{
    /// <summary>
    /// 统一异常数字提示
    /// </summary>
    public class ExceptionType
    {
        /// <summary>
        /// 1成功
        /// </summary>
        public const int Success = 1;

        /// <summary>
        /// 0失败
        /// </summary>
        public const int Error = 0;

        /// <summary>
        /// -1学分不足
        /// </summary>
        public const int NoCoin = -1;

        /// <summary>
        /// -100活动已结束
        /// </summary>
        public const int ActivityFinished = -100;

        /// <summary>
        /// -101活动未开始
        /// </summary>
        public const int ActivityUnStart = -101;

        /// <summary>
        /// -102系统异常
        /// </summary>
        public const int SystemError = -102;

        /// <summary>
        /// -103密码错误
        /// </summary>
        public const int PasswordError = -103;

        /// <summary>
        /// -104非CM用户
        /// </summary>
        public const int NotMobileUserError = -104;

        /// <summary>
        /// -105用户未登录
        /// </summary>
        public const int NotLogin = -105;

        /// <summary>
        /// -106配置异常
        /// </summary>
        public const int ConfigError = -106;

        /// <summary>
        /// -107无效的参数信息
        /// </summary>
        public const int ArgumentError = -107;

        /// <summary>
        /// -108IP未在白名单
        /// </summary>
        public const int IPError = -108;

        /// <summary>
        /// -109接口信息不存在
        /// </summary>
        public const int InterfaceNotExist = -109;

        /// <summary>
        /// -110业务线信息不存在
        /// </summary>
        public const int SiteIdNotExist = -110;

        /// <summary>
        /// -111未授权
        /// </summary>
        public const int NotAuthorize = -111;

        public const int ErrorLevel = -112;

        /// <summary>
        /// -113 PC客户端不在线
        /// </summary>
        public const int NotPcOnline = -113;

        /// <summary>
        /// -114 抽奖未开始
        /// </summary>
        public const int NotTryAwardDate = -114;

        /// <summary>
        /// -115 抽奖结束
        /// </summary>
        public const int TryAwardFinished = -115;

        /// <summary>
        /// -116: 不是好友关系.
        /// </summary>
        public const int NotBuddy = -116;

        /// <summary>
        /// -117: 非指定省份用户.
        /// </summary>
        public const int NotGivenProvinceUser = -117;

        /// <summary>
        /// -118: 在黑名单中.
        /// </summary>
        public const int InBlackList = -118;

        /// <summary>
        /// -119: 时间点不处于当天所允许进行抽奖的时间间隔内.
        /// </summary>
        public const int NotInTryAwardTimeScope = -119;

        /// <summary>
        /// -120: 还没到抽奖开放日期.
        /// </summary>
        public const int TryAwardUnStarted = -120;

        /// <summary>
        /// -121: 奖品支付失败.
        /// </summary>
        public const int PayAwardFailure = -121;

        /// <summary>
        /// -122: 指定用户不存在
        /// </summary>
        public const int NotFindUser = -122;

        /// <summary>
        /// -123: 不可以添加自己为好友
        /// </summary>
        public const int NotInsertSelf = -123;

        /// <summary>
        /// -124: 已经是好友关系了
        /// </summary>
        public const int ThisMyFriend = -124;

        /// <summary>
        /// 程序在更新维护中
        /// </summary>
        public const int AppUpding = -125;
        /// <summary>
        /// 验证码错误
        /// </summary>
        public const int ValidCodeError = -126;
        /// <summary>
        /// 不是会员
        /// </summary>
        public const int IsNotVip = -127;
    }
}