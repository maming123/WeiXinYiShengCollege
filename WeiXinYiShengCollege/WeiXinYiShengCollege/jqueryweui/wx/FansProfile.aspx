<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FansProfile.aspx.cs" Inherits="WeiXinYiShengCollege.WebSite.jqueryweui.wx.FansProfile" %>


<%@ Import Namespace="WeiXinYiShengCollege.Business" %>

<!DOCTYPE html>
<html>
<head>
    <title>个人中心</title>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no">

    <meta name="description" content="">

    <link rel="stylesheet" href="../lib/weui.min.css">
    <link rel="stylesheet" href="../css/jquery-weui.css">
    <link rel="stylesheet" href="css/demos.css">
        <style>
        .demos-title {
            text-align: center;
            font-size: 30px;
            color: #3cc51f;
            font-weight: 400;
            margin: 0px 12px 0px 20px;
        }
    </style>
</head>
     
<body ontouchstart>

    
    <header style="padding:10px 0;background-color:#f2f2f2;">
        <div style="margin-left:10px;"><img id="imgPhoto" style="border-radius: 100px;border: none; width:120px;height:120px;" src="<%=sUser.HeadImgUrl %>" />
        <span class="demos-title"><%=sUser.NickName %></span> 
        </div>
    </header>

    <div class="page__bd">

        <div class="weui-panel">
            <div class="weui-panel__bd">
                <div class="weui-media-box weui-media-box_small-appmsg">
                    <div class="weui-cells">
                        <a class="weui-cell weui-cell_access" href="MyDoctor.aspx?OpenId=<%=sUser.OpenId %>">
                            <div class="weui-cell__bd weui-cell_primary">
                                <p>我的健康顾问</p>
                            </div>
                            <span class="weui-cell__ft"></span>
                        </a>
                       
                        <a class="weui-cell weui-cell_access" href="javascript:;">

                            <div class="weui-cell__bd weui-cell_primary">
                                <p>我的积分(<%=sUser.LastScore/100 %>)</p>
                            </div>

                        </a>
                        <a class="weui-cell weui-cell_access"  href="MyOrderList.aspx?OpenId=<%=sUser.OpenId %>">

                            <div class="weui-cell__bd weui-cell_primary">
                                <p>我的消费</p>
                            </div>
                            <span class="weui-cell__ft"></span>
                        </a>
                         
                        <a class="weui-cell weui-cell_access" href="FansProfileEdit.aspx?OpenId=<%=sUser.OpenId %>">

                            <div class="weui-cell__bd weui-cell_primary">
                                <p>个人信息设置</p>
                            </div>
                            <span class="weui-cell__ft"></span>
                        </a>
                        <a class="weui-cell weui-cell_access" href="DoctorWorkList.aspx?Id=<%=sUser.ParentId %>">

                            <div class="weui-cell__bd weui-cell_primary">
                                <p>医生出诊详情</p>
                            </div>
                            <span class="weui-cell__ft"></span>
                        </a>
                        
                        
                    </div>
                </div>
            </div>
        </div>

    </div>

    <script src="../lib/jquery-2.1.4.js"></script>
    <script src="../lib/fastclick.js"></script>
    <script>
        $(function () {
            FastClick.attach(document.body);
        });
    </script>
    <script src="../js/jquery-weui.js"></script>

</body>
</html>
