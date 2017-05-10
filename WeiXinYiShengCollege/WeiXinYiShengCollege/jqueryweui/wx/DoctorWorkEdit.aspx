<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DoctorWorkEdit.aspx.cs" Inherits="WeiXinYiShengCollege.WebSite.jqueryweui.wx.DoctorWorkEdit" %>

<!DOCTYPE html>
<html>
  <head>
    <title>修改出诊信息</title>
    <meta charset="utf-8">
<meta http-equiv="X-UA-Compatible" content="IE=edge">
<meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no">

<meta name="description" content="Write an awesome description for your new site here. You can edit this line in _config.yml. It will appear in your document head meta (for Google search results) and in your feed.xml site description.
">

<link rel="stylesheet" href="../lib/weui.min.css">
<link rel="stylesheet" href="../css/jquery-weui.css">
<link rel="stylesheet" href="css/demos.css">

  </head>

  <body ontouchstart>


    <header class='demos-header'>
      <h1 class="demos-title">修改出诊信息</h1>
    </header>
 
    <div class="weui-cells__title"></div>
    <div class="weui-cells weui-cells_form">
      <div class="weui-cell">
        <div class="weui-cell__hd"><label for="" class="weui-label">出诊日期</label></div>
        <div class="weui-cell__bd">
          <input class="weui-input" type="date" value="<%=dws.WorkDateTime.ToString("yyyy-MM-dd") %>" id="ddlDate">
        </div>
      </div>
      <div class="weui-cell weui-cell_select weui-cell_select-after">
        <div class="weui-cell__hd">
          <label for="" class="weui-label">出诊时段</label>
        </div>
        <div class="weui-cell__bd">
          <select class="weui-select" name="select2" id="ddlDayTime">
            <option value="9" <%=dws.DayTime ==9?"selected":"" %>>上午</option>
            <option value="15" <%=dws.DayTime ==15?"selected":"" %>>下午</option>
          </select>
        </div>
      </div>
      <div class="weui-cell weui-cell_select weui-cell_select-after">
        <div class="weui-cell__hd">
          <label for="" class="weui-label">选择医生</label>
        </div>
        <div class="weui-cell__bd">
          <select class="weui-select" name="select3" id="ddlDoctor">
            <option value="0">选择医生</option>
              <%foreach(Module.Models.DoctorInfo d in listD){ %>
                <option value="<%=d.Id %>" <%=dws.DoctorId ==d.Id?"selected":"" %>><%=d.DoctorName %></option>
              <%} %>
          </select>
        </div>
      </div>
     </div>
     
    <div class="weui-cells__tips">请按要求修改</div>

    <div class="weui-btn-area">
      <a class="weui-btn weui-btn_primary" href="javascript:" id="showTooltips">修改</a>
    </div>
      <div class="weui-btn-area">
      <a class="weui-btn weui-btn_primary" href="javascript:" id="btnDelete">删除</a>
    </div>
    <script src="../lib/jquery-2.1.4.js"></script>
<script src="../lib/fastclick.js"></script>
<script>
  $(function() {
    FastClick.attach(document.body);
  });
</script>
<script src="../js/jquery-weui.js"></script>


     <script>
         var OpenId = "<%=OpenId%>";
         var Id = "<%=dws.Id%>";
    </script>
    <script>
        $("#showTooltips").click(function () {
            var ddlDate = $("#ddlDate").val();
            var ddlDayTime = $("#ddlDayTime").val();
            var ddlDoctor = $("#ddlDoctor").val();
            var ddlDoctorName = $("#ddlDoctor").find("option:selected").text();
            if (ddlDate=='') {
                $.toptip('请选择日期');
                return false;
            }
            if (ddlDayTime == '') {
                $.toptip('请选择时间段');
                return false;
            }
            if (ddlDoctor == '0') {
                $.toptip('请选择医生');
                return false;
            }
            $.ajax({
                type: "POST",
                url: "../../handlerui/PageHandler.ashx",
                data: { Action: "UpdateDoctorWorkSchedule",Id:Id, OpenId: OpenId, ddlDate: ddlDate, ddlDayTime: ddlDayTime, ddlDoctorId: ddlDoctor, ddlDoctorName: ddlDoctorName, r: Math.random() },
                dataType: "json",
                async: true,
                success: function (result) {
                    if (result && typeof result == "object") {
                        if (result.code == 1) {

                            $.toast("添加成功", function () {
                                //console.log('close');
                                //操作成功弹窗消失后执行
                                document.location.href = "DoctorWorkManage.aspx?OpenId=" + OpenId;
                            });
                            //console.log(result.m);
                        } else {
                            $.toptip('提交失败');
                        }
                    }
                }
            });
        });

        $("#btnDelete").click(function () {
            
            $.ajax({
                type: "POST",
                url: "../../handlerui/PageHandler.ashx",
                data: { Action: "DeleteDoctorWorkSchedule", Id: Id, OpenId: OpenId, r: Math.random() },
                dataType: "json",
                async: true,
                success: function (result) {
                    if (result && typeof result == "object") {
                        if (result.code == 1) {

                            $.toast("删除成功", function () {
                                //console.log('close');
                                //操作成功弹窗消失后执行
                                document.location.href = "DoctorWorkManage.aspx?OpenId=" + OpenId;
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

