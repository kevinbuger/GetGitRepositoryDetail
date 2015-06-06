var app = angular.module('myApp', ["chart.js"]);


app.controller('RepCtrl', function ($scope, $http) {
    $scope.repositoryUrl = "https://github.com/duojs/test";
    $scope.getdetail = function () {
        $http({ url: "/api/git", method: "GET", params: { rUrl: $scope.repositoryUrl} })
            .success(function (response) { $scope.gitInfo = response; });
    };
});

app.controller("BarCtrl", function ($scope, $http) {
    $scope.repositoryUrl = "https://github.com/duojs/test";
    $scope.getBar = function () {
        $http({ url: "/api/branches", method: "GET", params: { rUrl: $scope.repositoryUrl} })
        .success(function (response) {
            $scope.labels = response.Days;
            $scope.series = response.BranchesID;
            $scope.data = response.CountCommitsPerDay;
            $scope.onClick = function (points, evt) { console.log(points, evt); };
        });
    };
});


app.controller("PieCtrl", function ($scope, $http) {
    $scope.repositoryUrl = "https://github.com/duojs/test";
    $scope.getPie = function () {
        $http({ url: "/api/Contributors", method: "GET", params: { rUrl: $scope.repositoryUrl} })
    .success(function (response) {
        var labarr = [];
        var dataarr = [];
        for (var i = 0; i < response.length; ++i) {
            labarr.push(response[i]["Name"]);
            dataarr.push(response[i]["CountCommits"]);
        }
        $scope.labels = labarr;
        $scope.data = dataarr;
    });
    };
});