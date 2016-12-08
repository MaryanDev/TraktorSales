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
                adminAjaxService.getMachineDetails($scope.machineId)
                    .then(function (response) {
                        $scope.machine = response.data;
                    }, function (error) {
                        console.error("error loading machine info");
                    });
            }
        }

        $scope.updateMachine = function (machine) {
            var isUpdate = confirm("Ви дійсно хочете змінити дану машину?");
            if (isUpdate) {
                adminAjaxService.updateMachine(machine)
                .then(function (response) {
                    location.assign("/");
                }, function (error) {
                    console.error("error updating machine");
                });
            }
        }

        activate();
    };
})(angular);