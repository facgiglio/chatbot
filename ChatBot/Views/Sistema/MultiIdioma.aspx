<%@ Page Title="MultiLanguage" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MultiIdioma.aspx.cs" Inherits="ChatBot.MultiIdioma" %>
<%@ Register TagPrefix="CW" Namespace="Framework.WebControls" Assembly="Framework" %>
<%@ Import Namespace="Framework" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <% const string _seccion = "MultiIdioma";%>

    <div class="form-group">
        <div class="row">
            <div class="col-sm-6">
                <h2><label id="tituloPrincipal" runat="server" class="col-form-label"></label></h2>
            </div>
            <div class="col-sm-6 text-right">
                <div class="btn-group" role="group">
                    <button type="button" runat="server" id="btnNuevo" class="btn btn-primary" data-toggle="modal" data-target="#exampleModal" data-mode="@New"><span class="glyphicon glyphicon-file" aria-hidden="true"></span>&nbsp;<span id="lblNuevo" runat="server"></span></button>
                    <button type="button" runat="server" id="btnRefrescarCache" class="btn btn-info"><span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>&nbsp;<span id="lblRefrescarCache" runat="server"></span></button>
                    <button type="submit" runat="server" id="btnFiltrar" class="btn btn-success"><span class="glyphicon glyphicon-search" aria-hidden="true"></span>&nbsp;<span id="lblFiltrar" runat="server"></span></button>
                </div>
            </div>
        </div>
        <div class="row">
            <label id="lblSeccionFiltrar" runat="server" for="ddlSeccionFiltrar" class="col-sm-1 col-form-label"></label>
            <div class="col-sm-2">
                <asp:DropDownList ID="ddlSeccionFilter" runat="server" CssClass="form-control form-control-sm"/>
            </div>
        </div> 
    </div>
   
    <cw:Grid runat="server" id="grdMultiIdioma"/>

    <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                 <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title"><label id="tituloSeccion" runat="server" class="col-form-label"></label></h4>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <label id="lblSeccion" runat="server" for="ddlSeccion" class="col-form-label"></label>
                        <asp:DropDownList ID="ddlSeccion" runat="server" CssClass="form-control" data-languaje="seccion"/>
                    </div>
                    <div class="form-group">
                        <label id="lblDescripcion" runat="server" for="txtDescripcion" class="col-form-label"></label>
                        <input type="text" class="form-control" id="txtDescripcion">
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" runat="server" id="btnCancelar" class="btn btn-secondary" data-dismiss="modal"></button>
                    <button type="button" runat="server" id="btnGuardar" class="btn btn-primary" onclick="Accion(); return false"></button>
                </div>
            </div>
            <input type="hidden" ID="hddModo" Value="alta" />
            <input type="hidden" ID="hddId" Value="0" />
        </div>
    </div>

    <script type="text/javascript">
        const tituloNuevo = '<%: MultiLanguage.GetTranslate(_seccion, "tituloNuevo")%>';
        const tituloActualizar = '<%: MultiLanguage.GetTranslate(_seccion, "tituloActualizar")%>';
        const tituloEliminar = '<%: MultiLanguage.GetTranslate(_seccion, "tituloEliminar")%>';

        $( document ).ready(function() {
            $("table input").change(function () {
                
                var valores = $(this).attr("id").replace("txtTraduccion_", "").split('_')
                var modo = (valores[0] == "0" ? "@New" : "@Upd");
                var jsonData = {
                    "IdTraduccion": valores[0],
                    "IdMultiIdioma": valores[1],
                    "IdIdioma": valores[2],
                    "Texto": $(this).val()
                }

                $.ajax({
                    type: "POST",
                    url: getActionUrl(modo) + "Traduccion",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({ "traduccion": jsonData }),
                    context: $(this).attr("id"),
                    success: function (result) {
                        $("#" + this).attr("id", this.replace("0_", result.d + "_")); 
                    }
                });
            })

            $("#btnRefrescarCache").click(function () {
                $.ajax({
                    type: "POST",
                    url: getActionUrl("RefreshCache"),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (result) {
                        location.reload(); 
                    }
                });
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
                    $("#ddlSeccion").val($("#ddlSeccionFilter").val());
                    title = tituloNuevo;
                    break;
                case "@Upd":
                    Get();
                    title = tituloActualizar;
                    break;
                case "@Del":
                    Get();
                    title = tituloEliminar;
                    break;
            }
            
            modal.find('.modal-title').text(title)
        })
        //--||-----------------------------------------------------------------------------------||--//
        function Get() {
            var entity = JSON.parse($(grdMultiIdioma.activeRow).attr("rowdata"));
            var jsonData = { "IdMultiLenguaje": entity.IdMultiLenguaje }
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

        function LoadData(MultiIdioma) {
            $("#hddId").val(MultiIdioma.IdMultiLenguaje);
            $("#ddlSeccion").val(MultiIdioma.IdSeccion);
            $("#txtDescripcion").val(MultiIdioma.Descripcion);
        }

        function ClearData() {
            $("#hddId").val(0);
            $("#ddlSeccion").val(0);
            $("#txtDescripcion").val("");
        }

        function Accion() {
            var jsonData = {
                "IdMultiLenguaje": $("#hddId").val(),
                "IdSeccion": $("#ddlSeccion").val(),
                "Descripcion": $("#txtDescripcion").val()
            }

            $.ajax({
                type: "POST",
                url: getActionUrl($("#hddModo").val()),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify({ "multiLanguage": jsonData }),
                success: function (result) {
                    location.reload();
                }
            });
        }
        //--||-----------------------------------------------------------------------------------||--//
    </script>

</asp:Content>
