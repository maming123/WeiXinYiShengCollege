<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerManagerList.aspx.cs" Inherits="WeiXinYiShengCollege.WebSite.Home.CustomerMgr.CustomerManagerList" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <title>客户经理管理</title>
    <link href="../css/admin.global.css" rel="stylesheet" type="text/css" />
    <link href="../css/admin.content.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../js/jquery-1.9.1.min.js"></script>
    <script type="text/javascript" src="../../js/jquery.utils.js"></script>
    <script type="text/javascript" src="../../js/admin.js"></script>
    <script type="text/javascript" src="../../js/jsonlint.js"></script>
    <script type="text/javascript" src="../../js/My97DatePicker/WdatePicker.js"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
            Width="100%" BorderStyle="None" BorderWidth="0px" CellPadding="1" CellSpacing="1">
            <Columns>

                <asp:TemplateField HeaderText="Id">
                    <ItemTemplate>
                        <%# Eval("Id")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="姓名">
                    <ItemTemplate>
                        <%# Eval("Name")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="手机">
                    <ItemTemplate>
                        <%#Eval("Mobile")%>
                    </ItemTemplate>
                </asp:TemplateField>
               
            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
