<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="add.aspx.cs" Inherits="MLK.SystemModule.Sys.Modules.add" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>添加新模块</title>
        <link href="../../../css/sysmodule.css" rel="stylesheet" />
        <script src="/js/jquery-1.11.1.min.js" type="text/javascript"></script>
        <script src="/js/layer/layer.js" type="text/javascript"></script>
        <script src="/js/common.js" type="text/javascript"></script>

		<script  type="text/javascript">
			function setParentArea(isRootModule){
				document.all.btnBrowseParent.disabled = isRootModule;
				if(document.all.chkIsRootModule.checked){
					document.all.txtParentName.value = "";
					document.all.txtParentID.value = "0";
				}
				else{
					document.all.txtParentID.value = "";
				}
			}
			function ShowSelectParentModule() {
			    IframLayer("selectparentmodule.aspx", 400, 200);
			}
			
		</script>
	</HEAD>
	<body onload="spanChkCreateTable.style.display=spanTemplate.style.display=spanUserDefineReport.style.display='none';">
		<form id="Form1" method="post" runat="server">
			<FONT face="宋体">
				<TABLE id="Table1" style="HEIGHT: 183px" cellSpacing="0" cellPadding="0" width="100%" border="0">
					
					<TR>
						<TD vAlign="top">
							<TABLE id="Table3" cellSpacing="1" cellPadding="2" border="1" style="border-collapse:collapse" borderColor="#333333">
								<TR class="TableDataRow">
									<TD  colspan="2" style="HEIGHT: 12px" align="center"><STRONG style="FONT-SIZE: 10pt">添加新节点</STRONG></TD>
									
								</TR>
                                <TR class="TableDataRow">
									<TD style="WIDTH: 61px">节点名称</TD>
									<TD><asp:textbox id="txtModuleName" runat="server" Width="458px" CssClass="BigInput"></asp:textbox></TD>
								</TR>
								<TR class="TableDataRow">
									<TD style="WIDTH: 61px">所属父节点</TD>
									<TD><asp:textbox id="txtParentName" runat="server" Width="296px" ReadOnly="True" CssClass="BigInput"></asp:textbox>
										<%--<INPUT type="button" value="浏览" class="BigButton" onclick="window.showModalDialog('SelectParentModule.aspx', window, 'dialogWidth:350px');"
											name="btnBrowseParent">--%>
										<INPUT type="button" value="浏览" class="BigButton" onclick="ShowSelectParentModule();"
											name="btnBrowseParent">
										<asp:CheckBox id="chkIsRootModule" runat="server" Text="当前模块为根模块" Visible="false"></asp:CheckBox>
										<asp:HiddenField id="txtParentID" runat="server"></asp:HiddenField>
                                        
									</TD>
								</TR>
                                
<%--								<TR class="TableDataRow">
									<TD style="WIDTH: 61px">指向的链接</TD>
									<TD>
										<P><asp:textbox id="txtLinkName" runat="server" Width="418px" CssClass="BigInput"></asp:textbox>
											<%--<INPUT class="BigButton" onclick="window.showModalDialog('SelectLinkObject.aspx',window,'dialogWidth:550px');"
												type="button" value="浏览">--%>&nbsp;<INPUT class="BigButton" type="button" value="无链接" onclick="document.all.txtLinkID.value='0';document.all.txtLinkName.value='';" style="display:none"></P>
									</TD>
								</TR>
                               <%-- <tr class="TableDataRow">
                                    <td style="width: 61px">
                                        指向的目标</td>
                                    <td>
                                        <asp:TextBox ID="txtTargetName" runat="server" CssClass="BigInput" Width="211px"></asp:TextBox></td>
                                </tr>
                                <tr class="TableDataRow">
                                    <td style="width: 61px">
                                        链接类型</td>
                                    <td>
                                        <asp:DropDownList ID="ddlistLinkType" runat="server">
                                            <asp:ListItem Value="0">无链接</asp:ListItem>
                                            <asp:ListItem Value="1">普通链接</asp:ListItem>
                                            <asp:ListItem Value="2">静态链接</asp:ListItem>
                                        </asp:DropDownList>
                                        </td>
                                </tr>--%>
								<TR class="TableDataRow">
									<TD style="WIDTH: 61px"></TD>
									<TD><asp:checkbox id="chkIsDisplay" runat="server" Text="是否显示" Checked="True"></asp:checkbox>
                                        </TD>
								</TR>
								<TR class="TableDataRow">
									<TD align="center" colSpan="2">
										<asp:Button id="btnAdd" runat="server" Text="添加" CssClass="BigButton" onclick="btnAdd_Click"></asp:Button>&nbsp;<INPUT type="button" value="返回" class="BigButton" onclick="history.go(-1);"></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
				</TABLE>
			</FONT>
		</form>
	</body>
</HTML>