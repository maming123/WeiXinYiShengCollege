var LayerIndex = 0; //当前显示的层索引

//关闭层
var CloseLayer = function () {
    if (LayerIndex > 0) {
        layer.close(LayerIndex);
        LayerIndex--;
    }
}

//提示框有一个确认按钮
var AlertMsg = function (msg, fun, IsTrueOrFalsecloseBtn)
{
    CloseLayer();
    //layer.alert(msg, 11, "提示");
    if (!IsTrueOrFalsecloseBtn)
    {
        IsTrueOrFalsecloseBtn = false;
    }
    var i= $.layer({
        shade: [0.1, '#000', true],
        area: ['auto', 'auto'],
        dialog: {
            msg: msg,
            btns: 1,
            type: 11,
            btn: ['确定'],
            yes: function () {
                if (typeof (fun) == "function") {
                    fun();
                }
            }
        },
        closeBtn: [0, IsTrueOrFalsecloseBtn]
    });
    LayerIndex = i;
    return i;
}

//模态提示框 不带确认按钮
var AlertMsg2 = function (msg, fun,btnNum) {
    CloseLayer();
    //layer.alert(msg, 11, "提示");
    if (!btnNum)
        btnNum = 0;

    var i = $.layer({
        shade: [0.1, '#000', true],
        area: ['auto', 'auto'],
        dialog: {
            msg: msg + '<br/>&nbsp;',
            btns: btnNum,
            type: 8
        },
        yes: function () {
            if (typeof (fun) == "function") {
                fun();
            }
        },
        closeBtn: [0, false]
    });
    LayerIndex = i;
    return i;
}

//关闭弹窗 点击空白可以关闭
var ShowTips = function (cssSelecter, width, height, heightOffset) {
    setTimeout(function () {
        CloseLayer();
        PageLayer(cssSelecter, width, height, heightOffset);
    }, 0)
}
//关闭弹窗 点击空白不能关闭
var ShowTips2 = function (cssSelecter, width, height, heightOffset) {
    setTimeout(function () {
        CloseLayer();
        PageLayer2(cssSelecter, width, height, heightOffset);
    }, 0)
}

//iframe弹层,width, height不加px
//shadeClose true 点击阴影不取消弹窗 false 点击阴影取消弹窗
var IframLayer = function (url, width, height, shadeClose) {
    var i = $.layer({
        type: 2,
        //bgcolor: 'transparent',
        shade: [0.1, '#000', true],
        title: ['', false],
        //closeBtn: [0, false],
        shadeClose: shadeClose,
        offset: [($(window).height() - height) / 2, '50%'],
        area: [width + 'px', height + 'px'],
        border: [5, 0.1, '#000', true],
        iframe: { src: url },
        success: function () {

        }
    });
    LayerIndex = i;
    return i;
}


//iframe弹层,width, height不加px //弹窗不随父窗口滚动条滚动
var IframLayerV2 = function (url, width, height, heightOffset) {
    if (heightOffset == "undefined" || $.type(heightOffset) == "undefined" || heightOffset == null) {
        heightOffset = 0;
    }
    var i = $.layer({
        type: 2,
        bgcolor: 'transparent',
        fix: false, //true  不随父窗口滚动  false 随父窗口滚动
        shade: [0.3, 'transparent', true],
        title: ['', false],
        //closeBtn: [0, false],
        shadeClose: true,
        offset: [($(window).height() - height - heightOffset) / 2, '50%'],
        area: [width + 'px', height + 'px'],
        border: [5, 0.1, '#000', true],
        iframe: { src: url },
        success: function () {
        }
    });
    LayerIndex = i;
    return i;
}

//iframe弹层,width, height不加px
//shadeClose true 点击阴影不取消弹窗 false 点击阴影取消弹窗
//必须自定义关闭按钮
var IframLayerV3 = function (url, width, height) {
    var i = $.layer({
        type: 2,
        bgcolor: 'transparent',
        shade: [0.3, '#000', true],
        title: ['', false],
        closeBtn: [0, false],
        shadeClose: false,
        offset: [($(window).height() - height) / 2, '50%'],
        area: [width + 'px', height + 'px'],
        border: [5, 0.1, '#000', true],
        // border: [0, 0, '', false],
        iframe: { src: url },
        success: function () {

        }
    });
    LayerIndex = i;
    return i;
}

//页面弹层cssSelecter:例#id,等,width, height不加px
var PageLayer = function (cssSelecter, width, height, heightOffset) {
    if (heightOffset == "undefined" || $.type(heightOffset) == "undefined" || heightOffset == null) {
        var heighttmp = height;
        if (height == 'auto') {
            heighttmp = $(cssSelecter).height();
        }
        heightOffset = ($(window).height() - heighttmp) / 2;
    }

    var i = $.layer({
        type: 1,
        //bgcolor: 'transparent',
        shade: [0.3, '#000', true],
        title: ['', false],
        //closeBtn: [0, false],
        shadeClose: false,
        offset: [heightOffset, '50%'],
        area: [width, height],
        border: [5, 0.1, '#000', true],
        fix: false, //true  不随父窗口滚动  false 随父窗口滚动
        page: {
            dom: cssSelecter
        },
        success: function (layerDom) {

        }
    });
    LayerIndex = i;
    return i;
}

//页面弹层cssSelecter:例#id,等,width, height不加px
var PageLayer2 = function (cssSelecter, width, height, heightOffset) {
    if (heightOffset == "undefined" || $.type(heightOffset) == "undefined" || heightOffset == null) {
        var heighttmp = height;
        if (height == 'auto') {
            heighttmp = $(cssSelecter).height();
        }
        heightOffset = ($(window).height() - heighttmp) / 2;
    }

    var i = $.layer({
        type: 1,
        bgcolor: 'transparent',
        shade: [0.3, '#000', true],
        title: ['', false],
        //closeBtn: [0, false],
        shadeClose: false,
        fix: false, //true  不随父窗口滚动  false 随父窗口滚动
        offset: [heightOffset, '50%'],
        area: [width, height],
        border: [5, 0.1, '#000', true],
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



/**
* 时间对象的格式化;
*/
Date.prototype.format = function (format) {
    /*
    * eg:format="yyyy-MM-dd hh:mm:ss";
    */
    var o = {
        "M+": this.getMonth() + 1, // month
        "d+": this.getDate(), // day
        "h+": this.getHours(), // hour
        "m+": this.getMinutes(), // minute
        "s+": this.getSeconds(), // second
        "q+": Math.floor((this.getMonth() + 3) / 3), // quarter
        "S": this.getMilliseconds()
        // millisecond
    }

    if (/(y+)/.test(format)) {
        format = format.replace(RegExp.$1, (this.getFullYear() + "").substr(4
                        - RegExp.$1.length));
    }

    for (var k in o) {
        if (new RegExp("(" + k + ")").test(format)) {
            format = format.replace(RegExp.$1, RegExp.$1.length == 1
                            ? o[k]
                            : ("00" + o[k]).substr(("" + o[k]).length));
        }
    }
    return format;
}
//Json 获取的日期类型解析
function JsonDateFormatter(value, rec) {
    var date = new Date(parseInt(value.replace("/Date(", "").replace(")/", ""), 10));
    return date.format("yyyy-MM-dd");
    //    return date.format("yyyy-MM-dd hh:mm");
}
//Json 获取的日期类型解析
function JsonDateFormatterWithTime(value, rec) {
    var date = new Date(parseInt(value.replace("/Date(", "").replace(")/", ""), 10));
    return date.format("yyyy-MM-dd hh:mm");
}
//Json 获取的日期类型解析
function JsonDateFormatterWithZh(value, rec) {
    var date = new Date(parseInt(value.replace("/Date(", "").replace(")/", ""), 10));
    return date.format("yyyy年MM月dd日");
}

//删除数组中的元素
function arrayRemove(array, elementName) {
    //初始化数组
    //检测要删除的元素(删除元素值为：7name)
    for (var i = 0; i < array.length; i++) {
        if (array[i] == elementName) {
            array = removeElement(i, array);//删除方法
        }
    }
    function removeElement(index, array) {
        if (index >= 0 && index < array.length) {
            for (var i = index; i < array.length; i++) {
                array[i] = array[i + 1];
            }
            array.length = array.length - 1;
        }
        return array;
    }
}


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
