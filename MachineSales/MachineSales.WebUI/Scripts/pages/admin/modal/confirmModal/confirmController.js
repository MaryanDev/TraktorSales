(function (angular) {
    angular.module("machineSalesModule")
        .controller("confirmController", confirmController);

    confirmController.$inject = ["$scope", "$uibModalInstance", "mode"];

    function confirmController($scope, $uibModalInstance, mode) {
        $scope.mode = mode;
        console.log($scope.mode);

        $scope.ok = function () {
            $uibModalInstance.close(true);
        };

        $scope.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };
    };
})(angular);