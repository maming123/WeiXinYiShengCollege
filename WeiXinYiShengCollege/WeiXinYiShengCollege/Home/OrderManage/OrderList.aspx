<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderList.aspx.cs" Inherits="WeiXinYiShengCollege.WebSite.Home.OrderManage.OrderList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>

    <title>获取订单信息</title>
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
        
        &nbsp;选择订单状态：<asp:DropDownList ID="ddlOrderStatus" runat="server">
            <asp:ListItem Value="3">已发货</asp:ListItem>
            <asp:ListItem Value="2">待发货</asp:ListItem>
            <asp:ListItem Selected="True" Value="5">已完成</asp:ListItem>
        </asp:DropDownList>
        &nbsp;
        订单创建时间选择：开始时间：<asp:TextBox ID="txtBeginDate" runat="server" class="Wdate" onfocus="WdatePicker({dateFmt: 'yyyy年MM月dd日', minDate: '20140401', maxDate: '20200401' })"></asp:TextBox>
                        结束时间：<asp:TextBox ID="txtEndDate" runat="server" class="Wdate" onfocus="WdatePicker({dateFmt: 'yyyy年MM月dd日', minDate: '20140401', maxDate: '20200401' })"></asp:TextBox> &nbsp; <asp:Button ID="btnSyncFromWeiXin" runat="server" Text="从微信同步订单数据到本地" OnClick="btnSyncFromWeiXin_Click" />
&nbsp; <asp:Button ID="btnFromDB" runat="server" Text="直接从本地数据库获取" OnClick="btnFromDB_Click" />
&nbsp;<asp:Button ID="btnAddScore" runat="server" Text="添加积分" OnClick="btnAddScore_Click" />
        </div>

        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                Width="100%" BorderStyle="None" BorderWidth="0px" CellPadding="1" CellSpacing="1">
            <Columns>

                    <asp:TemplateField HeaderText="Id">
                        <ItemTemplate>
                            <%# Eval("Id")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                <asp:TemplateField HeaderText="订单ID">
                        <ItemTemplate>
                            <%# Eval("OrderId")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                 <asp:TemplateField HeaderText="订单状态">
                        <ItemTemplate>
                            <%#GetStatus(Eval("OrderStatus"))%>
                        </ItemTemplate>
                    </asp:TemplateField>
                <asp:TemplateField HeaderText="订单总金额(分)">
                        <ItemTemplate>
                            <%#Eval("OrderTotalPrice")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                 <asp:TemplateField HeaderText="订单创建时间">
                        <ItemTemplate>
                            <%#Eval("OrderCreateDateTime")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                 <asp:TemplateField HeaderText="购买者昵称">
                        <ItemTemplate>
                            <%#Eval("BuyerNickName")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                <asp:TemplateField HeaderText="购买者OpenId">
                        <ItemTemplate>
                            <%#Eval("BuyerOpenId")%>
                        </ItemTemplate>
                    </asp:TemplateField>
            </Columns>
        </asp:GridView>
         <br />
        <asp:Label ID="lblMsg" runat="server" ForeColor="#FF6600"></asp:Label>
    </form>
</body>
</html>
