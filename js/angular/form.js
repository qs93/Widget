(function () {
    var $app = angular.module("MyApp", []);
    var mobile_regexp = /^[5|6|9]\d{7}$/;
    $app.controller("MyCtrl", function ($scope) {
        $scope.submit = function () {
            //MyForm.txtEmail.value  獲取單個value值
            console.log(MyForm.txtEmail.value);
            $.ajax({
                url: "/api/AjaxHelper.ashx?option=angular_sub_ajax",
                type: "Post",
                dataType: "json",
                data: $scope.fillForm,
                beforeSend: function () {
                    //開始提交時，可用於顯示正在執行提示
                },
                success: function (json) {
                    console.log(json);
                    //成功
                },
                complete: function () {
                    //完成時
                },
                error: function () {
                    //錯誤時
                }
            });
        };
    });
    //定義香港電話號碼格式
    $app.directive("hkmobile", function () {
        return {
            require: 'ngModel',
            link: function (scope, elm, attrs, ctrl) {
                ctrl.$validators.hkmobile = function (modelValue, viewValue) {
                    if (ctrl.$isEmpty(modelValue)) {  //允許為空
                        return true;
                    }
                    if (mobile_regexp.test(viewValue)) {
                        return true;
                    }
                    return false;
                };
            }
        };
    });
})();