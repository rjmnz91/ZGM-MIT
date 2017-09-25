$(document).ready(function () {
    $('#tabs').tabs({
        activate: function () {
            var newIdx = $('#tabs').tabs('option', 'active');
            $('#<%=hidSelectedTab.ClientID%>').val(newIdx);
        }, heightStyle: "auto",
        active: previouslySelectedTab
    });

// Quito el efecto, parece que si cambias rapido de pestaña se pierden la visibilidad del contenido.
//        show: { effect: "fadeIn", duration: 1000 }

    $('.ddlEstadoSolicitud').each(function () {
        // Deshabilitar Vendidos
        if (this.value == 6) {
            $(this).attr('disabled', 'disabled');
        }
    });

    $('select').change(function () {
        var combo = this;
        $('.ddlEstadoSolicitud').each(function () {
            if (combo != this) {
                $(this).attr('disabled', 'disabled');
            }
        });
        return true;
    });

});
