<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageFrontend.master" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="index" Theme="frontend" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphFrontend" Runat="Server">
<div class="row step-1--wrapper"> 
    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
        <div class="step-1">
            <div class="col-xs-10 col-sm-10 col-md-10 col-lg-10 col-xs-offset-1 col-sm-offset-1 col-md-offset-1 col-lg-offset-1">
                <div class="headline">
                    <h2 class=""> <b>easyTV</b> er den nemmeste måde at finde din nye TV-udbyder til den billigste pris.</h2>
                    <h2 class="">Indstast dit postnummer for at starte din søgning</h2>
                </div>
                <div class="search-content">
                    <div class="col-xs-10 col-sm-10 col-md-10 col-lg-10 col-xs-offset-1 col-sm-offset-1 col-md-offset-1 col-lg-offset-1">
                        <asp:TextBox ID="txtStartSearch" CssClass="input-global input--search " runat="server" placeholder="f.eks. 8000"></asp:TextBox>
                        <asp:Button ID="btnStartSearch" CssClass="btn-global btn-search btn-warning" runat="server" Text="Start søgning" OnClick="btnStartSearch_Click" />
                        <br /><br />
                        <!-- VALIDATION -->
                        <asp:RequiredFieldValidator 
                            ID="rfvTxtStartSearch" 
                            ControlToValidate="txtStartSearch" 
                            Display="Dynamic" 
                            runat="server">
                            <div class="alert alert-warning"><span class="glyphicon glyphicon-info-sign"></span> Husk at indtaste et postnummer</div>
                        </asp:RequiredFieldValidator>
                        <asp:RangeValidator 
                            ID="rvTxtStartSearch" 
                            runat="server"
                            ControlToValidate="txtStartSearch"
                            Display="Dynamic"  
                            Type="Integer" 
                            MaximumValue="9999" 
                            MinimumValue="1000">
                        <div class="alert alert-warning"><span class="glyphicon glyphicon-info-sign"></span> Husk at postnumre skal være mellem 1000-9999</div>
                        </asp:RangeValidator>


                        <!-- NOK UNØDVENDIG?
                        <asp:RegularExpressionValidator 
                            ID="reTxtStartSearch" 
                            runat="server" 
                            ValidationExpression="[\d]{4}" 
                            ControlToValidate="txtStartSearch">
                        <div class="alert alert-warning"><span class="glyphicon glyphicon-info-sign"></span> Husk at postnumre skal være 4 cifre</div>
                        </asp:RegularExpressionValidator>
                        -->
                    </div>
                </div>
            </div>
            <div class="col-xs-10 col-sm-10 col-md-10 col-lg-10 col-xs-offset-1 col-sm-offset-1 col-md-offset-1 col-lg-offset-1">
                <div class="step-graphs">
                    <img src="assets/img/step-graphs.png" alt="steps" />
                </div>
            </div>
        </div>
    </div>
</div>  
</asp:Content>

