(function (angular) {
    angular
        .module("machineSalesModule")
        .config(function routeConfig($routeProvider) {
            $routeProvider
                .when("/machines/:id", {
                    templateUrl: "/Scripts/pages/admin/templates/machineDetails.html",
                    controller: "adminController",
                    controllerAs: "ac",
                    resolve: {
                        mode: function() {
                            return "edit/deleteMode";
                        }
                    }
                })
                .when("/addmachine", {
                    templateUrl: "/Scripts/pages/admin/templates/machineDetails.html",
                    controller: "adminController",
                    controllerAs: "ac",
                    resolve: {
                        mode: function() {
                            return "createMode";
                        }
                    }
                });
        });
})(angular);