<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserManage.aspx.cs" Inherits="HospitalBookWebSite.Home.UserManage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="css/admin.global.css" rel="stylesheet" type="text/css" />
    <link href="css/admin.content.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery-1.9.1.min.js"></script>
    <script type="text/javascript" src="../js/jquery.utils.js"></script>
    <script type="text/javascript" src="../js/admin.js"></script>
    <script type="text/javascript" src="../js/jsonlint.js"></script>
    <title>用户管理</title>
    <script type="text/javascript">
        var pageCount = 0;
        $(document).ready(function () {
            GetList(1);
        });
        //获取中奖纪录
        function GetList(pageIndex) {
            var mobile = $("#txtmobile").val();
            var usertype = $("#ddlUserType").val();
            var userlevel = $("#ddlUserLevel").val();
            var cmid = $("#ddlCustomerManagerId").val();
            $.ajax({
                type: "POST",
                url: "handler/PageHandler.ashx",
                data: { Action: "GetUserList", mobile: mobile, UserType: usertype, UserLevel: userlevel, CustomerManagerId: cmid, PageIndex: pageIndex, r: Math.random() },
                dataType: "json",
                async: true,
                success: function (result) {
                    if (result && typeof result == "object") {
                        if (result.code == -105) {
                            alert("未登录");

                            return false;
                        } else if (result.code == 1 && result.m && result.m.Source.length > 0) {
                            GetListHtml(result.m);
                            //console.log(result.m);
                        }
                    }
                    if (result.m && result.m.Source.length > 0) {
                        var data = result.m;
                        $("#pageFirst").bind("click", function () { GetList(1) });
                        $("#pageLast").bind("click", function () { GetList(data.TotalPages) });
                        if (data.HasPreviousPage) {
                            $("#prev").unbind("click").bind("click", function () { GetList(data.PageIndex - 1) });
                        }
                        else {
                            $("#prev").unbind("click").removeClass("on");
                        }
                        $("#spanNum").html("");
                        for (var n = 0; n < data.NumberPage.length; n++) {
                            if (data.NumberPage[n] == data.PageIndex) {
                                //<a href="">1</a>
                                $("#spanNum").append(' <a onclick="GetList(' + data.NumberPage[n] + ')" href="javascript:void(0);"  class="bian">' + data.NumberPage[n] + '</a>');
                            } else {
                                $("#spanNum").append(' <a onclick="GetList(' + data.NumberPage[n] + ')" href="javascript:void(0);"  class="bian">' + data.NumberPage[n] + '</a>');
                            }
                        }

                        if (data.HasNextPage) {
                            $("#next").unbind("click").bind("click", function () { GetList(data.PageIndex + 1) });
                        }
                        else {
                            $("#next").unbind("click").removeClass("on");
                        }
                        pageCount = data.TotalPages;
                        $("#pageNumber").val(data.PageIndex);
                        PageShow(data.PageIndex, data.TotalPages);
                        $("#divPage").show();
                        
                    } else {
                        $("#tbodyHtml").hide();
                        $("#divPage").hide();
                        
                    }
                }
            });
        }
        //显示多少页和第几页
        function PageShow(PageIndex, PageCount) {
            PageIndex <= 0 ? 1 : PageCount
            PageCount <= 0 ? 1 : PageCount;
            $("#pageIndex").text("第" + PageIndex + "/" + PageCount + "页");
        }

        //输入页码
        function NumberPage() {
            var reg = new RegExp("^[0-9]*$");
            var PageNumber = $.trim($("#pageNumber").val());
            if (reg.test(PageNumber) && PageNumber != "") {
                if (PageNumber <= pageCount) {
                    GetList(parseInt(PageNumber));
                } else {
                    GetList(parseInt(pageCount));
                }
            }
        }

        function GetListHtml(data) {
            //深度copy表头
            var $firstTr = $("#tbodyHtml tr").eq(0).clone();
            //深度copy数据行
            var $contentTr = $("#tbodyHtml tr").eq(1).clone();
            //清空表内容
            var $tbodyHtml = $("#tbodyHtml");
            $tbodyHtml.empty().show();
            //表头附加到表中
            $firstTr.appendTo("#tbodyHtml");
            //循环
            if (data.Source.length > 0) {
                for (var i = 0; i < data.Source.length; i++) {
                    //再复制行
                    var $contentTrTmp = $contentTr.clone();
                    var tmpItem = data.Source[i];
                    $contentTrTmp.find("td").eq(0).html(tmpItem.Id);
                    $contentTrTmp.find("td").eq(1).html(tmpItem.OpenId);
                    $contentTrTmp.find("td").eq(2).html(tmpItem.Mobile);
                    $contentTrTmp.find("td").eq(3).html(tmpItem.NickName);
                    $contentTrTmp.find("td").eq(4).html(tmpItem.UserType + tmpItem.UserTypeStr);
                    $contentTrTmp.find("td").eq(5).html(tmpItem.UserLevel + tmpItem.UserLevelStr);
                    var parentIdStr = '';
                    if (tmpItem.ParentId > 0)
                    {
                        parentIdStr ='<a href="UserEdit.aspx?Id=' + tmpItem.ParentId + '" target="_blank">'+tmpItem.ParentNickName+'</a>'
                    }
                    $contentTrTmp.find("td").eq(6).html(parentIdStr);
                    $contentTrTmp.find("td").eq(7).html(tmpItem.ApproveFlag + tmpItem.ApproveFlagStr);
                    $contentTrTmp.find("td").eq(8).html(tmpItem.Score);
                    $contentTrTmp.find("td").eq(9).html(tmpItem.QrCodeScene_id);
                    $contentTrTmp.find("td").eq(10).html(tmpItem.CustomerManagerId + tmpItem.CustomerManagerName + tmpItem.CustomerManagerMobile);
                    //$contentTrTmp.find("td").eq(11).html(tmpItem.UserInfoJson);
                    $contentTrTmp.find("td").eq(11).html(tmpItem.CreateDateTime);
                    //在编辑里进行验证通过操作（验证通过的同时需要生成场景ID）
                    $contentTrTmp.find("td").eq(12).html('<a href="UserEdit.aspx?OpenId=' + tmpItem.OpenId + '" target="_blank">编辑</a>');

                    var qrcode = '';
                    if (tmpItem.QrCodeScene_id > 0)
                    {
                        qrcode = ('<a href="CreateQrCode.aspx?OpenId=' + tmpItem.OpenId + '" target="_blank">生成二维码</a>');
                    }
                    $contentTrTmp.find("td").eq(13).html(qrcode);

                    $contentTrTmp.appendTo("#tbodyHtml");
                }
                //console.log(firstTr);
                //$firstTr.prop("outerHTML") 可以获得节点外边的包含父节点的属性 如果直接用html()获取 只是获取到子节点的html
                //$("#tbodyHtml").html($firstTr.prop("outerHTML") + tdtml);
            }
        }
    </script>
    <style type="text/css">

select option {
	padding-right:3px;
}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="divList">
        <div class="block">
            <div class="h">
                <span class="icon-sprite icon-list"></span>
                <h3>用户列表</h3>
                <div class="bar">
                     手机号：<input id="txtmobile" type="text" />用户类型：<asp:DropDownList ID="ddlUserType" runat="server" >
                        <asp:ListItem Value="-1">请选择</asp:ListItem>
                         <asp:ListItem Value="0">未分配(不详)</asp:ListItem>
                        <asp:ListItem Value="1">粉丝</asp:ListItem>
                        <asp:ListItem Value="2">理事</asp:ListItem>
                    </asp:DropDownList>
                     用户级别：<asp:DropDownList ID="ddlUserLevel" runat="server">
                         <asp:ListItem Value="-1">请选择</asp:ListItem>
                        <asp:ListItem Value="0">未分配（不详）</asp:ListItem>
                        <asp:ListItem Value="1">理事</asp:ListItem>
                        <asp:ListItem Value="2">常务理事</asp:ListItem>
                        <asp:ListItem Value="3">荣誉理事</asp:ListItem>
                    </asp:DropDownList>
                     客户经理：<asp:DropDownList ID="ddlCustomerManagerId" runat="server"  Width="145px"></asp:DropDownList>
                &nbsp;<input id="btnQuery" type="button" value="查询" onclick="GetList(1);" />
                </div>
            </div>
            <div class="tl corner">
            </div>
            <div class="tr corner">
            </div>
            <div class="bl corner">
            </div>
            <div class="br corner">
            </div>
            <div class="cnt-wp">
                <div class="cnt">
                    <div>
                        <table  id="tbodyHtml" class="data-table" cellspacing="0" cellpadding="0" id="gvList" style="border-collapse: collapse;">
                            <tr>
                                <th scope="col" >序号</th>
                                <th align="left" scope="col" >OpenId</th>
                                <th scope="col" >手机号</th>
                                <th scope="col" >姓名</th>
                                <th scope="col" >用户类型</th>
                                <th scope="col" >用户级别</th>
                                <th scope="col" >所属理事</th>
                                <th scope="col" >认证状态</th>
                                <th scope="col" >积分</th>
                                <th scope="col" >QR场景值</th>
                                <th scope="col" >客户经理</th>
                                
                                <th scope="col" >创建时间</th>
                                <th scope="col" >编辑</th>
                                <th scope="col" >生成二维码</th>
                            </tr>
                            <tr>
                                <td align="center">&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                             
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                 <td>&nbsp;</td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div>
        <table id="divPage" border="0" cellspacing="6" cellpadding="0" class="size18black2">
            <tr valign="bottom">
                <td height="40" align="center">
                    <a href="javascript:void(0);" id="pageFirst">首页</a>
                </td>
                <td align="center">
                    <a href="javascript:void(0);" id="prev">上一页</a>
                </td>
                <td align="center" id="spanNum" style="vertical-align: bottom"></td>
                <td align="center" class="style1">
                    <a href="javascript:void(0);" id="next">下一页</a>
                </td>
                <td align="center">
                    <a href="javascript:void(0);" id="pageLast">尾页</a>
                </td>
                <td align="center" style="vertical-align: bottom">
                    <input id="pageNumber" type="text" onkeyup="this.value=this.value.replace(/\D/g,'')" class="bian" />
                </td>
                <td align="center">
                    <a href="javascript:void(0);" onclick="NumberPage();">GO</a>
                </td>
                <td align="center" id="pageIndex" style="vertical-align: bottom"></td>
            </tr>
        </table>
    </div>
        </form>
</body>
</html>
