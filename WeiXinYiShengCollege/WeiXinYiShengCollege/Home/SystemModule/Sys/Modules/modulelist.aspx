<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="modulelist.aspx.cs" Inherits="MLK.SystemModule.Sys.Modules.modulelist" %>

<html>
<head runat="server">
    <title>系统模块列表</title>
    <link href="../../../css/sysmodule.css" rel="stylesheet" />
   
    <script language="javascript">
        function setModuleOrder(moduleID){
            document.all.hiddenTxtModuleID.value = moduleID;
            document.forms[0].submit();
        }
    </script>
</head>
	<body enableviewstate="false">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0" height="100%">
				<TR>
					<TD vAlign="top" width="230" height="30" colSpan="3">
						<TABLE id="Table2" cellSpacing="4" cellPadding="4" border="0">
							<TR>
								<TD><A href="Add.aspx" target="ifrmModuleInfo">添加新节点</A></TD>
								<TD>
									<asp:LinkButton id="linkBtnDelete" Visible="false" runat="server" onclick="linkBtnDelete_Click">删除选中的模块</asp:LinkButton>
								</TD>
							</TR>
						</TABLE>
						<A href="Add.aspx"></A>
					</TD>
				</TR>
				<TR>
					<TD width="210" vAlign="top">
						<div style="overflow:auto;height:100%">
                            <asp:TreeView ID="treeModules"  runat="server" OnSelectedNodeChanged="treeModules_SelectedNodeChanged" ShowLines="True">
                            </asp:TreeView>
                        </div>
					</TD>
					<TD width="8"></TD>
					<TD vAlign="top">
						<iframe name="ifrmModuleInfo" frameborder="0"   scrolling="no" width="100%" height="100%"></iframe>
					</TD>
				</TR>
			</TABLE>
			<input type="hidden" name="hiddenTxtModuleID" />
		</form>
	</body>
</html>
