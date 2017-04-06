var CmsApiProxy = function () {
    var self = this;
    var m = {};

    self.GetAllContent = function () {
        return $.ajax({
            url: '/api/cms/content',
            type: 'GET',
            contentType: 'application/json',
            dataType: 'json',
        });
    }

    self.GetContent = function (contentId) {
        return $.ajax({
            url: '/api/cms/content/' + contentId,
            type: 'GET',
            contentType: 'application/json',
            dataType: 'json',
        });
    }

    self.GetAllLocations = function () {
        return $.ajax({
            url: '/api/cms/location',
            type: 'GET',
            contentType: 'application/json',
            dataType: 'json',
        });
    }

    self.GetLocation = function (locationId) {
        return $.ajax({
            url: '/api/cms/location/' + locationId,
            type: 'GET',
            contentType: 'application/json',
            dataType: 'json',
        });
    }

    //TODO-SM neat trick, think I like it, keeps the items defined here and doesn't let c# affect js
    self.GetLocationRelationships = function (locationId) {
        return m.Call('Get', '/api/cms/location/' + locationId + '/relationship',
            function (data) {
                if (!data) {
                    return [];
                }

                var relationships = data.map(function (item) { 
                    return {
                        contentId: item.ContentId,
                        priority: item.Priority,
                        requirements: item.Requirements.map(function (reqItem) {
                            return {
                                key: reqItem.Key,
                                value: reqItem.Value,
                                matchType: reqItem.MatchType,
                            }
                        }),
                    };
                });
                
                return relationships;
            });
    }

    m.Call = function (method, url, buildData) {
        return new SpecialPromise($.ajax({
            url: url,
            type: method,
            contentType: 'application/json',
            dataType: 'json',
        }), buildData);
    }
}

var SpecialPromise = function (jqPromise, buildViewmodelFromData) {
    var self = this;
    var m = {};

    m.promise = jqPromise;
    m.sucessCallbacks = [];
    m.buildViewmodelFromData = buildViewmodelFromData;

    self.done = function (callback) {
        m.sucessCallbacks.push(callback);
    }

    self.fail = function (callback) {
        m.promise.fail(callback);
    }

    self.always = function (callback) {
        m.promise.always(callback);
    }

    //shim in the transform
    m.promise.done(function (data) {
        m.sucessCallbacks.forEach(function (item) {
            item(buildViewmodelFromData(data));
        });
    })
}