angular.module('MitchellVehicleApp')
    .controller('VehicleController', [
    '$scope',
    '$timeout',
    'VehicleService',
    function($scope, $timeout, vehicleService) {
        $scope.vehicles = [];

        var retrieveVehicles = function () {
            vehicleService.getVehicles().$promise
                .then(function (vehicles) {
                    $scope.vehicles = vehicles;
                });
        };

        var displayError = function (error) {
            if (error.data && error.data.Message) {
                $scope.err = error.data.Message;
            }
            $timeout(function () {
                $scope.err = "";
            }, 4000);
        };

        retrieveVehicles();

        $scope.vehicle = {};
        $scope.err = "";

        $scope.createVehicle = function (newVehicleData) {
            
            vehicleService.createVehicle(newVehicleData).$promise
                .then(function (data) {
                    $scope.vehicle = {};
                    retrieveVehicles();

                }).catch(function (err) {
                    displayError(err);
                });
        };

        $scope.selectedVehicle = {};

        $scope.displaySelectedVehicle = function (vehicle) {
            var copyVehicle = jQuery.extend(true, {}, vehicle);
            $scope.selectedVehicle = copyVehicle;
        };

        $scope.updateVehicle = function (selectedVehicle) {
            console.log(selectedVehicle);
            vehicleService.updateVehicle(selectedVehicle).$promise
                .then(function (data) {
                    alert("Vehicle has been updated");
                    retrieveVehicles();

                }).catch(function (err) {
                    displayError(err);
                });
        };

        $scope.deleteVehicle = function (selectedVehicle) {
            vehicleService.deleteVehicle(selectedVehicle.Id).$promise
                .then(function (data) {
                    alert("Vehicle has been deleted");
                    retrieveVehicles();

                }).catch(function (err) {
                    alert("Something went wrong with deleting the vehicle!");
                });
        };
    }]
);