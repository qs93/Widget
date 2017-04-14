// 定义模块  
var scroll = angular.module('scroll', []);
// 定义滚动指令  
scroll.directive('whenScrolled', function () {
    return function (scope, elm, attr) {
        // body窗口的滚动加载--需要Jquery  
        $(window).scroll(function () {
            //滚动条距离顶部的距离  
            var scrollTop = $(window).scrollTop();
            //滚动条的高度  
            var scrollHeight = $(document).height();
            //窗口的高度  
            var windowHeight = $(window).height();
            if (scrollTop + windowHeight >= scrollHeight) {
                scope.$apply(attr.whenScrolled);
            }
        });
    };
});

scroll.controller('Main', ['$scope', '$http',
    function ($scope, $http) {
        // 当前页数  
        $scope.currentPage = 0;
        // 总页数  
        $scope.totalPages = 1;
        // 防止重复加载  
        $scope.busy = false;
        // 存放数据  
        $scope.items = [];
        // 请求数据方法  
        $scope.loadMore = function () {
            if ($scope.currentPage < $scope.totalPages) {
                $scope.currentPage++;
                if ($scope.busy) {
                    return false;
                }
                $scope.busy = true;
                $scope.items.push("111");
                $scope.items.push("222");
                $scope.totalPages = 1;
            }
        };
        // 默认第一次加载数据  
        $scope.loadMore();
    }]);