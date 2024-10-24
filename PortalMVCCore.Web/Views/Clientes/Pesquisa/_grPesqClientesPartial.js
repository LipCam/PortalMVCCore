
$(window).on("resize", function () {
    OnResize_grPesqClientesPartial();
});

var IdsgrPesqClientesPartial = "";
$(document).ready(function () {
    $("#grPesqClientesPartial").jqGrid({
        datatype: "local",
        loadonce: true,
        pager: "#PagergrPesqClientes",
        url: '/Clientes/grPesqClientesPartial',
        datatype: "json",
        autowidth: true,
        shrinkToFit: true,
        rowNum: 500,
        height: 300,
        colNames: ["Código", "Nome"],
        colModel: [
            { name: 'ID_CLIENTE', width: 60 },
            { name: 'NOME', width: 300 },
        ],
        loadComplete: function (result) {
            IdsgrPesqClientesPartial = $(this).getDataIDs();
            OnResize_grPesqClientesPartial();
        },
        ondblClickRow: function (id) {
            var Row = $(this).getLocalRow(id);
            OnClientesRowDoubleClick(Row);
        },
    }).jqGrid('bindKeys', {
        onEnter: function (id) {
            EnterGridPesqClientes = true;
            var Row = $(this).getLocalRow(id);
            OnClientesRowDoubleClick(Row);
        },
        scrollingRows: true
    }).jqGrid('filterToolbar', {
        defaultSearch: "cn",
        searchOnEnter: false
    });
})

function OnLoad_grPesqClientesPartial() {
    OnLoadingPanelObj($('#grPesqClientesPartial').closest(".ui-jqgrid").parent(), true, "");
    $('#grPesqClientesPartial').jqGrid("clearGridData");

    $.ajax({
        url: '/Clientes/grPesqClientesPartial',
        success: function (result) {
            if (result.Erro != null) {
                OnAlertLobibox(2, "", result.Erro);
            }
            else {
                $('#grPesqClientesPartial').jqGrid("setGridParam", { data: result });
                $('#grPesqClientesPartial').trigger("reloadGrid");
            }
            OnLoadingPanelObj($('#grPesqClientesPartial').closest(".ui-jqgrid").parent(), false, "");
        }
    });
}

function OnResize_grPesqClientesPartial() {
    var newWidth = $("#grPesqClientesPartial").closest(".ui-jqgrid").parent().width();
    $("#grPesqClientesPartial").jqGrid("setGridWidth", newWidth, true);
}
