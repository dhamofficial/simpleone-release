var app = angular.module('myapp', ['LocalStorageModule', 'ngSanitize', 'ngRoute', 'ngAnimate','ngTagsInput']);
app.run(['$rootScope', function ($rootScope) {
    $rootScope.$on('$stateChangeStart', function () {
        $rootScope.stateLoading = true;
    });

    $rootScope.$on('$stateChangeSuccess', function () {
        $rootScope.stateLoading = false;
    });
}]);
app.config(['localStorageServiceProvider', '$routeProvider', function (localStorageServiceProvider, $routeProvider) {
    localStorageServiceProvider.setPrefix('featuremgmt');
    $routeProvider
           .when('/', { templateUrl: 'features/home/dashboard', controller: 'dashboardController' })
           .when('/add', { templateUrl: 'features/home/add', controller: 'featureController' })
           .when('/list', { templateUrl: 'features/home/list', controller: 'featureController' })
           .when('/edit/:ind', { templateUrl: 'features/home/edit', controller: 'featureController' });
}]);