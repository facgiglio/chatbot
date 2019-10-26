<%@ Page Title="Chatbot - Backup Administration" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Backup.aspx.cs" Inherits="ChatBot.Backup" %>
<%@ Register TagPrefix="CW" Namespace="Framework.WebControls" Assembly="Framework" %>
<%@ Import Namespace="Framework" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <% const string _seccion = "Backup";%>

    <div class="form-group">
        <div class="row">
            <div class="col-sm-6">
                <h2><label id="tituloPrincipal" runat="server" class="col-form-label"></label></h2>
            </div>
            <div class="col-sm-6 text-right">
                <div class="btn-group" role="group">
                    <button type="button" runat="server" id="btnBackup" class="btn btn-primary"><span class="glyphicon glyphicon-export" aria-hidden="true"></span>&nbsp;<span id="lblBackup" runat="server"></span></button>
                </div>
            </div>
        </div>
        <div class="row">
            <label id="lblFileName" runat="server" for="txtFileName" class="col-sm-1 col-form-label"></label>
            <div class="col-sm-4">
                <input type="text" class="form-control" id="txtFileName">
            </div>
        </div> 
    </div>
   
    <cw:Grid runat="server" id="grdBackup"/>

    <script type="text/javascript">
        const tituloNuevo = '<%: MultiLanguage.GetTranslate(_seccion, "tituloNuevo")%>';
        const tituloActualizar = '<%: MultiLanguage.GetTranslate(_seccion, "tituloActualizar")%>';
        const tituloEliminar = '<%: MultiLanguage.GetTranslate(_seccion, "tituloEliminar")%>';

        $(document).ready(function () {
            $("#cmnuRestore").click(function () {
                var entity = JSON.parse($(grdBackup.activeRow).attr("rowdata"));

                Action("RestoreDatabase", entity.Nombre);
            });

             $("#cmnuEliminar").click(function () {
                var entity = JSON.parse($(grdBackup.activeRow).attr("rowdata"));

                Action("DeleteDatabase", entity.Nombre);
            });

            $("#btnBackup").click(function () {

                if ($("#txtFileName").val() == "") {
                    const message = '<%: MultiLanguage.GetTranslate(_seccion, "lblFileName") + ": " + MultiLanguage.GetTranslate("errorVacioString")%>';

                    showMessage("body", message, 5000, "danger");
                }
                else {
                    Action("BackupDatabase",  $("#txtFileName").val());
                }
            });

            grdBackup.click(function () {
                var entity = JSON.parse($(grdBackup.activeRow).attr("rowdata"));
                $("#txtFileName").val(entity.Nombre.replace(".bak", ""));
            });
        });

        function Action(action, fileName) {
            $.ajax({
                type: "POST",
                url: getActionUrl(action),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify({ "fileName": fileName}),
                success: function (result) {
                    location.reload();
                }
            });
        }
        //--||-----------------------------------------------------------------------------------||--//
    </script>

</asp:Content>
