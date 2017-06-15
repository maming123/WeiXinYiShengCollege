<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyCollect.aspx.cs" Inherits="WeiXinYiShengCollege.WebSite.jqueryweui.wx.MyCollect" %>

<%@ Import Namespace="WeiXinYiShengCollege.Business" %>

<!DOCTYPE html>
<html>
<head>
    <title>我的收藏</title>
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
        <%foreach (dynamic sUser in myCollect)
          {
              IDictionary<string, object> d = sUser;
        %>
        <%if (Convert.ToInt32(d["Link_Type"]) == (int)SysModuleLinkType.临证参考)
          { %>
        <a class="weui-cell weui-cell_access" href="MedicalGuide.aspx?moduleId=<%=d["MODULE_ID"] %>">
            <div class="weui-cell__bd">
                <p><%=d["Title"] %></p>
            </div>
            <div class="weui-cell__ft"></div>
        </a>
        <%} %>
        <%if (Convert.ToInt32(d["Link_Type"]) == (int)SysModuleLinkType.经典方剂)
          { %>
        <a class="weui-cell weui-cell_access" href="ClassicPrescription.aspx?moduleId=<%=d["MODULE_ID"] %>">
            <div class="weui-cell__bd">
                <p><%=d["Title"] %></p>
            </div>
            <div class="weui-cell__ft"></div>
        </a>
        <%} %>


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
