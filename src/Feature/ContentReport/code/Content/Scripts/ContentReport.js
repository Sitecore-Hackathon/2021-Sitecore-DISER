$(document).ready(function () {

    // Datepicker initialisation
    var options = {
        format: 'd M yyyy',
        todayHighlight: true,
        endDate: "0d",
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
        if (validateInput()) {
            var params = {
                "StartDate": $('#fromdate').val(),
                "EndDate": $('#todate').val(),
                "Type": type
            };
            downloadReport(params)
        }
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
        if (validateInput()) {
            var params = {
                "StartDate": $('#fromdate').val(),
                "EndDate": $('#todate').val()
            };
            getReport(params);
        }
    };

    validateInput = function () {
        $('#error-message').empty();

        if (!$('#fromdate').val() || !$('#todate').val()) {
            $('#error-message').append("Please provide a date range");
            return;
        }

        var startDate = new Date($('#fromdate').val());
        var endDate = new Date($('#todate').val());
        if (startDate <= endDate) {
            return true;
        }
        else {
            $('#error-message').append("Start date cannot be greater than End date");
            return false;
        }
    }

    generateTabReport = function (e, type) {
        if (validateInput()) {
            var params = {
                "StartDate": $('#fromdate').val(),
                "EndDate": $('#todate').val(),
                "Type": type
            };
            getReport(params);
        }
    };

    getReport = function (params) {
        return $.ajax({
            url: "/ReportApi/GetReport",
            type: 'Post',
            data: params,
            success: function (results) {
                $(results).each(function (index, result) {
                    $('#summary-label').html("CONTENT REPORT DETAILS");
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