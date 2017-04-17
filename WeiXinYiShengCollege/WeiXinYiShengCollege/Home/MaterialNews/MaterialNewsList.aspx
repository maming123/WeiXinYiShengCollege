<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MaterialNewsList.aspx.cs" Inherits="WeiXinYiShengCollege.WebSite.Home.MaterialNews.MaterialNewsList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        选择要获取的序号：<asp:DropDownList ID="DropDownList1" runat="server">
            <asp:ListItem Selected="True" Value="0">1-20条</asp:ListItem>
            <asp:ListItem Value="19">21-40条</asp:ListItem>
            <asp:ListItem Value="39">41-60条</asp:ListItem>
            <asp:ListItem Value="59">61-80条</asp:ListItem>
            <asp:ListItem Value="79">101-120条</asp:ListItem>
        </asp:DropDownList>
    
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="获取图文素材列表" />
    
        <br />
        <asp:GridView ID="GridView1" runat="server" Width="100%">
        </asp:GridView>

        <br />
        <asp:Label ID="Label1" runat="server" ForeColor="#FF6600"></asp:Label>

    </div>
    </form>
</body>
</html>
