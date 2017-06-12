<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyExchangeList.aspx.cs" Inherits="WeiXinYiShengCollege.WebSite.jqueryweui.wx.MyExchangeList" %>


<!DOCTYPE html>
<html>
<head>
    <title>积分兑换日志</title>
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
        <%foreach(Module.Models.ExchangeLog exchange in myExchangeList) {%>
        
        <a class="weui-cell weui-cell_access" href="javascript:;">
            <div class="weui-cell__bd">
                <%--<p>2017年3月28日已成功兑换80积分</p>--%>
                <p>
                <%if(exchange.PayStatus == (int)WeiXinYiShengCollege.Business.PayStatus.HavePayed){ %>
                
                <%=String.Format(@"{0}已成功兑换{1}积分",exchange.CreateDatetime.ToString("yyyy年MM月dd日"),exchange.Score/100) %>
                <%} %>
                <%if(exchange.PayStatus == (int)WeiXinYiShengCollege.Business.PayStatus.Paying){ %>
                
                <%=String.Format(@"{0}已申请兑换{1}积分",exchange.CreateDatetime.ToString("yyyy年MM月dd日"),exchange.Score/100) %>
                <%} %>
                </p>
            </div>
            
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

