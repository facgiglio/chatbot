// #region Funciones genéricas de pantalla de ABM


function getActionUrl(modo) {
    var loc = window.location.pathname + (window.location.pathname.indexOf('.aspx') == -1 ? '.aspx' : '');
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
        default:
            return page + "/" + modo;
    }
}
// #endregion



// #region Administracion de Errores
function CheckError(result) {

    switch (result.d.Result) {
        case "Message":
            showMessage("body", result.d.Message, 5000, "success");
            break;
        case "Success":
            return false;
            break;
        case "Error":
            showMessage("body", result.d.Message, 5000, "danger");
            return true;
            break;
        case "DataBaseCorrupt":
            showMessage("body", result.d.Message, 5000, "danger");

            window.setTimeout(function () {
                window.location.replace("http://localhost/SitioWeb/Seguridad/BaseDeDatos.aspx");
            }, 5100);

            break;
    }

    return true;
}

function showMessage(container, message, delay, style) {
    style = (style == null ? "success" : style);

    var div = '<div id="__messageError" class="alert alert-' + style + '" role="alert" style="position: absolute; z-index: 10; top:20px; width:100%; text-align:center">#MESSAGE#</div>'

    $("#__messageError").remove();

    $(container).append(div.replace("#MESSAGE#", message));

    window.setTimeout(function () {
        $("#__messageError").fadeOut("slow");
    }, delay);
}
// #endregion

