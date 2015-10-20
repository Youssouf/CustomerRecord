//$(document).ready(function () {
//    $(".customerDetails").click(function (evt) {
//        var cell = $(evt.target).closest("tr").children().first();
//        var custID = cell.val();
//        $("#detailsView").load("/customers/getview",
//        { customerID: custID, viewName: "CustomerDetails" });
//    });

//    $(".orderDetails").click(function (evt) {
//        var cell = $(evt.target).closest("tr").children().first();
//        var custID = cell.val();
//        $("#detailsView").load("/customers/getview",
//        { customerID: custID, viewName: "OrderDetails" });
//    });
//});

    $(document).ready(function () {

        $(".notfoundbox").fadeOut(5000);
    });
    $(document).ready(function () {

        $(".empdetailsbox").fadeOut(50000);
    });


