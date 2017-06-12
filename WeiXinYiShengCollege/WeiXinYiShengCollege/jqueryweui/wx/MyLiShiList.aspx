<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyLiShiList.aspx.cs" Inherits="WeiXinYiShengCollege.WebSite.jqueryweui.wx.MyLiShiList" %>



<!DOCTYPE html>
<html>
<head>
    <title>我的理事列表</title>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no">

    <meta name="description" content="">
    <link rel="stylesheet" href="../lib/weui.min.css">
    <link rel="stylesheet" href="../css/jquery-weui.css">
    <link rel="stylesheet" href="css/demos.css">
</head>

<body ontouchstart>

    <div class="weui-cells">
        <%foreach(Module.Models.Sys_User sUser in myLiShiList) {%>
        
        <a class="weui-cell weui-cell_access" href="javascript:;">
            <div class="weui-cell__bd">
                <p><%=sUser.NickName %></p>
            </div>
            <div class="weui-cell__ft">手机(<%=sUser.Mobile %>)总积分(<%=sUser.LastScore/100 %>)</div>
        </a>

        <%} %>

    </div>
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