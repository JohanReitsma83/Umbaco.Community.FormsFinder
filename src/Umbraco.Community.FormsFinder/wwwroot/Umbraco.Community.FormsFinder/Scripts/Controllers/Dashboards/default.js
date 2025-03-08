angular.module("umbraco").controller("FormsFinderApiController", function ($http, localizationService, $scope) {
    const vm = this;
    const API_URL = '/umbraco/backoffice/Community/FormsFinderApi/';

    $scope.searchTerm = '';

    vm.loading = true;
    vm.searchQeury = "";
    vm.pagination = {
        pageNumber: 1,
        totalPages: 1
    };

    vm.init = function () {
        vm.page = 1;
        this.get(vm.page);
        localizationService.localize("FormsFinder_Title").then(
            function(value) {
                vm.title = value;
            }
        );
    }


    vm.get = function (page) {
        vm.loading = true;
        $http.get(API_URL + 'Get/?page=' + page )
            .then(function (response) {
                vm.data = response.data;
                vm.loading = false;
            });
    };

    vm.getFormByName = function (page, formName) {
        vm.loading = true;
        $http.get(API_URL + 'GetFormsWithName?page='+ page +'&name=' + formName)
            .then(function (response) {
                vm.data = response.data;
                vm.loading = false;
            });
    };

    vm.openForm = function(formGuid) {
        this.windowOpen("/umbraco#/forms/Form/edit/" + formGuid);
    }

    vm.openContent = function(contentId) {
        this.windowOpen("/umbraco#/content/content/edit/" + contentId);
    }

    vm.change = function (page, searchTerm) {
        this.getFormByName(page, searchTerm);
    }

    vm.windowOpen = function (url) {
        window.open(url).focus();
    }

    vm.changePage = function (pageNumber) {
        vm.getFormByName(pageNumber, $scope.searchTerm);
    }

    vm.init();
});
