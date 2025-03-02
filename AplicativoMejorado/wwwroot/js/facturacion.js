window.onload = function () {
    listarFacturacion();
};

async function listarFacturacion() {
    let objFacturacion = {
        url: "Facturacion/ListarFacturacion",
        cabeceras: ["ID", "ID Paciente", "Monto", "Método de Pago", "Fecha de Pago"],
        propiedades: ["id", "pacienteId", "monto", "metodoPago", "fechaPago"],
        editar: true,
        eliminar: true,
        propiedadId: "id"
    };
    pintar(objFacturacion);
}

function GuardarFacturacion() {
    let forma = document.getElementById("frmGuardarFacturacion");
    let frm = new FormData(forma);

    fetchPost("Facturacion/GuardarFacturacion", "text", frm, function (res) {
        Swal.fire({
            icon: 'success',
            title: 'Éxito',
            text: 'Factura agregada correctamente'
        });
        listarFacturacion();
        cerrarModal();
    });
}

function Eliminar(id) {
    Swal.fire({
        title: "¿Estás seguro?",
        text: "Esta acción no se puede deshacer",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#d33",
        cancelButtonColor: "#3085d6",
        confirmButtonText: "Sí, eliminar"
    }).then((result) => {
        if (result.isConfirmed) {
            fetchGet("Facturacion/EliminarFacturacion/?id=" + id, "json", function (res) {
                if (res) { // Verifica que 'res' tenga la propiedad success
                    Swal.fire({
                        icon: 'success',
                        title: 'Eliminado',
                        text: 'Factura eliminada correctamente'
                    });
                    toastr.success("Factura eliminada correctamente.");

                    // Eliminar foco del modal antes de cerrarlo
                    $("#modalFacturacion").find(":focus").blur();

                    // Cerrar el modal
                    $("#modalFacturacion").modal("hide");

                    // Mover el foco a un elemento visible fuera del modal
                    $("#someOtherElement").focus(); // Asegúrate de poner un selector válido para este elemento

                    listarFacturacion(); // Actualizar la lista de facturación
                } else {
                    Swal.fire({
                        icon: 'error',
                        title: 'Error',
                        text: 'No se pudo eliminar. Puede que la factura no exista.'
                    });
                    toastr.error("No se pudo eliminar la factura.");
                }
            });
        }
    });
}



function Editar(id) {
    fetchGet("Facturacion/RecuperarFacturacion/?id=" + id, "json", function (data) {
        setN("id", data.id);
        setN("pacienteId", data.pacienteId);
        setN("monto", data.monto);
        setN("metodoPago", data.metodoPago);
        setN("fechaPago", data.fechaPago);
        abrirModal();
    });
}

function GuardarCambiosFacturacion() {
    let forma = document.getElementById("frmGuardarFacturacion");
    let frm = new FormData(forma);

    fetchPost("Facturacion/GuardarCambiosFacturacion", "text", frm, function (res) {
        Swal.fire({
            icon: 'success',
            title: 'Éxito',
            text: 'Factura modificada correctamente'
        });
        listarFacturacion();
        cerrarModal();
    });
}

function abrirModal() {
    let modalEl = document.getElementById("modalFacturacion");
    if (!modalEl) {
        console.error("Error: No se encontró el modal con id 'modalFacturacion'.");
        return;
    }
    let modal = new bootstrap.Modal(modalEl);
    modal.show();
}

function cerrarModal() {
    let modalEl = document.getElementById("modalFacturacion");
    let modal = bootstrap.Modal.getInstance(modalEl);
    if (modal) {
        modal.hide();
    }
}
