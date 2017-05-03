using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Module.Models;
using Module.Utils;

namespace WeiXinYiShengCollege.Business
{
    /// <summary>
    /// 积分兑换类
    /// </summary>
    public class ScoreBusiness
    {
        /// <summary>
        /// 缓存秒
        /// </summary>
        public static int cacheSecond = 5 * 60;

        /// <summary>
        /// 获取积分和钱的配置
        /// </summary>
        /// <returns></returns>
        public static List<ScoreMoneyConfig> GetScoreCfg()
        {
            string cacheKey = string.Format(@"GetScoreCfg");
            if (BaseCommon.HasCache(cacheKey))
            {
                return BaseCommon.GetCache<List<ScoreMoneyConfig>>(cacheKey);
            }

            List<ScoreMoneyConfig> list = ScoreMoneyConfig.Query("").ToList();
            if (null != list)
            {
                BaseCommon.CacheInsert(cacheKey, list, DateTime.Now.AddSeconds(cacheSecond));
                return list;
            }
            return null;
        }

        /// <summary>
        /// 1分钱换多少积分
        /// </summary>
        /// <param name="sUser"></param>
        /// <returns></returns>
        private static decimal GetScoreByOneCentMoney(int userLevel,int userType)
        {
            //积分兑换配置
            List<ScoreMoneyConfig> listScoreCfg = GetScoreCfg();
            foreach(ScoreMoneyConfig cfg in listScoreCfg)
            {
                if(userLevel ==cfg.UserLevel && userType == cfg.UserType)
                {
                    return Convert.ToDecimal(cfg.Score)/cfg.Money;
                }
            }
            return 0;
        }

        /// <summary>
        /// 获取积分日志
        /// </summary>
        /// <param name="openId"></param>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public static AddScoreLog GetScoreLogItem(string openId, string orderid)
        {
            string strSql = string.Format(@"where OpenId=@0 and OrderId=@1");
            AddScoreLog item = AddScoreLog.SingleOrDefault(strSql, openId, orderid);
            if (null != item)
                return item;
            else
                return null;
        }

        /// <summary>
        /// 添加积分
        /// </summary>
        /// <param name="info">订单信息</param>
        public static void AddScore(OrderInfo info)
        {
            //查看是否添加过积分，如果没添加过则添加积分
            AddScoreLog item = GetScoreLogItem(info.BuyerOpenId, info.OrderId);
            if (null != item)
            {
                //已添加过记录不作处理
            }
            else
            {
                Sys_User sUser = UserBusiness.GetUserInfo(info.BuyerOpenId);
                if (null != sUser)
                {
                    //看看用户是什么类型 怎么加分  
                    decimal scoreForOneCent = GetScoreByOneCentMoney(Convert.ToInt32(sUser.UserLevel),sUser.UserType);

                    //TODO:用事务保证积分一致性 未添加积分 则添加积分 记录添加日志
                    var db = CoreDB.GetInstance();
                    try
                    {
                        db.BeginTransaction();
                        
                        AddScoreLog itemNew = new AddScoreLog() { OpenId = info.BuyerOpenId, OrderId = info.OrderId, CreateDateTime = DateTime.Now, OrderTotalPrice = info.OrderTotalPrice, Score = info.OrderTotalPrice * scoreForOneCent, ExchangeStatus=0 };

                        object obj = db.Insert(itemNew);
                        
                        //钱和积分的换算
                        string strUpdate = string.Format(@"update Sys_User set Score=Score+@0,LastScore=LastScore+@0  where OpenId=@1");
                        int r = db.Execute(strUpdate, itemNew.Score, itemNew.OpenId);
                        
                        #region //查看用户是否有上级（理事）如果有 那么给理事添加相应的积分在
                        if (sUser.ParentId>0)
                        {
                            //有上级
                            Sys_User parentUser = db.SingleOrDefault<Sys_User>((object)sUser.ParentId);
                            //看看用户是什么类型 怎么加分  
                            decimal scoreForOneCentForParent = GetScoreByOneCentMoney(Convert.ToInt32(parentUser.UserLevel), parentUser.UserType);
                            AddScoreLog itemParentNew = new AddScoreLog() { OpenId = parentUser.OpenId, OrderId = "0", CreateDateTime = DateTime.Now, OrderTotalPrice = info.OrderTotalPrice, Score = info.OrderTotalPrice * scoreForOneCentForParent, ExchangeStatus = 0 };
                            object objParent = db.Insert(itemParentNew);
                            //钱和积分的换算
                            String strUpdateParent = string.Format(@"update Sys_User set Score=Score+@0,LastScore=LastScore+@0  where OpenId=@1");
                            int rParent = db.Execute(strUpdateParent, itemParentNew.Score, itemParentNew.OpenId);
                            if (rParent <= 0)
                            {
                                LogHelper.WriteLogError(typeof(ScoreBusiness)
                                    , string.Format(@"未能增加Sys_User相应的Score，OpenId：{0},Score:{1},{2}", itemParentNew.OpenId, itemParentNew.Score, strUpdate)
                                    );
                                db.AbortTransaction();
                                return;
                            }
                        }
                        #endregion

                        if (r <= 0 )
                        {
                            LogHelper.WriteLogError(typeof(ScoreBusiness)
                                , string.Format(@"未能增加Sys_User相应的Score，OpenId：{0},Score:{1},{2}", itemNew.OpenId, itemNew.Score, strUpdate)
                                );
                            db.AbortTransaction();
                            return;
                        }
                        db.CompleteTransaction();
                    }
                    catch(Exception ex)
                    {
                        db.AbortTransaction();
                        LogHelper.WriteLogError(typeof(ScoreBusiness), ex);
                    }
                    finally
                    {
                        db.CloseSharedConnection();
                    }
                }
            }
        }

        /// <summary>
        /// 用户申请兑换积分
        /// </summary>
        /// <param name="score"></param>
        public static int UserExchangeScore(Sys_User sUser,decimal score)
        {
            int result = 0;
            decimal scoreInput =score;
            var db = CoreDB.GetInstance();
            try
            {
                //看看用户是什么类型 怎么加分  
                decimal scoreForOneCent = GetScoreByOneCentMoney(Convert.ToInt32(sUser.UserLevel),sUser.UserType);

                db.BeginTransaction();
                ExchangeLog exchange = new ExchangeLog()
                {
                    CreateDatetime = DateTime.Now
                    , Score=scoreInput
                    ,
                    Money = scoreInput /*目前定的积分兑换钱是 1:1兑换关系 （1积分兑换1分钱）*/
                    , OpenId=sUser.OpenId
                    , NickName=sUser.NickName
                    , PayStatus= (int)PayStatus.Paying
                };
                db.Insert(exchange);
                //减少积分

                //string strUpdate = string.Format(@"update Sys_User set Score=Score-@0,LastScore=LastScore-@0  where OpenId=@1");
                string strUpdate = string.Format(@"update Sys_User set LastScore=LastScore-@0  where OpenId=@1");
                int r = db.Execute(strUpdate, score, sUser.OpenId);
                if(r<=0)
                {
                    LogHelper.WriteLogError(typeof(ScoreBusiness)
                               , string.Format(@"未能减少Sys_User相应的Score，OpenId：{0},Score:{1},{2}", sUser.OpenId, score, strUpdate)
                               );
                    
                    db.AbortTransaction();
                }
                db.CompleteTransaction();
                result = 1;
            }
            catch (Exception ex)
            {
                db.AbortTransaction();
                LogHelper.WriteLogError(typeof(ScoreBusiness), ex);
            }
            finally
            {
                db.CloseSharedConnection();
            }
            return result;
        }

        /// <summary>
        /// 获取不可兑换的积分（规则暂定：积分的可兑换规则：每月5号之后可以提取每月5号之前所有未兑换的积分）
        /// </summary>
        /// <returns></returns>
        private static decimal GetInValidExchangeScore(string openId)
        {
            //查找指定规则不可兑换的积分
            ////每月5号之后可以提取每月5号前的所有未兑换积分
            DateTime dtbegin=DateTime.Now;
            DateTime dtend =DateTime.Now;
            if(DateTime.Now.Day-5<=0)
            {
               dtbegin =  Convert.ToDateTime(DateTime.Now.AddMonths(-1).ToString("yyyy-MM-5"));
               dtend =DateTime.Now;
            }else
            {
               dtbegin =  Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-5"));
               dtend =DateTime.Now;
            }
            //先默认可兑换全部,待正式上线再注释掉
            dtbegin = dtend.AddYears(1);

            //不可兑换的积分
            decimal totalNoExChangeScore=0;
            List<AddScoreLog> listScoreLog =AddScoreLog.Query("where  OpenId=@2 and CreateDateTime between @0 and @1 ",dtbegin,dtend,openId).ToList();
            if(null !=listScoreLog)
            {
                totalNoExChangeScore =listScoreLog.Sum(m => m.Score);
            }

            return totalNoExChangeScore;
            
        }

        /// <summary>
        /// 获取可用的兑换积分 公式： 剩余积分(sys_user的lastscore) 减去 不可兑换积分 = 可兑换的积分
        /// </summary>
        /// <param name="openId"></param>
        /// <returns></returns>
        public static decimal GetValidExchangeScore(string openId,decimal lastScore)
        {
            // 剩余积分(sys_user的lastscore) 减去 不可兑换积分 = 可兑换的积分
            decimal validScore = lastScore - GetInValidExchangeScore(openId);
            return validScore;
        }

        /// <summary>
        /// 获取指定用户的积分兑换列表
        /// </summary>
        /// <param name="openId"></param>
        /// <returns></returns>
        public static List<ExchangeLog> GetExchangeLogList(string openId)
        {
            List<ExchangeLog> list = ExchangeLog.Query("where OpenId=@0 order by CreateDatetime desc", openId).ToList();
            return list;
        }

        /// <summary>
        /// 积分兑换列表
        /// </summary>
        /// <param name="payStatus">兑换状态</param>
        /// <param name="dtBegin">积分申请兑换开始时间</param>
        /// <param name="dtEnd">积分申请兑换结束时间</param>
        /// <returns></returns>
        public static List<ExchangeLog> GetExchangeLogList(int payStatus, DateTime dtBegin, DateTime dtEnd,long mobile)
        {
            String strSql = string.Format(@"select * from ExchangeLog where 1=1 ");

            if (mobile>0)
            {
                strSql += string.Format(@" and Mobile ={0}", mobile);
            }
            if (payStatus>=0)
            {
                strSql += string.Format(@" and PayStatus ={0}", payStatus);
            }
            if (null != dtBegin)
            {
                strSql += string.Format(@" and CreateDatetime >='{0}'", dtBegin.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            if (null != dtEnd)
            {
                strSql += string.Format(@" and CreateDatetime <='{0}'", dtEnd.ToString("yyyy-MM-dd HH:mm:ss"));
            }

            List<ExchangeLog> list = ExchangeLog.Query(strSql).ToList();
            return list;

        }
    }
}
