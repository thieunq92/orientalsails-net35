<%@ Page Language="C#" MasterPageFile="SailsMaster.Master" AutoEventWireup="true"
    CodeBehind="FeedbackResponse.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.FeedbackResponse" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="CMS.ServerControls" Namespace="CMS.ServerControls" TagPrefix="svc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <fieldset>
        <div class="basicinfo">
            <table>
                <tr>
                    <td>
                        Date
                    </td>
                    <td>
                        <asp:TextBox ID="txtDate" runat="server"></asp:TextBox><ajax:CalendarExtender ID="calendarDate"
                            runat="server" TargetControlID="txtDate" Format="dd/MM/yyyy">
                        </ajax:CalendarExtender>
                    </td>
                </tr>
            </table>
            <asp:Button runat="server" ID="btnSelect" CssClass="button" Text="Select" OnClick="btnSelect_Click" />
        </div>
        <asp:PlaceHolder runat="server" ID="plhSummary" Visible="false">
        <table>
            <tr>
                <td>Number of booking:</td>
                <td><asp:Literal runat="server" ID="litNumberOfBookings"></asp:Literal></td>
                <td>Number of feedback</td>
                <td><asp:Literal runat="server" ID="litNumberOfFeedback"></asp:Literal></td>
            </tr>
        </table>
        </asp:PlaceHolder>
        <div class="data_table">
            <div class="data_grid">                
                <table>                    
                    <tr>
                        <td>Booking code</td>
                        <td>Customer</td>
                        <td>Overall</td>
                        <td>Status</td>
                        <td style="width: 150px;"></td>
                        <td style="width: 150px;"></td>
                        <td style="width: 150px;"></td>
                    </tr>
                    <asp:Repeater runat="server" ID="rptFeedbacks" OnItemDataBound="rptFeedbacks_ItemDataBound">
                        <ItemTemplate>
                            <tr>
                                <td><asp:Literal runat="server" ID="litCode"></asp:Literal><asp:HyperLink runat="server" ID="hplBookingCode"></asp:HyperLink></td>
                                <td><asp:Literal runat="server" ID="litName"></asp:Literal></td>
                                <td><asp:Literal runat="server" ID="litOverall"></asp:Literal></td>
                                <td><asp:Literal runat="server" ID="litStatus"></asp:Literal></td>
                                <td>
                                    <asp:DropDownList runat="server" ID="ddlSend">
                                        <asp:ListItem Value="1">Send or resend</asp:ListItem>
                                        <asp:ListItem Value="0">Don't send</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="ddlTemplates"></asp:DropDownList>
                                </td>
                                <td><asp:Button runat="server" ID="btnSend" OnClick="btnSend_Click" Text="Send" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>'/></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </div>
        <asp:Button runat="server" ID="btnSendAll" Text="Send All" OnClick="btnSendAll_Click"/>
    </fieldset>
</asp:Content>
