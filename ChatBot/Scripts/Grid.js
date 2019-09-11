var Grid = function (id) {
    this.id = id;
    this.jQueryId = "#" + id;
    this.jQueryIdContextMenu = "#" + id + "CM";
    this.activeRow = null;
    this.config = null;

    this._constructor = function () {
        //Inicializo las funciones propias de la Grilla.
        this.SetClick(event);
        this.SetRigthClick(event);
    };

    this.SetClick = function (e) {

        $(this.jQueryId + " .row-container").on("click", { grd: this }, function (event) {
            //Guardo la fila seleccionada.
            event.data.grd.activeRow = this;

            //Controlo si la fila seleccionada es la activa.
            if ($(this).hasClass("row-container-click")) {
                $(this).removeClass("row-container-click");
            } else {
                //Remuevo todas las filas seleccionadas de la grilla.
                $(event.data.grd.jQueryId + " .row-container-click").each(function (index) {
                    $(this).removeClass("row-container-click");
                });

                //Seteo la fila seleccionada.
                $(this).addClass("row-container-click");
            }
        });
    };

    this.SetRigthClick = function (e) {
        //Si existe el context menu.
        if ($(this.jQueryIdContextMenu + ".dropdown-menu").length) {

            $(this.jQueryId + " tbody").on('contextmenu', this, function (e) {
                e.preventDefault();

                //Si son multiples grillas, cierre el otro context menu.
                $("div[aria-type='context-menu']").hide();

                var grdId = "#" + $(e.target).closest(".table").attr("id")

                tempX = e.pageX; //- $(grdId).offset().left;
                tempY = e.pageY;                

                $(grdId + "CM" + ".dropdown-menu").css({ 'top': tempY, 'left': tempX }).show();

                //Guardo la fila seleccionada.
                e.data.activeRow = $(e.target).closest("tr");
                /*
                //Si no seleccionó nada, deshabilito los divs marcados para deshabilitar.
                if (j$(e.target).closest(".row-container").length == 0) {
                    j$(".custom-menu .disabled .column").each(function () {
                        j$(this).css("color", "#888")
                    });
                } else {
                    j$(grdId + "CM" + ".custom-menu .disabled .column").each(function () {
                        j$(this).css("color", "")
                    });
                }*/
            })
        }
    }

    //---------------------------------------------------------------------------------------//
    // FUNCIONES PERSONALIZADAS 
    //---------------------------------------------------------------------------------------//
    this.click = function (e) {
        $(this.jQueryId + " .row-container").click(e);
    };
};

$(document).click(function () { $("ul[aria-type='context-menu']").hide(); });