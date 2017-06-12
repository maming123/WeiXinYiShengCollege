<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MedicineList.aspx.cs" Inherits="WeiXinYiShengCollege.WebSite.jqueryweui.wx.MedicineList" %>


<!DOCTYPE html>
<html>
<head>
    <title>药方</title>
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
        <%foreach(Module.Models.Sys_Module m in mySysModuleList) {%>
        
        <a class="weui-cell weui-cell_access" href="MedicineList.aspx?moduleId=<%=m.MODULE_ID %>&linkType=<%=m.Link_Type %>">
            <div class="weui-cell__bd">
                <p><%=m.MODULE_NAME %></p>
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

