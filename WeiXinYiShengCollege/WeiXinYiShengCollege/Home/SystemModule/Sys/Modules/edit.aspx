<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="edit.aspx.cs" Inherits="MLK.SystemModule.Sys.Modules.edit" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>修改模块信息</title>
         <link href="../../../css/sysmodule.css" rel="stylesheet" />
        <script src="/js/jquery-1.11.1.min.js" type="text/javascript"></script>
        <script src="/js/layer/layer.js" type="text/javascript"></script>
        <script src="/js/common.js" type="text/javascript"></script>
    <script  type="text/javascript">
        function setParentArea(isRootModule) {
            document.all.btnBrowseParent.disabled = isRootModule;
            if (document.all.chkIsRootModule.checked) {
                document.all.txtParentName.value = "";
                document.all.txtParentID.value = "0";
            }
            else {
                document.all.txtParentID.value = "";
            }
        }
        function ShowSelectParentModule() {
            IframLayer("selectparentmodule.aspx", 400, 200);
        }

    </script>
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <font face="宋体">
            <table id="Table1" style="height: 183px" cellspacing="0" cellpadding="0" width="100%" border="0">

                <tr>
                    <td valign="top">
                        <table id="Table3" cellspacing="1" cellpadding="2" border="1" style="border-collapse: collapse" bordercolor="#333333">
                            <TR class="TableDataRow">
									<TD  colspan="2" style="HEIGHT: 12px" align="center"><STRONG style="FONT-SIZE: 10pt">编辑节点信息</STRONG></TD>
									
								</TR>
                            <tr class="TableDataRow">
                                <td style="width: 61px">节点名称</td>
                                <td>
                                    <asp:TextBox ID="txtModuleName" runat="server" Width="458px" CssClass="BigInput"></asp:TextBox></td>
                            </tr>
                            <tr class="TableDataRow">
                                <td style="width: 61px">所属父节点</td>
                                <td>
                                    <asp:TextBox ID="txtParentName" runat="server" Width="296px" ReadOnly="True" CssClass="BigInput"></asp:TextBox>
                                    <input type="button" value="浏览" class="BigButton" onclick="ShowSelectParentModule();"
                                        name="btnBrowseParent">
                                    <asp:CheckBox ID="chkIsRootModule" runat="server" Text="当前模块为根模块" Visible="false"></asp:CheckBox>
                                    <asp:HiddenField id="txtParentID" runat="server"></asp:HiddenField></td>
                            </tr>
                            <%--<tr class="TableDataRow">
                                <td style="width: 61px">指向的链接</td>
                                <td>
                                    <p>
                                        <asp:TextBox ID="txtLinkName" runat="server" Width="418px" CssClass="BigInput"></asp:TextBox>
                                        &nbsp;<input class="BigButton" type="button" value="无链接" onclick="document.all.txtLinkID.value = '0'; document.all.txtLinkName.value = '';" style="display: none">
                                    </p>
                                </td>
                            </tr>
                            <tr class="TableDataRow">
                                <td style="width: 61px">指向的目标</td>
                                <td>
                                    <asp:TextBox ID="txtTargetName" runat="server" CssClass="BigInput" Width="211px"></asp:TextBox></td>
                            </tr>
                            <tr class="TableDataRow">
                                <td style="width: 61px">链接类型</td>
                                <td>
                                    <asp:DropDownList ID="ddlistLinkType" runat="server">
                                        <asp:ListItem Value="0">无链接</asp:ListItem>
                                        <asp:ListItem Value="1">普通链接</asp:ListItem>
                                        <asp:ListItem Value="2">静态链接</asp:ListItem>
                                        <asp:ListItem Value="3">任务列表</asp:ListItem>
                                        <asp:ListItem Value="13">公共列表</asp:ListItem>
                                        <asp:ListItem Value="4">业务录入</asp:ListItem>
                                        <asp:ListItem Value="11">业务查询</asp:ListItem>
                                        <asp:ListItem Value="5">档案管理</asp:ListItem>
                                        <asp:ListItem Value="6">GIS查询</asp:ListItem>
                                        <asp:ListItem Value="7">办公申请</asp:ListItem>
                                        <asp:ListItem Value="8">办公初审</asp:ListItem>
                                        <asp:ListItem Value="9">办公复审</asp:ListItem>
                                        <asp:ListItem Value="10">办公查询</asp:ListItem>
                                        <asp:ListItem Value="12">汇总表</asp:ListItem>
                                        <asp:ListItem Value="20">汇总表展示模块2011年</asp:ListItem>
                                        <asp:ListItem Value="14">进度统计</asp:ListItem>

                                    </asp:DropDownList>
                                </td>
                            </tr>--%>
                            <tr class="TableDataRow">
                                <td style="width: 61px"></td>
                                <td>
                                    <asp:CheckBox ID="chkIsDisplay" runat="server" Text="是否显示"></asp:CheckBox>
                                </td>
                            </tr>
                            <tr class="TableDataRow">
                                <td align="center" colspan="2">
                                    <asp:Button ID="btnEdit" runat="server" Text="修改" CssClass="BigButton" OnClick="btnEdit_Click"></asp:Button>
                                    &nbsp;&nbsp;<input type="button" value="返回" class="BigButton" onclick="history.go(-1);">
                                <%if(!isHaveChild){ %>
                                    <a href="/Home/EditPoint.aspx?ModuleID=<%=Request["ModuleID"] %>">具体内容修改</a>
                                    <%} %>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </font>
    </form>
</body>
</html>

