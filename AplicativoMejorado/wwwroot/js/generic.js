function get(idControl) {
    return document.getElementById(idControl).value;
}

function set(idControl, valor) {
    document.getElementById(idControl).value = valor;
}

function setN(namecontrol, valor) {
    document.getElementsByName(namecontrol)[0].value = valor;
}

function LimpiarDatos(idFormulario) {
    let elementosName = document.querySelectorAll('#' + idFormulario + " [name]");
    let elementoActual;
    let elementoName;
    for (let i = 0; i < elementosName.length; i++) {
        elementoActual = elementosName[i];
        elementoName = elementoActual.name;
        setN(elementoName, "");
    }
}

async function fetchGet(url, tipoRespuesta, callback) {
    try {
        let raiz = document.getElementById("hdfOculto").value;
        let urlCompleta = `${window.location.protocol}//${window.location.host}/${url}`;

        let res = await fetch(urlCompleta);
        if (tipoRespuesta === "json") {
            res = await res.json();
        } else if (tipoRespuesta === "text") {
            res = await res.text();
        }

        callback(res);
    } catch (e) {
        alert("Ocurrió un problema: " + e.message);
    }
}

async function fetchPost(url, tipoRespuesta, frm, callback) {
    try {
        let raiz = document.getElementById("hdfOculto").value;
        let urlCompleta = `${window.location.protocol}//${window.location.host}/${url}`;

        let res = await fetch(urlCompleta, {
            method: "POST",
            body: frm
        });

        if (tipoRespuesta === "json") {
            res = await res.json();
        } else if (tipoRespuesta === "text") {
            res = await res.text();
        }

        callback(res);
    } catch (e) {
        //alert("Ocurrio un problema en POST")
    }
}

let objConfiguracionGlobal;

//{url: "", cebeceras[], propiedades: []}
function pintar(objConfiguracion) {
    objConfiguracionGlobal = objConfiguracion;

    fetchGet(objConfiguracion.url, "json", function (res) {
        document.getElementById("divTable").innerHTML = generarTabla(res);

        // Inicializar DataTables con la nueva sintaxis (versión 2.x)
        setTimeout(() => {
            if (typeof DataTable !== "undefined") {
                let table = new DataTable("#myTable", {
                    /*responsive: true,*/
                    language: {
                        search: "Buscar:",
                        lengthMenu: "Mostrar _MENU_ registros",
                        info: "_START_ a _END_ de _TOTAL_",
                        infoEmpty: "0 a 0 de 0",
                        infoFiltered: "(filtrado de _MAX_ registros totales)",
                        zeroRecords: "No se encontraron resultados",
                        //paginate: {
                        //    first: "Primero",
                        //    last: "Último",
                        //    next: "Siguiente",
                        //    previous: "Anterior"
                        //}
                    },
                    initComplete: function () {
                        this.api().columns().every(function () {
                            var that = this;

                            $('input', this.header()).on('keyup change clear', function () {
                                if (that.search() !== this.value) {
                                    that.search(this.value).draw();
                                }
                            });
                        });
                    }
                });
            } else {
                console.error("Error: DataTables no está definido. Verifica que los scripts están cargados.");
            }
        }, 300);
    });
}
function generarTabla(res) {
    let contenido = "";
    let cabeceras = objConfiguracionGlobal.cabeceras;
    let propiedades = objConfiguracionGlobal.propiedades;

    contenido += "<table id='myTable' class='table table-striped' style='width:100%'>";
    contenido += "<thead><tr>";

    for (let i = 0; i < cabeceras.length; i++) {
        contenido += "<th>" + cabeceras[i] + "</th>";
    }

    if (objConfiguracionGlobal.editar || objConfiguracionGlobal.eliminar) {
        contenido += "<th>Operaciones</th>";
    }

    contenido += "</tr>";    /*</thead > <tbody>";*/
    contenido += "<tr>";
    for (let i = 0; i < cabeceras.length; i++) {
        contenido += "<th><input type='text' placeholder='Buscar' style='width: 100%;'/></th>";
    }
    if (objConfiguracionGlobal.editar || objConfiguracionGlobal.eliminar) {
        contenido += "<th></th>"; // Sin filtro en operaciones
    }
    contenido += "</tr></thead>";

    contenido += "<tbody>";


    let numRegistros = res.length;
    let espacio = " ";
    for (let i = 0; i < numRegistros; i++) {
        let obj = res[i];
        contenido += "<tr>";
        for (let propiedadActual of propiedades) {
            contenido += `<td>${obj[propiedadActual]}</td>`;
        }

        if (objConfiguracionGlobal.editar || objConfiguracionGlobal.eliminar) {
            let propiedadID = objConfiguracionGlobal.propiedadId;
            contenido += "<td>";
            if (objConfiguracionGlobal.editar == true) {
                contenido += `<i onclick="Editar(${obj[propiedadID]})" class="btn btn-primary"><svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-pencil-square" viewBox="0 0 16 16">
    <path d="M15.502 1.94a.5.5 0 0 1 0 .706L14.459 3.69l-2-2L13.502.646a.5.5 0 0 1 .707 0l1.293 1.293zm-1.75 2.456-2-2L4.939 9.21a.5.5 0 0 0-.121.196l-.805 2.414a.25.25 0 0 0 .316.316l2.414-.805a.5.5 0 0 0 .196-.12l6.813-6.814z"/>
    <path fill-rule="evenodd" d="M1 13.5A1.5 1.5 0 0 0 2.5 15h11a1.5 1.5 0 0 0 1.5-1.5v-6a.5.5 0 0 0-1 0v6a.5.5 0 0 1-.5.5h-11a.5.5 0 0 1-.5-.5v-11a.5.5 0 0 1 .5-.5H9a.5.5 0 0 0 0-1H2.5A1.5 1.5 0 0 0 1 2.5z"/>
    </svg> </i> ${espacio}`
            }



            if (objConfiguracionGlobal.eliminar == true) {
                contenido += `<i onclick="Eliminar(${obj[propiedadID]})" class="btn btn-danger"><svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-trash3-fill" viewBox="0 0 16 16">
        <path d="M11 1.5v1h3.5a.5.5 0 0 1 0 1h-.538l-.853 10.66A2 2 0 0 1 11.115 16h-6.23a2 2 0 0 1-1.994-1.84L2.038 3.5H1.5a.5.5 0 0 1 0-1H5v-1A1.5 1.5 0 0 1 6.5 0h3A1.5 1.5 0 0 1 11 1.5m-5 0v1h4v-1a.5.5 0 0 0-.5-.5h-3a.5.5 0 0 0-.5.5M4.5 5.029l.5 8.5a.5.5 0 1 0 .998-.06l-.5-8.5a.5.5 0 1 0-.998.06m6.53-.528a.5.5 0 0 0-.528.47l-.5 8.5a.5.5 0 0 0 .998.058l.5-8.5a.5.5 0 0 0-.47-.528M8 4.5a.5.5 0 0 0-.5.5v8.5a.5.5 0 0 0 1 0V5a.5.5 0 0 0-.5-.5"/>
        </svg> </i>`
            }
            contenido += "</td>";
        }
        contenido += "</tr>";
    }

    contenido += "</tbody></table>";
    return contenido;
}



function mostrarAlertaEliminar(tipo, mensaje, callback) {
    Swal.fire({
        title: tipo,
        text: mensaje,
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Si, por supuesto!"
    }).then((result) => {
        if (result.isConfirmed) {
            callback();
        }
    });
}

function confirmacionCreacion(tipo, mensaje, callback) {
    Swal.fire({
        title: tipo,
        text: mensaje,
        icon: "info",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Si, por supuesto!"
    }).then((result) => {
        if (result.isConfirmed) {
            callback();
        }
    });
}

function confirmacionActualizacion(tipo, mensaje, callback) {
    Swal.fire({
        title: tipo,
        text: mensaje,
        icon: "info",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Si, por supuesto!"
    }).then((result) => {
        if (result.isConfirmed) {
            callback();
        }
    });
}
