<%@ Page Title="ChatBot" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LogIn.aspx.cs" Inherits="ChatBot.LogIn" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        body {
            padding-top:4.2rem;
		    padding-bottom:4.2rem;
		    background: #ebebeb;
        }
        a {
            text-decoration:none !important;
        }
        h1,h2,h3{
            font-family: 'Kaushan Script', cursive;
        }
        .myform{
		    position: relative;
		    display: -ms-flexbox;
		    display: flex;
		    padding: 1rem;
		    -ms-flex-direction: column;
		    flex-direction: column;
		    width: 100%;
		    pointer-events: auto;
		    background-color: #fff;
		    background-clip: padding-box;
		    border: 1px solid rgba(0,0,0,.2);
		    border-radius: 1.1rem;
		    outline: 0;
		    max-width: 500px;
            padding: 40px 40px;
		}
        .mybtn{
            border-radius:50px;
        }
        
         form .error {
            color: #ff0000;
        }
    </style>
    <div class="row" style="margin-top:50px;">
        <div class="col-md-3"></div>
        <div class="col-md-6">
            <div id="first">
                <div class="myform form">
                    <div class="logo mb-3">
                        <div class="col-md-12 text-center">
                            <h1>Login</h1>
                        </div>
                    </div>
                    <form>
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
                            <button id="btnLogIn" type="submit" class="btn btn-block mybtn btn-primary">LOGIN</button>
                        </div>
                    </form>
                </div>
            </div>
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
