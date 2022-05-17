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
            { "data": "title", "width": "15%" },
            { "data": "isbn", "width": "15%" },
            { "data": "price", "width": "15%" },
            { "data": "category.name", "width": "15%" },
            { "data": "coverType.name", "width": "15%" },
            {
                "data": "id",
                "width": "15%",
                "render": function (data) {
                    return `
                        <a href="/Admin/Product/UpsertProduct?id=${data}" class="btn btn-secondary">Edit</a>
                        <a class="btn btn-danger">Delete</a>
                    `;
                }
            }
        ]
    })
}