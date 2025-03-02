window.onload = function () {
    listarCitas();
};

async function listarCitas() {
    let objCitas = {
        url: "Citas/listarCitas",
        cabeceras: ["ID Cita", "Paciente", "Médico", "Fecha y Hora", "Estado"],
        propiedades: ["id", "pacienteId", "medicoId", "fechaHora", "estado"],
        editar: true,
        eliminar: true,
        propiedadId: "id"
    };
    pintar(objCitas);
}

function GuardarCita() {
    let forma = document.getElementById("frmGuardarCita");
    let frm = new FormData(forma);

    fetchPost("Citas/GuardarCita", "text", frm, function (res) {
        Swal.fire({
            icon: 'success',
            title: 'Éxito',
            text: 'Cita agregada correctamente'
        });
        listarCitas();
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
            fetchGet("Citas/EliminarCita/?id=" + id, "json", function (res) {


                if (res) {
                    Swal.fire({
                        icon: 'success',
                        title: 'Eliminado',
                        text: 'Cita eliminada correctamente'
                    });
                    toastr.success("Cita eliminada correctamente.");
                    listarCitas();
                } else {
                    Swal.fire({
                        icon: 'error',
                        title: 'Error',
                        text: 'No se pudo eliminar. Puede que la cita no exista.'
                    });
                    toastr.error("No se pudo eliminar la cita.");
                }
            });
        }
    });
}

function Editar(id) {
    fetchGet("Citas/recuperarCita/?id=" + id, "json", function (data) {
        setN("id", data.id);
        setN("pacienteId", data.pacienteId);
        setN("medicoId", data.medicoId);
        setN("fechaHora", data.fechaHora);
        setN("estado", data.estado);
        abrirModal();
    });
}

function GuardarCambiosCita() {
    let forma = document.getElementById("frmGuardarCita");
    let frm = new FormData(forma);

    fetchPost("Citas/GuardarCambiosCita", "text", frm, function (res) {
        Swal.fire({
            icon: 'success',
            title: 'Éxito',
            text: 'Cita modificada correctamente'
        });
        listarCitas();
        cerrarModal();
    });
}
function abrirModal() {
    let modalEl = document.getElementById("modalCita");
    if (!modalEl) {
        console.error("Error: No se encontró el modal con id 'modalCita'.");
        return;
    }
    let modal = new bootstrap.Modal(modalEl);
    modal.show();
}

function cerrarModal() {
    let modalEl = document.getElementById("modalCita");
    let modal = bootstrap.Modal.getInstance(modalEl);
    if (modal) {
        modal.hide();
    }
}
