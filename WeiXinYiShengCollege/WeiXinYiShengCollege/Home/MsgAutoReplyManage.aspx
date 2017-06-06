<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MsgAutoReplyManage.aspx.cs" Inherits="WeiXinYiShengCollege.WebSite.Home.MsgAutoReplyManage" %>


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
    <title>自动回复列表</title>
    <script type="text/javascript">
        var pageCount = 0;
        $(document).ready(function () {
            GetList(1);
        });
        //获取中奖纪录
        function GetList(pageIndex) {
            var upkey = $("#txtupkey").val();
           
            $.ajax({
                type: "POST",
                url: "handler/PageHandler.ashx",
                data: { Action: "GetReplyContentList", upkey: upkey, PageIndex: pageIndex, r: Math.random() },
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
                    $contentTrTmp.find("td").eq(1).html(tmpItem.UpKey);
                    $contentTrTmp.find("td").eq(2).html(tmpItem.ResponseMsgType);
                    $contentTrTmp.find("td").eq(3).html(tmpItem.Remark);
                    $contentTrTmp.find("td").eq(4).html(tmpItem.CreateDatetime);
                    //（1:删除 0：未删除）
                    var isDelete = '正常显示';
                    if (tmpItem.IsDelete == 1)
                    {
                        isDelete = '已被删除';
                    }
                    $contentTrTmp.find("td").eq(5).html(isDelete);
                    $contentTrTmp.find("td").eq(6).html('<a href="MsgAutoReplyEdit.aspx?msgtype=' + tmpItem.ResponseMsgType + '&Id=' + tmpItem.Id + '" >编辑</a>');
                    
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
                <h3>自动回复列表</h3>
                <div class="bar">
                    <a href="MsgAutoReplyEdit.aspx">添加新回复</a>&nbsp;&nbsp; 用户上行内容：<input id="txtupkey" type="text" /><input id="btnQuery" type="button" value="查询" onclick="GetList(1);" />
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
                                <th align="left" scope="col" >用户上行语</th>
                                <th scope="col" >回复类型</th>
                                <th scope="col" >备注</th>
                                <th scope="col" >创建时间</th>
                                <th scope="col" >状态</th>
                                <th scope="col" >编辑</th>
                                
                            </tr>
                            <tr>
                                <td align="center">&nbsp;</td>
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

