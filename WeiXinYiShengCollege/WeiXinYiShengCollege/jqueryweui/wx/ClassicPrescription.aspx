<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClassicPrescription.aspx.cs" Inherits="WeiXinYiShengCollege.WebSite.jqueryweui.wx.ClassicPrescription" %>


<!DOCTYPE html>
<html>
  <head>
    <title>经典方剂</title>
    <meta charset="utf-8">
<meta http-equiv="X-UA-Compatible" content="IE=edge">
<meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no">

<meta name="description" content="">

<link rel="stylesheet" href="../lib/weui.min.css">
<link rel="stylesheet" href="../css/jquery-weui.css">
<link rel="stylesheet" href="css/demos.css">

  </head>

  <body ontouchstart>

    <%--<header class='demos-header'>
      <h1 class="demos-title">Article</h1>
    </header>--%>
    <article class="weui-article">
      <h1><%=sysPoint.Title %></h1>
      <section>
        <%--<h2 class="title">章标题</h2>--%>
        <section>
          <%--<h3>1.1 节标题</h3>--%>
          <p>
            <%=sysPoint.Content %>
          </p>
        </section>
      </section>
    </article>
    <%--<div class="button_sp_area">
        <!--weui-btn_disabled-->
        <a href="javascript:;" id="zan" class="weui-btn weui-btn_mini weui-btn_primary">赞</a>
    </div>--%>
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

