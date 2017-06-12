<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProfileApprove.aspx.cs" Inherits="WeiXinYiShengCollege.WebSite.jqueryweui.wx.ProfileApprove" %>

<!DOCTYPE html>
<html>
<head>
    <title>认证申请</title>
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
        <h1 class="demos-title">认证申请</h1>
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

        <%--<div class="weui-cell weui-cell_select weui-cell_select-after">
            <div class="weui-cell__hd">
                <label for="" class="weui-label">省份</label>
            </div>
            <div class="weui-cell__bd">
                <select class="weui-select" name="select2">
                    <option value="1">中国</option>
                    <option value="2">美国</option>
                    <option value="3">英国</option>
                </select>
            </div>
        </div>
        <div class="weui-cell weui-cell_select weui-cell_select-after">
            <div class="weui-cell__hd">
                <label for="" class="weui-label">地市</label>
            </div>
            <div class="weui-cell__bd">
                <select class="weui-select" name="select2">
                    <option value="1">中国</option>
                    <option value="2">美国</option>
                    <option value="3">英国</option>
                </select>
            </div>
        </div>--%>

        <div class="weui-cell">
            <div class="weui-cell__hd">
                <label class="weui-label">单位名称</label></div>
            <div class="weui-cell__bd">
                <input class="weui-input" type="text" placeholder="请填写名称" id="txtcompanyname">
            </div>
        </div>


    </div>


    <div class="weui-cells__tips">描述:请完成申请，等待申请通过，才能查看相关功能和菜单</div>


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
            var companyname = $("#txtcompanyname").val();
            if (!mobile || !/1[3|4|5|7|8]\d{9}/.test(mobile)) {
                $.toptip('请输入手机号');
                return false;
            }
            if (nickname == '')
            {
                $.toptip('请输入姓名');
                return false;
            }
            if (companyname == '') {
                $.toptip('请输入单位名称');
                return false;
            }
            $.ajax({
                type: "POST",
                url: "../../handlerui/PageHandler.ashx",
                data: { Action: "SubmitApprove",OpenId:OpenId, mobile: mobile, nickname: nickname, companyname: companyname, r: Math.random() },
                dataType: "json",
                async: true,
                success: function (result) {
                    if (result && typeof result == "object") {
                        if (result.code == 1) {
                            $.toast("申请成功", function () {
                                //console.log('close');
                                //操作成功弹窗消失后执行
                                document.location.href = "Profile.aspx?OpenId=" + OpenId;
                            });
                        }else
                        {
                            $.toptip('提交失败');
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

