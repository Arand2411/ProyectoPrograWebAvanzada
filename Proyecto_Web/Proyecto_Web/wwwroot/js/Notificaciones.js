document.addEventListener('DOMContentLoaded', function () {
    if (window.ViewBagSuccess !== undefined && window.ViewBagMessage !== undefined) {
        if (window.ViewBagSuccess === true) {
            Swal.fire({
                icon: 'success',
                title: 'Éxito',
                text: window.ViewBagMessage
            });
        } else {
            Swal.fire({
                icon: 'error',
                title: 'Error',
                text: window.ViewBagMessage
            });
        }
    }
});
