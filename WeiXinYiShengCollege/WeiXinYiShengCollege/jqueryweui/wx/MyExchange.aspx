<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyExchange.aspx.cs" Inherits="WeiXinYiShengCollege.WebSite.jqueryweui.wx.Exchange" %>

<!DOCTYPE html>
<html>
<head>
    <title>积分兑换</title>
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
        <h1 class="demos-title">积分兑换申请</h1>
    </header>

    <div class="weui-cells__title"></div>
    <div class="weui-cells weui-cells_form">
        <div class="weui-cell">
            <div class="weui-cell__hd">
                <label class="weui-label">总获得积分</label></div>
            <div class="weui-cell__bd">
                <label class="weui-label" ><%=totalScore/100 %></label>
            </div>
        </div>
        <div class="weui-cell">
            <div class="weui-cell__hd">
                <label class="weui-label">总剩余积分</label></div>
            <div class="weui-cell__bd">
                <label class="weui-label" ><%=totalLastScore/100 %></label>
            </div>
        </div>
        <div class="weui-cell ">
            <div class="weui-cell__hd">
                <label class="weui-label">可兑换积分</label>
            </div>
            <div class="weui-cell__bd">
                <label class="weui-label"><%=validScore/100 %></label>
            </div>

        </div>

        <div class="weui-cell">
            <div class="weui-cell__hd">
                <label class="weui-label">兑换</label></div>
            <div class="weui-cell__bd">
                <input class="weui-input" type="text" placeholder="填写要兑换的积分" id="txtScore">
            </div>
        </div>

    </div>


    <div class="weui-cells__tips">描述:请申请兑换，待2个工作日后积分自动扣除，请关注账户</div>


    <div class="weui-btn-area">
        <a class="weui-btn weui-btn_primary" href="javascript:" id="showTooltips">申请兑换</a>
    </div>

    <script src="../lib/jquery-2.1.4.js"></script>
    <script src="../lib/fastclick.js"></script>
    <script>
        $(function () {
            FastClick.attach(document.body);
        });
    </script>
    <script src="../js/jquery-weui.js"></script>
     <script src="../js/../../js/validate.js"></script>

    <script>
        var OpenId = "<%=OpenId%>";
        var validScore = "<%=validScore/100 %>";
    </script>
    <script>
        
        $("#showTooltips").click(function () {

            var scoreStr = $("#txtScore").val();
            //  /^(([0-9]+\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\.[0-9]+)|([0-9]*[1-9][0-9]*))$/　　//正浮点数 
            
            if (scoreStr=="") {
                $.toptip('请输入兑换积分');
                return false;
            }
            if (!IsFloat(scoreStr))
            {
                $.toptip('请输入正确格式的兑换积分');
                return false;
            }

            var score = parseFloat(scoreStr);
            if (score > parseFloat(validScore))
            {
                $.toptip('输入的积分不能大于可兑换积分');
                return false;
            }
            
            $.ajax({
                type: "POST",
                url: "../../handlerui/PageHandler.ashx",
                data: { Action: "Exchange", OpenId: OpenId, score: score,r: Math.random() },
                dataType: "json",
                async: true,
                success: function (result) {
                    if (result && typeof result == "object") {
                        if (result.code == 1) {

                            $.toast("操作成功", function () {
                                //console.log('close');
                                //操作成功弹窗消失后执行
                                document.location.href = "Profile.aspx?OpenId=" + OpenId;
                            });
                            
                        } else {
                            $.toptip('提交失败');
                        }
                    }
                }
            });

            //var tel = $('#tel').val();
            //var code = $('#code').val();
            //if (!tel || !/1[3|4|5|7|8]\d{9}/.test(tel)) $.toptip('请输入手机号');
            //else if (!code || !/\d{6}/.test(code)) $.toptip('请输入六位手机验证码');
            //else $.toptip('提交成功', 'success');
        });
    </script>
</body>
</html>

