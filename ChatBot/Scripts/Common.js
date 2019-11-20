function getActionUrl(modo, url) {
    var loc = window.location.pathname + (window.location.pathname.indexOf('.aspx') === -1 ? '.aspx' : '');
    var page = loc.substring(loc.lastIndexOf('/') + 1, loc.length);

    switch (modo) {
        case "@Sav":
            return page + "/Guardar";
        case "@New":
            return page + "/Insertar";
        case "@Upd":
            return page + "/Modificar";
        case "@Del":
            return page + "/Eliminar";
        case "@Get":
            return page + "/Obtener";
        case "@Fil":
            return page + "/Filtrar";
        case "@Idioma":
            return url;
        default:
            return page + "/" + modo;
    }
}

function checkError(result) {

    if (result.responseJSON != undefined) {
        showMessage(result.responseJSON.Message, 5000, "danger");
        return true;
    }
    
    switch (result.d.Result) {
        case "Message":
            showMessage(result.d.Message, 5000, "success");
            return false;
        case "Success":
            return false;
        case "Error":
            showMessage(result.d.Message, 5000, "danger");
            return true;
        case "DataBaseCorrupt":
            showMessage(result.d.Message, 5000, "danger");

            window.setTimeout(function () {
                window.location.replace("http://localhost/SitioWeb/Seguridad/BaseDeDatos.aspx");
            }, 5100);

            break;
    }

    return true;
}

function showMessage(message, delay, style) {
    let container = "body";

    if ($('.modal').hasClass("in")) {
        container = "#" + $('.modal').attr("id");
    }

    style = (style === null ? "success" : style);
    message = message.replace(/\r\n/g, "<br/>");

    var div = document.createElement('div');
    div.id = "__messageError";
    div.classList.add("alert");
    div.classList.add("alert-" + style);
    div.setAttribute('style', 'position: absolute; z-index: 1; top:50px; width:100%; text-align:center');
    div.innerHTML = message;
    
    $("#__messageError").remove();

    $(container).append(div);

    window.setTimeout(function () {
        $("#__messageError").fadeOut("slow");
    }, delay);
}

function genericAction(action, parameters, url) {
    //Para evitar multiples requests.
    $(event.target).attr("disabled", true);

    $.ajax({
        type: "POST",
        url: getActionUrl(action, url),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify(parameters),
        context: { button: event.target },
        success: function (result) {
            location.reload();
        },
        error: function (error) {
            $(this.button).attr("disabled", false);
            checkError(error);
        }
    });
}


