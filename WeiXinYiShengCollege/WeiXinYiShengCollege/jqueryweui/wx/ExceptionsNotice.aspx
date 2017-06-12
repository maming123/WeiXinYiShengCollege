<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExceptionsNotice.aspx.cs" Inherits="WeiXinYiShengCollege.WebSite.jqueryweui.wx.ExceptionsNotice" %>


<!DOCTYPE html>
<html>
<head>
    <title>免责声明</title>
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
        <h1 class="demos-title">免责声明</h1>
    </header>
    <article class="weui-article">

        <section>

            <section>

                <p>
                    免责声明
                </p>
            </section>

        </section>
    </article>
    <div class="button_sp_area">
        <a href="javascript:;" id="btnOK" class="weui-btn weui-btn_primary">同意</a>
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
        var moduleId = "<%=moduleId%>";
        var linkType = "<%=linkType%>";
    </script>
    <script>
        $("#btnOK").click(function () {
            $.ajax({
                type: "POST",
                url: "../../handlerui/PageHandler.ashx",
                data: { Action: "InsertUserExceptions", moduleId: moduleId, linkType: linkType, r: Math.random() },
                dataType: "json",
                async: true,
                success: function (result) {
                    if (result && typeof result == "object") {
                        if (result.code == 1) {

                            $.toast("已同意，跳转中...", function () {
                                //console.log('close');
                                //操作成功弹窗消失后执行
                                document.location.href = "MedicineList.aspx?moduleId=" + moduleId + "&linkType=" + linkType;
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

