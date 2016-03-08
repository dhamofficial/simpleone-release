function dashboardController($scope) {
    $scope.Title = 'Dashboard';
}
function featureController($scope) {
    $scope.Title = 'Features';
}

app.controller('dashboardController', dashboardController);
dashboardController.$inject = ['$scope'];

app.controller('featureController', featureController);
featureController.$inject = ['$scope'];