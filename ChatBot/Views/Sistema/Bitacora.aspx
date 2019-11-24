<%@ Page Title="Listado de Usuario" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Bitacora.aspx.cs" Inherits="ChatBot.Bitacora" %>
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
            <label id="lblMensaje" runat="server" for="txtPermiso" class="col-sm-2 col-form-label"></label>
            <div class="col-sm-3">
                <input id="txtMensaje" type="text" class="form-control form-control-sm" runat="server">
            </div>
            <label id="lblUsuario" for="ddlUsuario" class="col-form-label" runat="server"></label>
            <div class="col-sm-3">
                <asp:DropDownList ID="ddlUsuario" runat="server" CssClass="form-control form-control-sm" />
            </div>
        </div> 
    </div>

    <CW:Grid runat="server" ID="grdLog" />
    
</asp:Content>
