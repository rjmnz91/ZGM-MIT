
$(document).ready(function () {

    var key = "Terminal";

    $('#lnkShow').click(function () {
        var val = $.jStorage.get(key);
        alert(val);
    });

    $('#lnkStorage').click(function () {
        var terminal = prompt('ID Terminal', '');
        if (terminal != null) {
            var val = $.jStorage.set(key, terminal);
        }
    });

});