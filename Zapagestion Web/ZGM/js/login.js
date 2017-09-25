var key = "IdTerminal";

$(document).ready(function () {

    // Secuencia:
    // 1) Si hay sesión se comprueba el terminal almacenado
    //      Si no hay valor se pide uno y se guarda.
    // 2) Se pregunta por usuario / password y se envia junto con el valor almacenado.
    // 3) Si el servidor valida la máquina y el login permite el acceso.

    if ($('#hidSesionActivaTpv').val().toString().trim() == '1') {
        // hay sesión abierta en el tpv
        verificarMaquina();
    }
    else {
        // sesión cerrada, informar al usuario
        $('#divLogin').hide();
        $('#divSesionCerrada').show();
    }

    $('#btnActualizar').click(function () {
        location.reload(true);
    });

    $('#lnkEliminarTerminal').click(function () {
        $.jStorage.flush();
        location.reload(true);
    });

    $('#cmdTerminal').click(function () {
        var terminal = $('#txtTerminal').val().toString().trim();

        if (terminal != null && terminal != "") {
            if (confirm("[ " + terminal + " ] \n\n¿ Establecer este valor como identificador de Terminal ?")) {
                $.jStorage.set(key, terminal);
                location.reload(true);
            }
        }
        else {
            alert("El identificador introducido no es válido.");
            $('#txtTerminal').focus();
        }
    });

    $('a[id="lnkEliminarTerminal"]').click( function () {
        var wpurl = $('#hidStringUrl').val();
        $(this).attr('href', wpurl);
    });

});

function verificarMaquina() {
    var terminalAlmacenado = $.jStorage.get(key);

    // No hay valor, mostrar ui
    if (terminalAlmacenado == null) {
        $('#divLogin').hide();
        $('#divTerminal').show();
        $('#txtTerminal').focus();
    }
    else {
        $('#hidNombreMaquina').val(terminalAlmacenado);

        $('#lblMaquina').val('Identificador: ' + terminalAlmacenado.toString());

        $('#divLogin').show();
        $('#divTerminal').hide();
        $('#txtLogin').focus();
    }
}