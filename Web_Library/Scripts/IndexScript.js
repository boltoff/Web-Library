$("#right-link").hover(function () {
    $("#left-link").addClass("hover");
}, function () {
    $("#left-link").removeClass("hover");
});
$("#left-link").hover(function () {
    $("#right-link").addClass("hover");
}, function () {
    $("#right-link").removeClass("hover");
});