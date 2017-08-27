<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuestionResult.aspx.cs" Inherits="WeiXinYiShengCollege.WebSite.jqueryweui.wx.QuestionResult" %>
<%@ Import Namespace="WeiXinYiShengCollege.Business.Common.Models" %>
<!DOCTYPE html>
<html>
  <head>
    <title>曲目列表</title>
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
      <h1 class="demos-title">请您试听</h1>
    </header>
    <article class="weui-article">
      
      <section>
        <%foreach(SickMusicItem music in listMusic){ %>
        <section>
          <p>
    			<a href="http://wx.yishengcollege.com/music/<%=music.MusicFileName %>"><%=music.MusicName %></a>
          </p>
        </section>
          <%} %>
      </section>
    </article>
	<div class="weui-cells__tips">请您收藏好此页面，如需要下载请复制此链接，用PC的浏览器打开并下载此页面的曲目</div>
    <script src="../lib/jquery-2.1.4.js"></script>
<script src="../lib/fastclick.js"></script>
<script>
  $(function() {
    FastClick.attach(document.body);
  });
</script>
<script src="../js/jquery-weui.js"></script>

  </body>
</html>

