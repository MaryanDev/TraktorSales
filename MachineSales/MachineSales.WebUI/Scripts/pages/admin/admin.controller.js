(function (angular) {
    angular
        .module("machineSalesModule")
        .controller("adminController", adminController);

    adminController.$inject = ["$scope", "$routeParams", "adminAjaxService"];

    function adminController($scope, $routeParams, adminAjaxService) {
        $scope.machineId = $routeParams.id || null;
        $scope.machine = {};

        function activate() {
            if ($scope.machineId !== null) {
                adminAjaxService.getMachineDetails()
                    .then(function (response) {
                        $scope.machine = response.data;
                    }, function (error) {
                        console.error("error loading machine info")
                    })
            }
        }

        activate();
    };
})(angular);