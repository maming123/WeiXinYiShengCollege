<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FansProfileComplete.aspx.cs" Inherits="WeiXinYiShengCollege.WebSite.jqueryweui.wx.FansProfileComplete" %>


<!DOCTYPE html>
<html>
<head>
    <title>粉丝信息完善</title>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no">

    <meta name="description" content="">

    <link rel="stylesheet" href="../lib/weui.min.css">
    <link rel="stylesheet" href="../css/jquery-weui.css">
    <link rel="stylesheet" href="css/demos.css">
</head>

<body ontouchstart>


    <header class='demos-header'>
        <h1 class="demos-title">粉丝信息完善</h1>
    </header>

    <div class="weui-cells__title"></div>
    <div class="weui-cells weui-cells_form">
        <div class="weui-cell">
            <div class="weui-cell__hd">
                <label class="weui-label">姓名</label></div>
            <div class="weui-cell__bd">
                <input class="weui-input" type="text" placeholder="请填写姓名" id="txtnickname">
            </div>
        </div>
        <div class="weui-cell ">
            <div class="weui-cell__hd">
                <label class="weui-label">手机号</label>
            </div>
            <div class="weui-cell__bd">
                <input class="weui-input" type="tel" placeholder="请输入手机号" id="txtmobile">
            </div>

        </div>

    </div>


    <div class="weui-cells__tips">描述:请完成相关信息后，查看相关功能和菜单</div>


    <div class="weui-btn-area">
        <a class="weui-btn weui-btn_primary" href="javascript:" id="showTooltips">确定</a>
    </div>

    <script src="../lib/jquery-2.1.4.js"></script>
    <script src="../lib/fastclick.js"></script>
    <script>
        $(function () {
            FastClick.attach(document.body);
        });
    </script>
    <script src="../js/jquery-weui.js"></script>

    <script>
        var OpenId = "<%=OpenId%>";
    </script>
    <script>
        $("#showTooltips").click(function () {
            var mobile = $("#txtmobile").val();
            var nickname = $("#txtnickname").val();
            
            if (!mobile || !/1[3|4|5|7|8]\d{9}/.test(mobile)) {
                $.toptip('请输入手机号');
                return false;
            }
            if (nickname == '')
            {
                $.toptip('请输入姓名');
                return false;
            }
       
            $.ajax({
                type: "POST",
                url: "../../handlerui/PageHandler.ashx",
                data: { Action: "FansInfoComplete",OpenId:OpenId, mobile: mobile, nickname: nickname, r: Math.random() },
                dataType: "json",
                async: true,
                success: function (result) {
                    if (result && typeof result == "object") {
                        if (result.code == 1) {
                            $.toptip('已提交信息，请返回个人中心');
                        }else
                        {
                            $.toptip('信息提交失败');
                        }
                    }
                }
            });

            
            //var tel = $('#tel').val();
            //var code = $('#code').val();
            //if (!tel || !/1[3|4|5|7|8]\d{9}/.test(tel)) {
            //    $.toptip('请输入手机号');
            //}
            //else if (!code || !/\d{6}/.test(code)) {
            //    $.toptip('请输入六位手机验证码');
            //}
            //else {
            //    $.toptip('提交成功', 'success');
            //}

        });
    </script>
</body>
</html>

