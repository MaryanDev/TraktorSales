(function (angular) {
    angular
        .module("machineSalesModule")
        .factory("adminAjaxService", adminAjaxService);

    adminAjaxService.$inject = ["$http"];

    function adminAjaxService($http) {
        var service = {
            getMachineDetails: getMachineDetailsAjax,
            deleteMachine: deleteMachineAjax,
            updateMachine: updateMachineAjax,
            createMachine: createMachineAjax
        }

        function getMachineDetailsAjax(id) {
            var promise = $http({
                method: "GET",
                url: "/Admin/GetMachineInfo",
                params: { id: id }
            });

            return promise;
        }

        function deleteMachineAjax() {

        }

        function updateMachineAjax(machine) {
            var promise = $http({
                method: "POST",
                url: "/Admin/UpdateMachine",
                data: { machineToUpdate: machine }
            });

            return promise;
        }
        
        function createMachineAjax() {

        }

        return service;
    }
})(angular);