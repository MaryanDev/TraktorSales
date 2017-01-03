(function (angular) {
    angular
        .module("machineSalesModule")
        .controller("adminController", adminController);

    adminController.$inject = ["$scope", "$routeParams", "FileUploader", "adminAjaxService", "mode", "$uibModal"];

    function adminController($scope, $routeParams, FileUploader, adminAjaxService, mode, $uibModal) {
        $scope.mode = mode;

        $scope.machineId = $routeParams.id || null;
        $scope.machine = {};

        $scope.mainPhotoUploader = new FileUploader();
        $scope.mainPhotoUploader.url = "/Admin/ModifyMainImage";

        $scope.photosUploader = new FileUploader();
        $scope.photosUploader.url = "/Admin/ModifyImages";

        function activate() {
            if ($scope.machineId !== null) {
                adminAjaxService.getMachineDetails($scope.machineId)
                    .then(function (response) {
                        $scope.machine = response.data;
                        $scope.mainPhotoUploader.onBeforeUploadItem = onBeforeUploadItem;

                        $scope.photosUploader.onBeforeUploadItem = onBeforeUploadItem;

                    }, function (error) {
                        console.error("error loading machine info");
                    });
                $scope.mainPhotoUploader.filters.push({
                    'name': 'enforceMaxFileSize',
                    'fn': function (item) {
                        return item.size <= 10485760; // 10 MiB to bytes
                    }
                });
                $scope.photosUploader.filters.push({
                    'name': 'enforceMaxFileSize',
                    'fn': function (item) {
                        return item.size <= 10485760; // 10 MiB to bytes
                    }
                });
            }
            $scope.mainPhotoUploader.onCompleteAll = onComleteAll;
            $scope.photosUploader.onCompleteAll = onComleteAll;
        }

        function onBeforeUploadItem(item) {
            item.formData.push({ machineId: $scope.machine.Id });
        }
        function onComleteAll() {
            if (!$scope.mainPhotoUploader.isUploading && !$scope.photosUploader.isUploading) {
                location.assign("/Admin/Dashboard");
            }
        }

        $scope.updateMachine = function (machine) {
            //var isUpdate = confirm("Ви дійсно хочете змінити дану машину?");
            
            openConfirm("updateMode").result.then(function () {
                adminAjaxService.updateMachine(machine)
               .then(function (response) {
                   if ($scope.mainPhotoUploader.queue.length === 0 && $scope.photosUploader.queue.length === 0) {
                       location.assign("/Admin/Dashboard");
                   } else {
                       $scope.mainPhotoUploader.uploadAll();
                       $scope.photosUploader.uploadAll();
                   }
               }, function (error) {
                   console.error("error updating machine");
               });
            }, function () { });
        }

        $scope.deleteMainImage = function (id) {
            openConfirm("deletePhotoMode").result.then(function () {
                adminAjaxService.deleteMainImage(id)
                    .then(function (response) {
                        location.assign("/Admin/Dashboard");
                    }, function (error) {
                        console.error("error deleting main photo");
                    });
            }, function () {
            }); 
        }

        $scope.deleteSecondaryImage = function (id) {
            openConfirm("deletePhotoMode").result.then(function () {
                adminAjaxService.deleteSecondaryImage(id)
                    .then(function (response) {
                        location.assign("/Admin/Dashboard");
                    }, function (error) {
                        console.error("error deleting main photo");
                    });
            }, function () { });
        }

        $scope.createMachine = function (machine) {
            openConfirm("createMode").result.then(function () {
                adminAjaxService.createMachine(machine)
                    .then(function (response) {
                        if ($scope.mainPhotoUploader.queue.length === 0 && $scope.photosUploader.queue.length === 0) {
                            location.assign("/Admin/Dashboard");
                        } else {
                            $scope.machine.Id = response.data;
                            $scope.mainPhotoUploader.onBeforeUploadItem = onBeforeUploadItem;
                            $scope.photosUploader.onBeforeUploadItem = onBeforeUploadItem;
                            $scope.mainPhotoUploader.uploadAll();
                            $scope.photosUploader.uploadAll();
                        }
                    }, function (error) {
                        console.error("error creating machine");
                    });
            }, function () { });
                
        }

        $scope.deleteMachine = function (machineId) {
            openConfirm("deleteMode").result.then(function () {
                adminAjaxService.deleteMachine(machineId)
                    .then(function (response) {
                        location.assign("/Admin/Dashboard");
                    }, function (error) {
                        console.error("error deleting machine");
                    });
            }, function () { });
                
        }

        function openConfirm(mode) {
            var modalInstance = $uibModal.open({
                animation: true,
                templateUrl: "/Scripts/pages/admin/modal/confirmModal/confirmModal.html",
                controller: "confirmController",
                controllerAs: "cdCtrl",
                size: "sm",
                resolve: {
                    mode: function () {
                        return mode;
                    }
                }
            });

            return modalInstance;
        }

        activate();
    };
})(angular);