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

});