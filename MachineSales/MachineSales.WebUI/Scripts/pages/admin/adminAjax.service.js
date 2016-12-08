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

        function getMachineDetailsAjax() {

        }

        function deleteMachineAjax() {

        }

        function updateMachineAjax() {

        }
        
        function createMachineAjax() {

        }

        return service;
    }
})(angular);