
$(window).on("resize", function () {
    OnResize_grClientesContatosPartial();
});

var IdsGrid = "";
$(document).ready(function () {
    $("#grClientesContatosPartial").jqGrid({
        datatype: "local",
        loadonce: true,
        pager: "#pagergrClientesContatosPartial",
        url: '/Clientes/grClientesContatosPartial?IdCliente=' + IdReg,
        datatype: "json",
        //autowidth: true,
        shrinkToFit: false,
        forceFit: true,
        viewrecords: true,
        recordtext: "Item {0} de {1} ({2} Itens)",
        emptyrecords: "Sem dados",
        rowNum: 20,
        //height: 550,
        colNames: ["", "Nome", "Fone", "Celular", "E-mail", "", "<i class='novo-grid-button' onclick='OnShowWinAddEditClienteContato(0)'></i>"],
        colModel: [
            { name: 'ID_CONTATO', width: 0, hidden: true },
            { name: 'NOME', width: 80 },
            { name: 'FONE', width: 30 },
            { name: 'CELULAR', width: 30 },
            { name: 'EMAIL', width: 60 },
            {
                name: 'btnEdit', width: 40, sortable: false, fixed: true, formatter: function (e, s, item) { return "<div class='text-center'><i class='editar-grid-button' onclick='OnShowWinAddEditClienteContato(" + item.ID_CONTATO + ")'></i></div>"; }
            },
            {
                name: 'btnDelete', width: 40, sortable: false, fixed: true, formatter: function (e, s, item) { return "<div class='text-center'><i class='delete-grid-button' onclick='OnDeleteClienteContato(" + item.ID_CONTATO + ")'></i></div>"; }
            }
        ],
        loadComplete: function (result) {
            IdsGrid = $(this).getDataIDs();
            OnResize_grClientesContatosPartial();
        },
        beforeSelectRow: function (rowid, e) {
            return false;
        }
    }).jqGrid('bindKeys', {
        onDelete: function (id) {
            var Row = $(this).getLocalRow(id);
            OnDeleteClienteContato(Row.ID_TIPO);
        },
        scrollingRows: true
    }).jqGrid('filterToolbar', {
        defaultSearch: "cn",
        searchOnEnter: false
    });
})

function OnLoad_grClientesContatosPartial() {
    OnLoadingPanelObj($('#grClientesContatosPartial').closest(".ui-jqgrid").parent(), true, "");
    $('#grClientesContatosPartial').jqGrid("clearGridData");

    $.ajax({
        url: '/Clientes/grClientesContatosPartial',
        data: { IdCliente: IdReg },
        success: function (result) {
            if (result.Erro != null) {
                OnAlertLobibox(2, "", result.Erro);
            }
            else {
                $('#grClientesContatosPartial').jqGrid("setGridParam", { data: result });
                $('#grClientesContatosPartial').trigger("reloadGrid");
            }
            OnLoadingPanelObj($('#grClientesContatosPartial').closest(".ui-jqgrid").parent(), false, "");
        },
        error: function (request, status, error) {
            console.log(request.responseText);
        }
    });
}

function OnResize_grClientesContatosPartial() {
    var newWidth = $("#grClientesContatosPartial").closest(".ui-jqgrid").parent().width();
    $("#grClientesContatosPartial").jqGrid("setGridWidth", newWidth, true);
}
