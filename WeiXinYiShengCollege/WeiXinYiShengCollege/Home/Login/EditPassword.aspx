<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditPassword.aspx.cs" Inherits="HospitalBookWebSite.Home.Login.EditPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>修改密码</title>
    <link href="css/default.css" media="screen" rel="stylesheet" type="text/css" />
    <link href="css/register.css" media="screen" rel="stylesheet" type="text/css" />

    <script src="/js/jquery-1.11.0.min.js" type="text/javascript"></script>

    <script src="js/common.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <table width="100%" align="center" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td align="right" class="style1">原始密码：</td>
                <td>
                   <asp:TextBox ID="txtpwdold" runat="server" CssClass="input_205 input_txt" TextMode="Password"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" class="style1">新密码：</td>
                <td>
                   <asp:TextBox ID="txtpwdnew" runat="server" CssClass="input_205 input_txt" TextMode="Password"></asp:TextBox></td>
            </tr>
            <tr>
                <td align="right" class="style1">新密码确认：</td>
                <td>
                    <asp:TextBox ID="txtpwdnewsure" runat="server" CssClass="input_205 input_txt" TextMode="Password"></asp:TextBox></td>
            </tr>
            <tr>
                <td align="center" class="style1" colspan="2">
                    <asp:Button ID="btnEdit" runat="server" Text="修改" OnClick="btnEdit_Click" />
                </td>
                
            </tr>
        </table>

    </form>
</body>
</html>
