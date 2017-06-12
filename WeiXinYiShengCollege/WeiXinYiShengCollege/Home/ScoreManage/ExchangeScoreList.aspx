<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExchangeScoreList.aspx.cs" Inherits="WeiXinYiShengCollege.WebSite.Home.ScoreManage.ExchangeScoreList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <title>积分兑换管理</title>
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
            &nbsp;选择兑换状态：<asp:DropDownList ID="ddlPayStatus" runat="server">
                <asp:ListItem Value="-1" Selected="True">请选择</asp:ListItem>
                <asp:ListItem Value="0">未支付</asp:ListItem>
                <asp:ListItem Value="1">申请支付中</asp:ListItem>
                <asp:ListItem Value="2">已支付</asp:ListItem>
            </asp:DropDownList>
            &nbsp;
        申请兑换时间选择：开始时间：<asp:TextBox ID="txtBeginDate" runat="server" class="Wdate" onfocus="WdatePicker({dateFmt: 'yyyy年MM月dd日', minDate: '20140401', maxDate: '20200401' })"></asp:TextBox>
            结束时间：<asp:TextBox ID="txtEndDate" runat="server" class="Wdate" onfocus="WdatePicker({dateFmt: 'yyyy年MM月dd日', minDate: '20140401', maxDate: '20200401' })"></asp:TextBox>
            &nbsp; 
            手机号：<asp:TextBox ID="txtMobile" runat="server"></asp:TextBox>
            &nbsp;
            <asp:Button ID="btnQuery" runat="server" Text="查询" OnClick="btnQuery_Click" />
        </div>

        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
            Width="100%" BorderStyle="None" BorderWidth="0px" CellPadding="1" CellSpacing="1" OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound">
            <Columns>

                <asp:TemplateField HeaderText="Id">
                    <ItemTemplate>
                        <%# Eval("Id")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="手机号">
                    <ItemTemplate>
                        <%# Eval("Mobile")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="姓名">
                    <ItemTemplate>
                        <%#Eval("NickName")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="申请兑换的积分">
                    <ItemTemplate>
                        <%#Convert.ToDecimal(Eval("Score"))/100%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="积分对应的金额(元)">
                    <ItemTemplate>
                        <%#Convert.ToDecimal(Eval("Money"))/100%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="支付状态">
                    <ItemTemplate>
                        <%#GetPayStatus(Eval("PayStatus"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="申请时间">
                    <ItemTemplate>
                        <%#Eval("CreateDatetime")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="支付时间">
                    <ItemTemplate>
                        <%#Eval("PayDateTime")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="操作">
                    <ItemTemplate>
                       <asp:LinkButton ID="lbtnEdit" runat="server" ToolTip='<%#Eval("PayStatus") %>' CommandName="PaySure" CommandArgument='<%#Eval("Id") %>'>确定支付</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <br />
        <asp:Label ID="lblMsg" runat="server" ForeColor="#FF6600"></asp:Label>

    </form>
</body>
</html>
