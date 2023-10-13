var myModal = new bootstrap.Modal(document.getElementById('itemModal'), {});



$("#createDetailEntry").click(function () {
    var jobid = $("#jobid").val();
    var itemcode = $("#ajaxSelect2").val();
    var itemid = $("#ajaxSelect2").val();
    var itemqty = $("#item_qty").val();
    var itempuc = $("#item_puc").val();
    var itemnettotal = parseFloat(itemqty * itempuc);
    $.ajax({
        url: '/JobItemDetail/RespondToThis',
        type: 'post',
        data: {
            Id:0,
            ItemCode: itemcode,
            ItemQty: itemqty,
            ItemRate: itempuc,
            ItemNetTotal: itemnettotal,
            JobId: jobid,
            ItemId: itemid,
            __RequestVerificationToken: gettoken(),
        },
        success: function (result) {

            let msg = result.substring(0, 3);

            if (msg == 'Err') {
                $('#toastwarning').toast('show');
                $('#toastwarning .toast-body').html(result);
            } else {
                $('#toastsuccess').toast('show');
                $('#toastsuccess .toast-body').html(result);
            }
            myModal.hide();
        }, error: function (result) {
            $('#toasterror').toast('show');
            $('#toasterror .toast-body').html("Cannot perform operation at the moment.");
            myModal.hide();
        }
    });
});




$("#item_code, #item_qty, #item_puc").blur(function () {
    CalculateTotal();
});

function ResetEverything() {
    $("#ajaxSelect2").val('').trigger('change');
    $("#item_qty").val(1);
    $("#hid_itemcode").val(0);
    $("#item_puc").val(1);
}

function CalculateTotal() {
    var quantity = parseFloat($("#item_qty").val());
    var price = parseFloat($("#item_puc").val());
    $("#ttl_of_item").html(quantity*price);
}


$(document).ready(function () {
    $("#jobDatatable").DataTable({
        "processing": true,
        "serverSide": true,
        "filter": true,
        "sort": true,
        "ajax": {
            "url": "/Job/GetJobs",
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
            { "data": "jobCode", "name": "jobCode", "autoWidth": true },
            { "data": "jobTitle", "name": "jobTitle", "autoWidth": true },
            { "data": "jobDescription", "name": "jobDescription", "autoWidth": true },
            { "data": "jobStatus", "name": "jobStatus", "autoWidth": true },
            {
                "data": "id", render: function (data, row) {
                    return "<div class='btn-group btn-group-sm' role='group' aria-label='Button group with nested dropdown'><a href='Job/Edit/" + data + "' class='btn btn-success'> <i class='fa-sharp fa-solid fa-pen-to-square'></i> Edit Job</a>  <div class='btn-group btn-group-sm' role='group'><button id='btnGroupDrop1' type='button' class='btn btn-primary dropdown-toggle' data-bs-toggle='dropdown' aria-expanded='false'>Action</button><ul class='dropdown-menu' aria-labelledby='btnGroupDrop1'><li><a class='dropdown-item ids' onclick=JobItemAddClick(" + data + ") id='btn_itm_add_" + data + "' data-job-id='" + data + "' href='#openitemmodal'>Add Item</a></li><li><a class='dropdown-item'  href='Job/Finalize/" + data + "'>Finalize Job</a><li><a class='dropdown-item'  href='Job/Delete/" + data + "'>Delete Job</a></li></ul></div></div>";
                }
            }
        ]
    });


    $('.ids').click(function () {
        var thisjobid = $(this).data('job-id');
        $("#jobid").val(thisjobid);
        myModal.show();
        ResetEverything();
        CalculateTotal();
    });

});


function JobItemAddClick(id) {
    //var thisjobid = $(this).data('job-id');
    var thisjobid = id;
    $("#jobid").val(thisjobid);
    myModal.show();
    ResetEverything();
    CalculateTotal();
}



$("#simpleSelect2").select2({
    dropdownParent: $('#itemModal'),
    theme: 'classic',
    placeholder: "Select a Static Value",
    allowClear: true
});

$("#ajaxSelect2").select2({
    placeholder: "Select a Value from API Data",
    dropdownParent: $('#itemModal'),
    theme: "classic",
    minimumInputLength: 4,
    allowClear: true,
    ajax: {
        url: "/ItemList/search",
        contentType: "application/json; charset=utf-8",
        data: function (params) {
            var query =
            {
                term: params.term,
            };
            return query;
        },
        processResults: function (result) {
            return {
                results: $.map(result, function (item) {
                    return {
                        id: item.id,
                        itemcode: item.itemCode,
                        itemdescription: item.materialDescription,
                        itemperunitcost: item.perUnitCost,
                        text: 'Item code: ' + item.itemCode + ' Item description: ' + item.materialDescription + ' Per unit cost: ' + item.perUnitCost
                    };
                }),
            };
        }
    }
});

$('#ajaxSelect2').on('select2:select', function (e) {
    var data = e.params.data;
    $("#item_qty").val(1);
    $("#hid_itemcode").val(data.itemcode);
    $("#item_puc").val(data.itemperunitcost);
    CalculateTotal();
    console.log(data);
});


function formatRepo(repo) {
    if (repo.loading) {
        return repo.text;
    }

    var $container = $(
        "<div class='select2-result-repository__title'></div>" +
        "<div class='select2-result-repository__description'></div>"
    );

    $container.find(".select2-result-repository__title").text(repo.itemCode);
    $container.find(".select2-result-repository__description").text(repo.materialDescription);

    return $container;
}

function formatRepoSelection(repo) {
    return repo.itemCode || repo.materialDescription;
}

