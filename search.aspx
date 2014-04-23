<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageFrontend.master" AutoEventWireup="true" CodeFile="search.aspx.cs" Inherits="search" Theme="frontend" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
<script src="//ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>
<script src="//ajax.googleapis.com/ajax/libs/jqueryui/1.10.3/jquery-ui.min.js"></script>
<script>
    $(function () {
        var newPrice = $("#cphFrontend_txtPrice").val();

        $("#slider").slider({
            value: newPrice,
            min: 0,
            max: 700,
            step: 50,
            slide: function (event, ui) {
                $("#cphFrontend_txtPrice").val(ui.value);
            }
        });

        $("#cphFrontend_txtPrice").val($("#slider").slider("value"));
    });

</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphFrontend" Runat="Server">
<div class="search-content--wrapper">
    <div class="row">
        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
            <div class="row">
                <!-- SEARCH OPTIONS -->
                <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3">
                    <div class="avanced-search--wrapper">                       
                        <asp:Label id="lblTest" font-names="Verdana" font-size="8pt" runat="server"/>
                        <h2>Filtrering</h2>
                        <asp:Button id="btnSearch" CssClass="btn btn-primary col-xs-12 col-sm-12 col-md-12 col-lg-12" Text="Søg" onclick="btnSearch_Click" runat="server"/><br /><br />
                        <!-- PRICE SLIDERS -->
                        <div class="slider--wrapper">                            
                            <h4>Max pris i kr.</h4>
                            <p>
                            <asp:TextBox id="txtPrice" CssClass="input-global" runat="server"></asp:TextBox>
                            </p>
                            <div id="slider" class="ui-slider"></div>                            
                        </div>
                        <!-- INTERNET CHECKBOX -->
                        <div class="form-group checkbox--wrapper">                            
                            <div class="checkboxFour">
                                <asp:CheckBox runat="server" ID="internetCheck" />
                                <label for="cphFrontend_internetCheck"></label>                                
                            </div>
                            <h4 class="checkbox-header">Internet inkluderet</h4>                           
                        </div>
                        <!-- CHANNEL MULTISELECT  -->
                        <div class="form-group ms--wrapper">
                            <label for="lbFavouriteChannels"> <h4>Tilføj ønskede kanaler</h4> </label>
                            <asp:ListBox ID="lbFavouriteChannels" SelectionMode="Multiple" CssClass="form-control multiSelect" runat="server" />
                        </div>
                    </div>
                </div>
                <!-- PACKAGES -->
                <div class="col-xs-9 col-sm-9 col-md-9 col-lg-9">
                    <div class="packages--wrapper">
                        <asp:Panel ID="pnlPackages" runat="server">
                        </asp:Panel>
                    </div>
                </div>

            </div>
        </div>
    </div>
</div>

</asp:Content>