var $app = angular.module("PhotoWall", []);
$app.controller("PhotoCtrl", function ($scope) {
    $scope.photo = [{ src: "/images/logo.png", title: "title1" }, { src: "/images/logo.png", title: "title2" }];
});