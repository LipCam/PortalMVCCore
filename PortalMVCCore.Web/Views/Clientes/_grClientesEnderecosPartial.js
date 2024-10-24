
$(window).on("resize", function () {
    OnResize_grClientesEnderecosPartial();
});

var IdsGrid = "";
$(document).ready(function () {
    $("#grClientesEnderecosPartial").jqGrid({
        datatype: "local",
        loadonce: true,
        pager: "#pagergrClientesEnderecosPartial",
        url: '/Clientes/grClientesEnderecosPartial?IdCliente=' + IdReg,
        datatype: "json",
        //autowidth: true,
        shrinkToFit: false,
        forceFit: true,
        viewrecords: true,
        recordtext: "Item {0} de {1} ({2} Itens)",
        emptyrecords: "Sem dados",
        rowNum: 20,
        //height: 550,
        colNames: ["", "Endereço", "Número", "Complemento", "Bairro", "Cidade", "UF", "Cep", "", "<i class='novo-grid-button' onclick='OnShowWinAddEditClienteEndereco(0)'></i>"],
        colModel: [
            { name: 'ID_ENDERECO', width: 0, hidden: true },
            { name: 'ENDERECO', width: 60 },
            { name: 'NUMERO', width: 30 },
            { name: 'COMPL', width: 60 },
            { name: 'BAIRRO', width: 60 },
            { name: 'CIDADE', width: 60 },
            { name: 'UF', width: 30 },
            { name: 'CEP', width: 60 },
            {
                name: 'btnEdit', width: 40, sortable: false, fixed: true, formatter: function (e, s, item) { return "<div class='text-center'><i class='editar-grid-button' onclick='OnShowWinAddEditClienteEndereco(" + item.ID_ENDERECO + ")'></i></div>"; }
            },
            {
                name: 'btnDelete', width: 40, sortable: false, fixed: true, formatter: function (e, s, item) { return "<div class='text-center'><i class='delete-grid-button' onclick='OnDeleteClienteEndereco(" + item.ID_ENDERECO + ")'></i></div>"; }
            }
        ],
        loadComplete: function (result) {
            IdsGrid = $(this).getDataIDs();
            OnResize_grClientesEnderecosPartial();
        },
        beforeSelectRow: function (rowid, e) {
            return false;
        }
    }).jqGrid('bindKeys', {
        onDelete: function (id) {
            var Row = $(this).getLocalRow(id);
            OnDeleteClienteEndereco(Row.ID_TIPO);
        },
        scrollingRows: true
    }).jqGrid('filterToolbar', {
        defaultSearch: "cn",
        searchOnEnter: false
    });
})

function OnLoad_grClientesEnderecosPartial() {
    OnLoadingPanelObj($('#grClientesEnderecosPartial').closest(".ui-jqgrid").parent(), true, "");
    $('#grClientesEnderecosPartial').jqGrid("clearGridData");

    $.ajax({
        url: '/Clientes/grClientesEnderecosPartial',
        data: { IdCliente: IdReg },
        success: function (result) {
            if (result.Erro != null) {
                OnAlertLobibox(2, "", result.Erro);
            }
            else {
                $('#grClientesEnderecosPartial').jqGrid("setGridParam", { data: result });
                $('#grClientesEnderecosPartial').trigger("reloadGrid");
            }
            OnLoadingPanelObj($('#grClientesEnderecosPartial').closest(".ui-jqgrid").parent(), false, "");
        },
        error: function (request, status, error) {
            console.log(request.responseText);
        }
    });
}

function OnResize_grClientesEnderecosPartial() {
    var newWidth = $("#grClientesEnderecosPartial").closest(".ui-jqgrid").parent().width();
    $("#grClientesEnderecosPartial").jqGrid("setGridWidth", newWidth, true);
}
