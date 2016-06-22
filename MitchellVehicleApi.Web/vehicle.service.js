angular.module('MitchellVehicleApp')
    .factory('VehicleService', [
        '$resource',
        'ROOT_URL',
        function ($resource, ROOT_URL) {
            var resource = $resource(
                ROOT_URL + '/vehicles', {}, {
                    getVehicle: {
                        url: ROOT_URL + '/vehicles/:id',
                        method: 'GET'
                    },

                    getVehicles: {
                        url: ROOT_URL + '/vehicles/',
                        method: 'GET',
                        isArray: true
                    },

                    createVehicle: {
                        method: 'POST'
                    },

                    updateVehicle: {
                        url: ROOT_URL + '/vehicles/:id',
                        method: 'PUT'
                    },

                    deleteVehicle: {
                        url: ROOT_URL + '/vehicles/:id',
                        method: 'DELETE'
                    }
                }
            );

            return {
                getVehicle: function (vehicleId) {
                    return resource.getVehicle({ id: vehicleId });
                },

                getVehicles: function () {
                    return resource.getVehicles();
                },
                
                createVehicle: function (newVehicleData) {
                    return resource.createVehicle(newVehicleData);
                },

                updateVehicle: function (updatedVehicleData) {
                    return resource.updateVehicle(updatedVehicleData);
                },

                deleteVehicle: function (vehicleId) {
                    return resource.deleteVehicle({ id: vehicleId });
                }
            };
        }]
    );