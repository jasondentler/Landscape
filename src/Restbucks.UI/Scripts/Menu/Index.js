/// <reference path="../Shared/api.js"/>
/// <reference path="../libs/landscape.js"/>
landscape.viewModel = function () {
    var url = apiRoot + 'menu';
    var model = {};
    $.ajax({
        url: url,
        dataType: 'json',
        async: false
    })
    .done(function (content) {
        model = content;
    });
    console.log(model);
    return model;
};