﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserEdit.aspx.cs" Inherits="WeiXinYiShengCollege.WebSite.Home.UserEdit" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>修改个人信息</title>
    <link href="login/css/default.css" media="screen" rel="stylesheet" type="text/css" />
    <link href="login/css/register.css" media="screen" rel="stylesheet" type="text/css" />

    <script src="../js/jquery-1.11.0.min.js" type="text/javascript"></script>

    <script src="../js/common.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <table width="100%" align="center" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td align="right" class="style1">Id：</td>
                <td>

                    <asp:Label ID="lblId" runat="server" Text=""></asp:Label>
                </td>
                <td align="right" class="style1">OpenId：</td>
                <td>
                    <asp:Label ID="lblOpenId" runat="server" Text=""></asp:Label>
                </td>
                <td align="right" class="style1">手机号：</td>
                <td>
                    <asp:TextBox ID="txtMobile" runat="server" CssClass="input_205 input_txt"></asp:TextBox>
                </td>
                <td align="right" class="style1">姓名：</td>
                <td>
                    <asp:TextBox ID="txtNickName" runat="server" CssClass="input_205 input_txt"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" class="style1">用户类型：</td>
                <td>
                    <asp:DropDownList ID="ddlUserType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlUserType_SelectedIndexChanged">
                        <asp:ListItem Value="0">未分配(不详)</asp:ListItem>
                        <asp:ListItem Value="1">粉丝</asp:ListItem>
                        <asp:ListItem Value="2">理事</asp:ListItem>
                    </asp:DropDownList>
                    <asp:HiddenField ID="hidUserType" runat="server" />
                </td>
                <td align="right" class="style1">用户级别：</td>
                <td>
                    <asp:DropDownList ID="ddlUserLevel" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlUserLevel_SelectedIndexChanged">
                        <asp:ListItem Value="0">未分配（不详）</asp:ListItem>
                        <asp:ListItem Value="1">理事</asp:ListItem>
                        <asp:ListItem Value="2">常务理事</asp:ListItem>
                        <asp:ListItem Value="3">荣誉理事</asp:ListItem>
                    </asp:DropDownList>
                    <asp:HiddenField ID="hidUserLevel" runat="server" />
                </td>
                <td align="right" class="style1">所属普通理事：</td>
                <td>

                    <asp:DropDownList ID="ddlParentId" runat="server" Width="145px"></asp:DropDownList>
                </td>
                <td align="right" class="style1">所属荣誉理事：</td>
                <td>
                    <asp:DropDownList ID="ddlExpertsLiShi" runat="server" Width="145px"></asp:DropDownList>
                    <asp:HiddenField ID="hidExpertsLiShiId" runat="server" />
                </td>
            </tr>
            <tr>
                <td align="right" class="style1">审核状态：</td>
                <td>
                    <asp:DropDownList ID="ddlApprove" runat="server">
                        <asp:ListItem Value="0">未认证</asp:ListItem>
                        <asp:ListItem Value="1">已认证</asp:ListItem>
                        <asp:ListItem Value="2">已提交认证申请</asp:ListItem>
                        <asp:ListItem Value="3">认证未通过</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td align="right" class="style1">客户经理：</td>
                <td>

                    <asp:DropDownList ID="ddlCustomerManagerId" runat="server" Width="145px"></asp:DropDownList>
                </td>

                <td align="right" class="style1">总积分：</td>
                <td>
                    <asp:TextBox ID="txtScore" runat="server" CssClass="input_205 input_txt"></asp:TextBox>
                </td>
                <td align="right" class="style1">剩余积分：</td>
                <td>
                    <asp:TextBox ID="txtLastScore" runat="server" CssClass="input_205 input_txt"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" class="style1">所属地市：</td>
                <td>
                    <asp:DropDownList ID="ddlCity" runat="server">
                    </asp:DropDownList>
                </td>
                <td align="right" class="style1">所属省份：</td>
                <td>

                    <asp:DropDownList ID="ddlProvince" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlProvince_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td align="right" class="style1">二维码场景ID：</td>
                <td>
                    <asp:Label ID="lblQrCodeScene_id" runat="server" Text=""></asp:Label>

                </td>
            </tr>
            <tr>
                <td align="right" class="style1">单位名称：</td>
                <td colspan="7">

                    <asp:TextBox ID="txtCompanyName" runat="server" CssClass="input_205 input_txt"></asp:TextBox>
                    <asp:Panel ID="panelLiShiToFans" runat="server" Visible="false">
                        因将该理事降级成粉丝类型，请为该理事的粉丝指定新理事：
                        <asp:DropDownList ID="ddlNewLiShiId" runat="server" Width="145px"></asp:DropDownList>

                    </asp:Panel>
                </td>
            </tr>
           

            <tr>
                <td align="right" class="style1">个人简介：</td>
                <td colspan="7">
                    <asp:TextBox ID="txtRemark" runat="server" CssClass=" input_txt" Columns="5" Height="84px" Rows="10" TextMode="MultiLine" Width="645px"></asp:TextBox>
                </td>

            </tr>
            <tr>
                <td align="right" class="style1">从微信API获取的用户信息：</td>
                <td colspan="7">
                    <asp:TextBox ID="txtUserInfoJson" ReadOnly="true" runat="server" CssClass=" input_txt" Columns="5" Height="120px" Rows="10" TextMode="MultiLine" Width="645px"></asp:TextBox>
                </td>

            </tr>
            <tr>
                <td align="center" class="style1" colspan="8">
                    <asp:Button ID="btnEdit" runat="server" Text="修改" OnClick="btnEdit_Click" />
                </td>

            </tr>
        </table>

    </form>
</body>
</html>
