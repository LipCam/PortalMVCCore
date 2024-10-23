$(document).ready(function () {
    $('#main-menu').smartmenus();
})

//----- header nome usuario -----
$(document).delegate('header nav .user.off', 'click', function (event) {
    event.preventDefault();
    $(this).removeClass('off');
    $(this).addClass('on');
});

$(document).delegate('header nav .user.on', 'click', function (event) {
    event.preventDefault();
    $(this).removeClass('on');
    $(this).addClass('off');
});
//--------------------

//----- menu mobile -----
$(document).delegate('.full-area.menu-closed button.menu-mobile', 'click', function (event) {
    event.preventDefault();
    $('.full-area').removeClass('menu-closed');
    $('.full-area').addClass('menu-open');
});
$(document).delegate('.full-area.menu-open button.menu-mobile', 'click', function (event) {
    event.preventDefault();
    $('.full-area').removeClass('menu-open');
    $('.full-area').addClass('menu-closed');
});
//--------------------

function OnGetDadosHeader(NomeController) {
    $.ajax({
        url: '/Home/GetDadosHeader',
        data: { NomeController: NomeController },
        success: function (result) {
            $(result).each(function () {
                $("#spUsuario").text(result.Usuario);
                $("#strUsuario").attr("title", result.Usuario);
                $("#spUsuariopqn").text(result.Usuario);
                $("#lblDescricaoController").text(result.DescricaoController);
                document.title = result.DescricaoController + " - Portal MVC Core";

                if (result.IntervNotific > 0)
                    var refresh = setInterval(function () { OnGetNotificacao() }, result.IntervNotific * 1000);

                var IdTipoLogin = parseInt(result.IdTipoLogin) || 0;
                if (IdTipoLogin == 0)
                    $("#grmAreaAcesso").attr("style", "display:block;");

                if (IdTipoLogin == 0 || IdTipoLogin == 1) {
                    $("#grmCadastro").attr("style", "display:block;");
                    $("#grmEquipamentos").attr("style", "display:block;");
                }
            })
        }
    });
}

function OnLogout() {
    OnLoadingPanel(true, "");
    $.ajax({
        url: '/Login/LogOut',
        success: function (result) {
            document.location = '/Login/Index';
        }
    })
}