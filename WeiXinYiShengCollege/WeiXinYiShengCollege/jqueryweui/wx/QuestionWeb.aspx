<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuestionWeb.aspx.cs" Inherits="WeiXinYiShengCollege.WebSite.jqueryweui.wx.QuestionWeb" %>

<!DOCTYPE html>
<html>
<head>
    <title>问卷调查</title>
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
        <h1 class="demos-title">问卷调查</h1>
    </header>

    <div class="weui-cells__title"></div>
    <div class="weui-cells weui-cells_form">
        <div class="weui-cell">
            <div class="weui-cell__hd">
                <label class="weui-label">姓名</label></div>
            <div class="weui-cell__bd">
                <input class="weui-input" type="text" id="txtName" placeholder="请填写姓名">
            </div>
        </div>
        <div class="weui-cell weui-cell_select weui-cell_select-after">
            <div class="weui-cell__hd">
                <label for="" class="weui-label">性别</label>
            </div>
            <div class="weui-cell__bd">
                <select class="weui-select" id="selectSex" name="selectSex">
                    <option value="请选择">请选择</option>
                    <option value="男">男</option>
                    <option value="女">女</option>
                </select>
            </div>
        </div>
        <div class="weui-cell">
            <div class="weui-cell__hd">
                <label for="" class="weui-label">生日</label></div>
            <div class="weui-cell__bd">
                <input class="weui-input" type="date" value="" id="txtBirthday">
            </div>
        </div>
        <div class="weui-cell">
            <div class="weui-cell__hd">
                <label class="weui-label">职业</label></div>
            <div class="weui-cell__bd">
                <input class="weui-input" type="text" id="txtPressional" placeholder="请填写职业">
            </div>
        </div>
        <div class="weui-cell ">
            <div class="weui-cell__hd">
                <label class="weui-label">手机号</label>
            </div>
            <div class="weui-cell__bd">
                <input class="weui-input" type="tel" id="txtMobile" placeholder="请输入手机号">
            </div>

        </div>

        <div class="weui-cell weui-cell_select weui-cell_select-after">
            <div class="weui-cell__hd">
                <label for="" class="weui-label">症状</label>
            </div>
            <div class="weui-cell__bd">
                <select class="weui-select" name="selectSick1" id="selectSick1">
                    <option value="请选择">请选择</option>
                    <%foreach(Module.Models.SickMusicDic sick in listSickMusicDic){ %>

                        <option value="<%=sick.SickName %>"><%=sick.SickName %></option>

                    <%} %>
                </select>
            </div>
        </div>
        <%-- <div class="weui-cell weui-cell_select weui-cell_select-after">
        <div class="weui-cell__hd">
          <label for="" class="weui-label">第二种症状</label>
        </div>
        <div class="weui-cell__bd">
          <select class="weui-select" name="selectSick2" id="selectSick2">
            <option value="0">请选择</option>
            <option value="1">高血压</option>
            <option value="2">糖尿病</option>
            <option value="3">高血脂</option>
          </select>
        </div>
      </div>
      <div class="weui-cell weui-cell_select weui-cell_select-after">
        <div class="weui-cell__hd">
          <label for="" class="weui-label">第三种症状</label>
        </div>
        <div class="weui-cell__bd">
          <select class="weui-select" name="selectSick3" id="selectSick3">
            <option value="0">请选择</option>
            <option value="1">高血压</option>
            <option value="2">糖尿病</option>
            <option value="3">高血脂</option>
          </select>
        </div>
      </div>--%>
    </div>


    <div class="weui-cells__tips">描述:请完成问卷调查，并提交，然后可下载对应症状的曲目</div>

    <div class="weui-btn-area">
        <a class="weui-btn weui-btn_primary" href="javascript:" id="showTooltips">确定</a>
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
        $("#showTooltips").click(function () {
            var selectSex = $("#selectSex").val();
            var nickname = $("#txtName").val();
            var txtBirthday = $("#txtBirthday").val();
            var txtPressional = $("#txtPressional").val();
            var mobile = $("#txtMobile").val();
            var selectSick1 = $("#selectSick1").val();

            if (nickname == '') {
                $.toptip('请输入姓名');
                return false;
            }
            if (selectSex == '请选择') {
                $.toptip('请选择性别');
                return false;
            }
            if (txtBirthday == '') {
                $.toptip('请选择生日');
                return false;
            }
            if (!mobile || !/1[3|4|5|7|8]\d{9}/.test(mobile)) {
                $.toptip('请输入手机号');
                return false;
            }
            
            if (selectSick1 == '请选择') {
                $.toptip('请选择病症');
                return false;
            }
            $.ajax({
                type: "POST",
                url: "../../handlerui/PageHandler.ashx",
                data: {
                    Action: "SubmitQuestion"
                                                  , Name: nickname
                                                  , Sex: selectSex
                                                  , Birthday: txtBirthday
                                                  , Profession: txtPressional
                                                  , Mobile: mobile
                                                  , Sickness: selectSick1
                                                  , r: Math.random()
                },
                dataType: "json",
                async: true,
                success: function (result) {
                    if (result && typeof result == "object") {
                        if (result.code == 1) {

                            $.toast("提交成功", function () {
                                //console.log('close');
                                //操作成功弹窗消失后执行
                                document.location.href = "QuestionResult.aspx?mobile=" + mobile+"&birthday="+txtBirthday;
                            });
                            //console.log(result.m);
                        } else {
                            
                                $.toptip(result.m);
                            
                        }
                    }
                }
            });
        });
    </script>
</body>
</html>

