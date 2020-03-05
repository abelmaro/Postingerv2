var PostApp = angular.module('PostApp', []);

PostApp.controller('UserController', ['$scope', '$http', '$q', function ($scope, $http, $q) {
    $scope.usersList = [];
//#region Functions
//#enregion
//#region Auxiliares
//#enregion
//#region Services
    function getUserList() {
        $http.get("/User/GetAllUsers").then(function (response) {
            jQuery.each(response.data, function (i, val) {
                $scope.usersList.push(val.userName.split("@")[0]);
            });

            console.log($scope.usersList);
        });
    }    
    //#endregion

    var init = function () {
        moment.locale('es', {
            months: 'Enero_Febrero_Marzo_Abril_Mayo_Junio_Julio_Agosto_Septiembre_Octubre_Noviembre_Diciembre'.split('_'),
            monthsShort: 'Enero._Feb._Mar_Abr._May_Jun_Jul._Ago_Sept._Oct._Nov._Dec.'.split('_'),
            weekdays: 'Domingo_Lunes_Martes_Miercoles_Jueves_Viernes_Sabado'.split('_'),
            weekdaysShort: 'Dom._Lun._Mar._Mier._Jue._Vier._Sab.'.split('_'),
            weekdaysMin: 'Do_Lu_Ma_Mi_Ju_Vi_Sa'.split('_')
        });
        getUserList();
    };
    init();
}]);