<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuestionResult.aspx.cs" Inherits="WeiXinYiShengCollege.WebSite.jqueryweui.wx.QuestionResult" %>

<%@ Import Namespace="WeiXinYiShengCollege.Business.Common.Models" %>
<!DOCTYPE html>
<html>
<head>
    <title>五音疗愈曲目</title>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no">

    <meta name="description" content="">

    <link rel="stylesheet" href="../lib/weui.min.css">
    <link rel="stylesheet" href="../css/jquery-weui.css">
    <link rel="stylesheet" href="css/demos.css">
</head>

<body ontouchstart>

    <header class='demos-header'>
        <h1 class="demos-title">五音疗愈曲目</h1>
    </header>
    <article class="weui-article">

        <section>
            <%int i = 1; %>
            <%foreach (SickMusicItem music in listMusic)
              { %>
            <section>
                <p>
                    个性化曲目<%=i %>
                </p>
                <audio controls="controls" id="audio<%=i %>">
                    <source src="http://wx.yishengcollege.com/music/<%=music.MusicFileName %>" type="audio/mpeg" />
                    您浏览器不支持audio标签.
                </audio>
            </section>
            <%i++;
              } %>
        </section>
    </article>
    <div class="weui-cells__tips">
        如您有反复试听的需要，请搜索公众号：易生学苑大健康 或扫描下方二维码，进入个人中心-问卷调查结果继续试听。
        <br />
        建议：
        <br />
        (1)如您想获得更好的效果，请用专业设备播放（建议设备：）
        <br />
        (2)如您想获得更多的曲目，请联系客服或进入微店咨询详情
    </div>
    <div class="weui-cells__tips" style="text-align:center">
        
        <img src="/images/qrcode/QrCodeScene_id_37.jpg" style="width: 200px; height: 200px;" />
    </div>
    <script src="../lib/jquery-2.1.4.js"></script>
    <script src="../lib/fastclick.js"></script>
    <script>
        $(function () {
            FastClick.attach(document.body);
        });

    </script>
    <script src="../js/jquery-weui.js"></script>
    <script>function PlayAudio(msg) {
        if (msg == "1") {
            var audio = $("#audio1")[0];
            audio.play();
        }
}</script>
</body>
</html>

