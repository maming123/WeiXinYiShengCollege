<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DoctorWorkView.aspx.cs" Inherits="WeiXinYiShengCollege.WebSite.jqueryweui.wx.DoctorWorkView" %>

<!DOCTYPE html>
<html>
<head>
    <title>出诊信息</title>
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
        <h1 class="demos-title">出诊信息</h1>
    </header>

    <div class="weui-cells__title"></div>
    <div class="weui-cells weui-cells_form">
        <div class="weui-cell">
            <div class="weui-cell__hd">
                <label for="" class="weui-label">出诊日期</label></div>
            <div class="weui-cell__bd">
                <label for="" class="weui-label"><%=dws.WorkDateTime.ToString("yyyy-MM-dd") %></label>
            </div>
        </div>
        <div class="weui-cell">
            <div class="weui-cell__hd">
                <label for="" class="weui-label">出诊日期</label></div>
            <div class="weui-cell__bd">
                <label for="" class="weui-label"><%=dws.DayTime==9?"上午":"下午" %></label>
            </div>
        </div>

        <div class="weui-cell">
            <div class="weui-cell__hd">
                <label for="" class="weui-label">出诊医生</label></div>
            <div class="weui-cell__bd">
                <label for="" class="weui-label"><%=dws.DoctorName %></label>
            </div>
        </div>

    </div>
    <div class="weui-cells__title">医生简介</div>
    <div class="weui-cells weui-cells_form">
        <div class="weui-cell">
            <div class="weui-cell__bd">
                <textarea class="weui-textarea" placeholder="" rows="10"><%=docInfo.Remark %></textarea>

            </div>
        </div>
    </div>
    <div class="weui-cells__tips">底部说明文字底部说明文字</div>



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

