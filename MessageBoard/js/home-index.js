/**
 * home-index.js
 * As of AngularJS v1.3.0 $controller will no longer look for controllers on window. The old behavior
 * of looking on window for controllers was originally intended for use in examples,
 * demos, and toy apps. We found that allowing global controller functions encouraged poor practices,
 * so we resolved to disable this behavior by default.
 * That is why just defining the ng-controller function here is not enough.
 * Instead, set the ng-controller explicitely on the module
 *
 * Also, we need to attach the new angular-route.js library
 */

var module = angular.module('homeIndex', ['ngRoute']);
///////////////////
// MODULE.CONFIG //
///////////////////
module.config(function ($routeProvider) { // injected dependency
  $routeProvider.when("/", {
    controller: "topicsController",
    templateUrl: "/templates/topicsView.html" // we will be using here an external template (see templates folder)
  });

  $routeProvider.when("/newmessage", {
    controller: "newTopicController",
    templateUrl: "/templates/newTopicView.html"
  });

  $routeProvider.when("/message/:id", {
    controller: "singleTopicController",
    templateUrl: "/templates/singleTopicView.html"
  });

  // fall back route
  $routeProvider.otherwise({ redirectTo: "/" });
});

////////////////////
// MODULE.FACTORY //
////////////////////

// angularjs webservice on the client side; factory creates an injectable object
// the function here is a callback function and returns the newly created service
// this factory implements the singleton pattern and will be widely reused by many controllers
// angularjs webservice on the client side; factory creates an injectable object
module.factory("dataService", function ($http, $q) {
  var _topics = [];
  var _isInit = false;

  // _isInit //

  var _isReady = function () {
    return _isInit;
  };

  // _getTopics //

  var _getTopics = function () {
    // $q dependency is necessary for the implementation of the deferral object in order to expose our promise
    var deferred = $q.defer();

    $http.get("/api/v1/topics?includeReplies=true") // $http.get returns a promise
      .then(function (result) {
        // Successful
        angular.copy(result.data, _topics);
        _isInit = true;
        // resolved with a successful result. it is also possible to pass some data as a parameter but now it is not necessary
        deferred.resolve();
      },
      function () {
        // Error - but within the factory we don't want to interact with the UI
        // instead, we are going to use the callback function to whoever called this function
        // it is possible to call then() on this object to chain calls together
        // resolved with a failure. it is also possible to pass some data as a parameter but now it is not necessary
        deferred.reject();
      });
    return deferred.promise;
  };

  // _addTopic //

  var _addTopic = function (newTopic) {
    // $q dependency is necessary for the implementation of the deferral object in order to expose our promise
    var deferred = $q.defer();

    $http.post("/api/v1/topics", newTopic)
      .then(function (result) {
        // success
        var newlyCreatedTopic = result.data;
        _topics.splice(0, 0, newlyCreatedTopic); // here we add the newly created object as the first one without deletion to the array of topics
        deferred.resolve(newlyCreatedTopic); // here we can pass the new object back to the user just in case
      },
      function () {
        // error
        deferred.reject();
      });

    return deferred.promise;
  };

  // _findTopic //

  function _findTopic(id) {
    var found = null;

    // at them moment $.each is good enough
    // later for better performance we could create a dictionary or a lookup object
    $.each(_topics, function (i, item) {
      if (item.id == id) {
        found = item;
        return false;
      };
    });

    return found;
  }

  // _getTopicById //

  var _getTopicById = function (id) {
    var deferred = $q.defer();

    if (_isReady()) {
      var topic = _findTopic(id);
      if (topic) {
        deferred.resolve(topic);
      } else {
        deferred.reject();
      };
    } else {
      _getTopics()
        .then(function () {
          // success
          var topic = _findTopic(id);
          if (topic) {
            deferred.resolve(topic);
          } else {
            deferred.reject();
          };
        },
          function () {
            // error
            deferred.reject();
          });
    };

    return deferred.promise;
  };

  //// here we are making the assumption that the data is already loaded
  //function _findTopic(id) {
  //  var found = null;

  //  // we are using here the jQuery each method.
  //  // angular also has its own each method 
  //  // but it always goes through the entire collection
  //  // and does not allow us to break out of the loop
  //  // when we found what we were looking for
  //  $.each(_topics, function (i, item) {
  //    if (item.id == id) {
  //      found = item;
  //      return false; // return false is like break
  //    }
  //  });

  //  return found;
  //}

  //var _getTopicById = function (id) {
  //  var deferred = $q.defer(); // prepare the promise

  //  // we read in cache is the data is loaded, otherwise we make a deferred call to the database
  //  if (_isReady()) {
  //    var topic = _findTopic(id); // here we use a private function, which we are not going to expose to the outside world
  //    if (topic) {
  //      deferred.resolve(topic);
  //    } else {
  //      deferred.reject();
  //    }
  //  } else {
  //    _getTopics()
  //      .then(function () {
  //        // success
  //        topic = _findTopic(id); // here we use a private function, which we are not going to expose to the outside world
  //        if (topic) {
  //          deferred.resolve(topic);
  //        } else {
  //          deferred.reject();
  //        }
  //      },
  //        function () {
  //          // error
  //          deferred.reject();
  //        });
  //  }

  //  return deferred.promise; // return the promise
  //};

  //var _saveReply = function (topic, newReply) {
  //  var deferred = $q.defer;

  //  $http.post("/api/v1/topics/" + topic.id + "/replies", newReply)
  //    .then(function (result) {
  //      // initialize the collection if necessary
  //      if (topic.replies == null) {
  //        topic.replies = [];
  //      };
  //      // success - add the newly inserted reply with all its data to the cached collection
  //      topic.replies.push(result.data);
  //      deferred.resolve(result.data); // we return the data to the caller just in case
  //    },
  //      function () {
  //        // error
  //        deferred.reject();
  //      });

  //  return deferred.promise;
  //};

  return {
    topics: _topics,
    isReady: _isReady,
    getTopics: _getTopics,
    addTopic: _addTopic,
    getTopicById: _getTopicById
    //saveReply: _saveReply
  };
});

/////////////////
// CONTROLLERS //
/////////////////

// NB: it is very important that the following statements are AFTER the factory, not before!
module.controller('topicsController', topicsController);
module.controller('newTopicController', newTopicController);
//module.controller('singleTopicController', singleTopicController);

///////////////////////
// TOPICS CONTROLLER //
///////////////////////

function topicsController($scope, $http, dataService) {
  // one option is to bind topics directly to data as dataService.topics - then NG won't know if the topics have changed
  // therefore, we are binding the whole dataService object
  $scope.data = dataService;
  $scope.isBusy = false; // to show the user the status of the server call, false by default

  // a bit of caching
  if (dataService.isReady() == false) {
    $scope.isBusy = true; // to show the user that some operation (call to the server) is going on
    dataService.getTopics()
      .then(function () {
        // success
        // nothing more to do here as the dataService does the changes and angular bindings react to that
      },
        function () {
          // error - communicate to the user
          alert("could not load topics");
        })
      .then(function () { // this is a top level promise, sort of 'finally' clause
        $scope.isBusy = false;
      });
  }
}

//////////////////////////
// NEW TOPIC CONTROLLER //
//////////////////////////

function newTopicController($scope, $http, $window, dataService) {
  $scope.newTopic = {};

  $scope.save = function () {
    dataService.addTopic($scope.newTopic)
      .then(function () {
        // success
        $window.location = "#/"; // return the user to the main page
      },
        function () {
          // error
          alert("could not save the new topic");
        });
  };
}

/////////////////////////////
// SINGLE TOPIC CONTROLLER //
/////////////////////////////

//module.controller('singleTopicController', function($scope, dataService, $window, $routeParams) {
//function singleTopicController($scope, dataService, $window, $routeParams) {
//$scope.topics = null;
//$scope.newReply = {};

//dataService.getTopicById($routeParams.id) // id should have the same name as :id in the route!
//  .then(function (topic) {
//    // success
//    $scope.topics = topic;
//  },
//function () {
//  // error
//  $window.location = "#/";
//});

//$scope.addReply = function () {

//};
//});


module.controller("singleTopicController", function ($scope, dataService, $window, $routeParams) {
  $scope.topic = null;
  $scope.newReply = {};

  // we can't browse through the topics collection directly, 
  // instead we use the deferred service and pass it the topicId (:id from the route pattern)
  // this call to get topic by id may be asynchronous, so we work here with promise again
  dataService.getTopicById($routeParams.id) // id should have the same name as :id in the route!
    .then(function(topic) {
        //success
        $scope.topic = topic;
      },
      function() {
        //error - we don't bother, we just return the user to the main page
        $window.location = "#/";
      });

  $scope.addReply = function () {
    // filled by the form
    dataService.saveReply($scope.topic, $scope.newReply)
      .then(function() {
          // success
          // nothing more to do here about the reply itself, as the dataService that owns the collection 
          // will add the new reply to it, and do the necessary change so that angular bindings react to that.
          // we only need to reset the body of the reply for the possible new reply
          $scope.newReply.body = "";
        },
        function() {
          // error
          alert("could not save new reply");
        });
  };
});

//module.controller('topicsController', function ($scope, $http) {
//  //$scope.dataCount = 0; // no topics yet
//  $scope.data = []; // initially no data available in the controller
//  $scope.isBusy = true; // to show the user that some operation (call to the server) is going on

//  $http.get("/api/v1/topics?includeReplies=true") // $http.get returns a promise
//    .then(function (result) {
//      // Successful
//      // $scope.data = result.data // works well for single objects, not collections
//      angular.copy(result.data, $scope.data); // this works better for collections
//      //$scope.dataCount = result.data.length;
//    },
//    function () {
//      // Error
//      alert("could not load topics");
//    })
//  .then(function () { // this is a top level promise, sort of 'finally' clause
//    $scope.isBusy = false;
//  });
//});
