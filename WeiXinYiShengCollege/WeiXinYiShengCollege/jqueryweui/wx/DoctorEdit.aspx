<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DoctorEdit.aspx.cs" Inherits="WeiXinYiShengCollege.WebSite.jqueryweui.wx.DoctorEdit" %>



<!DOCTYPE html>
<html>
<head>
    <title>修改医生信息</title>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no">

    <meta name="description" content="Write an awesome description for your new site here. You can edit this line in _config.yml. It will appear in your document head meta (for Google search results) and in your feed.xml site description.
">

    <link rel="stylesheet" href="../lib/weui.min.css">
    <link rel="stylesheet" href="../css/jquery-weui.css">
    <link rel="stylesheet" href="css/demos.css">
</head>

<body ontouchstart>


    <header class='demos-header'>
        <h1 class="demos-title">修改医生信息</h1>
    </header>

    <div class="weui-cells__title"></div>
    <div class="weui-cells weui-cells_form">
        <div class="weui-cell">
            <div class="weui-cell__hd">
                <label for="" class="weui-label">姓名</label></div>
            <div class="weui-cell__bd">
                <input class="weui-input" type="text" value="<%=docInfo.DoctorName %>" id="txtName" placeholder="请填写姓名">
            </div>
        </div>
            <div class="weui-cells__title">医生简介</div>
    <div class="weui-cells weui-cells_form">
        <div class="weui-cell">
            <div class="weui-cell__bd">
                <textarea class="weui-textarea" placeholder="请添加简介" rows="10" id="txtRemark"><%=docInfo.Remark %></textarea>

            </div>
        </div>
    </div>
    </div>

    <div class="weui-cells__tips">请按要求修改</div>

    <div class="weui-btn-area">
        <a class="weui-btn weui-btn_primary" href="javascript:" id="showTooltips">修改</a>
    </div>
    <div class="weui-btn-area">
        <a class="weui-btn weui-btn_primary" href="javascript:" id="btnDelete">删除</a>
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
        var Id = "<%=docInfo.Id%>";
    </script>
    <script>
        $("#showTooltips").click(function () {
            var txtName = $("#txtName").val();
            var txtRemark = $("#txtRemark").val();
           
            if (txtName == '') {
                $.toptip('请添加医生名字');
                return false;
            }
            if (txtRemark == '') {
                $.toptip('请添加医生简介');
                return false;
            }
           
            $.ajax({
                type: "POST",
                url: "../../handlerui/PageHandler.ashx",
                data: { Action: "UpdateDoctorInfo",Id:Id, OpenId: OpenId, txtName: txtName, txtRemark: txtRemark, r: Math.random() },
                dataType: "json",
                async: true,
                success: function (result) {
                    if (result && typeof result == "object") {
                        if (result.code == 1) {

                            $.toast("修改成功", function () {
                                //console.log('close');
                                //操作成功弹窗消失后执行
                                document.location.href = "DoctorManage.aspx?OpenId=" + OpenId;
                            });
                            //console.log(result.m);
                        } else {
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
        $("#btnDelete").click(function () {

            $.ajax({
                type: "POST",
                url: "../../handlerui/PageHandler.ashx",
                data: { Action: "DeleteDoctorInfo", Id: Id, OpenId: OpenId, r: Math.random() },
                dataType: "json",
                async: true,
                success: function (result) {
                    if (result && typeof result == "object") {
                        if (result.code == 1) {

                            $.toast("删除成功", function () {
                                //console.log('close');
                                //操作成功弹窗消失后执行
                                document.location.href = "DoctorManage.aspx?OpenId=" + OpenId;
                            });
                            //console.log(result.m);
                        } else {
                            $.toptip('提交失败');
                        }
                    }
                }
            });
        });
    </script>
</body>
</html>

