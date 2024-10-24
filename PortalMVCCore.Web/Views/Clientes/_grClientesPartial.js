
$(window).on("resize", function () {
    OnResize_grClientesPartial();
});

var IdsGrid = "";
$(document).ready(function () {
    $("#grClientesPartial").jqGrid({
        datatype: "local",
        loadonce: true,
        pager: "#pager",
        url: '/Clientes/grClientesPartial',
        datatype: "json",
        //autowidth: true,
        shrinkToFit: false,
        forceFit: true,
        viewrecords: true,
        recordtext: "Item {0} de {1} ({2} Itens)",
        emptyrecords: "Sem dados",
        rowNum: 20,
        height: 550,
        colNames: ["Código", "Nome", "", "<i class='novo-grid-button' onclick='OnNovo()'></i>"],
        colModel: [
            { name: 'ID_CLIENTE', width: 100, fixed: true },
            { name: 'NOME', width: 300 },
            { name: 'btnEdit', width: 40, sortable: false, fixed: true, formatter: function (e, s, item) { return "<div class='text-center'><i class='editar-grid-button' onclick='OnEdit(" + item.ID_CLIENTE + ")'></i></div>"; } },
            { name: 'btnDelete', width: 40, sortable: false, fixed: true, formatter: function (e, s, item) { return "<div class='text-center'><i class='delete-grid-button' onclick='OnDelete(" + item.ID_CLIENTE + ")'></i></div>"; } }
        ],
        loadComplete: function (result) {
            IdsGrid = $(this).getDataIDs();
            OnResize_grClientesPartial();
        },
        ondblClickRow: function (id) {
            var Row = $(this).getLocalRow(id);
            OnEdit(Row.ID_CLIENTE);
        },
    }).jqGrid('bindKeys', {
        onEnter: function (id) {
            var Row = $(this).getLocalRow(id);
            OnEdit(Row.ID_CLIENTE);
        },
        onDelete: function (id) {
            var Row = $(this).getLocalRow(id);
            OnDelete(Row.ID_CLIENTE);
        },
        scrollingRows: true
    }).jqGrid('filterToolbar', {
        defaultSearch: "cn",
        searchOnEnter: false
    });
})

function OnLoad_grClientesPartial() {
    OnLoadingPanelObj($('#grClientesPartial').closest(".ui-jqgrid").parent(), true, "");
    $('#grClientesPartial').jqGrid("clearGridData");

    $.ajax({
        url: '/Clientes/grClientesPartial',
        success: function (result) {
            if (result.Erro != null) {
                OnAlertLobibox(2, "", result.Erro);
            }
            else {
                $('#grClientesPartial').jqGrid("setGridParam", { data: result });
                $('#grClientesPartial').trigger("reloadGrid");
            }
            OnLoadingPanelObj($('#grClientesPartial').closest(".ui-jqgrid").parent(), false, "");
        },
        error: function (request, status, error) {
            console.log(request.responseText);
        }
    });
}

function OnResize_grClientesPartial() {
    var newWidth = $("#grClientesPartial").closest(".ui-jqgrid").parent().width();
    $("#grClientesPartial").jqGrid("setGridWidth", newWidth, true);
}
