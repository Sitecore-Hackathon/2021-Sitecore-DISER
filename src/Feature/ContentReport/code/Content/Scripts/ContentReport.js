$(document).ready(function () {

    // Datepicker initialisation
    var options = {
        format: 'mm/dd/yyyy',
        todayHighlight: true,
        autoclose: true
    }
    $('#fromdate').datepicker(options);
    $('#todate').datepicker(options);


});