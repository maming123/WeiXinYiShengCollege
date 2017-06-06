<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MsgAutoReplyEdit.aspx.cs" Inherits="WeiXinYiShengCollege.WebSite.Home.MsgAutoReplyEdit" ValidateRequest="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="css/admin.global.css" rel="stylesheet" type="text/css" />
    <link href="css/admin.content.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery-1.9.1.min.js"></script>
    <script type="text/javascript" src="../js/jquery.utils.js"></script>
    <script type="text/javascript" src="../js/admin.js"></script>
    <script type="text/javascript" src="../js/jsonlint.js"></script>
    
    <script type="text/javascript" src="../js/layer/layer.js"></script>
    <script type="text/javascript" src="../js/common.js"></script>
    <title>自动回复消息编辑</title>

    <script>

        function OpenMaterialNewsListLayer()
        {
            IframLayerV3("MaterialNews/MaterialNewsListLayer.aspx?r="+Math.random(), 800, 400);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            选择回复的消息类型： 
        <asp:DropDownList ID="ddlMsgType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlMsgType_SelectedIndexChanged">
            <asp:ListItem Value="0" Selected="True">请选择</asp:ListItem>
            <asp:ListItem Value="news">图文</asp:ListItem>
            <asp:ListItem  Value="text">文本</asp:ListItem>
            
        </asp:DropDownList>
            &nbsp;填写用户上行内容：<asp:TextBox ID="txtUpKey" runat="server" Width="327px"></asp:TextBox>
            &nbsp;<br />
            填写备注：<asp:TextBox ID="txtRemark" runat="server" Width="574px"></asp:TextBox>
            <br />
            回复是否生效：<asp:DropDownList ID="ddlIsDelete" runat="server">
                <asp:ListItem Selected="True" Value="0">生效</asp:ListItem>
                <asp:ListItem Value="1">失效</asp:ListItem>
            </asp:DropDownList>
&nbsp;<asp:Panel ID="panelText" runat="server" Visible="False">
                文本消息：<br />
                <asp:TextBox ID="txtText" runat="server" Columns="100" Rows="20" TextMode="MultiLine"></asp:TextBox>
                <br />
            </asp:Panel>
            <asp:Panel ID="panelNews" runat="server" Visible="False">
                图文消息：<b>请先从图文素材列表中复制相关内容到此处，然后进行保存即可<br /> 
                <input id="btnSelectNewsLayer" type="button" value="选择图文" onclick="OpenMaterialNewsListLayer();" />
                </b>
                <asp:Button ID="btnSync" runat="server" Text="同步给图文列表" OnClick="btnSync_Click" />
                <asp:HiddenField ID="hdJson" runat="server" Value="asdfsd" />
                <br />
                <fieldset>
                    <legend>图文1</legend>标题：<asp:TextBox ID="txtTitle_1" runat="server" Width="663px"></asp:TextBox>
                    <br />
                    描述：<asp:TextBox ID="txtDescription_1" runat="server" Columns="100" Rows="3" TextMode="MultiLine" Width="660px"></asp:TextBox>
                    <br />
                    图片链接：<asp:TextBox ID="txtPicUrl_1" runat="server" Width="631px"></asp:TextBox>
                    <br />
                    文章链接：<asp:TextBox ID="txtUrl_1" runat="server" Width="636px"></asp:TextBox>
                </fieldset>
                <fieldset>
                    <legend>图文2</legend>
                    标题：<asp:TextBox ID="txtTitle_2" runat="server" Width="663px"></asp:TextBox>
                    <br />
                    描述：<asp:TextBox ID="txtDescription_2" runat="server" Columns="100" Rows="3" TextMode="MultiLine" Width="660px"></asp:TextBox>
                    <br />
                    图片链接：<asp:TextBox ID="txtPicUrl_2" runat="server" Width="631px"></asp:TextBox>
                    <br />
                    文章链接：<asp:TextBox ID="txtUrl_2" runat="server" Width="636px"></asp:TextBox>
                </fieldset>
               <fieldset>
                    <legend>图文3</legend>
                    标题：<asp:TextBox ID="txtTitle_3" runat="server" Width="663px"></asp:TextBox>
                    <br />
                    描述：<asp:TextBox ID="txtDescription_3" runat="server" Columns="100" Rows="3" TextMode="MultiLine" Width="660px"></asp:TextBox>
                    <br />
                    图片链接：<asp:TextBox ID="txtPicUrl_3" runat="server" Width="631px"></asp:TextBox>
                    <br />
                    文章链接：<asp:TextBox ID="txtUrl_3" runat="server" Width="636px"></asp:TextBox>
                </fieldset>
                <fieldset>
                    <legend>图文4</legend>
                    标题：<asp:TextBox ID="txtTitle_4" runat="server" Width="663px"></asp:TextBox>
                    <br />
                    描述：<asp:TextBox ID="txtDescription_4" runat="server" Columns="100" Rows="3" TextMode="MultiLine" Width="660px"></asp:TextBox>
                    <br />
                    图片链接：<asp:TextBox ID="txtPicUrl_4" runat="server" Width="631px"></asp:TextBox>
                    <br />
                    文章链接：<asp:TextBox ID="txtUrl_4" runat="server" Width="636px"></asp:TextBox>
                </fieldset>
                </asp:Panel>
            &nbsp;<asp:Button ID="Button1" runat="server" Text="添加" Height="44px" Width="76px" OnClick="Button1_Click" />
            <input style="height: 44px; width: 76px;" id="btnback" type="button" value="返回列表" onclick="javascript: document.location.href = 'MsgAutoReplyManage.aspx';" />
        </div>
    </form>
</body>
</html>
