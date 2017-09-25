$(document).ready(function () {

    $('input[type=radio]').change(function (e) {

        LimpiaControles();
    });

    $('#txtFechaNacimiento').blur(function () {
        if ($('#txtFechaNacimiento').val() != "")
            $('#txtFecNac').val($('#txtFechaNacimiento').val());
        else
            $('#txtFechaNacimiento').val($('#txtFecNac').val());
    });
}); 
function validaDatInitialTarjeta() {
    
var datTjt=$('#ctl00_ContentPlaceHolder2_txtNumTarjeta').val();
if (datTjt == '') $('#ctl00_ContentPlaceHolder2_txtNumTarjeta').val('22114005');
}

function ValidaTipoCliente() {
    
    $('#ctl00_ContentPlaceHolder2_hidTipoCliente').val('N');
    if ($('input#ctl00_ContentPlaceHolder2_RadioButtonlTipoCli_0').is(':checked')) {
        
      //  $('#ctl00_ContentPlaceHolder2_txtFecNac').show();
        //  $('#txtFechaNacimiento').hide();
        $('#ctl00_ContentPlaceHolder2_txtFecNac').hide();
        $('#txtFechaNacimiento').show();
        if ($('#ctl00_ContentPlaceHolder2_txtFecNac').val() != "")
            $('#txtFechaNacimiento').val($('#ctl00_ContentPlaceHolder2_txtFecNac').val());
        $('#ctl00_ContentPlaceHolder2_hidTipoCliente').val('C');
        $('#BusquedaCliente').show();
        //LimpiaControles();
        validaDatInitialTarjeta();
        HabilitaRegex(false);
        ValidaObligatorios();
        HabilitaRegex(true);
    }
    else {
        
        $('#ctl00_ContentPlaceHolder2_hidTipoCliente').val('N');
        $('#ctl00_ContentPlaceHolder2_txtFecNac').hide();
        if ($('#txtFechaNacimiento').is(':visible')) {
            if ($('#ctl00_ContentPlaceHolder2_txtFecNac').val() != "")
                $('#txtFechaNacimiento').val($('#ctl00_ContentPlaceHolder2_txtFecNac').val());
        }
        else {
            $('#txtFechaNacimiento').show();
        }
        $('#BusquedaCliente').hide();
        //LimpiaControles();
        validaDatInitialTarjeta()
        HabilitaRegex(false);
        ValidaObligatorios();
        HabilitaRegex(true);
       
    }

}
function ValidaObligatorios() {
    
    var value = true;
    if ($('#ctl00_ContentPlaceHolder2_txtNombre').val() == "") value = true;
    else value = false
    var nombrevalidator = document.getElementById("ctl00_ContentPlaceHolder2_RFVNombre");
    ValidatorEnable(nombrevalidator, value);

    if ($('#ctl00_ContentPlaceHolder2_txtApellidos').val() == "") value = true;
    else value = false
    var apelvalidator = document.getElementById("ctl00_ContentPlaceHolder2_RFVApellidos");
    ValidatorEnable(apelvalidator, value);

    if ($('#ctl00_ContentPlaceHolder2_txtApellidos').val() == "") value = true;
    else value = false
    var Fecvalidator = document.getElementById("ctl00_ContentPlaceHolder2_RFVFecha");
    ValidatorEnable(Fecvalidator, value);

    if ($('#ctl00_ContentPlaceHolder2_txtTfnoCasa').val() == "") value = true;
    else value = false
    var TfnoValidator = document.getElementById("ctl00_ContentPlaceHolder2_RFVTelefono")
    ValidatorEnable(TfnoValidator, value);
    
    if ($('#ctl00_ContentPlaceHolder2_txtEmail').val() == "") value = true;
    else value = false
    var mailvalidator = document.getElementById("ctl00_ContentPlaceHolder2_RFVMail");
    ValidatorEnable(mailvalidator, value);

    if ($('#ctl00_ContentPlaceHolder2_txtMovil').val() == "") value = true;
    else value = false   
    var MovilValidator = document.getElementById("ctl00_ContentPlaceHolder2_RFVMovil")
    ValidatorEnable(MovilValidator, value);
        

}
function HabilitaRegex(value) {
        
        if($('#ctl00_ContentPlaceHolder2_txtNombre').val()=="") value=true;
        else value = false
        var NomReg = document.getElementById("ctl00_ContentPlaceHolder2_reqNombre")
        ValidatorEnable(NomReg, value);
        if ($('#ctl00_ContentPlaceHolder2_txtApellidos').val() == "") value = true;
        else value = false
        var ApelReg = document.getElementById("ctl00_ContentPlaceHolder2_reqApellidos")
        ValidatorEnable(ApelReg, value);
        if ($('#ctl00_ContentPlaceHolder2_txtTfnoCasa').val() == "") value = true;
        else value = false
        var TfnoReg = document.getElementById("ctl00_ContentPlaceHolder2_reqTelefono")
        ValidatorEnable(TfnoReg, value);
        if ($('#ctl00_ContentPlaceHolder2_txtEmail').val() == "") value = true;
        else value = false
        var mailReg = document.getElementById("ctl00_ContentPlaceHolder2_reqEmail")
        ValidatorEnable(mailReg, value);
        if ($('#ctl00_ContentPlaceHolder2_txtMovil').val() == "") value = true;
        else value = false
        var MovilReg = document.getElementById("ctl00_ContentPlaceHolder2_reqMovil")
        ValidatorEnable(MovilReg, value);
        if ($('#ctl00_ContentPlaceHolder2_txtNumTarjeta').val() == "") value = true;
        else value = false
        var TjtReg = document.getElementById("ctl00_ContentPlaceHolder2_reqTJT")
        ValidatorEnable(TjtReg, value);
}
function LimpiaControles() {
    
    $('input[type=text]').each(function () {
        $(this).val('');
        $(this).css('background-color', 'white');
        $(this).prop("disabled", false)
    })
    $('#ctl00_ContentPlaceHolder2_nomcliente').val();
}
function ValidaTipoCambio() {
    
    $('#ctl00_ContentPlaceHolder2_hidTipoCambio').val('R');
    if ($('input#ctl00_ContentPlaceHolder2_RadioButtonlTipoCambio_0').is(':checked')) {
        $('#ctl00_ContentPlaceHolder2_hidTipoCambio').val('C');
        $("#btnAddProfile").attr('value', 'Save');
        $('#ctl00_ContentPlaceHolder2_btnReemplazarTjt').hide();
        $('#ctl00_ContentPlaceHolder2_btnCambiarNivel').show();
        $('#ctl00_ContentPlaceHolder2_tipCambio').hide();
        $('#ctl00_ContentPlaceHolder2_tipNivel').show();
        if ($('#ctl00_ContentPlaceHolder2_hidNuevoNivel').val() == "")
            $('#ctl00_ContentPlaceHolder2_hidNuevoNivel').val($("#ctl00_ContentPlaceHolder2_tipNivel").val());
       
        


    }
    else {
        $('#ctl00_ContentPlaceHolder2_hidTipoCambio').val('R');
        $('#ctl00_ContentPlaceHolder2_btnCambiarNivel').hide();
        $('#ctl00_ContentPlaceHolder2_btnReemplazarTjt').show();
        $('#ctl00_ContentPlaceHolder2_tipNivel').hide();
        $('#ctl00_ContentPlaceHolder2_tipCambio').show();
        if ($('#ctl00_ContentPlaceHolder2_hidNuevoCambio').val() == "")
            $('#ctl00_ContentPlaceHolder2_hidNuevoCambio').val($("#ctl00_ContentPlaceHolder2_tipCambio").val());
    }
}


