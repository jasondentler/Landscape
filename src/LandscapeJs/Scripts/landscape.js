var landscape = {};

landscape.viewModel = function () {
    if (typeof model == 'undefined')
        return {};
    return model;
};

landscape.templateFailedToLoad = function () {
    var content = "<div title='Error'>One or more external templates failed to load.</div>";
    $(content).dialog({
        modal: true,
        close: function () {
            window.history.back();
        }
    });
};

$(function () {

    var loadTemplates = function () {
        var items = $("script[src][type='text/html']");
        var promises = [];

        console.log("Loading " + items.length + " external jQuery template(s).");

        // If there are no external jQuery templates to load
        // Return a completed promise to continue to page load process
        if (items.length == 0)
            return $.Deferred().resolve().promise();

        ko.utils.arrayForEach(items, function(item) {
            var url = $(item).attr('src');
            var request = $.get(url);

            request
                .done(function (content) {
                    $(item).html(content);
                })
                .fail(function () {
                    console.log('Failed to load content from ' + this.url);
                });

            promises.push(request);
        });

        // Return a promise that will complete when all the external templates are loaded
        return $.when.apply(this, promises).fail(landscape.templateFailedToLoad);
    };

    var bind = function () {
        var viewModel = landscape.viewModel();
        ko.applyBindings(viewModel);
    };

    loadTemplates().done(bind);

});