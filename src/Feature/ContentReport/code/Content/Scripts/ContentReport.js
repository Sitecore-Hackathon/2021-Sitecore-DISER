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
        "paging": true,
        "ordering": true,
        "pagingType": "full_numbers"
    });
    //$('.dataTables_length').addClass('bs-select');

    $('#download-button').on('click', function (e) {
        e.preventDefault();
        e.stopImmediatePropagation();
        var params = {
            "Type": "CreatedItems"
        };
        processDownload(params)
    });

    processDownload = function (params) {
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
});