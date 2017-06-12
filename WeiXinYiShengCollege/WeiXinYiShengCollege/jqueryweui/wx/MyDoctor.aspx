<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyDoctor.aspx.cs" Inherits="WeiXinYiShengCollege.WebSite.jqueryweui.wx.MyDoctor" %>



<!DOCTYPE html>
<html>
<head>
    <title>我的健康顾问</title>
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

        <a class="weui-cell weui-cell_access" href="javascript:;">
            <div class="weui-cell__bd">
                <p>姓名：<%=cM.NickName %></p>
            </div>
            
        </a>
        <a class="weui-cell weui-cell_access" href="javascript:;">
            <div class="weui-cell__bd">
                <p>电话：<%=cM.Mobile %></p>
            </div>
            
        </a>
        <a class="weui-cell weui-cell_access" href="javascript:;">
            <div class="weui-cell__bd">
                <p>单位名称：<%=cM.CompanyName %></p>
            </div>
            
        </a>
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
