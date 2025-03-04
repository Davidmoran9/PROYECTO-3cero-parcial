window.onload = function () {
    listarTratamientos();
};

async function listarTratamientos() {
    let objTratamientos = {
        url: "Tratamientos/ListarTratamientos",
        cabeceras: ["ID Tratamiento", "ID Paciente", "Descripción", "Fecha", "Costo"],
        propiedades: ["id", "pacienteId", "descripcion", "fecha", "costo"],
        editar: true,
        eliminar: true,
        propiedadId: "id"
    };
    pintar(objTratamientos);
}

function GuardarTratamiento() {
    let forma = document.getElementById("frmAgregarTratamiento");
    if (!forma.checkValidity()) {
        forma.reportValidity();
        return;
    }
    let frm = new FormData(forma);

    fetchPost("Tratamientos/GuardarTratamiento", "text", frm, function (res) {
        Swal.fire({
            icon: 'success',
            title: 'Éxito',
            text: 'Tratamiento agregado correctamente'
        });
        listarTratamientos();
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
            fetchGet("Tratamientos/EliminarTratamiento/?id=" + id, "json", function (res) {
                if (res) {
                    Swal.fire({
                        icon: 'success',
                        title: 'Eliminado',
                        text: 'Tratamiento eliminado correctamente'
                    });
                    toastr.success("Tratamiento eliminado correctamente.");

                    // Cerrar modal si está abierto
                    $("#modalTratamiento").modal("hide");
                    document.activeElement.blur();

                    listarTratamientos();
                } else {
                    Swal.fire({
                        icon: 'error',
                        title: 'Error',
                        text: 'No se pudo eliminar. Puede que el tratamiento no exista.'
                    });
                    toastr.error("No se pudo eliminar el tratamiento.");
                }
            });
        }
    });
}

function Editar(id) {
    fetchGet("Tratamientos/RecuperarTratamiento/?id=" + id, "json", function (data) {
        setN("id", data.id);
        setN("pacienteId", data.pacienteId);
        setN("descripcion", data.descripcion);
        setN("fecha", data.fecha);
        setN("costo", data.costo);
        abrirModal();
    });
}

function GuardarCambiosTratamiento() {
    let forma = document.getElementById("frmEditarTratamiento");
    if (!forma.checkValidity()) {
        forma.reportValidity();
        return;
    }
    let frm = new FormData(forma);

    fetchPost("Tratamientos/GuardarCambiosTratamiento", "text", frm, function (res) {
        Swal.fire({
            icon: 'success',
            title: 'Éxito',
            text: 'Tratamiento modificado correctamente'
        });
        listarTratamientos();
        cerrarModal();
    });
}

function abrirModal() {
    let modalEl = document.getElementById("modalEditarTratamiento");
    if (!modalEl) {
        console.error("Error: No se encontró el modal con id 'modalTratamiento'.");
        return;
    }
    let modal = new bootstrap.Modal(modalEl);
    modal.show();
}

function cerrarModal() {
    let modalEl = document.getElementById("modalEditarTratamiento");
    let modal = bootstrap.Modal.getInstance(modalEl);
    if (modal) {
        modal.hide();
    }
}
