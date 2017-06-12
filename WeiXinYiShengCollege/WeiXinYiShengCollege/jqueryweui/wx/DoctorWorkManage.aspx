<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DoctorWorkManage.aspx.cs" Inherits="WeiXinYiShengCollege.WebSite.jqueryweui.wx.DoctorWorkManage" %>





<!DOCTYPE html>
<html>
<head>
    <title>出诊医生设置</title>
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
      <div class="weui-btn-area">
      <a class="weui-btn weui-btn_primary" href="DoctorWorkAdd.aspx?OpenId=<%=OpenId %>" >添加出诊信息</a>
      </div>
        <%foreach(Module.Models.DoctorWorkSchedule dws in listDws){ %>
        <a class="weui-cell weui-cell_access" href="DoctorWorkEdit.aspx?OpenId=<%=OpenId %>&Id=<%=dws.Id %>">
            <div class="weui-cell__bd">
                <%--<p>2017年8月2日 上午 (出诊医生：张三)</p>--%>
                <p><%=dws.WorkDateTime.ToString("yyyy年MM月dd日") %> <%=dws.DayTime==9?"上午":"下午" %> (出诊医生：<%=dws.DoctorName %>)</p>
            </div>
            <div class="weui-cell__ft"></div>
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
