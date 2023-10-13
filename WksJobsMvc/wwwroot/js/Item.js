$(document).ready(function () {
    $("#customerDatatable").DataTable({
        "processing": true,
        "serverSide": true,
        "filter": true,
        "sort":true,
        "ajax": {
            "url": "/Item/GetItems",
            "type": "POST",
            "datatype": "json"
        },
        "columnDefs": [{
            "targets": [0],
            "visible": false,
            "searchable": false
        }, {
                "targets": [5],
            "searchable": false,
                "sortable": false,
            }],
        "columns": [
            { "data": "id", "name": "Id", "autoWidth": true },
            { "data": "itemCode", "name": "itemCode", "autoWidth": true },
            { "data": "materialDescription", "name": "materialDescription", "autoWidth": true },
            { "data": "perUnitCost", "name": "perUnitCost", "autoWidth": true },
            { "data": "unitOfMeasurement", "name": "unitOfMeasurement", "autoWidth": true },
            {
                "data": "id", render: function (data, row) {
                    return "<div class='btn-group btn-group-sm' role='group' aria-label='Button group with nested dropdown'><a href='Item/Edit/" + data + "' class='btn btn-success'> <i class='fa-sharp fa-solid fa-pen-to-square'></i> Edit Item</a> <div class='btn-group btn-group-sm' role='group'><button id='btnGroupDrop1' type='button' class='btn btn-primary dropdown-toggle' data-bs-toggle='dropdown' aria-expanded='false'>Action</button><ul class='dropdown-menu' aria-labelledby='btnGroupDrop1'><li><a class='dropdown-item' href='Item/Details/" + data + "'>Details</a></li><li><a class='dropdown-item'  href='Item/Delete/" + data + "'>Delete Item</a></li></ul></div></div>";
                }
            }
        ]
    });
});


