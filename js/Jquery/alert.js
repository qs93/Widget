(function (window) {
    window.nativeAlert = window.alert;
    window.alert = function (text, opt) {
        var useNativeAlert = false;
        if (opt != null || opt != undefined) {
            useNativeAlert = opt.nativeAlert || false;
        }
        if (useNativeAlert) {
            window.nativeAlert(text);
        } else {
            WinTip(text, opt);
        }
    };
    window.confirm = function (text, opt)
    {
        WinTip(text, opt);
        return true;
        
    }
}(window));

function WinTip(text, link) {
    var wWidth = $(window).width();
    var wHeight = $(window).height();
    var $myAlert = $(".myalert");
    $myAlert.css("margin-left", (wWidth - $myAlert.width()) / 2 + "px").css("margin-top", (wHeight - $myAlert.height()) / 2 + "px");
    $myAlert.find("div.content").html(text);
    if (link) {
        if (link.substring(0, 5) === "call:") {
            $myAlert.find("button.btn-fixed").attr("onClick", link.substring(5));
        } else if (link.substring(0, 6) == "blank:") {
            $myAlert.find("button.btn-fixed").attr("href", link.substring(6)).attr("target", "blank");
        } else {
            $myAlert.find("button.btn-fixed").attr("href", link);
        }
    }
    $myAlert.css("display", "block");
}
$(window).ready(function () {
    var $myAlert = $(".myalert");
    $myAlert.find("button.btn-fixed").click(function () {
        $myAlert.hide();
    });
    $myAlert.find("button.cancel").click(function () {
        $myAlert.hide();
    });
});
//(function (window) {
//    window.nativeAlert = window.alert;

//    window.alert = function (text, opt) {
//        var useNativeAlert = false;

//        if (opt != null || opt != undefined) {
//            useNativeAlert = opt.nativeAlert || false;
//        }

//        //if (useNativeAlert) {
//        //    window.nativeAlert(text);
//        //} else {
//        WinTip1(text, opt);
//        //}
//    };

//}(window));

//var $ssa = function (id) { return document.getElementById(id); }
//function WinTip(text, link, mainPanel) {
//    var win = new WinSize();

//    if (!mainPanel)
//        mainPanel = "win1";

//    var Tip = $ssa("bg");
//    Tip.style.width = "100%";
//    Tip.style.height = "100%";
//    if (Tip.style.display == "block") {
//        Tip.style.display = "none";
//        $ssa(mainPanel).style.display = "none";

//        UnlockScrollbar();

//    } else {
//        Tip.style.display = "block";
//        if (text) {
//            $('#msg-text').html(text);
//        }

//        if (link) {
//            if (link.substring(0, 5) === 'call:') {
//                //$('#msg-link').attr('onClick', 'javascript:window.history.go(-1);');
//                $('#msg-link').attr('onClick', link.substring(5));
//            } else
//                $('#msg-link').attr('onClick', 'location.href="' + link + '"');
//        }

//        LockScrollbar();

//        $ssa(mainPanel).style.display = "block";
//    }
//}

//function WinTip1(text, link) {
//    WinTip(text, link, "win1");
//}

//function WinSize() //函数：获取尺寸
//{
//    var winWidth = 0;
//    var winHeight = 0;
//    if (window.innerWidth)
//        winWidth = window.innerWidth;
//    else if ((document.body) && (document.body.clientWidth))
//        winWidth = document.documentElement.clientWidth
//    if (window.innerHeight)
//        winHeight = window.innerHeight;
//    else if ((document.body) && (document.body.clientHeight))
//        winHeight = document.body.clientHeight;
//    if (document.documentElement && document.documentElement.clientHeight && document.documentElement.clientWidth) {
//        winHeight = document.documentElement.clientHeight;
//        winWidth = document.documentElement.clientWidth;
//    }
//    return { "W": winWidth, "H": winHeight }
//}

//function LockScrollbar() {
//    //if (location == top.location) {
//    $('html, body').css({
//        'overflow': 'hidden'
//    });
//    //}
//}

//function UnlockScrollbar() {
//    if (location == top.location) {
//        $('html, body').css({
//            'overflow': 'auto'
//        });
//    }
//}

//if (window.location != window.top.location) {
//    $('html, body').css('overflow', 'hidden');
//}