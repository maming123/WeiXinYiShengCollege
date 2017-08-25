<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditClassicPrescription.aspx.cs" Inherits="WeiXinYiShengCollege.WebSite.Home.EditClassicPrescription" ValidateRequest="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="login/css/default.css" media="screen" rel="stylesheet" type="text/css" />
    <link href="login/css/register.css" media="screen" rel="stylesheet" type="text/css" />

    <script src="../js/jquery-1.11.0.min.js" type="text/javascript"></script>

    <script src="../js/common.js"></script>

    <script type="text/javascript">
       
    </script>
</head>
<body>
    <form id="form1" runat="server">

        <table width="100%" align="center" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="4">PointID:<asp:Label ID="lblPointId" runat="server" Text=""></asp:Label>ModuleID:<asp:Label ID="lblModule" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
                <td>标题：</td>
                <td colspan="3">
                    <asp:TextBox ID="txttitle" runat="server" CssClass=" input_txt"></asp:TextBox></td>
            </tr>
            <tr>
                <%--<td>病症：</td>
                <td>
                    <asp:TextBox ID="txtbingzheng" runat="server" CssClass=" input_txt"></asp:TextBox></td>
                <td>辩证：</td>
                <td>
                    <asp:TextBox ID="txtbianzheng" runat="server" CssClass=" input_txt"></asp:TextBox></td>
                --%>
                <td>来源：</td>
                <td colspan="3">
                    <asp:TextBox ID="txtlaiyuan" runat="server" CssClass=" input_txt" Rows="3" Width="521px"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="2">&nbsp;</td>
            </tr>
            <tr>
                <td>组成：</td>
                <td>
                    <asp:TextBox ID="txtzucheng" runat="server" CssClass=" input_txt" Height="63px" TextMode="MultiLine" Width="576px"></asp:TextBox></td>
                
            </tr>
            <tr><td>功效：</td>
                <td>
                    <asp:TextBox ID="txtgongxiao" runat="server" CssClass=" input_txt" Height="63px" Rows="3" TextMode="MultiLine" Width="576px"></asp:TextBox></td>

     </tr>
            <tr>
                <td colspan="3">&nbsp;</td>
            </tr>
            <tr>
                <td>主治：</td>
                <td >
                    <asp:TextBox ID="txtzhuzhi" runat="server" CssClass=" input_txt" Height="63px" TextMode="MultiLine" Width="576px"></asp:TextBox></td>
                
            </tr>
            <tr>
                <td>用法：</td>
                                <td >
                    <asp:TextBox ID="txtyongfa" runat="server" CssClass=" input_txt" Height="63px" Rows="3" TextMode="MultiLine" Width="576px"></asp:TextBox></td>


            </tr>
             <tr>
                <td>其他：</td>
                                <td >
                    <asp:TextBox ID="txtqita" runat="server" CssClass=" input_txt" Height="63px" Rows="3" TextMode="MultiLine" Width="576px"></asp:TextBox></td>


            </tr>
            <tr>
                <td colspan="4" align="center" class="style1">
                    <asp:Button ID="btnSave" OnClick="btnSave_Click" runat="server" Text="保存" />
                </td>
            </tr>
        </table>
    </form>

</body>
</html>

