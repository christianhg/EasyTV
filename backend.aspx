<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageBackend.master" AutoEventWireup="true" CodeFile="backend.aspx.cs" Inherits="backend" Theme="backend" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBackendHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBackend" Runat="Server">
    <!-- Nav tabs -->
    <ul class="nav nav-tabs">
        <li><a href="#tvproviders" data-toggle="tab">Udbydere</a></li>
        <li><a href="#packages" data-toggle="tab">Pakker</a></li>
        <li><a href="#channels" data-toggle="tab">Kanaler</a></li>
        <li><a href="#streamingservices" data-toggle="tab">Streaming-tjenester</a></li>
    </ul>

    <!-- Tab panes -->
    <div class="tab-content">
        <!-- TVPROVIDERS -->
        <div class="tab-pane active" id="tvproviders">
            <asp:Button ID="btnInsertTvProvider" class="btn btn-success" runat="server" Text="Opret udbyder" OnClick="btnInsertTvProvider_Click" />
            <br /><br />
            <asp:Panel ID="pnlTvProviders" runat="server">
                <asp:Table ID="tblTvProviders" CssClass="table table-bordered" runat="server">
                    <asp:TableHeaderRow>
                        <asp:TableHeaderCell>Navn</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Info</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Logo</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Adresse</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Telefon</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Url</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Redigér</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Slet</asp:TableHeaderCell>
                    </asp:TableHeaderRow>
                </asp:Table>
            </asp:Panel>
        </div>
        <!-- CHANNELS -->
        <div class="tab-pane" id="channels">
            <asp:Button ID="btnInsertChannel" class="btn btn-success" runat="server" Text="Opret kanal" OnClick="btnInsertChannel_Click" />
            <br /><br />
            <asp:Panel ID="pnlChannels" runat="server">
                <asp:Table ID="tblChannels" CssClass="table table-bordered" runat="server">
                    <asp:TableHeaderRow>
                        <asp:TableHeaderCell>Navn</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Info</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Logo</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Redigér</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Slet</asp:TableHeaderCell>
                    </asp:TableHeaderRow>
                </asp:Table>
            </asp:Panel>
        </div>
        <!-- STREAMINGSERVICES -->
        <div class="tab-pane" id="streamingservices">
            <asp:Button ID="btnInsertStreamingService" class="btn btn-success" runat="server" Text="Opret streaming-tjeneste" OnClick="btnInsertStreamingService_Click" />
            <br /><br />    
                <asp:Table ID="tblStreamingServices" CssClass="table table-bordered" runat="server">
                    <asp:TableHeaderRow>
                        <asp:TableHeaderCell>Navn</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Redigér</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Slet</asp:TableHeaderCell>
                    </asp:TableHeaderRow>
                </asp:Table>
        </div>
        <!-- PACKAGES -->
        <div class="tab-pane" id="packages">
            <asp:Button ID="btnInsertPackage" class="btn btn-success" runat="server" Text="Opret pakke" OnClick="btnInsertPackage_Click" />
            <br /><br />
            <asp:Panel ID="pnlPackages" runat="server">
                <asp:Table ID="tblPackages" CssClass="table table-bordered" runat="server">
                    <asp:TableHeaderRow>
                        <asp:TableHeaderCell>Udbyder</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Navn</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Info</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Url</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Pris/md</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Oprettelsespris</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Mindstepris i 6 mdr.</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Internet</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Kanaler</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Streaming-tjenester</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Redigér</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Slet</asp:TableHeaderCell>
                    </asp:TableHeaderRow>
                </asp:Table>
            </asp:Panel>
        </div>
    </div>
</asp:Content>