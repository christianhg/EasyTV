<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageBackend.master" AutoEventWireup="true" CodeFile="packagedetails.aspx.cs" Inherits="packagedetails" Theme="backend" %>

<asp:Content ID="hPacakgeDetails" ContentPlaceHolderID="cphBackendHead" Runat="Server">
</asp:Content>
<asp:Content ID="cPackageDetails" ContentPlaceHolderID="cphBackend" Runat="Server">
    <asp:Panel ID="pnlInsertPackage" runat="server">
        <asp:Panel ID="pnlAlert" runat="server" Visible="false">
            <asp:Literal ID="ltrAlert" runat="server" />
        </asp:Panel>
        <div class="row">
            <asp:Panel CssClass="col-md-6" ID="pnlPackageDetails" runat="server">
                <div class="form-group">
                    <label for="ddlPackageTvProvider">Udbyder</label>
                    <asp:DropDownList CssClass="form-control" ID="ddlPackageTvProvider" runat="server"></asp:DropDownList>
                </div>
                <div class="form-group">
                    <label for="txtPackageName">Navn</label>
                    <asp:TextBox CssClass="form-control" ID="txtPackageName" runat="server"></asp:TextBox>
                    <!-- VALIDATION -->
                    <asp:RequiredFieldValidator 
                        ID="rfvTxtPackageName" 
                        ControlToValidate="txtPackageName" 
                        Display="Dynamic" 
                        runat="server">
                        <div class="alert alert-warning"><span class="glyphicon glyphicon-info-sign"></span> Husk at indtaste et navn</div>
                    </asp:RequiredFieldValidator>                                            
                    <asp:RegularExpressionValidator 
                        ID="reTxtPackageName" 
                        runat="server"
                        display="Dynamic" 
                        ValidationExpression="^\s*([^\s]\s*){1,50}" 
                        controltovalidate="txtPackageName">
                    <div class="alert alert-warning"><span class="glyphicon glyphicon-info-sign"></span> Husk at navn max. kan være 50 karakter</div>
                    </asp:RegularExpressionValidator>
                    <asp:CustomValidator 
                        ID="cvTxtPackageName" 
                        runat="server" 
                        ErrorMessage="CustomValidator"
                        controltovalidate="txtPackageName"
                        Display="Dynamic"
                        OnServerValidate="cvTxtPackageName_ServerValidate">
                        <div class="alert alert-warning"><span class="glyphicon glyphicon-info-sign"></span> TV-udbyderen har allerede en pakke med dette navn </div>
                    </asp:CustomValidator>
                </div>
                <div class="form-group">
                    <label for="txtPackageInfo">Info</label>
                    <asp:TextBox CssClass="form-control" ID="txtPackageInfo" runat="server"></asp:TextBox>
                    <!-- VALIDATION -->
                    <asp:RequiredFieldValidator 
                        ID="rfvTxtPackageInfo" 
                        ControlToValidate="txtPackageInfo" 
                        Display="Dynamic" 
                        runat="server">
                        <div class="alert alert-warning"><span class="glyphicon glyphicon-info-sign"></span> Husk at indtaste info</div>
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator 
                        ID="reTxtPackageInfo" 
                        runat="server"
                        display="Dynamic" 
                        ValidationExpression="^\s*([^\s]\s*){1,200}" 
                        controltovalidate="txtPackageInfo">
                    <div class="alert alert-warning"><span class="glyphicon glyphicon-info-sign"></span> Husk at info max. kan være 200 karakter</div>
                    </asp:RegularExpressionValidator>
                </div>
                <div class="form-group">
                    <label for="txtPackageUrl">Url</label>
                    <asp:TextBox CssClass="form-control" ID="txtPackageUrl" runat="server"></asp:TextBox>
                    <!-- VALIDATION -->
                    <asp:RequiredFieldValidator 
                        ID="rfvTxtPackageUrl" 
                        ControlToValidate="txtPackageUrl" 
                        Display="Dynamic" 
                        runat="server">
                        <div class="alert alert-warning"><span class="glyphicon glyphicon-info-sign"></span> Husk at indtaste en url</div>
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator 
                        ID="reTxtPackageUrl" 
                        runat="server"
                        display="Dynamic" 
                        ValidationExpression="^\s*([^\s]\s*){1,200}" 
                        controltovalidate="txtPackageUrl">
                    <div class="alert alert-warning"><span class="glyphicon glyphicon-info-sign"></span> Husk at url max. kan være 200 karakter</div>
                    </asp:RegularExpressionValidator>
                </div>
                <div class="form-group">
                    <label for="txtPackagePricePerMonth">Pris/md</label>
                    <asp:TextBox CssClass="form-control" ID="txtPackagePricePerMonth" runat="server"></asp:TextBox>
                    <!-- VALIDATION -->
                    <asp:RequiredFieldValidator 
                        ID="rfvTxtPackagePricePerMonth" 
                        ControlToValidate="txtPackagePricePerMonth" 
                        Display="Dynamic" 
                        runat="server">
                        <div class="alert alert-warning"><span class="glyphicon glyphicon-info-sign"></span> Husk at indtaste en pris/mn</div>
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator 
                        ID="revTxtPackagePricePerMonth" 
                        ControlToValidate="txtPackagePricePerMonth" 
                        runat="server" 
                        Display="Dynamic" 
                        ValidationExpression="[0-9]+((\.|\,)[0-9][0-9]?)?">
                        <div class="alert alert-warning"><span class="glyphicon glyphicon-info-sign"></span> Indtast venligst et tal med max to decimaler</div>
                    </asp:RegularExpressionValidator>
                </div>
                <div class="form-group">
                    <label for="txtPackageStartUpFee">Oprettelsespris</label>
                    <asp:TextBox CssClass="form-control" ID="txtPackageStartUpFee" runat="server"></asp:TextBox>
                    <!-- VALIDATION -->
                    <asp:RequiredFieldValidator 
                        ID="rfvTxtPackageStartUpFee" 
                        ControlToValidate="txtPackageStartUpFee" 
                        Display="Dynamic" 
                        runat="server">
                        <div class="alert alert-warning"><span class="glyphicon glyphicon-info-sign"></span> Husk at indtaste en oprettelsespris</div>
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator 
                        ID="revTxtPackageStartUpFee" 
                        ControlToValidate="txtPackageStartUpFee" 
                        runat="server" 
                        Display="Dynamic" 
                        ValidationExpression="[0-9]+((\.|\,)[0-9][0-9]?)?">
                        <div class="alert alert-warning"><span class="glyphicon glyphicon-info-sign"></span> Indtast venligst et tal med max to decimaler</div>
                    </asp:RegularExpressionValidator>
                </div>
                <div class="form-group">
                    <label for="cbInternet">Internet</label>
                    <asp:CheckBox ID="cbInternet" Checked="true" runat="server" AutoPostBack="true" OnCheckedChanged="cbInternet_CheckedChanged"></asp:CheckBox>
                </div>
                <asp:Panel ID="pnlPackageInternet" CssClass="row" runat="server">
                    <div class="form-group col-xs-6">
                        <label for="txtPackageDownload">Download (MB/s)</label>
                        <asp:TextBox CssClass="form-control" ID="txtPackageDownload" runat="server"></asp:TextBox>
                        <!-- VALIDATION -->
                        <asp:RequiredFieldValidator 
                            Display="Dynamic" 
                            runat="server" 
                            ID="rfvTxtPackageDownload" 
                            ControlToValidate="txtPackageDownload">
                            <div class="alert alert-warning"><span class="glyphicon glyphicon-info-sign"></span> Husk at indtaste download</div>
                        </asp:RequiredFieldValidator>
                        <asp:RangeValidator 
                            ID="rvTxtPackageDownload" 
                            runat="server"
                            ControlToValidate="txtPackageDownload"
                            Display="Dynamic"  
                            Type="Integer" 
                            MaximumValue="9999" 
                            MinimumValue="1">
                        <div class="alert alert-warning"><span class="glyphicon glyphicon-info-sign"></span> Husk at indtaste et tal</div>
                        </asp:RangeValidator>
                    </div>
                    <div class="form-group col-xs-6">
                        <label for="txtPackageUpload">Upload (MB/s)</label>
                        <asp:TextBox CssClass="form-control" ID="txtPackageUpload" runat="server"></asp:TextBox>
                        <!-- VALIDATION -->
                        <asp:RequiredFieldValidator 
                            Display="Dynamic" 
                            runat="server" 
                            ID="rfvTxtPackageUpload" 
                            ControlToValidate="txtPackageUpload">
                            <div class="alert alert-warning"><span class="glyphicon glyphicon-info-sign"></span> Husk at indtaste upload</div>
                        </asp:RequiredFieldValidator>
                        <asp:RangeValidator 
                            ID="rvTxtPackageUpload" 
                            runat="server"
                            ControlToValidate="txtPackageUpload"
                            Display="Dynamic"  
                            Type="Integer" 
                            MaximumValue="9999" 
                            MinimumValue="1">
                        <div class="alert alert-warning"><span class="glyphicon glyphicon-info-sign"></span> Husk at indtaste et tal</div>
                        </asp:RangeValidator>
                    </div>
                </asp:Panel>
            </asp:Panel>
            <asp:Panel ID="pnlPackageExtras" CssClass="col-md-6" runat="server">
                <div class="form-group">
                    <label for="lbPackageChannels">Tilføj kanaler til pakken</label>
                    <asp:ListBox ID="lbPackageChannels" SelectionMode="Multiple" CssClass="multiSelect" runat="server" />
                </div>
                <div class="form-group">
                    <label for="lbPackageChannels">Tilføj streaming-tjenester til pakken</label>
                    <asp:ListBox ID="lbPackageStreamingServices" SelectionMode="Multiple" CssClass="" runat="server" />
                </div>
            </asp:Panel>
        </div>
        <div class="form-group">
            <asp:Button OnClick="btnInsertPackage_Click" CssClass="btn btn-success" ID="btnInsertPackage" runat="server" Text="Opret Pakke" />
            <asp:Button OnClick="btnUpdatePackage_Click" Visible="false" CssClass="btn btn-primary" ID="btnUpdatePackage" runat="server" Text="Opdatér pakke" />            
            <a href="backend.aspx" class="btn btn-warning">Tilbage</a>
        </div>
    </asp:Panel>
</asp:Content>