/// <reference path="jquery-1.6.2.js"/>
/// <reference path="jQuery.tmpl.js"/>
/// <reference path="knockout-1.2.1.debug.js"/>
/// <reference path="jquery-ui-1.8.14.js"/>
/// <reference path="landscape.js"/>

module('viewModel function');

test('Undefined model returns empty view model', function () {
    window.model = undefined;
    var actual = landscape.viewModel();
    deepEqual(actual, {});
})

test('Defined model returns model as view model', function () {
    var expected = { name: 'Landscape' };
    window.model = expected;
    var actual = landscape.viewModel();
    equal(actual, expected);
});

module('templateFailedToLoad function');

test('Displays a modal UI dialog', function() {
    landscape.templateFailedToLoad();
});
