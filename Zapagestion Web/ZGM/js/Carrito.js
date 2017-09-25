
$(document).ready(function () {

    // Inicializar 
    InicializarPagina();

});

function InicializarPagina() {

    //    $('#tabPagos').tabs();
    //    $('.jqbutton').button();
    //    $('.btnBuscar').button({
    //        icons: { primary: "ui-icon-search" }
    //    });

    //    $('.btnBorrar').button({
    //        icons: { primary: "ui-icon-circle-close" },
    //        text: false
    //    });

    //    var hidTotal = $('#hidTotal');
    //    var txtTotal = $('#txtPagar');

    //    $('#optTarjeta').click(function () {
    //        var total = hidTotal.val();
    //        txtTotal.val(total.toString());
    //    });

    //    $('#optNotaEmpleado').click(function () {
    //        var total = hidTotal.val();
    //        txtTotal.val(total.toString());
    //    });

    //    $('#optCliente9').click(function () {
    //        CheckClienteNine();
    //    });
    //}

}
    function SetSource(SourceID) {
        var hidSourceID =
            document.getElementById("<%=hidSourceID.ClientID%>");
        hidSourceID.value = SourceID;
    }

    //Rellena los datos del formulario a enviar
    function getData() {
        $.ajax({
            type: "POST",
            url: "Carrito5.aspx/getdatosMPOS",
            data: "{}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (msg) {
                if (msg !== null) {
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
        debugger;
        var amount = $('.txtPago').val();
        var cliente = $('#ctl00_ContentPlaceHolder1_Nombre').html();
        var mail = "";
        var campo = document.getElementById("ctl00_ContentPlaceHolder1_txtemail");         //$('#ctl00_ContentPlaceHolder1_txtemail').html();
        if (cliente !== "")
             mail = campo.value;
        var datos = '{Amount:"' + amount.toString().replace(',', '') + '", Tarjeta:"' + $('.lstTarjetas option:selected').text() + '",Email:"' + mail + '" }';

        //TODO: Aqui hay que leer el valor puesto en la caja de texto del importe a pagar por tarjeta
        $.ajax({
            type: "POST",
            url: "Carrito5.aspx/getdatosEncriptadosMPOS",
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

    function handleClickNotaCredito(myRadio) {

        if (myRadio.checked) {
            // MJM 09/03/2014 INICIO
            var divPuntos = document.getElementById("ctl00_ContentPlaceHolder1_Divpuntos");
            divPuntos.style.display = 'none';
            var divTarjeta = document.getElementById("ctl00_ContentPlaceHolder1_DivTajeta");
            divTarjeta.style.display = 'none';

            //        var rblChild = null;
            //        for (i = 0; i < 10; i++) {
            //            rblChild = document.getElementById("ctl00_ContentPlaceHolder1_RadioButtonlTipoPago_" + i.toString());
            //            if (rblChild == null) break;
            //            if (rblChild.checked) {
            //                rblChild.checked = false;
            //            }
            //        }
            //        // MJM 09/03/2014 FIN

            var el = document.getElementById('DivPagar');
            el.style.display = 'block';
            var importe = document.getElementById("ctl00_ContentPlaceHolder1_txtPago");
            var pagar = document.getElementById("ctl00_ContentPlaceHolder1_TotPendiente");
            importe.value = pagar.innerHTML.replace("$", "");
            document.getElementById("ctl00_ContentPlaceHolder1_txtPago").focus();
        }
    }

    //function HacerClickPonerTarjeta(obj, items) {
    function HacerClickPonerTarjeta(tipo) {

        alert("1");

        // MJM 09/03/2014 INICIO
        // Quitar el check en el radio de Nota Empleado.
        var chkNotaEmpleado = document.getElementById('RadioButton1');
        if (chkNotaEmpleado.checked) chkNotaEmpleado.checked = false;

        // MJM 09/03/2014 FIN
        if (tipo === 'T') {
            //div de tarjeta
            var elp = document.getElementById("ctl00_ContentPlaceHolder1_Divpuntos");
            elp.style.display = 'none';
            var el1 = document.getElementById("ctl00_ContentPlaceHolder1_DivTajeta");
            var el = document.getElementById('DivPagar');
            el1.style.display = 'inline-block';
            el.style.display = 'block';
            var importe = document.getElementById("ctl00_ContentPlaceHolder1_txtPago");
            var pagar = document.getElementById("ctl00_ContentPlaceHolder1_TotPendiente");
            importe.value = pagar.innerHTML.replace("$", "");
            document.getElementById("ctl00_ContentPlaceHolder1_lstTarjetas").focus();
            $("#ctl00_ContentPlaceHolder1_RadioButton4").attr('checked', false);
            $("#ctl00_ContentPlaceHolder1_RadioButton3").attr('checked', false);
            $('#ctl00_ContentPlaceHolder1_txtPago').attr('readonly', false);
        }
        else {
            //div cliente 9
            var elp = document.getElementById("ctl00_ContentPlaceHolder1_DivTajeta");
            elp.style.display = 'none';
            var el1 = document.getElementById("ctl00_ContentPlaceHolder1_Divpuntos");
            var el = document.getElementById('DivPagar');
            el1.style.display = 'inline-block';
            el.style.display = 'block';
            var importe = document.getElementById("ctl00_ContentPlaceHolder1_txtPago");
            importe.value = "0.00";
            $('#ctl00_ContentPlaceHolder1_txtPago').attr('readonly', false);
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

                $("#ctl00_ContentPlaceHolder1_txtPago").val(cantidadTotal);
            }
            else {
                $("#ctl00_ContentPlaceHolder1_txtPago").val(valor);

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

                $("#ctl00_ContentPlaceHolder1_txtPago").val(cantidadTotal);

            }
            else {
                $("#ctl00_ContentPlaceHolder1_txtPago").val(valor);
            }

            $('#ctl00_ContentPlaceHolder1_txtPago').attr('readonly', true);

        }

    }

    function ClickCliente9(myRadio) {

        if (myRadio.checked) {
            $("#ctl00_ContentPlaceHolder1_RadioButton3").attr('checked', false);
            $("#ctl00_ContentPlaceHolder1_RadioButton4").attr('checked', false);

            var valor = $("#ctl00_ContentPlaceHolder1_Label10").text()
            var TotPendiente = document.getElementById("ctl00_ContentPlaceHolder1_TotPendiente");
            var cantidadTotal = parseFloat(TotPendiente.innerHTML.replace(',', '').replace('$', ''));

            if (valor > cantidadTotal && valor > 0) {

                $("#ctl00_ContentPlaceHolder1_txtPago").val(cantidadTotal);

            }
            else {
                $("#ctl00_ContentPlaceHolder1_txtPago").val(valor);
            }

            $('#ctl00_ContentPlaceHolder1_txtPago').attr('readonly', false);

        }
    }

    function ValidarImporte() {

        var txtPago = document.getElementById("txtPagar");
        var TotPendiente = document.getElementById("hidPendiente");
        var GetValue = '';
        var valorTarjetaMit = $('#optTarjeta').val();
        var valorCliente9 = $('#optCliente9').val();

        var cantidadPagar = parseFloat(txtPago.value.replace(',', '').replace('$', ''));
        var cantidadTotal = parseFloat(TotPendiente.innerHTML.replace(',', '').replace('$', ''));

        if (cantidadPagar > cantidadTotal) {
            alert("El importe entregado es superior al importe pendiente de Cobrar.");
            return false;
        }

        if (cantidadPagar === 0) {
            return false;
        }

        var valor = $('#lstTarjetas').val();

        // Si está marcado el check de pago con tarjeta enviamos el formulario que hay en el MasterPage.
        if ($('.optTarjeta:checked').val()) {
            if (valor === '0') {
                alert("Debe seleccionar un tipo de tarjeta para pagar en MIT");
                return false;
            }
            else {

                if (cantidadPagar <= 0) {
                    alert("No ha introducido el importe a cobrar.");
                    return false;
                }
                InicializaFormPOST();
                return false;
            }
        }
        else {
            if ($('.optCliente9:checked').val()) {
                if ($("#optParesAcumulados").is(':checked')) {
                    //pares
                    var pares = document.getElementById("ctl00_ContentPlaceHolder1_Label11");
                    var Totalpares = parseFloat(pares.innerHTML.replace(',', '').replace('$', ''));
                    if (cantidadPagar > Totalpares) {
                        alert("El promedio de pares no puede ser superior a la cantidad a Pagar")
                        return false;
                    }
                }
                else {
                    if ($("#optBolsasAcumuladas").is(':checked')) {
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

        $("#ButPagar").attr('disabled', 'disabled');
        $("#ctl00_ContentPlaceHolder1_BtnPagar").click();
        return true;
    }

    function ValidarClienteSelecionado() {
        var valor = $('#ctl00_ContentPlaceHolder1_LstClientes').val();
        alert(valor);
        if (valor === -1) {
            alert("Debe selecionar un cliente.");
            return false;
        }

        return true;

    }

    function ValidarEliminarCarrito() {

        var valor = $('#ctl00_ContentPlaceHolder1_RepeaterPago_ctl02_LabPAgo').length;


        if (valor > 0) {

            return false;

        }
        else {

            return confirm('¿Seguro que quiere quitar este producto?');

        }
    }



