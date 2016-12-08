(function (angular) {
    angular
        .module("machineSalesModule")
        .config(function routeConfig($routeProvider) {
            $routeProvider
                .when("/machines/:id", {
                    templateUrl: "/Scripts/pages/admin/templates/machineDetails.html",
                    controller: "adminController",
                    controllerAs: "ac"
                })
                .when("/addmachine", {
                    templateUrl: "/Scripts/pages/admin/templates/addMachine.html",
                    controller: "adminController",
                    controllerAs: "ac"
                });
        });
})(angular);