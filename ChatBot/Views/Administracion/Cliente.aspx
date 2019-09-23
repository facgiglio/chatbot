<%@ Page Title="Listado de Usuario" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Cliente.aspx.cs" Inherits="ChatBot.Cliente" %>
<%@ Register TagPrefix="CW" Namespace="Framework.WebControls" Assembly="Framework" %>
<%@ Import Namespace="Framework" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <% const string _page = "Cliente.aspx";%>

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
                    <div class="row">
                        <div class="col-sm-12">
                            <label id="lblRazonSocial" for="txtRazonSocial" class="col-form-label" runat="server"></label>
                            <input id="txtRazonSocial" type="text" class="form-control form-control-sm">
                        </div>
                    </div>
                    <div class="row">
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
        $(document).ready(function () {


        });

        function Accion() {
            var entity = {
                "IdCliente": $("#hddId").val(),
                "RazonSocial": $("#txtRazonSocial").val(),
                "Direccion": $("#txtDireccion").val(),
                "CodigoPostal": $("#txtCodigoPostal").val(),
                "Telefono": $("#txtTelefono").val()
            }

            $.ajax({
                type: "POST",
                url: getActionUrl($("#hddModo").val()),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify({ "cliente": entity }),
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
