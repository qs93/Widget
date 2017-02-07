var $app = angular.module("MyApp", []);
var mobile_regexp = /^[5|6|9]\d{7}$/;
$app.controller("MyCtrl", function ($scope) {
    $scope.submit = function () {
        //MyForm.txtEmail.value  獲取單個value值
        console.log(MyForm.txtEmail.value);
    };
});
//定義香港電話號碼格式
$app.directive("mobile", function () {
    return {
        require: 'ngModel',
        link: function (scope, elm, attrs, ctrl) {
            ctrl.$validators.igid = function (modelValue, viewValue) {
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