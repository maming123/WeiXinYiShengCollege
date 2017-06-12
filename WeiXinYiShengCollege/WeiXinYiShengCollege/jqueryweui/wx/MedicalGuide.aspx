<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MedicalGuide.aspx.cs" Inherits="WeiXinYiShengCollege.WebSite.jqueryweui.wx.MedicalGuide" %>


<!DOCTYPE html>
<html>
  <head>
    <title>临证参考</title>
    <meta charset="utf-8">
<meta http-equiv="X-UA-Compatible" content="IE=edge">
<meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no">

<meta name="description" content="">

<link rel="stylesheet" href="../lib/weui.min.css">
<link rel="stylesheet" href="../css/jquery-weui.css">
<link rel="stylesheet" href="css/demos.css">

  </head>

  <body ontouchstart>

    <article class="weui-article">
      <h1><%=sysPoint.Title %></h1>
      <section>
        <section>
          <%--<h2>病症：</h2>
          <p>
            <%=medicine.病症 %>
          </p>
          <h2>辩证：</h2>
          <p>
            <%=medicine.辩证 %>
          </p>--%>
          <h2>症候：</h2>
          <p>
            <%=medicine.症候 %>
          </p>
          <h2>内治法</h2>
          <h3>中成药：</h3>
          <p>
            <%=medicine.内治法.中成药 %>
          </p>
          <h3>经验方：</h3>
          <p>
            <%=medicine.内治法.经验方 %>
          </p>
          <h2>外治法</h2>
          <h3>穴位：</h3>
          <p>
            <%=medicine.外治法.穴位 %>
          </p>
          <h3>脊柱：</h3>
          <p>
            <%=medicine.外治法.脊柱 %>
          </p>
          <h3>耳穴：</h3>
          <p>
            <%=medicine.外治法.耳穴 %>
          </p>
          <h3>食疗：</h3>
          <p>
            <%=medicine.外治法.食疗 %>
          </p>
          <h3>运动：</h3>
          <p>
            <%=medicine.外治法.运动 %>
          </p>
          <h2>禁忌：</h2>
          <p>
             <%=medicine.禁忌 %>
          </p>
        </section>
        
      </section>
    </article>
	<%--<div class="button_sp_area">
        <a href="javascript:;" class="weui-btn weui-btn_mini weui-btn_primary">赞</a>
        <a href="javascript:;" class="weui-btn weui-btn_mini weui-btn_default weui-btn_disabled">按钮</a>
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

