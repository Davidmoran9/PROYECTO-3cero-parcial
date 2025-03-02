window.onload = function () {
    listarMedicos();
};

async function listarMedicos() {
    let objMedicos = {
        url: "Medicos/ListarMedicos",
        cabeceras: ["ID Médico", "Nombre", "Apellido", "Especialidad", "Teléfono", "Email"],
        propiedades: ["id", "nombre", "apellido", "especialidadId", "telefono", "email"],
        editar: true,
        eliminar: true,
        propiedadId: "id"
    };
    pintar(objMedicos);
}

function GuardarMedico() {
    let forma = document.getElementById("frmGuardarMedico");
    let frm = new FormData(forma);

    fetchPost("Medicos/GuardarMedico", "text", frm, function (res) {
        Swal.fire({
            icon: 'success',
            title: 'Éxito',
            text: 'Médico agregado correctamente'
        });
        listarMedicos();
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
            fetchGet("Medicos/EliminarMedico/?id=" + id, "json", function (res) {
                if (res) {
                    Swal.fire({
                        icon: 'success',
                        title: 'Eliminado',
                        text: 'Médico eliminado correctamente'
                    });
                    listarMedicos();
                } else {
                    Swal.fire({
                        icon: 'error',
                        title: 'Error',
                        text: 'No se pudo eliminar. Puede que el médico no exista.'
                    });
                }
            });
        }
    });
}

function Editar(id) {
    fetchGet("Medicos/RecuperarMedico/?id=" + id, "json", function (data) {
        setN("id", data.id);
        setN("nombre", data.nombre);
        setN("apellido", data.apellido);
        setN("especialidadId", data.especialidadId);
        setN("telefono", data.telefono);
        setN("email", data.email);
        abrirModal();
    });
}

function GuardarCambiosMedico() {
    let forma = document.getElementById("frmGuardarMedico");
    let frm = new FormData(forma);

    fetchPost("Medicos/GuardarCambiosMedico", "text", frm, function (res) {
        Swal.fire({
            icon: 'success',
            title: 'Éxito',
            text: 'Médico modificado correctamente'
        });
        listarMedicos();
        cerrarModal();
    });
}

function abrirModal() {
    let modalEl = document.getElementById("modalMedico");
    if (!modalEl) {
        console.error("Error: No se encontró el modal con id 'modalMedico'.");
        return;
    }
    let modal = new bootstrap.Modal(modalEl);
    modal.show();
}

function cerrarModal() {
    let modalEl = document.getElementById("modalMedico");
    let modal = bootstrap.Modal.getInstance(modalEl);
    if (modal) {
        modal.hide();
    }
}
