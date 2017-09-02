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
                
                <a href="javascript:void(0);" class="weui-btn weui-btn_mini weui-btn_primary" id="aplay<%=i %>" onclick="playMusic(<%=i %>);return false;">循环播放曲目<%=i %></a>
            </section>
            <%i++;
              } %>
            <a href="javascript:void(0);"  class="weui-btn weui-btn_mini weui-btn_primary" id="aplay">顺序循环播放所有曲目</a>
        </section>
    </article>
    <div class="weui-cells__tips">
        如您有反复收听的需要，请搜索公众号“易生学苑大健康”， 或扫描下方二维码，进入“我的”，点击“我的曲目”，如您还未定制五音曲目，请到“我的”-“定制五音曲目”生成我的曲目。
        <br />
        建议：
        <br />
        (1)因曲目中含有起到治疗作用的低频成分，如您想获得更好的效果，请使用专业设备播放（专业定制化音箱请咨询客服人员）
        <br />
        (2)如您想获得更多的五音疗愈曲目，请联系客服人员。
        <br />
        客服电话/微信：13520927078
    </div>
    <div class="weui-cells__tips" style="text-align:center">
        
        <img src="/images/qrcode/QrCodeScene_id_37.jpg" style="width: 200px; height: 200px;" />
    </div>
    <div class="weui-footer">
        <p class="weui-footer__text">Copyright © 2009-<%=DateTime.Now.Year %> YI SHENG</p>
    </div>
    <script src="../lib/jquery-2.1.4.js"></script>
    <script src="../lib/fastclick.js"></script>
    <script>
        $(function () {
            FastClick.attach(document.body);
        });

    </script>
    <script src="../js/jquery-weui.js"></script>
    <script>
        $(function () {
            $("#aplay").click(function () {

                var audio1 = $("#audio1")[0];
                var audio2 = $("#audio2")[0];
                var audio3 = $("#audio3")[0];
                audio1.pause();
                audio2.pause();
                audio3.pause();
                audio1.preload="metadata"; 
                audio1.play();
                audio1.addEventListener('ended', function () {
                    // Wait 500 milliseconds before next loop  
                    
                    audio2.preload = "metadata";
                    audio2.play();
                    audio2.addEventListener('ended', function () {
                        // Wait 500 milliseconds before next loop  
                        
                        audio3.preload = "metadata";
                        audio3.play();
                        audio3.addEventListener('ended', function () {
                            // Wait 500 milliseconds before next loop  
                            audio1.preload = "metadata";
                            audio1.play();
                        }, false);
                    }, false);
                }, false);
            });
            
        });

        function playMusic(musicnum) {
        
            var audio = $("#audio" + musicnum)[0];
            $("audio").each(function() {
                if($(this)[0].id != audio.id)
                {
                    //console.log($(this)[0]);
                    if ($(this)[0].paused==false) {
                        $(this)[0].pause();
                    }
                }
            });
            audio.preload = "metadata";
            audio.loop = "loop";
            audio.play();
            
            //audio.load();                //音频加载
            ////这里的监听事件，表示音频开始加载的时候触发
            //audio.addEventListener("loadstart", function () {
            //         var audioNew = new Audio();                //重新创建一个新的audio对象，为了下面获取长度的时候避免每次都获取同一个audio的长度
            //         audioNew.src = voicePath;　　　　　　　　　　 //重新设置新的audio对象的音频url　　
            //         audioNew.preload = "metadata";               //设置新的audio对象加载音频元数据
            //         audioNew.load();　　　　　　　　　　　　　　　　//新的audio对象开始加载
            //         //新的audio对象元数据加载成功之后的回调 audio.duration 获取音频的时长，需要音频元数据加载完成之后才会有，否则就是NaN
            //         audioNew.onloadedmetadata = function () {
            //             console.log("src=" + audioNew.currentSrc + "|||||||<><><><><><><><><>" + audioNew.duration);
            //             //这里获取到不同的消息对应的时长之后就可以将时长渲染到页面咯
            //         }
            //     });
        }
    </script>
</body>
</html>

