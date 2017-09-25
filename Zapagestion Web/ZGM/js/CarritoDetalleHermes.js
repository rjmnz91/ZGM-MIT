// MJM 23/04/2014 INICIO

$(document).ready(function () {

    $('.cboPlazoNormal').show();
    $('.cboPlazoAmex').hide();

    $('#ctl00_ContentPlaceHolder1_lstTipoMIT').change(function () {

        if ($(this).val() == 'VMC') {
            $('.cboPlazoNormal').show();
            $('.cboPlazoAmex').hide();
        }
        else {
            $('.cboPlazoAmex').show();
            $('.cboPlazoNormal').hide();
        }
    });

});
// MJM 23/04/2014 FIN

function SetSource(SourceID) {
    var hidSourceID =
        document.getElementById("<%=hidSourceID.ClientID%>");
    hidSourceID.value = SourceID;
}

//Rellena los datos del formulario a enviar
function getData() {
    $.ajax({
        type: "POST",
        url: "CarritoDetalleHermes.aspx/getdatosMPOS",
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (msg) {
            if (msg != null) {
                $('#id_company').val(msg.d.id_company);
                $('#id_branch').val(msg.d.id_branch);
                $('#country').val(msg.d.country);
            }
        },
        error: function (xhr, ajaxOptions, thrownError) { alert(xhr.responseText); }
    });
}


//Rellena valores del formulario a enviar
function getEncriptado() {

    var amount = $('#ctl00_ContentPlaceHolder1_txtPago').val();

    // MJM 23/04/2014 INICIO
    var merchantPlazo;
    var descripcionPlazo;
    if ($('#ctl00_ContentPlaceHolder1_lstTipoMIT').val() == 'VMC') {
        merchantPlazo = $('.cboPlazoNormal').val();
        descripcionPlazo = $('.cboPlazoNormal option:selected').text();
    }
    else {
        merchantPlazo = $('.cboPlazoAmex').val();
        descripcionPlazo = $('.cboPlazoAmex option:selected').text();
    }
    // MJM 23/04/2014 FIN

    // MJM 08/05/2014 INICIO
    var tarjeta = $('#ctl00_ContentPlaceHolder1_lstTarjetas option:selected').text() + ' ' + descripcionPlazo;
    // MJM 08/05/2014 FIN
    var cliente = $('#ctl00_ContentPlaceHolder1_Nombre').html();
    var mail = "";
    var campo = document.getElementById("ctl00_ContentPlaceHolder1_txtemail");         //$('#ctl00_ContentPlaceHolder1_txtemail').html();
    if (cliente != "")
        var mail = campo.value;
    var datos = '{Amount:"' + amount.toString().replace(',', '') + '", Tarjeta:"' + tarjeta + '", TipoMIT:"' + $('#ctl00_ContentPlaceHolder1_lstTipoMIT option:selected').text() + '", merchant:"' + merchantPlazo + '",Email:"' + mail + '" }'

    //TODO: Aqui hay que leer el valor puesto en la caja de texto del importe a pagar por tarjeta
    $.ajax({
        type: "POST",
        url: "CarritoDetalleHermes.aspx/getdatosEncriptadosMPOS",
        data: datos,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#xmlmpos').val(msg.d);
            $('#sendMPOSPAy').submit();
        },
        error: function (xhr, ajaxOptions, thrownError) { alert(xhr.responseText); }
    });
}

function InicializaFormPOST() {
    getData();
    getEncriptado();
}

// MJM 15/03/2014 INICIO
// Establece el valor de txtPago, es llamada en las opciones de Cliente 9
function SetImporteAPagar(valor) {

    var txtPago = document.getElementById("ctl00_ContentPlaceHolder1_txtPago");
    var total = parseFloat(valor.toString().replace("$", "").replace(",", "")); ;

    txtPago.value = parseInt(total.toString()).toString();

}

// Muestra el total calculado a pagar desde la etiqueta total (calculada)
function MostrarImporteAPagar() {

    var txtPago = document.getElementById("ctl00_ContentPlaceHolder1_txtPago");
    var TotPendiente = document.getElementById("ctl00_ContentPlaceHolder1_TotPendiente");

    var total = parseFloat(TotPendiente.innerHTML.replace("$", "").replace(",", "")); ;

    txtPago.value = parseInt(total.toString()).toString();
}
// MJM 15/03/2014 FIN

function handleClickNotaCredito(myRadio) {

    if (myRadio.checked) {

        var el = document.getElementById('DivPagar');
        el.style.display = 'block';
        var importe = document.getElementById("ctl00_ContentPlaceHolder1_txtPago");
        var pagar = document.getElementById("ctl00_ContentPlaceHolder1_TotPendiente");

        //importe.value = pagar.innerHTML.replace("$", "");

        MostrarImporteAPagar();

        document.getElementById("ctl00_ContentPlaceHolder1_txtPago").focus();

        var radio = document.getElementById("ctl00_ContentPlaceHolder1_RadioButtonlTipoPago_0")
        radio.checked = false;
        var radio = document.getElementById("ctl00_ContentPlaceHolder1_RadioButtonlTipoPago_1")
        radio.checked = false;
        var elp = document.getElementById("ctl00_ContentPlaceHolder1_Divpuntos");
        elp.style.display = 'none';
        elp = document.getElementById("ctl00_ContentPlaceHolder1_DivTajeta");
        elp.style.display = 'none';

    }
    $('#BtnCancelar').prop('disabled', true);
    $('#Button1').prop('disabled', true);

}

function HacerClickPonerTarjeta(obj, items) {

    // MJM 15/03/2014 INICIO
    // Controlar si hay algún elemento pulsado.
    var bAlgunoPulsado = false;
    // MJM 15/03/2014 FIN

    var rbl = document.getElementById(obj);

    var radioNotaPago = document.getElementById('ctl00_ContentPlaceHolder1_RadioButton1');
    if (eval(radioNotaPago)) radioNotaPago.checked = false;

    var rblChild = null;
    for (i = 0; i < items; i++) {
        rblChild = document.getElementById(obj + "_" + i.toString());
        if (rblChild.checked) {

            if (rblChild.value == 'T') {
                //div de tarjeta
                bAlgunoPulsado = true;

                var elp = document.getElementById("ctl00_ContentPlaceHolder1_Divpuntos");
                elp.style.display = 'none';
                var el1 = document.getElementById("ctl00_ContentPlaceHolder1_DivTajeta");
                var el = document.getElementById('DivPagar');
                el1.style.display = 'inline-block';
                el.style.display = 'block';
                var importe = document.getElementById("ctl00_ContentPlaceHolder1_txtPago");
                var pagar = document.getElementById("ctl00_ContentPlaceHolder1_TotPendiente");

                //importe.value = pagar.innerHTML.replace("$", "");
                MostrarImporteAPagar();

                //ACL. Lo comento, ya que he quitado de momento el combo de bancos o tarjetas.
                // document.getElementById("ctl00_ContentPlaceHolder1_lstTarjetas").focus();

                $("#ctl00_ContentPlaceHolder1_RadioButton4").attr('checked', false);
                $("#ctl00_ContentPlaceHolder1_RadioButton3").attr('checked', false);
                $('#ctl00_ContentPlaceHolder1_txtPago').attr('readonly', false);
            }
            else {
                //div cliente 9
                bAlgunoPulsado = true;


                var elp = document.getElementById("ctl00_ContentPlaceHolder1_DivTajeta");
                elp.style.display = 'none';
                var el1 = document.getElementById("ctl00_ContentPlaceHolder1_Divpuntos");
                var el = document.getElementById('DivPagar');
                el1.style.display = 'inline-block';
                el.style.display = 'block';
                var importe = document.getElementById("ctl00_ContentPlaceHolder1_txtPago");

                //importe.value = "0.00";
                importe.value = "0";
                $('#ctl00_ContentPlaceHolder1_txtPago').attr('readonly', false);
                //Si no ha habido un pago, se habilita, en caso contrario se inhabilita
                if (rblChild.value == '9') {
                    if ($('#ctl00_ContentPlaceHolder1_RepeaterPago_ctl01_LabImporte').html() != '$0.00') {
                        $('#BtnCancelar').prop('disabled', false);
                        $('#Button1').prop('disabled', false);
                    }
                    else {
                        $('#BtnCancelar').prop('disabled', true);
                    }
                }

            }
        }
    }

    // Si no hay ninguno pulsado, seleccionamos el primero que esté habilitado.
    if (!bAlgunoPulsado) {

        $('#ctl00_ContentPlaceHolder1_RadioButtonlTipoPago input:radio').find(":enabled").each(function () {



        });



    }

}

function ClickPar9(myRadio) {

    if (myRadio.checked) {
        $("#ctl00_ContentPlaceHolder1_RadioButton4").attr('checked', false);
        $("#ctl00_ContentPlaceHolder1_RadioButton2").attr('checked', false);

        var valor = $("#ctl00_ContentPlaceHolder1_Label11").text()
        var TotPendiente = document.getElementById("ctl00_ContentPlaceHolder1_TotPendiente");
        var cantidadTotal = parseFloat(TotPendiente.innerHTML.replace(',', '').replace('$', ''));

        if (valor > cantidadTotal && valor > 0) {
            //$("#ctl00_ContentPlaceHolder1_txtPago").val(cantidadTotal);
            SetImporteAPagar(cantidadTotal);
        }
        else {
            //$("#ctl00_ContentPlaceHolder1_txtPago").val(valor);
            SetImporteAPagar(valor);
        }

        $('#ctl00_ContentPlaceHolder1_txtPago').attr('readonly', true);
    }
}


function ClickBolsa9(myRadio) {

    if (myRadio.checked) {
        $("#ctl00_ContentPlaceHolder1_RadioButton3").attr('checked', false);
        $("#ctl00_ContentPlaceHolder1_RadioButton2").attr('checked', false);

        var valor = $("#ctl00_ContentPlaceHolder1_Label12").text()
        var TotPendiente = document.getElementById("ctl00_ContentPlaceHolder1_TotPendiente");
        var cantidadTotal = parseFloat(TotPendiente.innerHTML.replace(',', '').replace('$', ''));

        if (valor > cantidadTotal && valor > 0) {
            //$("#ctl00_ContentPlaceHolder1_txtPago").val(cantidadTotal);
            SetImporteAPagar(cantidadTotal);
        }
        else {
            //$("#ctl00_ContentPlaceHolder1_txtPago").val(valor);
            SetImporteAPagar(valor);
        }

        $('#ctl00_ContentPlaceHolder1_txtPago').attr('readonly', true);

    }

}

function ClickCliente9(myRadio) {
    debugger;

    if (myRadio.checked) {
        $("#ctl00_ContentPlaceHolder1_RadioButton3").attr('checked', false);
        $("#ctl00_ContentPlaceHolder1_RadioButton4").attr('checked', false);
        $("#ctl00_ContentPlaceHolder1_RadioButton2").attr('checked', true);
        $('#ctl00_ContentPlaceHolder1_hidPuntos').val(1);

        var valor = $("#ctl00_ContentPlaceHolder1_Label10").text()
        var TotPendiente = document.getElementById("ctl00_ContentPlaceHolder1_TotPendiente");
        var cantidadTotal = parseFloat(TotPendiente.innerHTML.replace(',', '').replace('$', ''));

        if (valor > cantidadTotal && valor > 0) {
            //$("#ctl00_ContentPlaceHolder1_txtPago").val(cantidadTotal);
            SetImporteAPagar(cantidadTotal);
        }
        else {
            //$("#ctl00_ContentPlaceHolder1_txtPago").val(valor);
            SetImporteAPagar(valor);
        }

        $('#ctl00_ContentPlaceHolder1_txtPago').attr('readonly', false);
    }
}

function validarEmail(email) {
    expr = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    if (!expr.test(email))
        alert("Error: La dirección de correo " + email + " es incorrecta.");
}

function ValidarImporte() {


    var txtPago = document.getElementById("ctl00_ContentPlaceHolder1_txtPago");
    var TotPendiente = document.getElementById("ctl00_ContentPlaceHolder1_TotPendiente");
    var GetValue = $('#ctl00_ContentPlaceHolder1_RadioButtonlTipoPago').find(":checked").val();
    var Promo = $('#ctl00_ContentPlaceHolder1_HiddenPromo').val();

    var cantidadPagar = parseFloat(txtPago.value.replace(',', '').replace('$', ''));
    var cantidadTotal = parseFloat(TotPendiente.innerHTML.replace(',', '').replace('$', ''));

    var txtEmail = document.getElementById("ctl00_ContentPlaceHolder1_txtemail");
    var txtNombre = document.getElementById("ctl00_ContentPlaceHolder1_txtNomCli");

    if (txtNombre.value == "") {
        alert("Introduzca a nombre de quien se enviara su compra");
        return;
    }

    if (txtEmail.value == "") {
        alert("Introduzca el Email al cual se enviara su comprobante electronico");
        return;
    }
    else {
        validarEmail(txtEmail.value);
    }


    if (Promo == "P") {
        alert("No puedes empezar a pagar hasta que tenga seleccionadas las promociones asociadas a la venta.");
        return false;
    }

    if (cantidadPagar < 0.01) {
        return false;
    }

    if ($('#ctl00_ContentPlaceHolder1_RadioButton2').is(':checked')) {
        if (cantidadPagar > cantidadTotal) {
            alert("El importe a Redimir en puntos es mayor al importe pendiente de pago.");
            return false;
        }
    }
    else {
        if (cantidadPagar > cantidadTotal) {
            alert("El importe entregado es superior al importe pendiente de Cobrar.");
            return false;
        }
    }

    var valor = $('#ctl00_ContentPlaceHolder1_lstTarjetas').val();

    // Si está marcado el check de pago con tarjeta enviamos el formulario que hay en el MasterPage.
    if (GetValue == 'T') {
        if (valor == '0') {
            alert("Debe seleccionar un tipo de tarjeta para pagar en MIT");
            return false;
        }
        else {
            if (cantidadPagar <= 0) {
                alert("No ha introducido el importe a cobrar.");
                return false;
            }

            //InicializaFormPOST();

            $("#ButPagar").attr('disabled', 'disabled');
            $("#ctl00_ContentPlaceHolder1_BtnPagar").click();
            //__dop
            return true;
        }

    }
    else {
        if (GetValue == '9') {
            if ($("#ctl00_ContentPlaceHolder1_RadioButton3").is(':checked')) {
                //pares
                var pares = document.getElementById("ctl00_ContentPlaceHolder1_Label11");
                var Totalpares = parseFloat(pares.innerHTML.replace(',', '').replace('$', ''));

                if (cantidadPagar > Totalpares) {
                    alert("El promedio de pares no puede ser superior a la cantidad a Pagar")
                    return false;
                }
            }
            else {

                if ($("#ctl00_ContentPlaceHolder1_RadioButton4").is(':checked')) {
                    //bolsas
                    var bolsas = document.getElementById("ctl00_ContentPlaceHolder1_Label12");
                    var Totalbolsas = parseFloat(bolsas.innerHTML.replace(',', '').replace('$', ''));

                    if (cantidadPagar > Totalbolsas) {
                        alert("El promedio de Bolsas no puede ser superior a la cantidad a Pagar")
                        return false;
                    }
                }
                else {
                    //nine
                    if (cantidadPagar <= 0) {
                        alert("No puede Redimir un importe de $0.00.");
                        return false;
                    }
                    var puntos = document.getElementById("ctl00_ContentPlaceHolder1_Label10");
                    var Totalpuntos = parseFloat(puntos.innerHTML.replace(',', '').replace('$', ''));

                    if (cantidadPagar > Totalpuntos) {
                        alert("No puede exceder los puntos disponibles para el pago")
                        return false;
                    }
                }
            }
        }
        else {
            $("#ButPagar").attr('disabled', 'disabled');
            $("#ctl00_ContentPlaceHolder1_BtnPagar").click();
            return true;
        }
    }
    //Acl. Habilitamos el botón de cancelar pago si es puntos nine
    if (GetValue == '9') {
        $('#BtnCancelar').prop('disabled', false);
        $('#Button1').prop('disabled', false);

    }
    else {
        $('#BtnCancelar').prop('disabled', true);
        $('#Button1').prop('disabled', true);
    }
    $("#ButPagar").attr('disabled', 'disabled');
    $("#ctl00_ContentPlaceHolder1_BtnPagar").click();
    return true;
    // OnClientClick="return ValidarImporte()
}
function ValidarCancelPago() {
    //Si existe un subtotal pagado, lanzamos la cancelación del pago
    if ($('#ctl00_ContentPlaceHolder1_RepeaterPago_ctl01_LabImporte').html() != '$0.00') {
        $("#ctl00_ContentPlaceHolder1_ButCancelarPago").click();
    }
}

function ValidarClienteSelecionado() {
    var valor = $('#ctl00_ContentPlaceHolder1_LstClientes').val();
    if (valor == -1) {
        alert("Debe seleccionar un cliente.");
        return false;
    }
    return true;
}

function ValidarEliminarCarrito() {
    var valor = $('#ctl00_ContentPlaceHolder1_RepeaterPago_ctl01_LabImporte').html();
    valor = valor.substring(1, valor.length - 1);
    if (parseFloat(valor) > 0) { //!="$0.00") {
        alert("No puede borrar lineas del carrito si ha efectuado algún pago.");
        return false;
    }
    else {
        return confirm('¿Seguro que quiere quitar este producto?');
    }
}
