<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateQrCodeUI.aspx.cs" Inherits="WeiXinYiShengCollege.WebSite.jqueryweui.wx.CreateQrCodeUI" %>

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
        .icon-box {
            margin-bottom: 25px;
            display: -webkit-box;
            display: -webkit-flex;
            display: flex;
            -webkit-box-align: center;
            -webkit-align-items: center;
            align-items: center;
        }
    </style>
</head>

<body ontouchstart>

    <div class="icon-box">
        <img src="<%=ImgUrl %>" />
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
