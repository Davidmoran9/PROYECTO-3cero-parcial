window.onload = function () {
    listarEspecialidades();
};

async function listarEspecialidades() {
    let objEspecialidades = {
        url: "Especialidades/ListarEspecialidades",
        cabeceras: ["ID Especialidad", "Nombre"],
        propiedades: ["id", "nombre"],
        editar: true,
        eliminar: true,
        propiedadId: "id"
    };
    pintar(objEspecialidades);
}

function GuardarEspecialidad() {
    let forma = document.getElementById("frmAgregarEspecialidad");

    if (!forma.checkValidity()) {
        forma.reportValidity();
        return;
    }

    let frm = new FormData(forma);

    fetchPost("Especialidades/GuardarEspecialidad", "text", frm, function (res) {
        Swal.fire({
            icon: 'success',
            title: 'Éxito',
            text: 'Especialidad agregada correctamente'
        });
        listarEspecialidades();
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
            fetchGet("Especialidades/EliminarEspecialidad/?id=" + id, "json", function (res) {
                if (res) {
                    Swal.fire({
                        icon: 'success',
                        title: 'Eliminado',
                        text: 'Especialidad eliminada correctamente'
                    });
                    listarEspecialidades();
                } else {
                    Swal.fire({
                        icon: 'error',
                        title: 'Error',
                        text: 'No se pudo eliminar. Puede que la especialidad no exista.'
                    });
                }
            });
        }
    });
}

function Editar(id) {
    fetchGet("Especialidades/RecuperarEspecialidad/?id=" + id, "json", function (data) {
        if (data) {
            document.getElementById("id").value = data.id;
            document.getElementById("nombre").value = data.nombre;
            abrirModal(false); // Se abre en modo edición
        } else {
            Swal.fire({
                icon: 'error',
                title: 'Error',
                text: 'No se encontraron datos de la especialidad.'
            });
        }
    });
}
function GuardarEspecialidad() {
    let forma = document.getElementById("frmAgregarEspecialidad");

    if (!forma.checkValidity()) {
        forma.reportValidity();
        return;
    }

    let frm = new FormData(forma);

    fetchPost("Especialidades/GuardarEspecialidad", "text", frm, function (res) {
        Swal.fire({
            icon: 'success',
            title: 'Éxito',
            text: 'Especialidad guardada correctamente'
        }).then(() => {
            listarEspecialidades();
            cerrarModal();
        });
    });
}

function GuardarCambiosEspecialidad() {
    let forma = document.getElementById("frmEditarEspecialidad");

    if (!forma.checkValidity()) {
        forma.reportValidity();
        return;
    }

    let frm = new FormData(forma);

    fetchPost("Especialidades/GuardarCambiosEspecialidad", "text", frm, function (res) {
        Swal.fire({
            icon: 'success',
            title: 'Éxito',
            text: 'Especialidad modificada correctamente'
        }).then(() => {
            listarEspecialidades();
            cerrarModal();
        });
    });
}

function abrirModal() {
    let modalEl = document.getElementById("modalEditarEspecialidad");
    if (!modalEl) {
        console.error("Error: No se encontró el modal con id 'modalMedico'.");
        return;
    }
    let modal = new bootstrap.Modal(modalEl);
    modal.show();
}

function cerrarModal() {
    let modalEl = document.getElementById("modalEditarEspecialidad");
    let modal = bootstrap.Modal.getInstance(modalEl);
    if (modal) {
        modal.hide();
    }
}