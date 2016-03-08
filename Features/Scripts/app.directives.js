app.directive('routeLoadingIndicator', ['$rootScope', '$timeout', function ($rootScope, $timeout) {
    return {
        restrict: 'E',
        template: "<div ng-show='isRouteLoading' class='loading-indicator'>" +
        "<div class='loading-indicator-body'>" +
        "<h3 class='loading-title'><i class='fa fa-spinner fa-spin'></i> Loading...</h3>" +
        "</div>" +
        "</div>",
        replace: true,
        link: function (scope, elem, attrs) {
            scope.isRouteLoading = false;

            $rootScope.$on('$routeChangeStart', function (event, currRoute, prevRoute) {
                scope.isRouteLoading = true;
                $rootScope.animation = currRoute.animation;
            });
            $rootScope.$on('$routeChangeSuccess', function () {
                scope.isRouteLoading = false;
            });
        }
    };
}]);