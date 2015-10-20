$(document).ready(function () {
    $(".customerDetails").click(function (evt) {
        var cell = $(evt.target).closest("tr").children().first();
        var custID = cell.val();
        $("#detailsView").load("/customers/getview",
        { customerID: custID, viewName: "CustomerDetails" });
    });

    $(".orderDetails").click(function (evt) {
        var cell = $(evt.target).closest("tr").children().first();
        var custID = cell.val();
        $("#detailsView").load("/customers/getview",
        { customerID: custID, viewName: "OrderDetails" });
    });
});
