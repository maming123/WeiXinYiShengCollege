//common js
$.ajaxSetup({ timeout: 1000 * 20 });

var LayerIndex = 0; //当前显示的层索引

//关闭层
var CloseLayer = function () {
    if (LayerIndex > 0) {
        layer.close(LayerIndex);
        LayerIndex--;
    }
}

var ShowTips = function (cssSelecter, width, height) {
    setTimeout(function () {
        CloseLayer();
        PageLayer(cssSelecter, width, height);
    }, 0)
}

//iframe弹层,width, height不加px
var IframLayer = function (url, width, height) {
    var i = $.layer({
        type: 2,
        bgcolor: 'transparent',
        shade: [0.5, '#000', true],
        title: ['', false],
        closeBtn: [0, false],
        shadeClose: false,
        offset: [($(window).height() - height) / 2, '50%'],
        area: [width + 'px', height + 'px'],
        border: [0, 0, '', false],
        iframe: { src: url },
        success: function () {

        }
    });
    LayerIndex = i;
    return i;
}

//iframe弹层,width, height不加px
var IframLayerV2 = function (url, width, height) {
    var i = $.layer({
        type: 2,
        bgcolor: 'transparent',
        shade: [0.5, 'transparent', true],
        title: ['', false],
        closeBtn: [0, false],
        shadeClose: false,
        offset: [($(window).height() - height) / 2, '50%'],
        area: [width + 'px', height + 'px'],
        border: [0, 0, '', false],
        iframe: { src: url },
        success: function () {

        }
    });
    LayerIndex = i;
    return i;
}

//页面弹层cssSelecter:例#id,等,width, height不加px
var PageLayer = function (cssSelecter, width, height) {
    var i = $.layer({
        type: 1,
        bgcolor: 'transparent',
        shade: [0.5, '#000', true],
        title: ['', false],
        closeBtn: [0, false],
        shadeClose: false,
        offset: [($(window).height() - height) / 2, '50%'],
        area: [width, height],
        border: [0, 0, '', false],
        page: {
            dom: cssSelecter
        },
        success: function (layerDom) {

        }
    });
    LayerIndex = i;
    return i;
}

//传入loading层显示时间，time：不传或传0为不限制,shade：透明度不传或传0为不显示遮罩,最大0.8
var LoadingLayer = function (time, shade) {
    if (shade === "" || shade == null || shade > 0.8)
        shade = 0;
    var i = $.layer({
        type: 3,
        time: time,
        shade: [shade, '#000', true],
        loading: { type: 0 }//,
        //offset: [($(window).height() - 60) / 2, ($(window).width() - 200) / 2]
    });
    LayerIndex = i;
    return i;
}

//cssSelecter:例#id,time:提示显示时间,type:指引方向（0：上下,1：左右）
var Tips = function (cssSelecter, msg, time, type, maxWidth) {
    $.layer({
        type: 4,
        time: time,
        shade: [0, '', false],
        tips: {
            msg: msg,
            follow: cssSelecter,
            guide: type,
            style: ['background-color:#FD6A42; color:#fff;width:' + maxWidth + 'px;', '#FD6A42']
        }
    });
}

//修复IE下setTimeout不能传参数的bug
if (!!(document.all && navigator.userAgent.indexOf('Opera') === -1)) {
    (function (f) {
        window.setTimeout = f(window.setTimeout);
        window.setInterval = f(window.setInterval);
    })(function (f) {
        return function (c, t) {
            var a = [].slice.call(arguments, 2);
            return f(function () {
                try {
                    c.apply(this, a)
                } catch (e)
                { }
            }, t)
        }
    });
}

//字符串填充格式化
String.Format = function () {
    if (arguments.length == 0) {
        return null;
    }
    var str = arguments[0];
    for (var i = 1; i < arguments.length; i++) {

        var re = new RegExp('\\{' + (i - 1) + '\\}', 'gm');
        str = str.replace(re, arguments[i]);
    }
    return str;
}

var Logins = {
    // 登录
    showLogin: function (load, script) {
        CloseLayer();
        if (!load) {
            load = 0;
        }
        var url = "EasyLogin.aspx?&load=" + load;
        if (script && script != "") {
            url += "&nextAction=" + script;
        }
        IframLayer(url, '521', '496');
    }
}


//begin login js
var load = 0;
function Close() {
    window.parent.CloseLayer();
}
//显示登陆错误信息弹窗
function ShowLoginErrorTip(msg, pClass, timeOut) {
    $("#erorContent").html('');
    if (msg != "") {
        $("#erorContent").html(msg);
    }
}
var Login = {
    mobilePattern: new RegExp("^(134|135|136|137|138|139|147|150|151|152|157|158|159|182|183|184|187|188|178)\\d{8}$"),
    fetionId: new RegExp("^-?\\d+$"),
    fetionIdPattern: /^\d{9}$/,
    loginType: 0,
    from: "miaosha16",
    mobileInputTips: "请输入移动手机号",
    mobileInputError: "请输入正确的移动手机号",
    passwordInputError: "请输入密码",
    passwordError: "账号或密码错误",
    tempMobileInputError: "请输入正确的移动手机号",
    tempPasswordSuccess: "临时短信密码已发送到您的手机，5分钟内有效,请接收后输入到“密码”输入框！",
    tempPasswordUpperLimit: "（当天下行次数已达上限！）",
    tempPasswordExist: "您刚刚获取过临时密码，5分钟内有效，无需多次获取！",
    systemError: "（系统忙，请稍后再试！）",
    verifyCodeInputError: "请输入验证码<span  style='color:Red;'>红色</span>内容",
    verifyCodeError: "验证码错误！",
    handle: 0,
    returnUrl: "/home/login/index.aspx",
    /*获取地址栏信息*/
    getQueryString: function (name) {
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
        var r = window.location.search.substr(1).match(reg);
        if (r != null) return unescape(r[2]); return null;
    },

    /*是否是手机号*/
    isMobile: function (mobile) {
        //return this.mobilePattern.test(mobile);
        return true;
    },

    /*是否是飞信号*/
    isFetionId: function (fetionId) {
        return this.fetionId.test(fetionId);
    },

    /*是否是密码*/
    isPassword: function (password) {
        return password && password.length > 0;
    },

    /*是否是验证码*/
    isVerifyCode: function (verifyCode) {
        return verifyCode && verifyCode.length > 0;
    },

    /*手机号错提示*/
    setWarningMobile: function (msg) {
        //$("#warningMobile").text(msg);
        ShowLoginErrorTip(msg);
    },

    /*密码错提示*/
    setWarningPassword: function (msg) {
        //$("#warningPassword").html(msg);
        ShowLoginErrorTip(msg);
    },

    /*验证码错提示*/
    setWarningVerifyCode: function (msg) {
        //$("#warningVerifyCode").html(msg);
        //ShowLoginErrorTip(msg, 'oneline');
    },

    /*输入手机号事件处理*/
    initMobileEvent: function () {
        $("#mobile").focus(function () {
            if ($("#mobile").val() == Login.mobileInputTips) {
                $("#mobile").val("");
            }
        }).focusout(function () {
            //if (Login.isMobile($("#mobile").val())) {
            //    Login.setWarningMobile("");
            //} else {
            //    Login.setWarningMobile(Login.mobileInputError);
            //    if ($("#mobile").val() == "") {
            //        $("#mobile").val(Login.mobileInputTips);
            //    }
            //}
        }).keydown(function (e) {
            if (!e) e = window.event;
            if ((e.keyCode || e.which) == 13) {
                if (Login.isMobile($("#mobile").val())) {
                    //Login.setWarningMobile(Login.mobileInputError);
                    $("#password").focus();
                } else {
                   // Login.setWarningMobile(Login.mobileInputError);
                }
            }
        });
    },

    /*输入密码事件处理*/
    initPasswordEvent: function () {
        $("#password").focusout(function () {
            if (Login.isPassword($("#password").val())) {
                Login.setWarningPassword("");
            } else {
                Login.setWarningPassword(Login.passwordInputError);
            }
        }).keydown(function (e) {
            if (!e) e = window.event;
            if ((e.keyCode || e.which) == 13) {
                if (Login.isPassword($("#password").val())) {
                    Login.setWarningPassword("");
                    //$("#verifyCode").focus();
                } else {
                    Login.setWarningPassword(Login.passwordInputError);
                }
            }
        });
    },

    /*显示临时密码提示*/
    showTempPasswordMessage:
    function (msg) {
        //alert(msg);
        ShowLoginErrorTip(msg);
    },

     
    /*发送临时密码*/
        sendTempPassword: function () {

            if (!Login.isMobile($("#mobile").val())) {
                //Login.setWarningMobile(Login.tempMobileInputError);
                return;
            }

            var mobile = $("#mobile").val();
            var verify = $("#verifyCode").val();
            $.ajax({
                type: "GET",
                url: "/Home/Login/EasyLogin.ashx?key=SendTempPass&Mobile=" + escape(mobile) + "&From=" + this.from + "&verify=" + verify + "&r=" + Math.random(),
                success: function (data) {
                    if (data == 1) {
                        Login.showTempPasswordMessage(Login.tempPasswordSuccess);
                        //Login.showTempPasswordMessage('temppassTips',1);                    
                    } else if (data == -1) {
                        Login.showTempPasswordMessage(Login.tempPasswordUpperLimit);
                        //Login.showTempPasswordMessage('temppassTips',-1);
                    } else if (data == -2) {
                        Login.showTempPasswordMessage(Login.tempPasswordExist);
                        //Login.showTempPasswordMessage('temppassTips',-2);
                    } else if (data == -3) {
                        Login.setWarningVerifyCode(Login.verifyCodeError);
                    } else {
                        Login.showTempPasswordMessage(Login.systemError);
                    }
                },
                error: function () { }
            });

            return false;
        },

    /*初始化登录框*/
    init: function () {

        //初始化手机号输入逻辑
        this.initMobileEvent();

        //初始化密码逻辑
        this.initPasswordEvent();

        $("#erorContent").html('');

        $("#btnGetTempPassword").click(function () {
            Login.sendTempPassword(); return false;
        });

    }
}

$(document).ready(function () {
    Login.init();
});

//end login js

function CheckLogin() { return Login.hasLogin(); }

function loginFramLayer() { document.location.href = "EasyLogin.aspx"; }

function CutString(str, len) {
    var increaseLength = 0;
    str_cut = new String();
    if (str == "" || str == null)
        return str;
    for (var i = 0; i < str.length; i++) {
        var a = str.charAt(i);
        increaseLength++;
        if (escape(a).length > 4) {
            increaseLength++;
            //中文字符的长度经编码之后大于4
        }
        if (increaseLength == len) {
            str_cut = str_cut.concat(a);
            if (str.length > str_cut.length) {
                str_cut = str_cut.concat("...");
            }
            return str_cut;
        }
        else if (increaseLength < len) {
            str_cut = str_cut.concat(a);
        }
        else {
            str_cut = str_cut.concat("...");
            return str_cut;
        }
    }
    return str_cut;
}

function CutStringNoEllipsis(str, len) {
    var increaseLength = 0;
    str_cut = new String();
    if (str == "" || str == null)
        return str;
    for (var i = 0; i < str.length; i++) {
        var a = str.charAt(i);
        increaseLength++;
        if (escape(a).length > 4) {
            increaseLength++;
            //中文字符的长度经编码之后大于4
        }

        if (increaseLength == len) {
            str_cut = str_cut.concat(a);
            return str_cut;
        }
        else if (increaseLength < len) {
            str_cut = str_cut.concat(a);
        }
        else {
            return str_cut;
        }
    }
    return str_cut;
}

function HTMLEncode(html) {
    var temp = document.createElement("div");
    (temp.textContent != null) ? (temp.textContent = html) : (temp.innerText = html);
    var output = temp.innerHTML;
    temp = null;
    return output;
}

function HTMLDecode(text) {
    var temp = document.createElement("div");
    temp.innerHTML = text;
    var output = temp.innerText || temp.textContent;
    temp = null;
    return output;
}


var stringFormat = function () {
    if (arguments.length == 0) {
        return null
    }
    var d = arguments[0];
    for (var a = 1, b = arguments.length; a < b; a++) {
        var c = new RegExp("\\{" + (a - 1) + "\\}", "gm");
        d = d.replace(c, arguments[a])
    }
    return d
};

jQuery.extend({
    request: {
        queryString: function (val) {
            var uri = window.location.search;
            var re = new RegExp("" + val + "\=([^\&\?]*)", "ig");
            return ((uri.match(re)) ? (uri.match(re)[0].substr(val.length + 1)) : null);
        },
        queryStrings: function (uri) {
            uri = uri || window.location.search;
            var re = /\w*\=([^\&\?]*)/ig;
            var retval = [];
            while ((arr = re.exec(uri)) != null)
                retval.push(arr[0]);
            return retval;
        },
        setQuery: function (a, val1, val2) {
            var a = this.queryStrings(a);
            var retval = "";
            var seted = false;
            var re = new RegExp("^" + val1 + "\=([^\&\?]*)$", "ig");
            for (var i = 0; i < a.length; i++) {
                if (re.test(a[i])) {

                    seted = true;
                    a[i] = val1 + "=" + val2;
                }
            }
            retval = a.join("&");
            return "?" + retval + (seted ? "" : (retval ? "&" : "") + val1 + "=" + val2);
        }
    }
});