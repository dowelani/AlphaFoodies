

/*Runs when the is a type of staff selected*/
function hideContent(type) {
    if (type === 'waiter') {

        document.getElementById('password_details').style.display = 'none';
    }
    else {
        document.getElementById('password_details').style.display = 'block';
    }
}

/*Delete function*/
//function deleteItem() {

//    console.log("Function entered");
//    var item_Code = $(this).data("model-id");
//    $.ajax({
//        url: "/Admin/deleteMenuItemPost" + item_Code,
//        type: "Post",
//    }).done(new function () {
//        alert("a menu item is successfully deleted")

//    }).ajaxError(new function () {
//        alert("Failed to remove an item")
//    });
//}
