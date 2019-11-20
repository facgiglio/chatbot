<%@ Page Title="Listado de Clientes" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Cliente.aspx.cs" Inherits="ChatBot.Cliente" %>
<%@ Register TagPrefix="CW" Namespace="Framework.WebControls" Assembly="Framework" %>
<%@ Import Namespace="Framework" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <% const string _seccion = "Cliente";%>

    <div class="form-group">
        <div class="row">
            <div class="col-sm-6">
                <h2><label id="tituloPrincipal" runat="server" class="col-form-label"></label></h2>
            </div>
            <div class="col-sm-6 text-right">
                <div class="btn-group" role="group">
                    <button type="button" runat="server" id="btnNuevo" class="btn btn-primary" data-toggle="modal" data-target="#exampleModal" data-mode="@New"><span class="glyphicon glyphicon-file" aria-hidden="true"></span>&nbsp;<span id="lblNuevo" runat="server"></span></button>
                    <button type="submit" runat="server" id="btnFiltrar" class="btn btn-success"><span class="glyphicon glyphicon-search" aria-hidden="true"></span>&nbsp;<span id="lblFiltrar" runat="server"></span></button>
                </div>
            </div>
        </div>
        <div class="row">
            <label id="lblRazonSocialFiltrar" runat="server" for="txtRazonSocialFiltrar" class="col-sm-2 col-form-label"></label>
            <div class="col-sm-3">
                <input id="txtRazonSocialFiltrar" type="text" class="form-control form-control-sm">
            </div>
        </div> 
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
                        <div class="col-sm-12">
                            <label id="lblRazonSocial" for="txtRazonSocial" class="col-form-label" runat="server"></label>
                            <input id="txtRazonSocial" type="text" class="form-control form-control-sm">
                        </div>
                    </div>
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
                        <div class="col-sm-12">
                            <label id="lblHostName" for="txtHostName" class="col-form-label" runat="server"></label>
                            <input id="txtHostName" class="form-control form-control-sm" />
                        </div>
                    </div>
                    <div class="row form-group">
                        <div class="col-sm-12">
                            <label id="lblHashKey" for="txtHashKey" class="col-form-label" runat="server"></label>
                            <input id="txtHashKey" class="form-control form-control-sm" disabled/>
                        </div>
                    </div>
                    <div class="row form-group">
                        <div class="col-sm-12">
                            <label id="lblChatbotName" for="txtHashKey" class="col-form-label" runat="server"></label>
                            <input id="txtChatbotName" class="form-control form-control-sm" disabled/>
                        </div>
                    </div>

                    <input type="hidden" id="hddModo" value="alta" />
                    <input type="hidden" id="hddId" value="0" />
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

        $(document).ready(function () {});

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
            $("#txtChatbotName").val(entity.ChatbotName);
        }

        function ClearData() {
            $("#hddId").val(0);
            $("#txtRazonSocial").val("");
            $("#txtDireccion").val("");
            $("#txtCodigoPostal").val("");
            $("#txtTelefono").val("");
            $("#txtHostName").val("");
            $("#txtHashKey").val("");
            $("#txtChatbotName").val("");
        }

        function Accion() {
             var entity = {
                 "IdCliente": $("#hddId").val(),
                 "RazonSocial": $("#txtRazonSocial").val(),
                 "Direccion": $("#txtDireccion").val(),
                 "CodigoPostal": $("#txtCodigoPostal").val(),
                 "Telefono": $("#txtTelefono").val(),
                 "HostName": $("#txtHostName").val(),
                 "HashKey": $("#txtHashKey").val(),
                 "ChatbotName": $("#txtChatbotName").val()
            }

            if (!Validaciones(entity)) return false;

            genericAction($("#hddModo").val(), { "cliente": entity });
        }

        function Validaciones(entity) {
            var validToSave = true;
            var errorMessage = "";

            if (entity.RazonSocial == "") {
                errorMessage += '<%:MultiLanguage.GetTranslate(_seccion, "lblRazonSocial") + ": "%>';
                errorMessage += '<%:MultiLanguage.GetTranslate("errorVacioString")%>';
            }

            if (!validToSave) {
                showMessage(error.responseJSON.Message, 5000, "info");
            }

            return validToSave;
        }
        //--||-----------------------------------------------------------------------------------||--//

    </script>

</asp:Content>
