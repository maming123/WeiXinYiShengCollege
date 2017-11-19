<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProfileMusicList.aspx.cs" Inherits="WeiXinYiShengCollege.WebSite.jqueryweui.wx.ProfileMusicList" %>

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
    <style type="text/css">
		html {
			-webkit-tap-highlight-color: rgba(0, 0, 0, 0);
		}

		.db {
			display: block;
		}

		.weixinAudio {
			line-height: 1.5;
		}

		.audio_area {
			display: inline-block;
			width: 100%;
			vertical-align: top;
			margin: 0px 1px 0px 0;
			font-size: 0;
			position: relative;
			font-weight: 400;
			text-decoration: none;
			-ms-text-size-adjust: none;
			-webkit-text-size-adjust: none;
			text-size-adjust: none;
		}

		.audio_wrp {
			border: 1px solid #ebebeb;
			background-color: #fcfcfc;
			overflow: hidden;
			padding: 12px 20px 12px 12px;
		}

		.audio_play_area {
			float: left;
			margin: 9px 22px 10px 5px;
			font-size: 0;
			width: 18px;
			height: 25px;
		}

		.playing .audio_play_area .icon_audio_default {
			display: block;
		}

		.audio_play_area .icon_audio_default {
			background: transparent url(images/iconloop.png) no-repeat 0 0;
			width: 18px;
			height: 25px;
			vertical-align: middle;
			display: inline-block;
			-webkit-background-size: 54px 25px;
			background-size: 54px 25px;
			background-position: -36px center;
		}

		.audio_play_area .icon_audio_playing {
			background: transparent url(images/iconloop.png) no-repeat 0 0;
			width: 18px;
			height: 25px;
			vertical-align: middle;
			display: inline-block;
			-webkit-background-size: 54px 25px;
			background-size: 54px 25px;
			-webkit-animation: audio_playing 1s infinite;
			background-position: 0px center;
			display: none;
		}

		.audio_area .pic_audio_default {
			display: none;
			width: 18px;
		}

		.tips_global {
			color: #8c8c8c;
		}

		.audio_area .audio_length {
			float: right;
			font-size: 14px;
			margin-top: 3px;
			margin-left: 1em;
		}

		.audio_info_area {
			overflow: hidden;
		}

		.audio_area .audio_title {
			font-weight: 400;
			font-size: 17px;
			margin-top: -2px;
			margin-bottom: -3px;
			width: auto;
			overflow: hidden;
			text-overflow: ellipsis;
			white-space: nowrap;
			word-wrap: normal;
		}

		.audio_area .audio_source {
			font-size: 14px;
		}

		.audio_area .progress_bar {
			position: absolute;
			left: 0;
			bottom: 0;
			background-color: #0cbb08;
			height: 2px;
		}

		.playing .audio_play_area .icon_audio_default {
			display: none;
		}

		.playing .audio_play_area .icon_audio_playing {
			display: inline-block;
		}

		@-webkit-keyframes audio_playing {
			30% {
				background-position: 0px center;
			}
			31% {
				background-position: -18px center;
			}
			61% {
				background-position: -18px center;
			}
			61.5% {
				background-position: -36px center;
			}
			100% {
				background-position: -36px center;
			}
		}
	</style>
</head>

<body ontouchstart>

    <header class='demos-header'>
        <h1 class="demos-title">五音疗愈曲目</h1>
    </header>
    <style>
      .weui-row {
        margin-top: 10px;
      }
      [class*="weui-col-"] {
        border: 0px solid #ccc;
        height: 40px;
        line-height: 40px;
        text-align: center;
      }

    </style>
    <%int i = 1; %>
    <%foreach (SickMusicItem music in listMusic)
              { %>
    <p class="weixinAudio">
                            <audio name="media" id="audio<%=i %>" width="1" height="1" preload = "metadata">
                                <source src="http://wx.yishengcollege.com/music/<%=music.MusicFileName %>" type="audio/mpeg" />
                                <%--<source src="/js/webchataudio/src/sound/8586.wav" type="audio/mpeg" /> --%>
                                您浏览器不支持audio标签.
                            </audio>
                            <span id="audio_area" class="db audio_area">
			<span class="audio_wrp db">
			<span class="audio_play_area" id="audio_play_area">
				<i class="icon_audio_default"></i>
				<i class="icon_audio_playing"></i>

            </span>
                            <span id="audio_length" class="audio_length tips_global"></span>
                            <span class="db audio_info_area">
                <strong class="db audio_title"><%=music.MusicName %>&nbsp;&nbsp;</strong>
                <span class="audio_source tips_global">易生学苑大健康</span>
                            </span>
                <span style="position: absolute;left: 265px;bottom: 15px;"><img src="images/7.png" style="width:48px;height:48px;" id="aplay<%=i %>" onclick="playMusic(<%=i %>);return false;"/></span>
                            <span id="audio_progress" class="progress_bar" style="width: 0%;"></span>
                            </span>
                            </span>
                        </p><br />
    
    
    <%i++; 
              } %>
   <%--<div class="weui-row weui-no-gutter">
      <div class="weui-col-20"></div>
      <div class="weui-col-60"><a href="javascript:void(0);"  class="weui-btn weui-btn_mini weui-btn_primary" id="aplay">循环播放所有曲目</a></div>
      <div class="weui-col-20"></div>
    </div>--%>
    
    <div class="weui-cells__tips">
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
    <script src="http://libs.baidu.com/jquery/2.0.0/jquery.min.js"></script>
    <script src="/js/webchataudio/src/js/weixinAudio.js"></script>
    <script>
        var options = { autoplay: false };
        $('.weixinAudio').weixinAudio(options);
    </script>
    <script>
        $(function () {
            FastClick.attach(document.body);
        });

    </script>
    <script src="../js/jquery-weui.js"></script>
    <script>
        //$(function () {
        //    $("#aplay").click(function () {

        //        var audio1 = $("#audio1")[0];
        //        var audio2 = $("#audio2")[0];
        //        var audio3 = $("#audio3")[0];
        //        var $audio_areaplaying1 = $(audio1).parent().find('#audio_area');
        //        $audio_areaplaying1.removeClass('playing');
        //        var $audio_areaplaying2 = $(audio2).parent().find('#audio_area');
        //        $audio_areaplaying2.removeClass('playing');
        //        var $audio_areaplaying3 = $(audio3).parent().find('#audio_area');
        //        $audio_areaplaying3.removeClass('playing');
        //        audio1.pause();
        //        audio2.pause();
        //        audio3.pause();
        //        audio1.loop = "";
        //        audio2.loop = "";
        //        audio3.loop = "";
        //        //audio1.preload="metadata"; 
        //        //audio1.play();
        //        var $audio_area1 = $(audio1).parent().find('#audio_play_area');
        //        $audio_area1.click();
        //        audio1.addEventListener('ended', function () {
        //            // Wait 500 milliseconds before next loop  
        //            var $audio_area2 = $(audio2).parent().find('#audio_play_area');
        //            $audio_area2.click();
        //            audio2.preload = "metadata";
        //            audio2.play();
        //            audio2.addEventListener('ended', function () {
        //                // Wait 500 milliseconds before next loop  
        //                var $audio_area3 = $(audio3).parent().find('#audio_play_area');
        //                $audio_area3.click();
        //                //audio3.preload = "metadata";
        //                //audio3.play();
        //                audio3.addEventListener('ended', function () {
        //                    // Wait 500 milliseconds before next loop  
        //                    //audio1.preload = "metadata";
        //                    //audio1.play();
        //                    var $audio_area1second = $(audio1).parent().find('#audio_play_area');
        //                    $audio_area1second.click();
        //                }, false);
        //            }, false);
        //        }, false);
        //    });
        //});

        function playMusic(musicnum) {
            
            var audio = $("#audio" + musicnum);
            audio[0].loop = "loop";
            $("audio").each(function() {
                if($(this)[0].id != audio.id)
                {
                    //console.log($(this)[0]);
                    //正在播放的要暂停
                     if ($(this)[0].paused == false)
                    {
                        
                        $(this)[0].pause();
                        var $audio_area = $(this).parent().find('#audio_area');
                        $audio_area.removeClass('playing');
                    }
                }
            });
            
            var $audio_area = audio.parent().find('#audio_play_area');
            $audio_area.click();
           
            //if(audio[0].paused==true)
            //{
            //    $("#aplay" + musicnum).html("开始循环播放")
            //    $audio_area.click();

            //}else
            //{
            //    $("#aplay" + musicnum).html("暂停循环播放")
            //}
            //$audio_area.addClass('playing');
            //audio[0].preload = "metadata";
            //audio[0].loop = "loop";
            //audio[0].play();
            
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

