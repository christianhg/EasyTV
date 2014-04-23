<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageBackend.master" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" Theme="backend" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBackendHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBackend" Runat="Server">
    <div class="row">
        <div class="col-lg-4 col-lg-offset-4">
            <div class="well login-form">
            <h1>Login</h1>
            <label for="txtUsername">Username</label>
            <input ID="txtUsername" class="form-control" type="text" runat="server" placeholder="Username"/><br />
            <label for="txtPassword">Password</label>
            <input ID="txtPassword" class="form-control" type="password" runat="server" placeholder="Password"/><br />
            <asp:Button ID="btnLogin" class="btn btn-primary col-md-12" runat="server" Text="Login" />
            <asp:Label id="lblLoginStatus" runat="server" />
            <br />
            <asp:RequiredFieldValidator 
                ID="rfvTxtUsername" 
                runat="server" 
                Display="Dynamic"
                ControlToValidate="txtUsername">
            <div class="alert alert-warning"><span class="glyphicon glyphicon-info-sign"></span> Husk at indtaste et brugernavn</div>
            </asp:RequiredFieldValidator><br />
            <asp:RequiredFieldValidator 
                ID="rfvTxtPassword" 
                runat="server" 
                Display="Dynamic"  
                ControlToValidate="txtPassword">
            <div class="alert alert-warning"><span class="glyphicon glyphicon-info-sign"></span> Husk at indtaste et password</div>
            </asp:RequiredFieldValidator><br />
            <asp:CustomValidator 
                ID="cvTxtUsername" 
                runat="server" 
                Display="Dynamic" 
                ControlToValidate="txtUsername" 
                OnServerValidate="cvTxtUsername_ServerValidate">
                <div class="alert alert-warning"><span class="glyphicon glyphicon-info-sign"></span> Det indtastede brugernavn eller password er desværre ikke korrekt</div>
            </asp:CustomValidator>
            </div>        
        </div>
    </div>
</asp:Content>

