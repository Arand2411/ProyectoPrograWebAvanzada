$(document).ready(function () {
    $('#tabla-Usuarios').DataTable({
        paging: false, // Desactiva la paginación
        language: {
            url: '//cdn.datatables.net/plug-ins/2.0.2/i18n/es-ES.json'
        },
        columnDefs: [{ type: 'string', target: [0] }]
    });
});


$(document).on("click", ".Modales", function () {
    $("#Consecutivo").val($(this).attr("data-id"));
    $("#Nombre").text($(this).attr("data-name"));
});
