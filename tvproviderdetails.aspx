<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageBackend.master" AutoEventWireup="true" CodeFile="tvproviderdetails.aspx.cs" Inherits="tvproviderdetails" Theme="backend" %>

<asp:Content ID="hTvProviderDetails" ContentPlaceHolderID="cphBackendHead" Runat="Server">
</asp:Content>
<asp:Content ID="cTvProviderDetails" ContentPlaceHolderID="cphBackend" Runat="Server">
    <asp:Literal ID="ltrResponse" runat="server" />
    <asp:Panel ID="pnlInsertTvProvider" runat="server">
        <asp:Panel ID="pnlAlert" runat="server" Visible="false">
            <asp:Literal ID="ltrAlert" runat="server" />
        </asp:Panel>
        <div class="row">
            <asp:Panel CssClass="col-md-6" ID="pnlTvProviderDetails" runat="server">
                <div class="form-group">
                    <label for="txtTvProviderName">Navn</label>
                    <asp:TextBox CssClass="form-control" id="txtTvProviderName" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ValidationGroup="vbTvProviderDetails" ID="rfvTxtTvProviderName" runat="server" Display="Dynamic" ControlToValidate="txtTvProviderName">
                        <div class="alert alert-warning"><span class="glyphicon glyphicon-info-sign"></span> Husk at indtaste et navn</div>
                    </asp:RequiredFieldValidator>
                    <asp:CustomValidator ValidationGroup="vbTvProviderDetails" ID="cvTxtTvProviderName" runat="server" Display="Dynamic" ControlToValidate="txtTvProviderName" OnServerValidate="cvTxtTvProviderName_ServerValidate">
                        <div class="alert alert-warning"><span class="glyphicon glyphicon-info-sign"></span> Det indtastede navn er desværre optaget</div>
                    </asp:CustomValidator>
                </div>
                <div class="form-group">
                    <label for="txtTvProviderInfo">Info</label>
                    <asp:TextBox CssClass="form-control" ID="txtTvProviderInfo" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ValidationGroup="vbTvProviderDetails" ID="rfvTxtTvProviderInfo" runat="server" Display="Dynamic" ControlToValidate="txtTvProviderInfo">
                        <div class="alert alert-warning"><span class="glyphicon glyphicon-info-sign"></span> Husk at indtaste info</div>
                    </asp:RequiredFieldValidator>
                </div>
                <div class="form-group">
                    <label for="ddlTvProviderLogo">Logo</label>
                    <asp:DropDownList CssClass="form-control" ID="ddlTvProviderLogo" runat="server"></asp:DropDownList>
                </div>
                <div class="form-group">
                    <label for="txtTvProviderAddress">Adresse</label>
                    <asp:TextBox CssClass="form-control" ID="txtTvProviderAddress" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ValidationGroup="vbTvProviderDetails" ID="rfvTxtTvProviderAddress" runat="server" Display="Dynamic" ControlToValidate="txtTvProviderAddress">
                        <div class="alert alert-warning"><span class="glyphicon glyphicon-info-sign"></span> Husk at indtaste en adresse</div>
                    </asp:RequiredFieldValidator>
                </div>
                <div class="form-group">
                    <label for="txtTvProviderPhone">Telefon</label>
                    <asp:TextBox CssClass="form-control" ID="txtTvProviderPhone" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ValidationGroup="vbTvProviderDetails" ID="rfvTxtTvProviderPhone" runat="server" Display="Dynamic" ControlToValidate="txtTvProviderPhone">
                        <div class="alert alert-warning"><span class="glyphicon glyphicon-info-sign"></span> Husk at indtaste et telefonnummer</div>
                    </asp:RequiredFieldValidator>
                </div>
                <div class="form-group">
                    <label for="txtTvProviderUrl">Url</label>
                    <asp:TextBox CssClass="form-control" ID="txtTvProviderUrl" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ValidationGroup="vbTvProviderDetails" ID="rfvTxtTvProviderUrl" runat="server" Display="Dynamic" ControlToValidate="txtTvProviderUrl">
                        <div class="alert alert-warning"><span class="glyphicon glyphicon-info-sign"></span> Husk at indtaste en url</div>
                    </asp:RequiredFieldValidator>
                </div>
                <!-- INSERT/UPDATE BUTTONS -->
                <div class="form-group">
                    <asp:Button ValidationGroup="vbTvProviderDetails" CssClass="btn btn-success" ID="btnInsertTvProvider" runat="server" Text="Opret udbyder" OnClick="btnInsertTvProvider_Click" />
                    <asp:Button ValidationGroup="vbTvProviderDetails" OnClick="btnUpdateTvProvider_Click" Visible="false" CssClass="btn btn-success" ID="btnUpdateTvProvider" runat="server" Text="Opdatér udbyder" />
                    <a href="backend.aspx" class="btn btn-warning">Tilbage</a>
                </div>
            </asp:Panel>
            <asp:Panel CssClass="col-md-6" ID="pnlTvProviderExclusionAreas" runat="server">
                <div class="row">
                    <div class="form-group col-lg-4">
                        <label for="txtLowZip">Dækningsløse områder</label>
                        <asp:TextBox CssClass="form-control" placeholder="Fra postnummer" ID="txtLowZip" runat="server" MaxLength="4"></asp:TextBox>
                        <asp:RequiredFieldValidator Display="Dynamic" ValidationGroup="vgExclusionArea" ID="rfvTxtLowZip" ControlToValidate="txtLowZip" runat="server" ErrorMessage="RequiredFieldValidator">
                            <div class="alert alert-warning"><span class="glyphicon glyphicon-info-sign"></span> Husk at indtaste et postnummer</div>
                        </asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group col-lg-4">
                        <label for="txtHighZip">&nbsp;</label>
                        <asp:TextBox CssClass="form-control" placeholder="Til postnummer" ID="txtHighZip" runat="server" MaxLength="4"></asp:TextBox>
                        <asp:RequiredFieldValidator Display="Dynamic" ValidationGroup="vgExclusionArea" ID="rfvTxtHighZip" ControlToValidate="txtHighZip" runat="server" ErrorMessage="RequiredFieldValidator">
                            <div class="alert alert-warning"><span class="glyphicon glyphicon-info-sign"></span> Husk at indtaste en et postnummer</div>
                        </asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group from-group--zipButtons col-md-4">
                        <label for="btnAddExclusionArea">&nbsp;</label>
                        <asp:Button ValidationGroup="vgExclusionArea" CssClass="btn btn-primary" ID="btnAddExclusionArea" OnClick="btnAddExclusionArea_Click" runat="server" Text="Tilføj" />
                        <asp:CustomValidator ID="cvAddExclusionArea" runat="server" OnServerValidate="cvAddExclusionArea_ServerValidate" Display="Dynamic" ControlToValidate="txtLowZip" ErrorMessage="CustomValidator"></asp:CustomValidator>
                        <label for="btnRemoveExclusionAreas">&nbsp;</label>
                        <asp:Button CssClass="btn btn-danger" ID="btnRemoveExclusionAreas" OnClick="btnRemoveExclusionAreas_Click" runat="server" Text="Clear" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-8">
                        <asp:Table runat="server" ID="tblExclusionAreas" class="table table-bordered " ViewStateMode="Enabled" EnableViewState="true">
                        <asp:TableHeaderRow>
                            <asp:TableHeaderCell CssClass="col-md-6">Fra</asp:TableHeaderCell>
                            <asp:TableHeaderCell CssClass="col-md-6">Til</asp:TableHeaderCell>
                        </asp:TableHeaderRow>
                    </asp:Table></div>
                </div>
            </asp:Panel>
        </div>
    </asp:Panel>
</asp:Content>