$(function () {
    var hideAdd = true;
    var hideDelete = true;
    var hideForm = true;
    var hideEditTodo = true;
    var id;

    $(".sortable").sortable();

    $(".add_todo_list").hide();
    $(".delete_category").hide();
    $(".edit_form_list").hide();
    $(".edit_footer").hide();
    $(".edit_todo_list_btn").hide();
    $(".edit_todo_hide").hide();
    $(".todo").show();


    $("#add_todo_btn").click(function () {
        if (hideAdd == true) {
            $(".add_todo_list").show();
            hideAdd = false;
        } else {
            $(".add_todo_list").hide();
            hideAdd = true;
        }
    });

    $("#delete_category_btn").click(function () {
        if (hideDelete == true) {
            $(".delete_category").show();
            hideDelete = false;
        } else {
            $(".delete_category").hide();
            hideDelete = true;
        }
    })
    
    $("#todo_name").keydown(function () {
        var val = $("#todo_name").val();
        $("#todo_name").css("width", ((val.length + 2) * 10).toString() + 'px');
    })

    $("#todo_description").keydown(function () {
        var val = $("#todo_description").val();
        $("#todo_description").css("width", ((val.length) * 7).toString() + 'px');
    })

    $("#edit_profile_btn").click(function () {
        if (hideForm == true) {
            $(".profile_list").hide();
            $(".edit_form_list").show();
            $(".edit_footer").show();
            hideForm = false;
        } else {
            $(".profile_list").show();
            $(".edit_form_list").hide();
            $(".edit_footer").hide();
            hideForm = true;
        }
    })

    $("#edit_todo_btn").click(function () {
        if (hideEditTodo == true) {
            $(".edit_todo_list_btn").show();
            hideEditTodo = false;
        } else {
            $(".edit_todo_list_btn").hide();
            $(".edit_todo_hide").hide();
            $(".todo").show();
            hideEditTodo = true;
        }
    })

    $(".edit_todo_list_btn").click(function () {
        $(this).parents(".todo").hide();
        $(".edit_todo_list_btn").hide();
        $(".add_todo_list").hide();
        id = $(this).parents(".todo").attr('id');
        $("#edit_todo_list_click_" + id).show();
        $("#edit_todo_listC_click_" + id).show();
    })

    $(".edit_todo_cancel_btn").click(function () {
        $(this).parents(".edit_todo_hide").hide();
        $(".todo").show();
        $(".edit_todo_list_btn").show();

    })

    $("#edit_name_" + id).keydown(function () {
        var val = $("#edit_name_" + id).val();
        $("#edit_name_" + id).css("width", ((val.length + 2) * 10).toString() + 'px');
    })

    $("#edit_description_" + id).keydown(function () {
        var val = $("#edit_description_" + id).val();
        $("#edit_description_" + id).css("width", ((val.length) * 7).toString() + 'px');
    })
});