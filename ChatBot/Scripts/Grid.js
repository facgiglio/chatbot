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

        $(this.jQueryId + " tbody").on("click", { grd: this }, function (event) {
            //Guardo la fila seleccionada.
            event.data.grd.activeRow = $(event.target).closest("tr");
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
            })
        }
    }

    //---------------------------------------------------------------------------------------//
    // FUNCIONES PERSONALIZADAS 
    //---------------------------------------------------------------------------------------//
    this.click = function (e) {
        $(this.jQueryId + " tbody").click(e);
    };
};

$(document).click(function () { $("ul[aria-type='context-menu']").hide(); });