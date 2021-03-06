﻿var PostApp = angular.module('PostApp', []);

PostApp.controller('PostController', ['$scope', '$http', '$q', function ($scope, $http, $q) {
    $scope.Posts = [];
    $scope.Comments = [];
    $scope.postData = {};
    $scope.postName = "";
    $scope.postAutor= "";
    $scope.fechaPublicacion = "";
    $scope.contenido = ""; 
    $scope.habilitaMostrar = false;
    $scope.commentData = "";
    $scope.usersList = [];
    $scope.AllPosts = [];

    
    //#region Functions
    $scope.createNewPost = function () {
        if ($scope.habilitaMostrar == true) {
            $scope.habilitaMostrar = false;
        }
        else {
            $scope.habilitaMostrar = true
        } 
    }

    //#region Auxiliares

        var sortBy = (function () {
        var toString = Object.prototype.toString,
            // default parser function
            parse = function (x) { return x; },
            // gets the item to be sorted
            getItem = function (x) {
                var isObject = x != null && typeof x === "object";
                var isProp = isObject && this.prop in x;
                return this.parser(isProp ? x[this.prop] : x);
            };

        return function sortby(array, cfg) {
            if (!(array instanceof Array && array.length)) return [];
            if (toString.call(cfg) !== "[object Object]") cfg = {};
            if (typeof cfg.parser !== "function") cfg.parser = parse;
            cfg.desc = !!cfg.desc ? -1 : 1;
            return array.sort(function (a, b) {
                a = getItem.call(cfg, a);
                b = getItem.call(cfg, b);
                return cfg.desc * (a < b ? +1 : -(a > b));
            });
        };

    }());

    function getAllPosts() {
        $http.get("/Post/AllPosts").then(function (response) {
            //jQuery.each(response.data, function (i, val) {
            //    $scope.AllPosts.push(val);
            //});
            $scope.AllPosts = response.data;
            console.log($scope.postList);
        });
    }  


    $scope.addLike = function (postId) {
        var vm = {
            UserId: $scope.UserToken,
            PostId: postId,
            Tipo: "like"
        }
        addNewLike(vm);
    }

    $scope.addDislike = function (postId) {
        var vm = {
            UserId: $scope.UserToken,
            PostId: postId,
            Tipo: "dislike"
        }
        addNewLike(vm);
    }

    $scope.showModal = function (id) {
        angular.forEach($scope.Posts, function (value, key) {
            if (value.id == id) {
                $scope.postName = value.postName;
                $scope.fechaPublicacion = moment(value.fechaPublicacion).format("dddd, hA");
                $scope.contenido = "Contenido de prueba mockeado";
                $scope.currentPostId = id;
                getComments(id);
                $("#exampleModalLong").modal("toggle");
            }
        });
    }

    $scope.addComment = function (commentData, id) {
        if (commentData != undefined && commentData != '') {
            $scope.postData = {
                Comentario: commentData,
                PostID: id
            }
            addNewComment($scope.postData);
            $scope.commentData = "";
        }    
    }

    $scope.addPost = function () {
        $scope.postData = {
            PostName: $scope.postName,
            Autor: $scope.postAutor,
            FechaPublicacion: moment().format(),
            UserId: $scope.UserToken
        }
        addNewPost($scope.postData);
        $scope.habilitaMostrar = false;
    }

    //#endregion

    //#region Services
    //function getUserList() {
    //    $http.get("/User/GetAllUsers").then(function (response) {
    //        $scope.usersList = response.data;
    //    });
    //}

    function addNewLike(like) {
        var deferred = $q.defer();
        $http.post('/Post/AddLike', like)
            .then(function (response) {
               
            });
    }

    function addNewPost(postData) {
        var deferred = $q.defer();
        $http.post('/Post/AddNewPost', postData)
            .then(function (response) {
                getAll();
                deferred.resolve('request successful');
            });
    }

    function addNewComment(postData) {
        var deferred = $q.defer();
        $http.post('/Comment/AddComment', postData)
            .then(function (response) {
                getComments($scope.currentPostId);
                getAll();
                getAllPosts();
            });
    }

    function getAll() {
        $http.get("/Post/GetAll").then(function (response) {

            $scope.Posts = sortBy(response.data, { prop: "fechaPublicacion" });
            console.log($scope.Posts);
        });
    }

    function getComments(id) {
        $http.get("/Comment/GetComentariosByPostId?id=" + id).then(function (response) {
            $scope.Comments = response.data;
        });
    }

    function getUserToken() {
        $http.post("/Post/GetCurrentUserId").then(function (response) {
            $scope.UserToken = response.data;
        });
    }
    controladorPost = $scope;
    //#endregion

    var init = function () {
        moment.locale('es', {
            months: 'Enero_Febrero_Marzo_Abril_Mayo_Junio_Julio_Agosto_Septiembre_Octubre_Noviembre_Diciembre'.split('_'),
            monthsShort: 'Enero._Feb._Mar_Abr._May_Jun_Jul._Ago_Sept._Oct._Nov._Dec.'.split('_'),
            weekdays: 'Domingo_Lunes_Martes_Miercoles_Jueves_Viernes_Sabado'.split('_'),
            weekdaysShort: 'Dom._Lun._Mar._Mier._Jue._Vier._Sab.'.split('_'),
            weekdaysMin: 'Do_Lu_Ma_Mi_Ju_Vi_Sa'.split('_')
        });
        getAll();
        getUserToken();
        getAllPosts();
    };
    init();
}]);

var controladorPost;