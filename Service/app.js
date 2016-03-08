var app = angular.module('myapp', ['LocalStorageModule', 'ngSanitize', 'ngRoute', 'ngAnimate', 'angular-growl', 'ngTagsInput', 'angular-loading-bar']);
app.config(function (localStorageServiceProvider, $routeProvider, cfpLoadingBarProvider) {
    localStorageServiceProvider.setPrefix('releasemgmt');
    cfpLoadingBarProvider.includeSpinner = true;
    cfpLoadingBarProvider.includeBar = true;
  
  $routeProvider
    .when('/', {templateUrl : 'dashboard.html',controller  : 'MainCtrl'})
    .when('/list', { templateUrl: 'releaselist.html', controller: 'MainCtrl' })
    .when('/list/:flag', { templateUrl: 'releaselist.html', controller: 'MainCtrl' })
    .when('/add', { templateUrl: 'addexpense.html', controller: 'MainCtrl' })
    .when('/add/:ind',{controller: 'MainCtrl',templateUrl: 'addexpense.html'});

  //localStorageServiceProvider.setStorageType('sessionStorage');//default: localStorage
  //https://github.com/grevory/angular-local-storage
});
app.directive('monthControl', function() {
  return {
      restrict: 'AE',
      replace: 'true',
      template: '<div class="btn-group"><button type="button" class="btn btn-default">{{currentMonth}} 2016</button><button type="button" class="btn btn-default dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"><span class="caret"></span><span class="sr-only">Toggle Dropdown</span></button><ul class="dropdown-menu"><li><a href="#" ng-repeat="m in months" ng-click="changeMonth($index)">{{m}} 2016</a></li></ul></div>'
  };
}).directive('datetimePicker', function() {
    var link = function(scope, element, attrs) {
      var datetimeformat='YYYY-MM-DD HH:mm:ss';
      var dateformat='YYYY-MM-DD'; 
        var modelName = attrs['datetimePicker'];
        $(element).datetimepicker({format: datetimeformat});
              $(element).blur(function(){scope.$apply(function(){
                  var sv = 'scope.' + modelName + '="' + $(element).val() + '";';
                  //console.log('selected date:', $(element).val());
                  //console.log(sv);
                  eval(sv);
                  });});
    };
    return {
        require: 'ngModel',
        restrict: 'A',
        link: link
    }
});

app.service('ReleaseService', ['$q', function ($q) {
    var mode = 'local';
    var Items = [];
    var init = function () {
        var a = localStorage.getItem('__ReleaseTracker');
        if (a && a != '') Items= JSON.parse(a); else Items= [];
    }
    init();
    var now = function () {
        return moment().format("DD-MM-YYYY hh:mm:ss");
    }
    var addItem = function (item) {
        var d = $q.defer();
        item.uid = Items.length + 1;
        item.Created = now();
        Items.push(item);
        commit().then(function () { d.resolve('Release detail added.'); });
        return d.promise;
    };

    var getItems = function () {
        return Items;
    };

    var updateItem = function (index, item) {
        var d = $q.defer();
        item.Updated = now();
        Items[index] = item;
        commit().then(function () { d.resolve('Release detail updated.'); });
        return d.promise;
    };

    var commit = function () {
        var d = $q.defer();
        if (mode == 'local') {
            localStorage.setItem('__ReleaseTracker', JSON.stringify(Items));
            console.log('local commit - done');
            d.resolve();
        }
        return d.promise;
    }
    var getMasterData = function () {
        var s = {
  "Masters": {
    "Customers": [
      {
        "text": "Dell",
        "value": "1"
      },
      {
        "text": "Hp",
        "value": "2"
      },
      {
        "text": "Lenovo",
        "value": "3"
      },
      {
        "text": "Compaq",
        "value": "4"
      }
    ],
    "Environments": [
      {
        "text": "On-Premise",
        "value": "On-Premise"
      },
      {
        "text": "Cloud",
        "value": "Cloud"
      }
    ],
    "ReleaseTypes": [
      {
        "text": "Production",
        "value": "1"
      },
      {
        "text": "Implementation",
        "value": "2"
      },
      {
        "text": "POC",
        "value": "3"
      },
      {
        "text": "Testing",
        "value": "4"
      }
    ],
    "Locations": [
      {
        "text": "India",
        "value": "1"
      },
      {
        "text": "US",
        "value": "2"
      },
      {
        "text": "Singapore",
        "value": "3"
      },
      {
        "text": "London",
        "value": "4"
      }
    ],
    "Subscriptions": [
      {
        "text": "South-Asia",
        "value": "1"
      },
      {
        "text": "West-Asia",
        "value": "2"
      },
      {
        "text": "East-Asia",
        "value": "3"
      }
    ]
  }
};
        return s.Masters;
    }
    var selectedItem;
    var selectedRow = function (o) {
        selectedItem = o;
    }
    var getSelectedRow = function () {
        console.log('getSelectedRow:', selectedItem);
    }

    return {
        add: addItem,
        get: getItems,
        update: updateItem,
        master: getMasterData,
        selected: selectedRow,
        getSelected: getSelectedRow
    };
}]);
app.factory('DataService',function($q,$http){
  var service={};
  var config={};
  service.init=function(p){
    config.url='/release/api/master';
    if(p)config=p;
  }
  service.getdata=function(){
    var d = $q.defer();
    var response = $http({
            method: "GET", url: config.url, params: {}, headers: { 'Accept': 'application/json, text/javascript', 'Content-Type': 'application/json; charset=utf-8' }
    }).success(function (data, status, headers, config) {
        d.resolve(data);
    }).error(function (data, status, headers, config) {
        console.log('error');
        return null;
    });
    return d.promise;
  }
  service.savedata=function(item){
      var d = $q.defer();
      $http.post(config.url, JSON.stringify(item), { headers: { 'Content-Type': 'application/json' } }).success(function (data) { d.resolve(data); }).error(function (data) { d.reject(data); });
      return d.promise;
  }
  service.getrecentlist = function (p) {
      var d = $q.defer();      
      var param = "";
      if (p) { p.action = "filterlist"; }else if (!p) { p = {}; p.action = "recentlist"; }
      param = "?action=" + JSON.stringify(p);
      $http.get(config.url+param,"", { headers: { 'Content-Type': 'application/json' } }).success(function (data) { d.resolve(data); }).error(function (data) { d.reject(data); });
      return d.promise;
  }
  return service;
});
app.controller('MainCtrl', function ($scope, localStorageService, $location, $route, $routeParams, growl, DataService, $q, $filter, $timeout) {
    var pagename = '';
    if ($route.current && $route.current.loadedTemplateUrl) pagename= $route.current.loadedTemplateUrl;
    var remotedb = true;
  $scope.Items=[];
  var key='release_tracker';
  var datetimeformat='YYYY-MM-DD HH:mm:ss';
  var dateformat='YYYY-MM-DD'; 
  var data;
  $scope.selectedIndex=undefined;
        
  $scope.changeView = function(view){$location.path(view);}
  $scope.changeMonth=function(index){
    $scope.currentMonth=$scope.months[index];
  }
  $scope.init = function (settings) {
    data = DataService;
    data.init(settings);
    $scope.Filter = {};

    $scope.$watch('[Item.Environment]', function (values) {
        if (values && values.length > 0 && values[0]) {
            var obj = $filter('filter')($scope.Environments, { Id: values[0] });
            if (obj && obj.length > 0) {
                var myRedObjects = $filter('filter')($scope.Environments, { Id: values[0] })[0];
                if (myRedObjects.Name == 'Cloud') $scope.visibility1 = true; else $scope.visibility1 = false;
            }
            //console.log('watcher:', myRedObjects.Name,values[0], $scope.Environments[values[0] - 1]);
        }
    });
    if (pagename == 'releaselist.html') {
        $scope.$watch('[Filter.Customer,Filter.Environment,Filter.ReleaseType,Filter.BuildDate]', function (values) {
            $scope.FilterReleases();
        });
        $scope.FilterReleases = function () {
            var p = { Customer: $scope.Filter.Customer, Environment: $scope.Filter.Environment, ReleaseType: $scope.Filter.ReleaseType, BuildDate: $scope.Filter.BuildDate };
            if ($routeParams.flag) {
                p.flag = $routeParams.flag;
            }
            data.getrecentlist(p).then(function (list) {
                $scope.FilterList = list;
            });
        }
    }
    $scope.new = function () {
        $scope.Item = { Customer: 0, Environment: 0, Location: 0, Subscription:0,Environment: 0, ReleaseType: 0 };
    }
    
    //$scope.Item={};
    $('.datetimepicker').datetimepicker({format: datetimeformat});
    
    $scope.months=moment.monthsShort();
    $scope.changeMonth(moment().month());
     
    var v=localStorageService.get(key);
    if(v && v!='')$scope.Items=JSON.parse(v);
    $scope.newmode = true;
    

    if ($routeParams.ind) {
        console.log('edit mode');
      $scope.newmode=false;
      $scope.loadData($routeParams.ind);
    } else {
        console.log('new mode');
        $scope.new();
        
        data.getdata().then(function (d) {
            $scope.Customers = d.Customers;
            $scope.ReleaseTypes = d.ReleaseTypes;
            $scope.Environments = d.Environments;
            $scope.Locations = d.Locations;
            $scope.Subscriptions = d.Subscriptions;
        });
        data.getrecentlist().then(function (r) {
            $scope.RecentList = r.recentitems;
            $scope.tiledata = r.tiledata;
        });
    }
  }

  $scope.init();
  $scope.showlist = function (flag) {
      $location.path('/list/' + flag);
  }
  $scope.edit = function (id) {
      $location.path('/add/' + id);
  }
  $scope.loadData = function (id) {
      var p = { Id: id };
      //$timeout(function () {
      data.getrecentlist(p).then(function (d) {
          $scope.Item = d;
          if (d.SharedInstance) $scope.Item.SharedInstance = 'true'; else $scope.Item.SharedInstance = 'false';
          //console.log('edi item:',$scope.Item);
      });
      //}, 1000);
  }
  $scope.savetodb = function (t) {
      var d = $q.defer();
      if (!remotedb) {
          if ($scope.selectedIndex)
              $scope.Items[$scope.selectedIndex] = t;
          else
              $scope.Items.push(t);

          var value=JSON.stringify($scope.Items);
          localStorageService.set(key, value);
          d.resolve('ok');
    }else{
        console.log('adding item:', t);
        data.savedata(t).then(function () {
            d.resolve('ok');
        }, function (reason) {
            d.reject(reason);
        });
    }
    return d.promise;
  }
  
  
  $scope.saveTrans=function(t){
      //if(t.due && t.due!='')t.due=moment(t.due).format(datetimeformat);
      //console.log('saveobject:',t);
      $scope.saving = true;
      $scope.savetodb(t).then(function (data) {
          growl.success("Great, Release information saved");
          $scope.saving = false;
          $scope.cancel();
      }, function (reason) {
          growl.error(reason, { title: 'Oh no, Something went wrong! :-)' });
      });
    
    //growl.warning("This adds a warn message", {title: 'Warning!'});
    //growl.info("This adds a info message", {title: 'Random Information'});
    //growl.error("This adds a error message", {title: 'ALERT WE GOT ERROR'});
  }
   
  $scope.clear=function(){
    $scope.selectedIndex=undefined;
    $scope.Item={};
  }
   $scope.cancel=function(){
    $scope.clear();
    $scope.changeView('/');
  }
});
