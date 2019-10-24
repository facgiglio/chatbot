<%@ Page Title="Listado de Usuario" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Usuario.aspx.cs" Inherits="ChatBot.Usuario" %>
<%@ Register TagPrefix="CW" Namespace="Framework.WebControls" Assembly="Framework" %>
<%@ Import Namespace="Framework" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <% const string _seccion = "Usuario";%>

    <h2><%: Title %>.</h2>
    
    <CW:Grid runat="server" ID="grdUsuario" />
    
    <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Modal title</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-sm-6">
                            <div >
                                <label id="lblCliente" for="ddlCliente" class="col-form-label" runat="server">Idioma:</label>
                                <asp:DropDownList ID="ddlCliente" runat="server" CssClass="form-control form-control-sm" />
                            </div>
                            <div >
                                <label id="lblEmail" for="txtEmail" class="col-form-label" runat="server">Email:</label>
                                <input id="txtEmail" type="text" class="form-control form-control-sm">
                            </div>
                            <div >
                                <label id="lblContrasena" for="txtContrasena" class="col-form-label" runat="server">Email:</label>
                                <input id="txtContrasena" type="text" class="form-control form-control-sm">
                            </div>
                            <div >
                                <label id="lblNombre" for="txtNombre" class="col-form-label" runat="server">Nombre:</label>
                                <input id="txtNombre" class="form-control form-control-sm" />
                            </div>
                            <div >
                                <label id="lblApellido" for="txtApellido" class="col-form-label" runat="server">Apellido:</label>
                                <input id="txtApellido" class="form-control form-control-sm"/>
                            </div>
                            <div >
                                <label id="lblIdioma" for="ddlIdioma" class="col-form-label" runat="server">Idioma:</label>
                                <asp:DropDownList ID="ddlIdioma" runat="server" CssClass="form-control form-control-sm" />
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <CW:Grid runat="server" ID="grdRoles" />
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
                    Get(grdUsuario);
                    title = tituloActualizar;
                    break;
                case "@Del":
                    Get(grdUsuario);
                    title = tituloEliminar;
                    break;
            }

            modal.find('.modal-title').text(title)
        })
        //--||-----------------------------------------------------------------------------------||--//
        function Get(grid) {
            var entity = JSON.parse($(grid.activeRow).attr("rowdata"));
            var jsonData = { "Id": entity.IdUsuario }
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

        function LoadData(Usuario) {
            ClearData();

            $("#hddId").val(Usuario.IdUsuario);
            $("#ddlCliente").val(Usuario.IdCliente);
            $("#txtEmail").val(Usuario.Email);
            $("#txtContrasena").val(Usuario.Contrasena);
            $("#txtNombre").val(Usuario.Nombre);
            $("#txtApellido").val(Usuario.Apellido);
            $("#ddlIdioma").val(Usuario.IdIdioma);

            $.each(Usuario.Roles, function (i, rol) {
                $("#grdRoles #chk_" + rol.IdRol).prop('checked', true)
            });            
        }

        function ClearData() {
            $("#hddId").val(0);
            $("#ddlCliente").val(0);
            $("#txtEmail").val("");
            $("#txtContrasena").val();
            $("#txtNombre").val("");
            $("#txtApellido").val("");
            $("#ddlIdioma").val(0);

            $("input:checked").each(function () {
                 $(this).prop('checked', false);
            });
        }

        function Accion() {
            
            var roles = [];

            $("#grdRoles input:checked").each(function () {
                var rol = {IdRol: $(this).attr("id").replace("chk_", ""), Descripcion: ""};
                roles.push(rol);
            });            
            
            var usuario = {
                "IdUsuario": $("#hddId").val(),
                "IdCliente": $("#ddlCliente").val(),
                "Email": $("#txtEmail").val(),
                "Contrasena": $("#txtContrasena").val(),
                "Nombre": $("#txtNombre").val(),
                "Apellido": $("#txtApellido").val(),
                "IdIdioma": $("#ddlIdioma").val(),
                "Roles": roles
            }

            $.ajax({
                type: "POST",
                url: getActionUrl($("#hddModo").val()),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify({ "usuario": usuario }),
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
