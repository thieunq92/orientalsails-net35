<%@ Page Language="C#" MasterPageFile="Popup.Master" AutoEventWireup="true" CodeBehind="CheckVoucher.aspx.cs"
    Inherits="Portal.Modules.OrientalSails.Web.Admin.CheckVoucher" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="System.Web.Extensions" Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="CMS.ServerControls" Namespace="CMS.ServerControls" TagPrefix="svc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <fieldset>
        <legend>
            <img alt="Room" src="../Images/sails.gif" align="absMiddle" />
            Check voucher validity </legend>
        <div class="settinglist">
            <div class="search_panel" style="display:none">
                <table>
                    <tr>
                        <td>
                            <asp:TextBox runat="server" ID="txtVoucherCode" Width="400px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <asp:Button runat="server" ID="btnCheck" OnClick="btnCheck_ItemDataBound" Text="Check" />
            </div>
        </div>
        <asp:Repeater runat="server" ID="rptVoucher" OnItemDataBound="rptVoucher_OnItemDataBound">
            <ItemTemplate>
                <div class="data_table">
                    <div class="data_grid">
                        <table>
                            <tr>
                                <td style="width:30%">
                                    Voucher Code
                                </td>
                                <td>
                                    <asp:Literal runat="server" ID="litVoucherCode"></asp:Literal>
                                </td>
                            </tr>
                            <asp:PlaceHolder runat="server" ID="plhValid">
                                <tr>
                                    <td>
                                        Program name
                                    </td>
                                    <td>
                                        <asp:Literal runat="server" ID="litProgramName"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Status:
                                    </td>
                                    <td>
                                        <asp:Literal runat="server" ID="litStatus"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Vouchers issue for
                                    </td>
                                    <td>
                                        <asp:Literal runat="server" ID="litAgency"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Quantity
                                    </td>
                                    <td>
                                        <asp:Literal runat="server" ID="litQuantity"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Apply for
                                    </td>
                                    <td>
                                        <asp:Literal runat="server" ID="litApplyFor"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Cruise:
                                    </td>
                                    <td>
                                        <asp:Literal runat="server" ID="litCruise"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Trip:
                                    </td>
                                    <td>
                                        <asp:Literal runat="server" ID="litTrip"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Value
                                    </td>
                                    <td>
                                        <asp:Literal runat="server" ID="litValue"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Valid until
                                    </td>
                                    <td>
                                        <asp:Literal ID="litValidUntil" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Issue date
                                    </td>
                                    <td>
                                        <asp:Literal runat="server" ID="litIssueDate"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Contract file
                                    </td>
                                    <td>
                                        <asp:HyperLink runat="server" ID="hplContract">Contract</asp:HyperLink>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Note
                                    </td>
                                    <td>
                                        <asp:Literal runat="server" ID="litNote"></asp:Literal>
                                    </td>
                                </tr>
                            </asp:PlaceHolder>
                            <asp:PlaceHolder runat="server" ID="plhInvalid" Visible="False">
                                <tr>
                                    <td colspan="3">
                                        <strong>This voucher code is not valid, please check again</strong>
                                    </td>
                                </tr>
                            </asp:PlaceHolder>
                        </table>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </fieldset>
</asp:Content>
