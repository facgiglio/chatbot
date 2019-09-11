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
                            <input type="email" class="form-control" id="exampleInputEmail1" placeholder="Email">
                          </div>
                        <div class="form-group">
                            <label for="exampleInputEmail1">Password</label>
                            <input type="password" name="password" id="password" class="form-control" aria-describedby="emailHelp" placeholder="Enter Password">
                        </div>
                        <div class="form-group">
                            <p class="text-center">By signing up you accept our <a href="#">Terms Of Use</a></p>
                        </div>
                        <div class="col-md-12 text-center ">
                            <button type="submit" class=" btn btn-block mybtn btn-primary">LOGIN</button>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div> 
</asp:Content>
