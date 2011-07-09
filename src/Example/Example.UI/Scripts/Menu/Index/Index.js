/// <reference path="../../lib/landscape.js"/>

function pageIndexer(pageCount) {
    var pageNumbers = [];
    for (var i = 1; i <= pageCount; i++) {
        pageNumbers.push(i);
    }
    return pageNumbers;
}

function formatCurrency(amount) {
    return '$' + parseFloat(amount).toFixed(2);
}