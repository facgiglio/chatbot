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
        <div class="bs-example">
            Para implementar el chatbot en su página, seleccione el código de abajo y presione
            <kbd><kbd>ctrl</kbd> + <kbd>C</kbd></kbd> para copiar el código y luego
            <kbd><kbd>ctrl</kbd> + <kbd>V</kbd></kbd> para pegar el código en su sitio web.
        </div>
        <pre><code class="language-html" data-lang="html" id="chatbotCode" runat="server"></code></pre>
    </div>
    
    <CW:Grid runat="server" ID="grdCliente" />
    
    <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Modal title</h4>
                </div>
                <div class="modal-body">
                    
                    <div class="row form-group">
                        <div class="col-sm-6">
                            <label id="lblDireccion" for="txtDireccion" class="col-form-label" runat="server"></label>
                            <input id="txtDireccion" class="form-control form-control-sm" />
                        </div>
                        <div class="col-sm-3">
                            <label id="lblCodigoPostal" for="txtCodigoPostal" class="col-form-label" runat="server"></label>
                            <input id="txtCodigoPostal" class="form-control form-control-sm"/>
                        </div>
                        <div class="col-sm-3">
                            <label id="lblTelefono" for="txtTelefono" class="col-form-label" runat="server"></label>
                            <input id="txtTelefono" class="form-control form-control-sm"/>
                        </div>
                    </div>
                    <div class="row form-group">
                        <div class="col-sm-6">
                            <label id="lblHostName" for="txtHostName" class="col-form-label" runat="server"></label>
                            <input id="txtHostName" class="form-control form-control-sm" />
                        </div>
                        <div class="col-sm-6">
                            <label id="lblHashKey" for="txtHashKey" class="col-form-label" runat="server"></label>
                            <input id="txtHashKey" class="form-control form-control-sm" disabled/>
                        </div>
                    </div>

                    <input type="hidden" id="hddModo" value="alta" />
                    <input type="hidden" id="hddId" value="0" />
                    <input type="hidden" id="hddContrasena" value="0" />
                </div>
                <div class="modal-footer">
                    <button id="btnCancelar" runat="server" type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    <button id="btnGuardar" runat="server" type="button" class="btn btn-primary" onclick="Accion(); return false;">Save changes</button>
                </div>
            </div><!-- /.modal-content -->
        </div><!-- /.modal-dialog -->
    </div>

    <script type="text/javascript">
        const tituloNuevo = '<%: MultiLanguage.GetTranslate(_seccion, "tituloNuevoCliente")%>';
        const tituloActualizar = '<%: MultiLanguage.GetTranslate(_seccion, "tituloActualizarCliente")%>';
        const tituloEliminar = '<%: MultiLanguage.GetTranslate(_seccion, "tituloEliminarCliente")%>';

        $(document).ready(function () {
            $("#btnGuardarChatbotName").click(function () {
                var parameters = {
                    "IdCliente": parseInt($("#ddlCliente").val()),
                    "ChatbotName": $("#txtChatbotName").val()
                }

                Accion("GuardarChatbotName", parameters);
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
                    ClearData();
                    title = tituloNuevo;
                    break;
                case "@Upd":
                    Get(grdCliente);
                    title = tituloActualizar;
                    break;
                case "@Del":
                    Get(grdCliente);
                    title = tituloEliminar;
                    break;
            }

            modal.find('.modal-title').text(title)
        })
        //--||-----------------------------------------------------------------------------------||--//
        function Get(grid) {
            var entity = JSON.parse($(grid.activeRow).attr("rowdata"));
            var jsonData = { "Id": entity.IdCliente}
            $.ajax({
                type: "POST",
                url: getActionUrl("@Get"),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify(jsonData),
                success: function (result) {
                    LoadData(result.d)
                }
            });
        }

        function HashKey(grid, type) {
            var entity = JSON.parse($(grid.activeRow).attr("rowdata"));
            var jsonData = { "Id": entity.IdCliente}
            $.ajax({
                type: "POST",
                url: getActionUrl(type),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify(jsonData),
                success: function (result) {
                    location.reload();
                }
            });
        }

        function LoadData(entity) { 
            ClearData();

            $("#hddId").val(entity.IdCliente);
            $("#txtRazonSocial").val(entity.RazonSocial);
            $("#txtDireccion").val(entity.Direccion);
            $("#txtCodigoPostal").val(entity.CodigoPostal);
            $("#txtTelefono").val(entity.Telefono);
            $("#txtHostName").val(entity.HostName);
            $("#txtHashKey").val(entity.HashKey);
        }

        function ClearData() {
            $("#hddId").val(0);
            $("#txtRazonSocial").val("");
            $("#txtDireccion").val("");
            $("#txtCodigoPostal").val("");
            $("#txtTelefono").val("");
            $("#txtHostName").val("");
            $("#txtHashKey").val("");
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
                    showMessage("#exampleModal", error.responseJSON.Message, 5000, "danger");
                }
            });
        }
        //--||-----------------------------------------------------------------------------------||--//

    </script>

</asp:Content>
