<%@ Page Title="Administración del Chatbot" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ChatbotAdm.aspx.cs" Inherits="ChatBot.ChatbotAdm" %>
<%@ Register TagPrefix="CW" Namespace="Framework.WebControls" Assembly="Framework" %>
<%@ Import Namespace="Framework" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <% const string _seccion = "Chatbot";%>

    <style>
        .bs-example {
            position: relative;
            padding: 15px 15px 15px;
            border-color: #e5e5e5 #eee #eee;
            border-style: solid;
            border-width: 1px 1px 0px 1px;
        }

        .bs-example + pre {
            /* border-width: 0 0 1px; */
            border-radius: 0px !important;
        }

        kbd {
            padding: 2px 4px;
            font-size: 90%;
            color: #fff;
            background-color: #333;
            border-radius: 3px;
            -webkit-box-shadow: inset 0 -1px 0 rgba(0,0,0,.25);
            box-shadow: inset 0 -1px 0 rgba(0,0,0,.25);
        }

        span[name='spanPalabra']{
            cursor:pointer;
            margin-right:10px;
            user-select: none;
        }
    </style>

    <div class="form-group">
        <div class="row">
            <div class="col-sm-6">
                <h2><label id="tituloPrincipal" runat="server" class="col-form-label"></label></h2>
            </div>
            <div class="col-sm-6 text-right">
                <div class="btn-group" role="group">
                    <button type="button" runat="server" id="btnGuardarName" class="btn btn-primary"><span class="glyphicon glyphicon-floppy-disk" aria-hidden="true"></span>&nbsp;<span id="lblGuardarName" runat="server"></span></button>
                    <button type="submit" runat="server" id="btnFiltrar" class="btn btn-success"><span class="glyphicon glyphicon-search" aria-hidden="true"></span>&nbsp;<span id="lblFiltrar" runat="server"></span></button>
                </div>
            </div>
        </div>
    </div>

    <div class="form-group">
        <div class="row">
            <label id="lblCliente" for="ddlCliente" class="col-sm-2 col-form-label" runat="server" />
            <div class="col-sm-3">
                <asp:DropDownList ID="ddlCliente" runat="server" CssClass="form-control form-control-sm" />
            </div>

            <label id="lblChatbotName" runat="server" for="txtChatbotName" class="col-sm-2 col-form-label" />
            <div class="col-sm-3">
                <div class="input-group">
                    <input id="txtChatbotName" type="text" class="form-control form-control-sm" runat="server"/>
                    <span class="input-group-btn">
                        <button id="btnGuardarChatbotName" class="btn btn-primary" type="button"><span class="glyphicon glyphicon-floppy-disk" aria-hidden="true"></span></button>
                    </span>
                </div><!-- /input-group -->
            </div>
        </div>        
    </div> 
    
    <div class="form-group">
        <div class="bs-example" id="lblAyuda" runat="server">
            
        </div>
        <pre><code class="language-html" data-lang="html" id="chatbotCode" runat="server"></code></pre>
    </div>
    
    <CW:Grid runat="server" ID="grdAprender" />
    
    <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Modal title</h4>
                </div>
                <div class="modal-body">
                    
                    <div class="row form-group">
                        <div class="col-sm-12">
                            <label id="lblFrase" class="col-form-label" runat="server"></label>
                            <h4 id="divPalabras">
                            </h4>
                        </div>
                    </div>
                    <div class="row form-group">
                        <div class="col-sm-12">
                            <input id="txtFrase" class="form-control form-control-sm" />
                        </div>
                    </div>

                    <div class="row form-group">
                        <div class="col-sm-12">
                            <label id="lblRespuesta" for="txtRespuesta" class="col-form-label" runat="server"></label>
                            <input id="txtRespuesta" class="form-control form-control-sm" />
                        </div>
                    </div>

                    <input type="hidden" id="hddModo" value="alta" />
                    <input type="hidden" id="hddId" value="0" />
                </div>
                <div class="modal-footer">
                    <button id="btnCancelar" runat="server" type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    <button id="btnGuardar" runat="server" type="button" class="btn btn-primary">Save changes</button>
                </div>
            </div><!-- /.modal-content -->
        </div><!-- /.modal-dialog -->
    </div>

    <script type="text/javascript">
        const tituloNuevo = '<%: MultiLanguage.GetTranslate(_seccion, "tituloNuevo")%>';
        var cant = 0;

        $(document).ready(function () {
            $("#btnGuardarChatbotName").click(function () {
                let parameters = {
                    "IdCliente": parseInt($("#ddlCliente").val()),
                    "ChatbotName": $("#txtChatbotName").val()
                }

                Accion("GuardarChatbotName", parameters);
            });

            $("#btnGuardar").click(function () {
                let palabras = $(".label-success").map(function () {
                    return this.innerHTML;
                }).get();

                console.log(palabras);

                let parameters = {
                    "IdAprender": $("#hddId").val(),
                    "Palabras": palabras,
                    "Frase": $("#txtFrase").val(),
                    "Respuesta": $("#txtRespuesta").val()
                }

                Accion("GuardarFrasePalabra", parameters);
            });
        });

        //--||-----------------------------------------------------------------------------------||--//
        $('#exampleModal').on('show.bs.modal', function (event) {
            var button = $(event.relatedTarget) // Button that triggered the modal
            var mode = button.data('mode') // Extract info from data-* attributes

            //Guardo el modo en el que ingreso a la pantalla.
            $("#hddModo").val(mode);

            // If necessary, you could initiate an AJAX request here (and then do the updating in a callback).
            // Update the modal's content. We'll use jQuery here, but you could use a data binding library or other methods instead.
            var modal = $(this);
            var title;

            switch (mode) {
                case "@New":
                    cant = 0;
                    Get(grdAprender);
                    title = tituloNuevo;
                    break;
            }

            modal.find('.modal-title').text(title)
        })
        //--||-----------------------------------------------------------------------------------||--//
        function Get(grid) {
            let entity = JSON.parse($(grid.activeRow).attr("rowdata"));
            let palabra = "<span name='spanPalabra' class='label label-default'>{0}</span>";

            //Seteo el id del mensaje a aprender.
            $("#hddId").val(entity.IdAprender);

            //Seteo la frase para editar y guardar.
            $("#txtFrase").val(entity.Frase);

            //Limpio el div
            $("#divPalabras").html("");

            entity.Frase.split(" ").forEach(function (item) {
                $("#divPalabras").append(palabra.replace("{0}", item));
            });

            $("span[name='spanPalabra']").click(function () {
                if ($(this).hasClass("label-default")) {
                    if(cant == 3) return false;

                    $(this).removeClass("label-default");
                    $(this).addClass("label-success");
                    cant++;
                }
                else {
                    $(this).removeClass("label-success");
                    $(this).addClass("label-default");
                    cant--;
                }

                console.log(cant);
            });
        }

        function Accion(action, parameters) {
            $.ajax({
                type: "POST",
                url: getActionUrl(action),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify( parameters ),
                success: function (result) {
                    location.reload();
                },
                error: function (error) {
                    showMessage(error.responseJSON.Message, 5000, "danger");
                }
            });
        }
        //--||-----------------------------------------------------------------------------------||--//
    </script>

</asp:Content>
