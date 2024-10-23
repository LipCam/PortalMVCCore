function OnLoadingPanelObj(obj, show, text) {
    if (text == "" || text == null)
        text = "Carregando...";

    $(obj).unblock();
    if (show)
        $(obj).block({
            message: "<div style='height: 50px;line-height: 50px;padding: 0 50%;float: left;'><img src='/lib/jquery.blockUI/images/loading.gif' style='height: 100px;float: left;'><span style='float: left;width: 100%;margin-top: -25px;'>Carregando...</span></div>",
            //message: "<b>" + text + "</b>",
            css: {
                padding: '6px',
                "font-size": '15px',
                //"border-radius": '10px',
                border: '0px',
                backgroundColor: 'transparent',
                color: 'white'
            },
            overlayCSS: {
                backgroundColor: '#000',
                opacity: 0.7,
                cursor: 'wait'
            }
        });
}

function OnLoadingPanel(show, text) {
    if (text == "" || text == null)
        text = "Carregando...";

    $.unblockUI();
    if (show)
        $.blockUI({
            message: "<div style='height: 50px;line-height: 50px;padding: 0 50%;float: left;'><img src='/lib/jquery.blockUI/images/loading.gif' style='height: 100px;float: left;'><span style='float: left;width: 100%;margin-top: -25px;'>Carregando...</span></div>",
            //message: "<b>" + text + "</b>",
            css: {
                padding: '6px',
                "font-size": '15px',
                //"border-radius": '10px',
                border: '0px',
                backgroundColor: 'transparent',
                color: 'white'
            },
            overlayCSS: {
                backgroundColor: '#000',
                opacity: 0.7,
                cursor: 'wait'
            }
        });
}