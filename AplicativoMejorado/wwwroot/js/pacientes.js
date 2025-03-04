window.onload = function () {
    let fechaInput = document.getElementById('fechaNacimiento');
    let hoy = new Date().toISOString().split('T')[0];
    fechaInput.setAttribute('max', hoy);
    listarPacientes();
};

async function listarPacientes() {
    let objPacientes = {
        url: "Pacientes/ListarPacientes",
        cabeceras: ["ID Paciente", "Nombre", "Apellido", "Fecha Nacimiento", "Teléfono", "Email", "Dirección"],
        propiedades: ["id", "nombre", "apellido", "fechaNacimiento", "telefono", "email", "direccion"],
        editar: true,
        eliminar: true,
        propiedadId: "id"
    };
    pintar(objPacientes);
}

function GuardarPaciente() {
    let forma = document.getElementById("frmAgregarPaciente");

    if (!forma.checkValidity()) {
        forma.reportValidity();
        return;
    }

    let frm = new FormData(forma);

    fetchPost("Pacientes/GuardarPaciente", "text", frm, function (res) {
        Swal.fire({
            icon: 'success',
            title: 'Éxito',
            text: 'Paciente agregado correctamente'
        });
        listarPacientes();
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
            fetchGet("Pacientes/EliminarPaciente/?id=" + id, "json", function (res) {
                if (res) {
                    Swal.fire({
                        icon: 'success',
                        title: 'Eliminado',
                        text: 'Paciente eliminado correctamente'
                    });
                    listarPacientes();
                } else {
                    Swal.fire({
                        icon: 'error',
                        title: 'Error',
                        text: 'No se pudo eliminar. Puede que el paciente no exista.'
                    });
                }
            });
        }
    });
}

function Editar(id) {
    fetchGet("Pacientes/RecuperarPaciente/?id=" + id, "json", function (data) {
        setN("id", data.id);
        setN("nombre", data.nombre);
        setN("apellido", data.apellido);
        setN("fechaNacimiento", data.fechaNacimiento);
        setN("telefono", data.telefono);
        setN("email", data.email);
        setN("direccion", data.direccion);
        abrirModal();
    });
}

function GuardarCambiosPaciente() {
    let forma = document.getElementById("frmEditarPaciente");

    if (!forma.checkValidity()) {
        forma.reportValidity();
        return;
    }

    let frm = new FormData(forma);

    fetchPost("Pacientes/GuardarCambiosPaciente", "text", frm, function (res) {
        Swal.fire({
            icon: 'success',
            title: 'Éxito',
            text: 'Paciente modificado correctamente'
        });
        listarPacientes();
        cerrarModal();
    });
}

function abrirModal() {
    let modalEl = document.getElementById("modalEditarPaciente");
    if (!modalEl) {
        console.error("Error: No se encontró el modal con id 'modalEditarPaciente'.");
        return;
    }
    let modal = new bootstrap.Modal(modalEl);
    modal.show();
}

function cerrarModal() {
    let modalEl = document.getElementById("modalEditarPaciente");
    let modal = bootstrap.Modal.getInstance(modalEl);
    if (modal) {
        modal.hide();
    }
}
