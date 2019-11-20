<%@ Page Title="Listado de Usuario" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Rol.aspx.cs" Inherits="ChatBot.Rol" %>
<%@ Register TagPrefix="CW" Namespace="Framework.WebControls" Assembly="Framework" %>
<%@ Import Namespace="Framework" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <% const string _seccion = "Rol";%>

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
            <label id="lblRol" runat="server" for="txtPermiso" class="col-sm-2 col-form-label"></label>
            <div class="col-sm-3">
                <input id="txtRol" type="text" class="form-control form-control-sm" runat="server">
            </div>
        </div> 
    </div>

    <CW:Grid runat="server" ID="grdRol" />
    
    <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Modal title</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-sm-12">
                            <div>
                                <label id="lblDescripcion" for="txtEmail" class="col-form-label" runat="server">Descripción:</label>
                                <input id="txtDescripcion" type="text" class="form-control form-control-sm">
                            </div>
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
        const tituloNuevo = '<%: MultiLanguage.GetTranslate(_seccion, "tituloNuevo")%>';
        const tituloActualizar = '<%: MultiLanguage.GetTranslate(_seccion, "tituloActualizar")%>';
        const tituloEliminar = '<%: MultiLanguage.GetTranslate(_seccion, "tituloEliminar")%>';

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
                    Get(grdRol);
                    title = tituloActualizar;
                    break;
                case "@Del":
                    Get(grdRol);
                    title = tituloEliminar;
                    break;
            }

            modal.find('.modal-title').text(title)
        })
        //--||-----------------------------------------------------------------------------------||--//
        function Get(grid) {
            var entity = JSON.parse($(grid.activeRow).attr("rowdata"));
            var jsonData = { "Id": entity.IdRol}
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

        function LoadData(Rol) {
            ClearData();

            $("#hddId").val(Rol.IdRol);
            $("#txtDescripcion").val(Rol.Descripcion);
        }

        function ClearData() {
            $("#hddId").val(0);
            $("#txtDescripcion").val("");

            $("input:checked").each(function () {
                $(this).prop('checked', false);
            });
        }

        function Accion() {
            var entity = {
                "IdRol": $("#hddId").val(),
                "Descripcion": $("#txtDescripcion").val()
            }

            genericAction($("#hddModo").val(), { "rol": entity });
        }
        //--||-----------------------------------------------------------------------------------||--//
    </script>

</asp:Content>
