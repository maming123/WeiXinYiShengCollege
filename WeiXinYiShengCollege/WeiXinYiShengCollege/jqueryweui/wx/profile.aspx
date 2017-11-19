<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="WeiXinYiShengCollege.WebSite.wx.profile" %>

<%@ Import Namespace="WeiXinYiShengCollege.Business" %>

<!DOCTYPE html>
<html>
<head>
    <title>个人中心</title>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no">

    <meta name="description" content="">

    <link rel="stylesheet" href="../lib/weui.min.css">
    <link rel="stylesheet" href="../css/jquery-weui.css">
    <link rel="stylesheet" href="css/demos.css">
    <style>
        .demos-title {
            text-align: center;
            font-size: 30px;
            color: #3cc51f;
            font-weight: 400;
            margin: 0px 12px 0px 20px;
        }
    </style>
</head>

<body ontouchstart>

    <%--认证通过了才能看到菜单--%>
    <%if (sUser.ApproveFlag == 1)
      { %>
    <header style="padding:10px 0;background-color:#f2f2f2;">
        <div style="margin-left:10px;"><img id="imgPhoto" style="border-radius: 100px;border: none; width:120px;height:120px;" src="<%=sUser.HeadImgUrl %>" />
        <span class="demos-title"><%=sUser.NickName %></span> <span>(<%=Enum.GetName(typeof(UserLevel),sUser.UserLevel) %>)</span>
        </div>
    </header>

    <div class="page__bd">

        <div class="weui-panel">
            <div class="weui-panel__bd">
                <div class="weui-media-box weui-media-box_small-appmsg">
                    <div class="weui-cells">
                        <a class="weui-cell weui-cell_access" href="javascript:;">

                            <div class="weui-cell__bd weui-cell_primary">
                                <p>我的积分(<%=sUser.LastScore/100 %>)</p>
                            </div>
                        </a>
                         <%if(sUser.UserLevel== (int)UserLevel.荣誉理事){%>
                         <a class="weui-cell weui-cell_access" href="MyLiShiList.aspx?Id=<%=sUser.Id %>">

                            <div class="weui-cell__bd weui-cell_primary">
                                <p>我的理事</p>
                            </div>
                            <span class="weui-cell__ft"></span>
                        </a>
                        <%} %>
                        <a class="weui-cell weui-cell_access" href="MyFansList.aspx?Id=<%=sUser.Id %>">

                            <div class="weui-cell__bd weui-cell_primary">
                                <p>我的粉丝</p>
                            </div>
                            <span class="weui-cell__ft"></span>
                        </a>
                       <a class="weui-cell weui-cell_access"  href="MyOrderList.aspx?OpenId=<%=sUser.OpenId %>">

                            <div class="weui-cell__bd weui-cell_primary">
                                <p>我的消费</p>
                            </div>
                            <span class="weui-cell__ft"></span>
                        </a>
                         <a class="weui-cell weui-cell_access" href="CreateQrCodeUI.aspx?OpenId=<%=sUser.OpenId %>">
                            <div class="weui-cell__bd weui-cell_primary">
                                <p>我的二维码</p>
                            </div>
                            <span class="weui-cell__ft"></span>
                        </a>
                        <%if(UserBusiness.isInWhiteList()){ %>
                        <a class="weui-cell weui-cell_access" href="MyCollect.aspx?OpenId=<%=sUser.OpenId %>">
                            <div class="weui-cell__bd weui-cell_primary">
                                <p>我的收藏</p>
                            </div>
                            <span class="weui-cell__ft"></span>
                        </a>
                        
                        <a class="weui-cell weui-cell_access" href="MedicineList.aspx?moduleId=2&linkType=1">
                            <div class="weui-cell__bd weui-cell_primary">
                                <p>临证参考</p>
                            </div>
                            <span class="weui-cell__ft"></span>
                        </a>
                         <a class="weui-cell weui-cell_access" href="MedicineList.aspx?moduleId=6&linkType=2">
                            <div class="weui-cell__bd weui-cell_primary">
                                <p>经典方剂</p>
                            </div>
                            <span class="weui-cell__ft"></span>
                        </a>
                        <%} %>
                        <a class="weui-cell weui-cell_access" href="MyCustomerManager.aspx?CustomerManagerId=<%=sUser.CustomerManagerId %>">

                            <div class="weui-cell__bd weui-cell_primary">
                                <p>客服经理</p>
                            </div>
                            <span class="weui-cell__ft"></span>
                        </a>
                        
                        
                        <a class="weui-cell weui-cell_access" href="MyExchange.aspx?OpenId=<%=sUser.OpenId %>">

                            <div class="weui-cell__bd weui-cell_primary">
                                <p>兑换积分</p>
                            </div>
                            <span class="weui-cell__ft"></span>
                        </a>
                        <a class="weui-cell weui-cell_access" href="MyExchangeList.aspx?OpenId=<%=sUser.OpenId %>">

                            <div class="weui-cell__bd weui-cell_primary">
                                <p>兑换记录</p>
                            </div>
                            <span class="weui-cell__ft"></span>
                        </a>
                        <a class="weui-cell weui-cell_access" href="ProfileEdit.aspx?OpenId=<%=sUser.OpenId %>">

                            <div class="weui-cell__bd weui-cell_primary">
                                <p>个人设置</p>
                            </div>
                            <span class="weui-cell__ft"></span>
                        </a>
                        <a class="weui-cell weui-cell_access" href="DoctorManage.aspx?OpenId=<%=sUser.OpenId %>">

                            <div class="weui-cell__bd weui-cell_primary">
                                <p>出诊专家设置</p>
                            </div>
                            <span class="weui-cell__ft"></span>
                        </a>
                        <a class="weui-cell weui-cell_access" href="DoctorWorkManage.aspx?OpenId=<%=sUser.OpenId %>">

                            <div class="weui-cell__bd weui-cell_primary">
                                <p>出诊日期设置</p>
                            </div>
                            <span class="weui-cell__ft"></span>
                        </a>
                        <a class="weui-cell weui-cell_access" href="ProfileMusicType.aspx?OpenId=<%=sUser.OpenId %>">

                            <div class="weui-cell__bd weui-cell_primary">
                                <p>五音疗愈曲目</p>
                            </div>
                            <span class="weui-cell__ft"></span>
                        </a>
                    </div>
                </div>
            </div>
        </div>

    </div>
    <%}
      else
      { %>
    <header class='demos-header'>
        <h1 class="demos-title">
            <%if(sUser.ApproveFlag== (int)ApproveFlag.已提交认证申请){ %>
              已提交认证申请,请耐心等待审核结果，请关闭此窗口回到个人中心
            <%} else{%>
            <%=Enum.GetName(typeof(ApproveFlag),sUser.ApproveFlag)%>
            <%} %>
        </h1>
    </header>
    <%} %>

    <script src="../lib/jquery-2.1.4.js"></script>
    <script src="../lib/fastclick.js"></script>
    <script>
        $(function () {
            FastClick.attach(document.body);
        });
    </script>
    <script src="../js/jquery-weui.js"></script>

</body>
</html>


