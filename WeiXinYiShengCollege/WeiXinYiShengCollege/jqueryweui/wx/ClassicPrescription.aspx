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
        <section>
          <%--<h2>病症：</h2>
          <p>
            <%=medicine.病症 %>
          </p>
          <h2>辩证：</h2>
          <p>
            <%=medicine.辩证 %>
          </p>--%>
           
          <h2>【来源】</h2>
          <p>
            <%=cp.来源 %>
          </p>
          <h2>【组成】</h2>
          
          <p>
            <%=cp.组成 %>
          </p>
          <h2>【功效】</h2>
          <p>
            <%=cp.功效 %>
          </p>
          <h2>【主治】</h2>
          <p>
            <%=cp.主治 %>
          </p>
          <h2>【用法】</h2>
          <p>
            <%=cp.用法 %>
          </p>
          <h2>【其他】</h2>
          <p>
            <%=cp.其他 %>
          </p>
        </section>
        
      </section>
    </article>
	<div class="button_sp_area">
        <%if (sysPoint.Id>0)
          { %>
        <%if(IsHaveZan){%>
            <a href="javascript:;" class="weui-btn weui-btn_mini weui-btn_primary  weui-btn_disabled">已赞</a>
            <%}else{ %>
            <a href="javascript:;" id="azan" class="weui-btn weui-btn_mini weui-btn_primary">赞</a>
            <%}
               %>

        <%if(IsCollect){%>
            <a href="javascript:;" class="weui-btn weui-btn_mini weui-btn_primary  weui-btn_disabled">已收藏</a>
            <%}else{ %>
            <a href="javascript:;" id="acollect" class="weui-btn weui-btn_mini weui-btn_primary">收藏</a>
            <%}
               %>

        <%} %>
        赞(<%=sysPoint.ZanCount %>)浏览(<%=sysPoint.SeeCount %>)收藏(<%=sysPoint.CollectCount %>)
       <%-- <a href="javascript:;" class="weui-btn weui-btn_mini weui-btn_default weui-btn_disabled">按钮</a>--%>
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
          var pointid = "<%=sysPoint.Id %>";
      </script>
       <script>
           $("#azan").click(function () {

               $.ajax({
                   type: "POST",
                   url: "../../handlerui/PageHandler.ashx",
                   data: { Action: "InsertUserOpLogZan", pointid: pointid, r: Math.random() },
                   dataType: "json",
                   async: true,
                   success: function (result) {
                       if (result && typeof result == "object") {
                           if (result.code == 1) {

                               $.toast("已赞", function () {
                                   //console.log('close');
                                   //操作成功弹窗消失后执行
                                   //document.location.href = "Profile.aspx?OpenId=" + OpenId;
                                   $("#azan").addClass("weui-btn_disabled").html("已赞");
                                   $("#azan").unbind("click");
                               });
                               //console.log(result.m);
                           } else {
                               $.toptip('提交失败');
                           }
                       }
                   }
               });
           });
           $("#acollect").click(function () {

               $.ajax({
                   type: "POST",
                   url: "../../handlerui/PageHandler.ashx",
                   data: { Action: "InsertMyCollectMedicine", pointid: pointid, r: Math.random() },
                   dataType: "json",
                   async: true,
                   success: function (result) {
                       if (result && typeof result == "object") {
                           if (result.code == 1) {

                               $.toast("已收藏", function () {
                                   //console.log('close');
                                   //操作成功弹窗消失后执行
                                   //document.location.href = "Profile.aspx?OpenId=" + OpenId;
                                   $("#acollect").addClass("weui-btn_disabled").html("已收藏");
                                   $("#acollect").unbind("click");
                               });
                               //console.log(result.m);
                           } else {
                               $.toptip('提交失败');
                           }
                       }
                   }
               });
           });
    </script>
  </body>
</html>

