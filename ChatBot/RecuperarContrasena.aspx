<%@ Page Title="ChatBot" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RecuperarContrasena.aspx.cs" Inherits="ChatBot.RecuperarContrasena" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row" style="margin-top:50px;">
        <form>
            <div class="row">
                <div class="col-md-2">
                    &nbsp;
                </div>
                <div class="col-md-4">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title">Recuperar Contaseña</h3>
                        </div>
                        <div class="panel-body">
                            <div class="form-group" id="divMail">
                                <label for="exampleInputEmail1">Email address</label>
                                <input type="email" class="form-control" id="txtEmail" placeholder="Email">
                            </div>

                            <div class="form-group" name="cambiarContrasena" style="display: none">
                                <label for="txtCodigoRecuperacion">Codigo Recuperacion</label>
                                <input type="email" class="form-control" id="txtCodigoRecuperacion">
                            </div>
                            <div class="form-group" name="cambiarContrasena" style="display: none">
                                <label for="txtContraseña1">Nueva Contraseña</label>
                                <input type="password" class="form-control" id="txtContraseña1">
                            </div>
                            <div class="form-group" name="cambiarContrasena" style="display: none">
                                <label for="txtContraseña2">Repetir contraseña</label>
                                <input type="password" class="form-control" id="txtContraseña2">
                            </div>

                            <div class="col-md-12 text-center ">
                                <button id="btnRecuperar" type="submit" class="btn btn-primary btn-lg">Recuperar contraseña</button>
                                <button id="btnCambiar" type="submit" class="btn btn-primary btn-lg" style="display:none;">Cambiar contraseña</button>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <img src="Content/img/chatbot_login.png" class="img-responsive" alt="Responsive image">
                </div>
            </div>
        </form>
    </div> 
    <script>
        $(document).ready(function () {
            if (typeof(Storage) !== "undefined") {
                // Code for localStorage/sessionStorage.
                if (localStorage.getItem("rememberMe") == "yes") {
                    $("#rememberMe").prop('checked', true);
                    $("#txtEmail").val(localStorage.getItem("userName"));
                }
            } else {
                // Sorry! No Web Storage support..
            }

            $("#btnRecuperar").click(function () {
                var getUrl = "<%=Page.ResolveClientUrl("~/RecuperarContrasena.aspx/Recuperar")%>";
                var jsonData = {
                    usuario: $("#txtEmail").val()
                };

                if (typeof (Storage) !== "undefined") {
                    if ($("#rememberMe").prop('checked')) {
                        localStorage.setItem("rememberMe", "yes");
                        localStorage.setItem("userName", $("#txtEmail").val());
                    }                    
                }

                $.ajax({
                    type: "POST",
                    url: getUrl,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify(jsonData),
                    success: function (result) {
                        if (!checkError(result)) {
                            $("#divMail").hide();
                            $("#btnRecuperar").hide();
                            $("div[name='cambiarContrasena']").show();
                            $("#btnCambiar").show();
                        }
                    },
                    error: function (error) {
                        checkError(error);
                    }
                });

                return false;
            });

            $("#btnCambiar").click(function () {
                var getUrl = "<%=Page.ResolveClientUrl("~/RecuperarContrasena.aspx/Cambiar")%>";
                var jsonData = {
                    usuario: $("#txtEmail").val(),
                    codigoRecuperacion: $("#txtCodigoRecuperacion").val(),
                    nuevaContrasena: $("#txtContraseña1").val(),
                    repetirContrasena: $("#txtContraseña2").val()
                };

                $.ajax({
                    type: "POST",
                    url: getUrl,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify(jsonData),
                    success: function (result) {
                       if (!checkError(result)) {
                            var getUrl = "<%=Page.ResolveClientUrl("~/Login.aspx")%>";
                            location.href = getUrl;
                        }
                    },
                    error: function (error) {
                        showMessage("#form", error.responseJSON.Message, 5000);
                    }
                });

                return false;
            });
            
            btnCambiar
        });
    </script>
</asp:Content>
