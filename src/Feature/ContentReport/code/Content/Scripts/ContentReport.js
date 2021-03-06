$(document).ready(function () {

    // Datepicker initialisation
    var options = {
        format: 'dd/mm/yyyy',
        todayHighlight: true,
        autoclose: true
    }
    $('#fromdate').datepicker(options);
    $('#todate').datepicker(options);


    // Tab Data tables    
    $('#dt-tab-*').DataTable({
        //data: data,
        "paging": true,
        "ordering": true,
        "pagingType": "full_numbers"
    });


    processDownload = function (e, type) {
        var params = {
            "Type": type
        };
        downloadReport(params)
    };

    downloadReport = function (params) {
        return $.ajax({
            url: "/DownloadApi/DownloadReport",
            type: 'Post',
            data: params,
            xhrFields: {
                responseType: 'blob'
            },
            success: function (response, status, xhr) {
                var filename = "";
                var disposition = xhr.getResponseHeader('Content-Disposition');
                if (disposition && disposition.indexOf('attachment') !== -1) {
                    var filenameRegex = /filename[^;=\n]*=((['"]).*?\2|[^;\n]*)/;
                    var matches = filenameRegex.exec(disposition);
                    if (matches != null && matches[1]) filename = matches[1].replace(/['"]/g, '');
                }
                saveAs(response, filename);
            }
        });
    }

    generateReport = function (e) {
        var params = {
            "StartDate": $('#fromdate').val(),
            "EndDate": $('#todate').val()
        };
        getReport(params);
    };

    getReport = function (params) {
        return $.ajax({
            url: "/ReportApi/GetReport",
            type: 'Post',
            data: params,
            success: function (results) {
                $(results).each(function (index, result) {
                    console.log(result);
                    $('#summary-label').html("Content Report Details");
                    $('#summary-label').addClass("cr-light");
                    $('#item-created-label').text("Total Items Created : ");
                    $('#item-created').text(" " + result.CreatedPages);
                    $('#item-updated-label').text("Total Items Updated : ");
                    $('#item-updated').text(" " + result.UpdatedPages);
                    $('#item-archived-label').text("Total Items Archived : ");
                    $('#item-archived').text(" " + result.ArchivedPages);
                    $('#detail-label').text("Form more details - check other tabs");
                    $('#detail-label').addClass("cr-light");
                    $('#summary-table').addClass('cr-border');
                });
            }
        });
    }
});