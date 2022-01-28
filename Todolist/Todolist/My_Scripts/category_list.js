$(function () {
    var icon = 1;
    $(".ctg").hide();


    $("#category_list").click(function () {
        if (icon == 1) {
            $("#category_list_icon").removeClass("fa-stream", function () {
                $(this).addClass("fa-angle-down", function () {
                    $(".ctg").slideToggle();
                    icon = 2;
                })
            })
        } else if (icon == 2) {
            $("#category_list_icon").removeClass("fa-angel-down", function () {
                $(this).addClass("fa-stream", function () {
                    $(".ctg").slideToggle();
                    icon = 1;
                });
            })
        }
    });
})