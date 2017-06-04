<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditPoint.aspx.cs" Inherits="WeiXinYiShengCollege.WebSite.Home.EditPoint" ValidateRequest="false" %>

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
                <td colspan="6">PointID:<asp:Label ID="lblPointId" runat="server" Text=""></asp:Label>ModuleID:<asp:Label ID="lblModule" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
                <td>标题：</td>
                <td colspan="5">
                    <asp:TextBox ID="txttitle" runat="server" CssClass=" input_txt"></asp:TextBox></td>
            </tr>
            <tr>
                <td>病症：</td>
                <td>
                    <asp:TextBox ID="txtbingzheng" runat="server" CssClass=" input_txt"></asp:TextBox></td>
                <td>辩证：</td>
                <td>
                    <asp:TextBox ID="txtbianzheng" runat="server" CssClass=" input_txt"></asp:TextBox></td>
                <td>症候：</td>
                <td>
                    <asp:TextBox ID="txtzhenghou" runat="server" CssClass=" input_txt" Height="63px" Rows="3" TextMode="MultiLine" Width="243px"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="6">内治法</td>
            </tr>
            <tr>
                <td>中成药：</td>
                <td>
                    <asp:TextBox ID="txtzhongchengyao" runat="server" CssClass=" input_txt"></asp:TextBox></td>
                <td>经验方：</td>
                <td colspan="3">
                    <asp:TextBox ID="txtjingyanfang" runat="server" CssClass=" input_txt" Height="63px" Rows="3" TextMode="MultiLine" Width="576px"></asp:TextBox></td>

            </tr>
            <tr>
                <td colspan="6">外治法</td>
            </tr>
            <tr>
                <td>穴位：</td>
                <td>
                    <asp:TextBox ID="txtxuewei" runat="server" CssClass=" input_txt"></asp:TextBox></td>
                <td>脊柱：</td>
                <td>
                    <asp:TextBox ID="txtjizhu" runat="server" CssClass=" input_txt"></asp:TextBox></td>
                <td>耳穴：</td>
                <td>
                    <asp:TextBox ID="txterxue" runat="server" CssClass=" input_txt"></asp:TextBox></td>
            </tr>
            <tr>
                <td>食疗：</td>
                <td>
                    <asp:TextBox ID="txtshiliao" runat="server" CssClass=" input_txt"></asp:TextBox></td>

                <td >运动：</td>
                <td colspan="3">
                    <asp:TextBox ID="txtyundong" runat="server" CssClass=" input_txt"></asp:TextBox></td>


            </tr>
            <tr>
                <td>禁忌：</td>
                                <td colspan="5">
                    <asp:TextBox ID="txtjinji" runat="server" CssClass=" input_txt" Height="63px" Rows="3" TextMode="MultiLine" Width="576px"></asp:TextBox></td>


            </tr>
            <tr>
                <td colspan="6" align="center" class="style1">
                    <asp:Button ID="btnSave" OnClick="btnSave_Click" runat="server" Text="保存" />
                </td>
            </tr>
        </table>
    </form>

</body>
</html>

