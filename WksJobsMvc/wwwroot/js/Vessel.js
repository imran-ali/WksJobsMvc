$(document).ready(function () {
    $("#vesselDatatable").DataTable({
        "processing": true,
        "serverSide": true,
        "filter": true,
        "sort": true,
        "ajax": {
            "url": "/Vessel/GetVessels",
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
            { "data": "vesselCode", "name": "VesselCode", "autoWidth": true },
            { "data": "vesselType", "name": "VesselType", "autoWidth": true },
            { "data": "vesselName", "name": "VesselName", "autoWidth": true },
            {
                "data": "createDate", "name":"CreateDate", render: function (data, row) {
                    return moment(data).format("DD-MMM-YYYY");
                }
            },
            {
                "data": "id", render: function (data, row) {
                    return "<div class='btn-group btn-group-sm' role='group' aria-label='Button group with nested dropdown'><a href='Vessel/Edit/" + data + "' class='btn btn-success'> <i class='fa-sharp fa-solid fa-pen-to-square'></i> Edit Vessel</a> <div class='btn-group btn-group-sm' role='group'><button id='btnGroupDrop1' type='button' class='btn btn-primary dropdown-toggle' data-bs-toggle='dropdown' aria-expanded='false'>Action</button><ul class='dropdown-menu' aria-labelledby='btnGroupDrop1'><li><a class='dropdown-item' href='Vessel/Details/" + data + "'>Details</a></li><li><a class='dropdown-item'  href='Vessel/Delete/" + data + "'>Delete Vessel</a></li></ul></div></div>";
                }
            }
        ]
    });
});