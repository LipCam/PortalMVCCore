
$(window).on("resize", function () {
    OnResize_grUsuariosPartial();
});

var IdsGrid = "";
$(document).ready(function () {
    $("#grUsuariosPartial").jqGrid({
        datatype: "local",
        loadonce: true,
        pager: "#pager",
        url: '/Usuarios/grUsuariosPartial',
        datatype: "json",
        //autowidth: true,
        shrinkToFit: false,
        forceFit: true,
        viewrecords: true,
        recordtext: "Item {0} de {1} ({2} Itens)",
        emptyrecords: "Sem dados",
        rowNum: 20,
        height: 550,
        colNames: ["Código", "Nome", "Usuário", "", "<i class='novo-grid-button' onclick='OnNovo()'></i>"],
        colModel: [
            { name: 'ID_USUARIO', width: 100, fixed: true },
            { name: 'NOME', width: 300 },
            { name: 'USUARIO', width: 300 },
            { name: 'btnEdit', width: 40, sortable: false, fixed: true, formatter: function (e, s, item) { return "<div class='text-center'><i class='editar-grid-button' onclick='OnEdit(" + item.ID_USUARIO + ")'></i></div>"; } },
            { name: 'btnDelete', width: 40, sortable: false, fixed: true, formatter: function (e, s, item) { return "<div class='text-center'><i class='delete-grid-button' onclick='OnDelete(" + item.ID_USUARIO + ")'></i></div>"; } }
        ],
        loadComplete: function (result) {
            IdsGrid = $(this).getDataIDs();
            OnResize_grUsuariosPartial();
        },
        ondblClickRow: function (id) {
            var Row = $(this).getLocalRow(id);
            OnEdit(Row.ID_USUARIO);
        },
    }).jqGrid('bindKeys', {
        onEnter: function (id) {
            var Row = $(this).getLocalRow(id);
            OnEdit(Row.ID_USUARIO);
        },
        onDelete: function (id) {
            var Row = $(this).getLocalRow(id);
            OnDelete(Row.ID_USUARIO);
        },
        scrollingRows: true
    }).jqGrid('filterToolbar', {
        defaultSearch: "cn",
        searchOnEnter: false
    });
})

function OnLoad_grUsuariosPartial() {
    OnLoadingPanelObj($('#grUsuariosPartial').closest(".ui-jqgrid").parent(), true, "");
    $('#grUsuariosPartial').jqGrid("clearGridData");

    $.ajax({
        url: '/Usuarios/grUsuariosPartial',
        success: function (result) {
            if (result.Erro != null) {
                OnAlertLobibox(2, "", result.Erro);
            }
            else {
                $('#grUsuariosPartial').jqGrid("setGridParam", { data: result });
                $('#grUsuariosPartial').trigger("reloadGrid");
            }
            OnLoadingPanelObj($('#grUsuariosPartial').closest(".ui-jqgrid").parent(), false, "");
        },
        error: function (request, status, error) {
            console.log(request.responseText);
        }
    });
}

function OnResize_grUsuariosPartial() {
    var newWidth = $("#grUsuariosPartial").closest(".ui-jqgrid").parent().width();
    $("#grUsuariosPartial").jqGrid("setGridWidth", newWidth, true);
}
