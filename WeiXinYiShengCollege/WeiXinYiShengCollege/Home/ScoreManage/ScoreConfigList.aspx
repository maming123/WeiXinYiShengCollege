<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ScoreConfigList.aspx.cs" Inherits="WeiXinYiShengCollege.WebSite.Home.ScoreConfigManage.ScoreConfigList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <title>积分兑换配置</title>
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
            Width="100%" BorderStyle="None" BorderWidth="0px" CellPadding="1" CellSpacing="1" >
            <Columns>

                <asp:TemplateField HeaderText="用户类型<br/>（1：粉丝；2：理事）">
                    <ItemTemplate>
                        <%# Eval("UserType")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="用户级别<br/>（1：理事；2：常务理事；3：荣誉理事）">
                    <ItemTemplate>
                        <%# Eval("UserLevel")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="积分">
                    <ItemTemplate>
                        <%#Eval("Score")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="对照金额（分）">
                    <ItemTemplate>
                        <%#Eval("Money")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="备注">
                    <ItemTemplate>
                        <%#Eval("Remark")%>
                    </ItemTemplate>
                </asp:TemplateField>
                
               
            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
