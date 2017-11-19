<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProfileMusicType.aspx.cs" Inherits="WeiXinYiShengCollege.WebSite.jqueryweui.wx.ProfileMusicType" %>


<!DOCTYPE html>
<html>
<head>
    <title>五音疗愈曲目类别</title>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no">

    <meta name="description" content="">
    <link rel="stylesheet" href="../lib/weui.min.css">
    <link rel="stylesheet" href="../css/jquery-weui.css">
    <link rel="stylesheet" href="css/demos.css">
</head>

<body ontouchstart>

    <div class="weui-cells">
        
        <a class="weui-cell weui-cell_access" href="ProfileMusicList.aspx?Status=2&OpenId=<%=OpenId %>">
            <div class="weui-cell__bd">
                <p>五脏补泻</p>
            </div>
        </a>
        <a class="weui-cell weui-cell_access" href="ProfileMusicList.aspx?Status=1&OpenId=<%=OpenId %>">
            <div class="weui-cell__bd">
                <p>常见疾病</p>
            </div>
        </a>
       

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
