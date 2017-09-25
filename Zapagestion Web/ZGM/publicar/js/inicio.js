$(document).ready(function () {

//    $('.tdOpciones').mouseenter(function () {
//        $(this).addClass('highlight ui-corner-all');
//    });
//    $('.tdOpciones').mouseleave(function () {
//        $(this).removeClass('highlight ui-corner-all');
//    });

    $('#lnkCarrito')
    .button({
        icons: { primary: "ui-icon-cart" }
    })
    .addClass('botonCarrito');

    $('#lnkLogout').button({
        icons: { primary: "ui-icon-power" }
    });

    $('#lnkBuscar').button({
        icons: { primary: "ui-icon-search" }
    });
    $('#lnkVerSolicitudes').button({
        icons: { primary: "ui-icon-comment" }
    });

    $('#lnkPruebaRedir').button({
        icons: { primary: "ui-icon-favourites" }
    });
    

    $('#txtBuscar').focus();

});