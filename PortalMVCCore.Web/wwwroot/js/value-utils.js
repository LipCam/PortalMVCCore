$(document).ready(function () {
    //método para destacar os campos com erros
    $.validator.setDefaults({
        highlight: function (element, errorClass, validClass) {
            $(element).addClass("is-invalid");
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).removeClass("is-invalid");
        },
    });

    $(".cep-field").mask('00.000-000', { reverse: true });

    $(".fone-field").keyup(function (e) {
        var valor = $(this).val().replace("(", "").replace(")", "").replace("-", "");

        if ((valor + e.key).length == 11)
            $(this).mask('(00)00000-0000', { reverse: false });
        else if (valor.length <= 10)
            $(this).mask('(00)0000-0000', { reverse: false });
    })
    .focusout(function () {
        var valor = $(this).val().replace("(", "").replace(")", "").replace("-", "");
        if (valor.length <= 10)
            $(this).mask('(00)0000-0000', { reverse: false });
        else if (valor.length == 11)
            $(this).mask('(00)00000-0000', { reverse: false });
    })
    .focusin(function () {
        var valor = $(this).val().replace("(", "").replace(")", "").replace("-", "");
        if (valor.length <= 10)
            $(this).mask('(00)0000-0000', { reverse: false });
        else if (valor.length == 11)
            $(this).mask('(00)00000-0000', { reverse: false });
    })

    $(".cnpj-field").mask('00.000.000/0000-00', { reverse: true });
    $(".cnpj-field").focusout(function () {
        var CNPJ = $(this).val().replace(".", "").replace(".", "").replace("/", "").replace("-", "");
        var obj = $(this);

        if (CNPJ.trim() != "") {
            if (!OnValidaCNPJ(CNPJ)) {
                $("span[data-valmsg-for='" + obj.attr("id") + "']").text("CNPJ inválido.");
                obj.addClass("is-invalid");
                obj.val("");
            }
        }
    });

    $(".cpf-field").mask('000.000.000-00', { reverse: true });
    $(".cpf-field").focusout(function () {
        var CPF = $(this).val().replace(".", "").replace(".", "").replace("-", "");
        var obj = $(this);
        
        if (CPF.trim() != "") {
            if (!OnValidaCPF(CPF)) {
                $("span[data-valmsg-for='" + obj.attr("id") + "']").text("CPF inválido");
                obj.addClass("is-invalid");
                obj.val("");
            }
        }
    });

    $(".int1-field").focusout(function () {
        $(this).val(parseInt($(this).val()) || 1);
    });

    $(".int-null-field").focusout(function () {
        $(this).val(parseInt($(this).val()) || "");
    });

    $(".float2-field").focusout(function () {
        $(this).val(OnParseFloat_Str($(this).val(), 2, false));
    });

    $(".float3-field").focusout(function () {
        $(this).val(OnParseFloat_Str($(this).val(), 3, false));
    });

    $(".float4-field").focusout(function () {
        $(this).val(OnParseFloat_Str($(this).val(), 4, false));
    });

    $(".float5-field").focusout(function () {
        $(this).val(OnParseFloat_Str($(this).val(), 5, false));
    });

    //positivos
    //zerados
    $(".int-pos-field").focusout(function () {
        var valor = parseInt($(this).val()) || 0;
        $(this).val(valor < 0 ? 0 : valor);
    });

    $(".int-pos-null-field").focusout(function () {
        var valor = parseInt($(this).val()) || 0;
        $(this).val(valor <= 0 ? "" : valor);
    });

    $(".int-pos1-field").focusout(function () {
        var valor = parseInt($(this).val()) || 1;
        $(this).val(valor < 0 ? 0 : valor);
    });

    $(".float2-pos-field").focusout(function () {
        $(this).val(OnParseFloat_Str($(this).val(), 2, true));
    });

    $(".float3-pos-field").focusout(function () {
        $(this).val(OnParseFloat_Str($(this).val(), 3, true));
    });

    $(".float4-pos-field").focusout(function () {
        $(this).val(OnParseFloat_Str($(this).val(), 4, true));
    });

    $(".float5-pos-field").focusout(function () {
        $(this).val(OnParseFloat_Str($(this).val(), 5, true));
    });

    $(".float6-pos-field").focusout(function () {
        $(this).val(OnParseFloat_Str($(this).val(), 6, true));
    });

    $(".float7-pos-field").focusout(function () {
        $(this).val(OnParseFloat_Str($(this).val(), 7, true));
    });
    //---------------------
})

function OnParseFloat_Str(valor, numCasas, positivo) {
    valor = (parseFloat(valor.replace(",", ".")) || 0);

    if (positivo)
        return (valor < 0 ? 0 : valor).toFixed(numCasas).replace(".", ",");
    return valor.toFixed(numCasas).replace(".", ",");
}

function OnFormatNumLen(num, length) {
    var r = "" + num;
    while (r.length < length) {
        r = "0" + r;
    }
    return r;
}

function OnValidaData(data) {
    var bits = (data.split('-')[2] + "/" + data.split('-')[1] + "/" + data.split('-')[0]).split('/');
    var d = new Date(bits[2] + '/' + bits[1] + '/' + bits[0]);
    return !!(d && (d.getMonth() + 1) == bits[1] && d.getDate() == Number(bits[0]));
}

function ConvertFloat(valor, numcasa) {
    var value = parseFloat(valor.replace(',', '.')) || 0;
    return value.toFixed(numcasa).replace('.', ',');
}

//op = 0- dd/MM/yyyy, 1- yyyy-MM-dd, 2- dd/MM/yyyy HH:mm, 3- HH:mm, 4- yyyy-MM-dd HH:mm:ss
function OnConvertDataJsonStr(DataJson, op) {
    if (DataJson == null)
        return "";

    var Result = "";
    var dt = new Date();
    dt = new Date(parseInt(DataJson.substr(6)));

    if (op == 0)
        Result = OnFormatNumLen(dt.getDate(), 2) + "/" + OnFormatNumLen(dt.getMonth() + 1, 2) + "/" + dt.getFullYear();
    else if (op == 1)
        Result = dt.getFullYear() + "-" + OnFormatNumLen(dt.getMonth() + 1, 2) + "-" + OnFormatNumLen(dt.getDate(), 2);
    else if (op == 2)
        Result = OnFormatNumLen(dt.getDate(), 2) + "/" + OnFormatNumLen(dt.getMonth() + 1, 2) + "/" + dt.getFullYear() + " " + OnFormatNumLen(dt.getHours(), 2) + ":" + OnFormatNumLen(dt.getMinutes(), 2);
    else if (op == 3)
        Result = OnFormatNumLen(dt.getHours(), 2) + ":" + OnFormatNumLen(dt.getMinutes(), 2);
    else if (op == 4)
        Result = dt.getFullYear() + "-" + OnFormatNumLen(dt.getMonth() + 1, 2) + "-" + OnFormatNumLen(dt.getDate(), 2) + " " + OnFormatNumLen(dt.getHours(), 2) + ":" + OnFormatNumLen(dt.getMinutes(), 2) + ":" + OnFormatNumLen(dt.getSeconds(), 2);

    return Result;
}

function OnContemLetra(Valor) {
    var letterNumber = /[a-z]/i;
    if (Valor.match(letterNumber))
        return true;
    else
        return false;
}

function OnValidaCNPJ(cnpj) {
    cnpj = cnpj.replace(/[^\d]+/g, ''); // Remove caracteres não numéricos

    if (cnpj.length !== 14 || /^(\d)\1{13}$/.test(cnpj)) {
        return false; // Verifica se o CNPJ tem 14 dígitos e se não é uma sequência de números repetidos
    }

    let tamanho = cnpj.length - 2;
    let numeros = cnpj.substring(0, tamanho);
    let digitos = cnpj.substring(tamanho);
    let soma = 0;
    let pos = tamanho - 7;

    for (let i = tamanho; i >= 1; i--) {
        soma += numeros.charAt(tamanho - i) * pos--;
        if (pos < 2) pos = 9;
    }

    let resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;

    if (resultado !== parseInt(digitos.charAt(0))) {
        return false;
    }

    tamanho = tamanho + 1;
    numeros = cnpj.substring(0, tamanho);
    soma = 0;
    pos = tamanho - 7;

    for (let i = tamanho; i >= 1; i--) {
        soma += numeros.charAt(tamanho - i) * pos--;
        if (pos < 2) pos = 9;
    }

    resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;

    if (resultado !== parseInt(digitos.charAt(1))) {
        return false;
    }

    return true;
}

function OnValidaCPF(cpf) {
    cpf = cpf.replace(/[^\d]+/g, ''); // Remove caracteres não numéricos

    if (cpf.length !== 11 || /^(\d)\1{10}$/.test(cpf)) {
        return false; // Verifica se o CPF tem 11 dígitos e se não é uma sequência de números repetidos
    }

    let soma = 0;
    let resto;

    for (let i = 1; i <= 9; i++) {
        soma += parseInt(cpf.substring(i - 1, i)) * (11 - i);
    }

    resto = (soma * 10) % 11;

    if (resto === 10 || resto === 11) {
        resto = 0;
    }

    if (resto !== parseInt(cpf.substring(9, 10))) {
        return false;
    }

    soma = 0;

    for (let i = 1; i <= 10; i++) {
        soma += parseInt(cpf.substring(i - 1, i)) * (12 - i);
    }

    resto = (soma * 10) % 11;

    if (resto === 10 || resto === 11) {
        resto = 0;
    }

    if (resto !== parseInt(cpf.substring(10, 11))) {
        return false;
    }

    return true;
}

function OnLimpaValidacoesForm(form) {
    $("#" + $(form).attr("id") + " input").each(function () {
        $(this).removeClass("is-invalid");
        $("span[data-valmsg-for='" + $(this).attr("id") + "']").text("");
    })
}
