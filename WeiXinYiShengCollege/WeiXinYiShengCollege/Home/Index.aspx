<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="HospitalBook.WebSite.Home.Index" ValidateRequest="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>后台 - 首页</title>
    <link href="../js/easyui-1.4.1/themes/bootstrap/easyui.css" rel="stylesheet" />
    <link href="../js/easyui-1.4.1/themes/icon.css" rel="stylesheet" />
    <style>
        .tempul {
            list-style: none;
            margin: 0px;
        }

            .tempul li {
                line-height: 32px;
            }

                .tempul li a {
                    text-decoration: none;
                }
    </style>
    <script src="..//js/jquery-1.9.1.min.js"></script>
    <script src="..//js/easyui-1.4.1/jquery.easyui.min.js"></script>
    <script src="..//js/easyui-1.4.1/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript">
        function addTab(subtitle, url) {
            if (!$('#tabs').tabs('exists', subtitle)) {
                $('#tabs').tabs('add', {
                    title: subtitle,
                    content: createFrame(url),
                    closable: true
                });
            } else {
                $('#tabs').tabs('select', subtitle);
                var tab = $('#tabs').tabs('getSelected');
                $('#tabs').tabs('update', { tab: tab, options: { content: createFrame(url) } });
            }
        }

        function createFrame(url) {
            var s = '<iframe scrolling="auto" frameborder="0"  src="' + url + '" style="width:100%;height:100%;"></iframe>';
            return s;
        }
    </script>
</head>
<body class="easyui-layout">
   <div data-options="region:'north',border:false" style="background: #B3DFDA; padding: 10px">
        <%=LoginUserName %>后台管理系统
        <%--<image src="login/images/logo.png"></image>--%>
        <a href="login/LoginOut.aspx?U=../login/index.aspx">退出登录</a>
    </div>
    <div data-options="region:'west',split:false,title:'导航菜单'" style="width: 250px; padding: 10px;">


        <div id="accordion" class="easyui-accordion" style="width: 220px; height: auto;">

            <div title="功能管理" data-options="collapsed:true,collapsible:true" style="padding: 10px;">
                <ul class="tempul">

                    <li><a href="javascript:void(0)" onclick="addTab('自动回复管理','MsgAutoReplyManage.aspx')"><span style="color:#977171">>&nbsp;&nbsp;自动回复管理</span></a></li>
                    <li><a href="javascript:void(0)" onclick="addTab('图文素材管理','MaterialNews/MaterialNewsList.aspx')"><span style="color:#977171">>&nbsp;&nbsp;图文素材查看</span></a></li>
                    <li><a href="javascript:void(0)" onclick="addTab('订单积分管理','OrderManage/OrderList.aspx')"><span style="color:#977171">>&nbsp;&nbsp;订单积分管理</span></a></li>
                    <li><a href="javascript:void(0)" onclick="addTab('积分兑换管理','ScoreManage/ExchangeScoreList.aspx')"><span style="color:#977171">>&nbsp;&nbsp;积分兑换管理</span></a></li>
                    <li><a href="javascript:void(0)" onclick="addTab('兑换配置查看','ScoreManage/ScoreConfigList.aspx')"><span style="color:#977171">>&nbsp;&nbsp;兑换配置查看</span></a></li>
                    <li><a href="javascript:void(0)" onclick="addTab('客户经理查看','CustomerMgr/CustomerManagerList.aspx')"><span style="color:#977171">>&nbsp;&nbsp;客户经理查看</span></a></li>
                    
                </ul>
            </div>
            <div title="系统管理" data-options="collapsed:false,collapsible:false" style="padding: 10px;">
                <ul class="tempul">

                    <li><a href="javascript:void(0)" onclick="addTab('用户信息管理','UserManage.aspx')"><span style="color:#977171">>&nbsp;&nbsp;用户信息管理</span></a></li>
                    <li><a href="javascript:void(0)" onclick="addTab('密码修改','Login/EditPassword.aspx')"><span style="color:#977171">>&nbsp;&nbsp;密码修改</span></a></li>
                
                </ul>
            </div>
        </div>
    </div>
    <div data-options="region:'center',title:''">
        <div id="tabs" class="easyui-tabs" fit="true" border="false">
        </div>
    </div>

</body>
</html>
