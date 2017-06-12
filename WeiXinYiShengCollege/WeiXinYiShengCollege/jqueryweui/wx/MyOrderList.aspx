<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyOrderList.aspx.cs" Inherits="WeiXinYiShengCollege.WebSite.jqueryweui.wx.MyOrderList" %>



<!DOCTYPE html>
<html>
<head>
    <title>我的订单列表</title>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no">

    <meta name="description" content="">
    <link rel="stylesheet" href="../lib/weui.min.css">
    <link rel="stylesheet" href="../css/jquery-weui.css">
    <link rel="stylesheet" href="css/demos.css">
    <script>

        function openProductView(productid) {
            if (typeof WeixinJSBridge == "undefined")
                return false;

            var pid = productid;// "pd_wK1l8eoxw03XwijjfHp1hwSVI";//只需要传递
            WeixinJSBridge.invoke('openProductViewWithPid', {
                "pid": pid
            }, function (res) {
                // 返回res.err_msg,取值 
                // open_product_view_with_id:ok 打开成功
                //alert(res.err_msg);
                if (res.err_msg != "open_product_view_with_id:ok") {
                    WeixinJSBridge.invoke('openProductView', {
                        "productInfo": "{\"product_id\":\"" + pid + "\",\"product_type\":0}"
                    }, function (res) {
                        alert(res.err_msg);
                    });
                }
            });
        }

    </script>
</head>

<body ontouchstart>

    <div class="weui-cells">
        <%foreach(Module.Models.OrderInfo order in myOrderList) {%>
        
        <a class="weui-cell weui-cell_access" href="javascript:;" onclick="openProductView('<%=order.ProductId %>')">
            <div class="weui-cell__bd">
                <p><%=order.ProductName %></p>
            </div>
            <div class="weui-cell__ft"><%=GetStatus(order.OrderStatus) %></div>
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