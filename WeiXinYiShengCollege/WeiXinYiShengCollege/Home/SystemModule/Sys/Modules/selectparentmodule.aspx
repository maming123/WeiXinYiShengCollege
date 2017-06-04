<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="selectparentmodule.aspx.cs" Inherits="MLK.SystemModule.Sys.Modules.selectparentmodule" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>选择所属父模块</title>
         <link href="../../../css/sysmodule.css" rel="stylesheet" />
       

		<script language="javascript" type="text/javascript">
			function selectParentModule(parentModuleID,parentModuleName){
			    //var parentDocument = window.dialogArguments.document;
			    var parentDocument = parent.window.document;
				parentDocument.all.txtParentName.value = parentModuleName;
				parentDocument.all.txtParentID.value = parentModuleID;
				window.close();
			}
		</script>
		<base target="_self">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" height="100%" border="1" bordercolor="#111111" style="border-collapse:collapse">
				<TR>
					<TD>
						<div style="overflow:auto;height:100%">
                            <asp:TreeView ID="tvModules" runat="server" OnSelectedNodeChanged="tvModules_SelectedNodeChanged" ShowLines="True">
                            </asp:TreeView>
						</div>
					</TD>
				</TR>
				<!--<tr height="30">
					<td align="center" valign="center">
						<input type="button" value="取消" onclick="window.close();">
					</td>
				</tr>-->
			</TABLE>
		</form>
	</body>
</HTML>
