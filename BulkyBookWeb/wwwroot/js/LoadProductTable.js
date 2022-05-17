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
                        <a onclick="DeleteProduct(${data})" class="btn btn-danger">Delete</a>
                    `;
                }
            }
        ]
    })
}

function DeleteProduct(id) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax(
                {
                    url: "/Admin/Product/DeleteProduct?id=" + id,
                    type: "Delete",
                    success: function (data) {
                        if (data.success) {
                            toastr.success(data.message);
                            dataTable.ajax.reload();
                        } else {
                            toastr.error(data.message);
                        }
                    }
                }
            )
        }
    })
}