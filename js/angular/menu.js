var $app = angular.module("MyWall", ['ngAnimate']);
$app.controller("MyCtrl", function ($scope) {
    $scope.ishide = false;
    $scope.menu = [
    { "Id": 1, "Text": "一級菜單1" },
    {
        "Id": 2, "Text": "一級菜單2", "Items": [
            { "Id": 4, "Text": "二級菜單2-1" },
            {
                "Id": 5, "Text": "二級菜單2-2", "Items": [
                  { "Id": 8, "Text": "三級菜單2-2-1" },
                  { "Id": 8, "Text": "三級菜單2-2-2" }]
            }]
    },
    {
        "Id": 3, "Text": "一級菜單3", "Items": [
              { "Id": 6, "Text": "二級菜單3-1" },
              { "Id": 7, "Text": "二級菜單3-2" }]
    }
    ];
    $scope.currentMenu = $scope.menu;
    $scope.clickmenu = function (menu) {
        $scope.ishide = true;
        $scope.currentMenu = menu;
    }
});


