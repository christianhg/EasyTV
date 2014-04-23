<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageBackend.master" AutoEventWireup="true" CodeFile="streamingservicedetails.aspx.cs" Inherits="streamingservicedetails" Theme="backend" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBackendHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBackend" Runat="Server">
     <div class="row">
        <div class="col-md-6 col-md-3-offset">
            <div class="">
                <asp:Panel ID="pnlInsertStreamingService" runat="server">
                    <asp:Panel ID="pnlAlert" runat="server" Visible="false">
                        <asp:Literal ID="ltrAlert" runat="server" />
                    </asp:Panel>
                    <div class="form-group">
                        <label for="txtStreamingServiceName">Navn</label>
                        <asp:TextBox ID="txtStreamingServiceName" CssClass="form-control" runat="server"></asp:TextBox>
                        <br />
                        <!--  VALIDATION -->
                        <asp:RequiredFieldValidator 
                            ID="rfvTxtStreamingServiceName" 
                            runat="server" 
                            display="Dynamic"
                            controltovalidate="txtStreamingServiceName">
                            <div class="alert alert-warning"><span class="glyphicon glyphicon-info-sign"></span> Husk at indtaste et navn</div>
                        </asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator 
                            ID="reTxtStreamingServiceName" 
                            runat="server"
                            display="Dynamic" 
                            ValidationExpression="^\s*([^\s]\s*){1,50}" 
                            controltovalidate="txtStreamingServiceName">
                        <div class="alert alert-warning"><span class="glyphicon glyphicon-info-sign"></span> Husk at et navn max. kan være 50 karakter</div>
                        </asp:RegularExpressionValidator>
                        <asp:CustomValidator 
                            ID="cvTxtStreamingServiceName" 
                            runat="server" 
                            controltovalidate="txtStreamingServiceName"
                            Display="Dynamic"
                            OnServerValidate="cvTxtStreamingServiceName_ServerValidate">
                            <div class="alert alert-warning"><span class="glyphicon glyphicon-info-sign"></span> Husk at streamingtjenesten eksistrer allerede</div>
                        </asp:CustomValidator>
                    </div>
                    <div class="form-group">
                        <asp:Button ID="btnInsertStreamingService" CssClass="btn btn-success" runat="server" Text="Opret streaming-tjeneste" OnClick="InsertStreamingService_Click" />
                        <asp:Button ID="btnUpdateStreamingService" CssClass="btn btn-primary" runat="server" Text="Opdatér streaming-tjeneste" OnClick="UpdateStreamingService_Click" />
                        <a href="backend.aspx" class="btn btn-warning">Tilbage</a>
                    </div>                    
                    <br />
                    <asp:Literal ID="ltrSuccessMsg" runat="server"></asp:Literal>
                </asp:Panel>
             </div>
        </div>
    </div>
</asp:Content>

