<%@ Page Title="ChatBot" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LogIn.aspx.cs" Inherits="ChatBot.LogIn" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row" style="margin-top:50px;">
            <form>
                <div class="row">
                    <div class="col-md-4">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h3 class="panel-title">Log-In</h3>
                            </div>
                            <div class="panel-body">
                                <div class="form-group">
                                    <label for="exampleInputEmail1">Email address</label>
                                    <input type="email" class="form-control" id="txtEmail" placeholder="Email">
                                    </div>
                                <div class="form-group">
                                    <label for="exampleInputEmail1">Password</label>
                                    <input type="password" name="password" id="txtPassword" class="form-control" aria-describedby="emailHelp" placeholder="Enter Password">
                                </div>
                                <div class="checkbox mb-3">
                                    <label>
                                        <input type="checkbox" value="remember-me" id="rememberMe">
                                        Remember me
                                    </label>
                                </div>
                                <div class="form-group">
                                    <p class="text-center">By signing up you accept our <a href="#">Terms Of Use</a></p>
                                </div>
                                <div class="col-md-12 text-center ">
                                    <button id="btnLogIn" type="submit" class="btn btn-primary btn-lg">LOGIN</button>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-8">
                        <img src="Content/img/chatbot_login.png" class="img-responsive" alt="Responsive image">
                    </div>
                </div>
            </form>
        </div>

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

            $("#btnLogIn").click(function () {
                var getUrl = "<%=Page.ResolveClientUrl("~/LogIn.aspx/Login")%>";
                var jsonData = {
                    usuario: $("#txtEmail").val(),
                    contrasena: $("#txtPassword").val()
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
                        if (!CheckError(result)) {
                            var getUrl = "<%=Page.ResolveClientUrl("~/Default.aspx")%>";
                            location.href = getUrl;
                        }
                    },
                    error: function (error) {
                        showMessage("#form", error.responseJSON.Message, 5000);
                    }
                });

                return false;
            });
        });
    </script>
</asp:Content>
