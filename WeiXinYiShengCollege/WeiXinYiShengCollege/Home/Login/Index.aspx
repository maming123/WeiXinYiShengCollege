<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="DMedia.FetionActivity.WebSite.Utils.Login.home.Index" %>


<!DOCTYPE HTML>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <title>后台登录</title>
    <link href="css/default.css" media="screen" rel="stylesheet" type="text/css">
    <link href="css/register.css" media="screen" rel="stylesheet" type="text/css">

    <script src="/js/jquery-1.11.0.min.js" type="text/javascript"></script>

    <script src="js/common.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            var returnUrl = $.request.queryString("returnUrl");
            if (!(returnUrl == null)) {
                var descreturnUrl = decodeURIComponent(returnUrl);
                Login.returnUrl = descreturnUrl;
                $("#returnUrl").val(descreturnUrl);
            }

            });
             

       
    </script>

    <style type="text/css">
        .red
        {
            color: red;
        }

        .style1
        {
            width: 199px;
        }
        
    </style>
</head>
<body>

    <form id="form1" action="Index.aspx" runat="server">

        <div class="header">
            <div class="placeholder">
                <div class="main">
                    <%--<image src="images/logo.png"></image>--%>
                </div>
            </div>
        </div>
        <div id="center">
            <div class="register_content">
                <div class="register_content_top">
                </div>
                <div class="register_content_center">
                    <h3 class="second_title">管理员登录</h3>
                    <div class="kong5yuan_enterprise">
                    </div>
                    <table width="100%" align="center" border="0" cellpadding="0" cellspacing="0">
                        <tbody>
                            <tr>
                                <td class="style1"></td>
                                <td colspan="2">
                                    <h3 class="login_error"></h3>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="style1">用户名<span class="req">*</span>&nbsp;
                                </td>
                                <td>
                                    <input class="input_205 input_txt email" id="mobile" name="mobile" type="text"
                                        value="" size="11" />
                                </td>
                                <td class="status"></td>
                            </tr>
                            <tr>
                                <td align="right" class="style1">密码<span class="req">*</span>&nbsp;
                                </td>
                                <td>
                                    <input class="input_205 input_txt" size="20" id="password" name="password"
                                        type="password" value="" />
                                </td>
                                <td class="status"></td>
                            </tr>
                            <tr>
                                <td align="right" class="style1">验证码<span></span>*
                                </td>
                                <td>
                                    <input type="text" id="verifyCode"  style="width:80px;"  name="verifyCode" maxlength="4" class="input_205 input_txt email"><img id="verifyCodeImage"
                                        alt=""  src="CodePngComplex.ashx?scode=CodePngCheckCode"
                                        onclick="this.src='CodePngComplex.ashx?scode=CodePngCheckCode&r='+Math.random()"
                                        alt="" class="fl" /><a href="javascript:void(0);" onclick="javascript:document.getElementById('verifyCodeImage').src='CodePngComplex.ashx?scode=CodePngCheckCode&r='+Math.random();return false;"  id="changeVerifyCode" class="kbq">看不清，换一张</a>
                                    <span class="red" id="warningVerifyCode"></span>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td height="10" class="style1"></td>
                                <td>
                                    <span id="erorContent" class="red"></span>
                                </td>
                                <td></td>
                            </tr>
                            <tr class="act">
                                <td class="style1"></td>
                                <td colspan="2">
                                    <input class="new_button_submit" value="登录" type="submit" src="/images/user_signin.jpg">
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <div class="register_content_bottom">
                </div>
            </div>
        </div>
        <div class="hidden border5px new_popbox" id="question_wrap">
            <div class="question_content_top border_top5px new_popbox_head">
                <h3 class="title">
                    <span class="fl"><b></b></span><span class="fr tg_rss"></span>
                    <div class="clear">
                    </div>
                </h3>
            </div>
            <div class="question_content_center  new_popbox_body" style="padding: 15px 30px 0pt;">
                <div id="sending_info">
                </div>
                <div id="msg">
                </div>
            </div>
        </div>
        <div class="footer">
             <div style="color:#FFFFFF ; text-align:center"></div>
        </div>

        <input type="hidden" id="returnUrl" name="returnUrl" value="" />

    </form>
</body>
</html>
