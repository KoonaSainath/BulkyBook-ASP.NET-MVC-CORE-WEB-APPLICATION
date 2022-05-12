var dataTable;

$(document).ready(function () {
    loadProductTable();
})

function loadProductTable() {
    dataTable = $('#productsTable').DataTable({
        "ajax": {
            "url": "/Admin/Product/GetAllProducts"
        },
        "columns": [
            { "data": "title" },
            { "data": "isbn" },
            { "data": "price" }
        ]
    })
}