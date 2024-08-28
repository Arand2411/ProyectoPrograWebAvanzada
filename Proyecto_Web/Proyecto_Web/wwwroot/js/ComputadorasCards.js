function ValidarNumeros(evt) {
  var charCode = evt.charCode;

  if (charCode >= 48 && charCode <= 57) {
    return true;
  }
  else {
    return false;
  }
}

function AgregarProducto(IdProducto, Inventario) {

  let cantidad = $("#prd-" + IdProducto).val();

  if (cantidad > Inventario) {
    MostrarMensajeInformativo("Información","Debe ingresar una cantidad inferior al inventario", "info");
    return;
  }

  if (cantidad <= 0) {
    MostrarMensajeInformativo("Información", "Debe ingresar una cantidad válida", "info");
    return;
  }

  $.ajax({
    type: "POST",
    url: "/Carrito/RegistrarCarrito",
    datatype: "json",
    data: {
      "IdProducto": IdProducto,
      "Cantidad": cantidad
    },
    success: function (response) {
      MostrarMensajeAccion("Información", "Producto agregado correctamente", "success");
    }
  });

}

function MostrarMensajeInformativo(titulo, texto, icono) {
  Swal.fire({
    title: titulo,
    text: texto,
    icon: icono
  });
}

function MostrarMensajeAccion(titulo, texto, icono) {
  Swal.fire({
    title: titulo,
    text: texto,
    icon: icono,
    showDenyButton: false,
    showCancelButton: false,
    confirmButtonText: "Aceptar",
  }).then((result) => {
    if (result.isConfirmed) {
        window.location.href = "/Producto/ConsultarComputadoras";
    }
  });

}