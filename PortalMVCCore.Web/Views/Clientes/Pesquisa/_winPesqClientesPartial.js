
var EnterGridPesqClientes = false;
$(document).ready(function () {
    $("#divWinPesqClientes").html(
       "<div id='winPesqClientesPartial' title='Clientes'>\
            <div class='container'>\
                <div class='my-2'>\
                    <table id='grPesqClientesPartial'></table>\
                    <div id='PagergrPesqClientes'></div>\
                </div>\
            </div>\
        </div>");

    $("#winPesqClientesPartial").dialog({
        autoOpen: false,
        modal: true,
        width: 700,
        resizable: false,
        draggable: false
    });

    $("#winPesqClientesPartial").on("dialogopen", function (event) {
        var myVar = setTimeout(function () { $("#txtTPesqPesqClientes").focus(); }, 100);
    });
})

function OnShowWinPesqClientes() {
    if (!EnterGridPesqClientes) {
        OnFiltrarPesqClientes();
        $("#winPesqClientesPartial").dialog("open");
    }
    else
        EnterGridPesqClientes = false;
}

function OnFiltrarPesqClientes() {
    OnLoad_grPesqClientesPartial();
}
