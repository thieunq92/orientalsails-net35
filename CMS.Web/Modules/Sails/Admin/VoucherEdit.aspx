<%@ Page Title="" Language="C#" MasterPageFile="SailsMaster.Master" AutoEventWireup="true"
    CodeBehind="VoucherEdit.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.VoucherEdit" %>

<%@ Register Assembly="Portal.Modules.OrientalSails" Namespace="Portal.Modules.OrientalSails.Web.Controls"
    TagPrefix="orc" %>
<%@ Register TagPrefix="ajax" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=1.0.20229.20821, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <fieldset>
        <legend>Voucher Batch </legend>
        <div class="settinglist">
            <em>Note: After issue, you can not change voucher information, unless you're administrator</em>
            <div class="basicinfo">
                <table>
                    <tr>
                        <td>
                            Program name
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtName"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Vouchers issue for
                        </td>
                        <td>
                            <orc:AgencySelector ID="agencySelector" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Quantity
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtVoucher"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Apply for
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlPersons">
                                <asp:ListItem Text="1 person" Value="1"></asp:ListItem>
                                <asp:ListItem Text="2 people (1 cabin)" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Cruise:
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlCruises" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Trip:
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlTrips" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Value (leave or input 0 to auto calculate)
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtValue"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Valid until
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtValidUntil"></asp:TextBox><ajax:CalendarExtender
                                ID="calendarValidUntil" runat="server" TargetControlID="txtValidUntil" Format="dd/MM/yyyy">
                            </ajax:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Issue date
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtIssueDate"></asp:TextBox><ajax:CalendarExtender
                                ID="calendarDate" runat="server" TargetControlID="txtIssueDate" Format="dd/MM/yyyy">
                            </ajax:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Voucher template
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlTemplates" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Contract file (if any)
                        </td>
                        <td>
                            <asp:HyperLink runat="server" ID="hplContract" Visible="false"></asp:HyperLink>
                            <asp:FileUpload runat="server" ID="fileuploadContract" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Voucher code (after save):
                        </td>
                        <td>
                            <asp:Repeater runat="server" ID="rptVouchers" OnItemDataBound="rptVouchers_ItemDataBound">
                                <ItemTemplate>
                                    <asp:Literal runat="server" ID="litCode"></asp:Literal>;
                                </ItemTemplate>
                            </asp:Repeater>
                        </td>
                    </tr>
                    <tr>
                        <td>Note:</td>
                        <td>
                            <asp:TextBox runat="server" TextMode="MultiLine" ID="txtNote"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
            <asp:Button ID="buttonSave" runat="server" OnClick="buttonSave_Click" CssClass="button"
                ValidationGroup="valid" />
            <asp:Button ID="buttonIssue" Text="Issue (Word)" runat="server" OnClick="buttonIssue_Click"
                CssClass="button" ValidationGroup="valid" />
            <asp:Button ID="buttonIssuePDF" Text="Issue (PDF)" runat="server" OnClick="buttonIssuePDF_Click"
                CssClass="button" ValidationGroup="valid" />
        </div>
    </fieldset>
</asp:Content>
