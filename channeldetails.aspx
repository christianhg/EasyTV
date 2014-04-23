<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageBackend.master" AutoEventWireup="true" CodeFile="channeldetails.aspx.cs" Inherits="channeldetails" Theme="backend" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBackendHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBackend" Runat="Server">
    <div class="row">
        <div class="col-md-6 col-md-3-offset">
            <div class="">
                <asp:Panel ID="pnlInsertChannel" runat="server">
                    <asp:Panel ID="pnlAlert" runat="server" Visible="false">
                        <asp:Literal ID="ltrAlert" runat="server" />
                    </asp:Panel>
                    <div class="form-group">
                        <label for="txtChannelName">Navn</label>
                        <asp:TextBox ID="txtChannelName" CssClass="form-control" runat="server"></asp:TextBox>
                        <!-- VALIDATION -->
                        <asp:RequiredFieldValidator 
                            ID="rfvTxtChannelName" 
                            runat="server" 
                            Display="Dynamic" 
                            ControlToValidate="txtChannelName">
                        <div class="alert alert-warning"><span class="glyphicon glyphicon-info-sign"></span> Husk at indtaste et navn</div>
                        </asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator 
                            ID="reTxtChannelName" 
                            runat="server"
                            display="Dynamic" 
                            ValidationExpression="^\s*([^\s]\s*){1,50}" 
                            controltovalidate="txtChannelName">
                        <div class="alert alert-warning"><span class="glyphicon glyphicon-info-sign"></span> Husk at et navn max. kan være 50 karakter</div>
                        </asp:RegularExpressionValidator>
                        <asp:CustomValidator 
                            ID="cvTxtChannelName" 
                            runat="server" 
                            controltovalidate="txtChannelName"
                            Display="Dynamic"
                            OnServerValidate="cvTxtChannelName_ServerValidate">
                            <div class="alert alert-warning"><span class="glyphicon glyphicon-info-sign"></span> Det indtastede navn er desværre optaget </div>
                        </asp:CustomValidator>
                    </div>
                    <div class="form-group">
                        <label for="txtChannelInfo">Info</label>
                        <asp:TextBox ID="txtChannelInfo" CssClass="form-control" runat="server"></asp:TextBox>
                        <!-- VALIDATION -->
                        <asp:RequiredFieldValidator 
                            ID="rfvTxtChannelInfo" 
                            runat="server" 
                            Display="Dynamic" 
                            ControlToValidate="txtChannelInfo">
                        <div class="alert alert-warning"><span class="glyphicon glyphicon-info-sign"></span> Husk at indtaste info</div>
                        </asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator 
                            ID="reTxtChannelInfo" 
                            runat="server"
                            display="Dynamic" 
                            ValidationExpression="^\s*([^\s]\s*){1,200}" 
                            controltovalidate="txtChannelInfo">
                        <div class="alert alert-warning"><span class="glyphicon glyphicon-info-sign"></span> Husk at info max. kan være 200 karakter</div>
                        </asp:RegularExpressionValidator>
                    </div>
                    <div class="form-group">
                        <label for="ddlChannelLogo">Logo</label>
                        <asp:DropDownList ID="ddlChannelLogo" CssClass="form-control" runat="server"></asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <asp:Button OnClick="btnInsertChannel_Click" CssClass="btn btn-success" ID="btnInsertChannel" runat="server" Text="Opret kanal" />
                        <asp:Button OnClick="btnUpdateChannel_Click" Visible="false" CssClass="btn btn-primary" ID="btnUpdateChannel" runat="server" Text="Opdatér kanal" />
                        <a href="backend.aspx" class="btn btn-warning">Tilbage</a>
                    </div>
                    
                </asp:Panel>
             </div>
        </div>
    </div>
</asp:Content>