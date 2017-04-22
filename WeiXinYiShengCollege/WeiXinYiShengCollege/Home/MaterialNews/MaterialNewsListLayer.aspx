<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MaterialNewsListLayer.aspx.cs" Inherits="WeiXinYiShengCollege.WebSite.Home.MaterialNews.MaterialNewsListLayer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>

    <title></title>
        <link href="../css/admin.global.css" rel="stylesheet" type="text/css" />
    <link href="../css/admin.content.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../js/jquery-1.9.1.min.js"></script>
    <script type="text/javascript" src="../../js/jquery.utils.js"></script>
    <script type="text/javascript" src="../../js/admin.js"></script>
    <script type="text/javascript" src="../../js/jsonlint.js"></script>
    <script>

        function GetNewsJson() {

            //console.log(parent.document.getElementById("hdJson").value);

            $("#GridView1 :radio").each(function (i) {

                //console.log($(this).attr("id"));
                //console.log($(this).is(":checked"));
                //console.log($("#span_" + $(this).attr("id")).html());
                if ($(this).is(":checked")) {

                    parent.document.getElementById("hdJson").value = $("#span_" + $(this).attr("id")).html();
                   
                }
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        选择要获取的序号(每页20条)：<asp:DropDownList ID="DropDownList1" runat="server">

        </asp:DropDownList>
    
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="从微信同步下载图文素材列表" />
    
        <input id="Button4" type="button" value="确定" class="btnstyle" onclick="GetNewsJson();" /><input id="btnClose" type="button" value="关闭窗口" onclick="    parent.CloseLayer();" /><br />
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                Width="100%" BorderStyle="None" BorderWidth="0px" CellPadding="1" CellSpacing="1">
            <Columns>
               
                    <asp:TemplateField HeaderText="Media_Id">
                        <ItemTemplate>
                            <input id="<%# Eval("media_id")%>" type="radio" name="radiogroup1" /><%# Eval("media_id")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                <asp:TemplateField HeaderText="title">
                        <ItemTemplate>
                            <%# Eval("title")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                 <asp:TemplateField HeaderText="Json">
                        <ItemTemplate>
                            <span id="span_<%# Eval("media_id")%>"><%#Eval("Json")%></span>
                        </ItemTemplate>
                    </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <br />
        <asp:Label ID="Label1" runat="server" ForeColor="#FF6600"></asp:Label>

    </div>
    </form>
</body>
</html>
