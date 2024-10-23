var JsonAlertLobiboxSeq = null;
//Tipo = 1-success, 2-error, 3-warning, 4-info
function OnAlertLobibox(Tipo, Titulo, Mensagem, ObjDialog, ObjFocus, UrlRedirect) {
    var tt = "Sucesso", tp = "success";
    if (Tipo == 2) {
        tt = "Erro";
        tp = "error";
    }
    else if (Tipo == 3) {
        tt = "Atenção";
        tp = "warning";
    }
    else if (Tipo == 4) {
        tt = "Informação";
        tp = "info";
    }

    Lobibox.alert(tp, {
        title: Titulo != "" ? Titulo : tt,
        msg: Mensagem,
        buttons: {
            ok: {
                //'class': 'lobibox-btn lobibox-btn-default',
                text: 'OK (Esc)',
                closeOnClick: true
            }
        },
        beforeClose: function (a) {
            //gambiarra monstrenga descoberta com muita luta
            //para fechar apenas o alert e não o popup com o esc

            //ObjDialog = Dialog
            //ObjFocus = campo para retornar o foco

            if (JsonAlertLobiboxSeq == null) {
                if (ObjDialog != null)
                    $(ObjDialog).dialog("open");

                if (ObjFocus != null)
                    $(ObjFocus).focus();

                ObjDialog = null;
                ObjFocus = null;

                if (UrlRedirect != null)
                    document.location = UrlRedirect;
            }
            else {
                JsonAlertLobiboxSeq.splice(0, 1);
                if (JsonAlertLobiboxSeq.length > 0) {
                    OnAlertLobibox(JsonAlertLobiboxSeq[0].Tipo, JsonAlertLobiboxSeq[0].Titulo, JsonAlertLobiboxSeq[0].Mensagem, ObjDialog, ObjFocus, UrlRedirect);
                }
                else {
                    if (ObjDialog != null)
                        $(ObjDialog).dialog("open");

                    if (ObjFocus != null)
                        $(ObjFocus).focus();

                    ObjDialog = null;
                    ObjFocus = null;

                    if (UrlRedirect != null)
                        document.location = UrlRedirect;
                }
            }
        }
    });
}

//Tipo = 1-success, 2-error, 3-warning, 4-info
function OnNotifyLobibox(Tipo, Titulo, Mensagem, ObjFocus) {
    var tt = "Sucesso", tp = "success";
    if (Tipo == 2) {
        tt = "Erro";
        tp = "error";
    }
    else if (Tipo == 3) {
        tt = "Atenção";
        tp = "warning";
    }
    else if (Tipo == 4) {
        tt = "Informação";
        tp = "info";
    }

    Lobibox.notify(tp, {
        size: 'normal',// normal, mini, large
        icon: true,
        title: Titulo != "" ? Titulo : tt,
        msg: Mensagem
    });

    if (ObjFocus != null)
        $(ObjFocus).focus();
}

function OnConfirmLobibox(Mensagem, FunctionSim, FunctionNao, FunctionBeforeClose) {
    Lobibox.confirm({
        title: "Mensagem",
        msg: Mensagem,
        buttons: {
            yes: { text: 'Sim', closeOnClick: true },
            no: { text: 'Não', closeOnClick: true }
        },
        callback: function ($this, type, ev) {
            if (type == "yes" && FunctionSim != null) {
                FunctionSim();
            }
            else if (type == "no" && FunctionNao != null) {
                FunctionNao();
            }
        },
        beforeClose: function () {
            if (FunctionBeforeClose != null)
                FunctionBeforeClose();
        }
    });
}