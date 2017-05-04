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
</head>

<body ontouchstart>

    <%--认证通过了才能看到菜单--%>
    <%if (sUser.ApproveFlag == 1)
      { %>
    <header class='demos-header'>
        <h1 class="demos-title"><%=sUser.NickName %></h1>
    </header>

    <div class="page__bd">

        <div class="weui-panel">
            <div class="weui-panel__bd">
                <div class="weui-media-box weui-media-box_small-appmsg">
                    <div class="weui-cells">
                        <a class="weui-cell weui-cell_access" href="javascript:;">
                            <div class="weui-cell__bd weui-cell_primary">
                                <p>我的级别(<%=Enum.GetName(typeof(UserLevel),sUser.UserLevel) %>)</p>
                            </div>
                            
                        </a>
                       
                        <a class="weui-cell weui-cell_access" href="javascript:;">

                            <div class="weui-cell__bd weui-cell_primary">
                                <p>我的积分(<%=sUser.LastScore %>)</p>
                            </div>

                        </a>
                        <a class="weui-cell weui-cell_access" href="MyFansList.aspx?Id=<%=sUser.Id %>">

                            <div class="weui-cell__bd weui-cell_primary">
                                <p>我的粉丝</p>
                            </div>
                            <span class="weui-cell__ft"></span>
                        </a>
                        <a class="weui-cell weui-cell_access" href="MyCustomerManager.aspx?CustomerManagerId=<%=sUser.CustomerManagerId %>">

                            <div class="weui-cell__bd weui-cell_primary">
                                <p>我的客服经理</p>
                            </div>
                            <span class="weui-cell__ft"></span>
                        </a>
                        <a class="weui-cell weui-cell_access"  href="MyOrderList.aspx?OpenId=<%=sUser.OpenId %>">

                            <div class="weui-cell__bd weui-cell_primary">
                                <p>我买了什么</p>
                            </div>
                            <span class="weui-cell__ft"></span>
                        </a>
                         <a class="weui-cell weui-cell_access" href="CreateQrCodeUI.aspx?OpenId=<%=sUser.OpenId %>">
                            <div class="weui-cell__bd weui-cell_primary">
                                <p>生成二维码</p>
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
                                <p>积分兑换记录</p>
                            </div>
                            <span class="weui-cell__ft"></span>
                        </a>
                        <a class="weui-cell weui-cell_access" href="ProfileEdit.aspx?OpenId=<%=sUser.OpenId %>">

                            <div class="weui-cell__bd weui-cell_primary">
                                <p>个人信息设置</p>
                            </div>
                            <span class="weui-cell__ft"></span>
                        </a>
                        <a class="weui-cell weui-cell_access" href="DoctorManage.aspx?OpenId=<%=sUser.OpenId %>">

                            <div class="weui-cell__bd weui-cell_primary">
                                <p>出诊医生设置</p>
                            </div>
                            <span class="weui-cell__ft"></span>
                        </a>
                        <a class="weui-cell weui-cell_access" href="DoctorWorkManage.aspx?OpenId=<%=sUser.OpenId %>">

                            <div class="weui-cell__bd weui-cell_primary">
                                <p>出诊排期设置</p>
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


