(function (angular) {
    angular
        .module("machineSalesModule")
        .controller("adminController", adminController);

    adminController.$inject = ["$scope", "$routeParams", "FileUploader", "adminAjaxService", "mode"];

    function adminController($scope, $routeParams, FileUploader, adminAjaxService, mode) {
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
            var isUpdate = confirm("Ви дійсно хочете змінити дану машину?");
            if (isUpdate) {
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
            }

        }

        $scope.deleteMainImage = function (id) {
            var isDeletingMain = confirm("Ви дійсно хочете видалити основне фото?");
            if (isDeletingMain) {
                adminAjaxService.deleteMainImage(id)
                    .then(function (response) {
                        location.assign("/Admin/Dashboard");
                    }, function (error) {
                        console.error("error deleting main photo");
                    });
            }
        }

        $scope.deleteSecondaryImage = function (id) {
            var isDeletingSecondary = confirm("Ви дійсно хочете видалити це фото?");
            if (isDeletingSecondary) {
                adminAjaxService.deleteSecondaryImage(id)
                    .then(function (response) {
                        location.assign("/Admin/Dashboard");
                    }, function (error) {
                        console.error("error deleting main photo");
                    });
            }
        }

        $scope.createMachine = function (machine) {
            var isCreating = confirm("Ви дійсно хочете створити нову машину?");
            if (isCreating) {
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
            }
        }

        $scope.deleteMachine = function (machineId) {
            var isDeleting = confirm("Ви дійсно хочете видалити цю машину?");
            if (isDeleting) {
                adminAjaxService.deleteMachine(machineId)
                    .then(function (response) {
                        location.assign("/Admin/Dashboard");
                    }, function (error) {
                        console.error("error deleting machine");
                    });
            }
        }

        activate();
    };
})(angular);