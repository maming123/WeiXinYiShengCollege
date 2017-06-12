<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DoctorManage.aspx.cs" Inherits="WeiXinYiShengCollege.WebSite.jqueryweui.wx.DoctorManage" %>





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
      <a class="weui-btn weui-btn_primary" href="DoctorAdd.aspx?OpenId=<%=OpenId %>" >添加医生信息</a>
      </div>
        <%foreach (Module.Models.DoctorInfo d in listDoctor)
          { %>
        <a class="weui-cell weui-cell_access" href="DoctorEdit.aspx?OpenId=<%=OpenId %>&Id=<%=d.Id %>">
            <div class="weui-cell__bd">
                <%--<p>2017年8月2日 上午 (出诊医生：张三)</p>--%>
                <p> 医生：<%=d.DoctorName %></p>
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

