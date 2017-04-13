(function (window, $) {
    window.myalert = function (text, fixed) {
        AlertDemo(text,"alert", fixed);
    }
    window.myconfirm = function (text, fixed) {
        AlertDemo(text, "confirm", fixed);
    }
}(window, $));


function AlertDemo(text, type, fixed) {
    var $shadow = $("<div>").addClass("shadow-layer");  //遮蓋層
    var $demo = $("<div>").addClass("myalert");  //主容器
    var $type = $("<div>").addClass("myalert-typemsg");  //提示類型
    var $text = $("<div>").addClass("myalert-text").html(text);  //提示內容
    var $button = $("<div>").addClass("myalert-button");  //按鈕層
    var $fixedButton = $("<button>").addClass("fixed").html("確定");  //確定按鈕

    $("body").append($demo);
    $("body").append($shadow);
    $type.html("提示");
    $demo.append($type);
    $demo.append($text);
    $button.append($fixedButton);
    $demo.append($button);
    if (fixed) {
        $fixedButton.click(function () {
            $demo.remove();
            $shadow.remove();
            fixed();
        });
    } else {
        $fixedButton.click(function () {
            $demo.remove();
            $shadow.remove();
        });
    }
    if (type == "confirm")
    {
        var $cancelButton = $("<button>").addClass("cancel").html("取消"); //取消按鈕
        $button.append($cancelButton);
        $cancelButton.click(function () {
            $demo.remove();
            $shadow.remove();
        });
    }
    $demo.css("margin-top", -($demo.height() / 2));
}