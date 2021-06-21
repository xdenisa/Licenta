Handlebars.registerHelper('isvalid', function (value) {
    var today = new Date();
    return value > today;
});