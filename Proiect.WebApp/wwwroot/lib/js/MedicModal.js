$(".modalButton").click(function () {
    let id = $(this).data("id");
    $(`#${id}`).modal();
});