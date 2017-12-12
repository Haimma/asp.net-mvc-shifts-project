angular.module('popup_worker', ['ngRoute', 'ngDialog'])
.controller('ManagerController', function ($scope, ngDialog) {
    $scope.openDialog = function () {
        ngDialog.openConfirm({
            template: '<div> Hello <br> <h>Hellooo</h></div>',
            plain: true,
            scope:$scope
        }).then(function (value){
        console.log(value);
        },function(reject){
        console.log(reject);
        });
    };
});