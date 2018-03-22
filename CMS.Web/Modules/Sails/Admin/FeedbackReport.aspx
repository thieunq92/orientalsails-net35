<%@ Page Language="C#" MasterPageFile="SailsMaster.Master" AutoEventWireup="true"
    CodeBehind="FeedbackReport.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.FeedbackReport"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="CMS.ServerControls" Namespace="CMS.ServerControls" TagPrefix="svc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <fieldset>
        <style>
            .sent
            {
                background-color: #92D050;
            }
        </style>
        <svc:Popup runat="server" ID="popupManager">
        </svc:Popup>
        <div class="basicinfo">
            <table>
                <tr>
                    <td>
                        Group
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlGroups" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td>
                        From
                    </td>
                    <td>
                        <asp:TextBox ID="txtFrom" runat="server"></asp:TextBox><ajax:CalendarExtender ID="calendarDate"
                            runat="server" TargetControlID="txtFrom" Format="dd/MM/yyyy">
                        </ajax:CalendarExtender>
                    </td>
                    <td>
                        To
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtTo"></asp:TextBox><ajax:CalendarExtender ID="CalendarExtender1"
                            runat="server" TargetControlID="txtTo" Format="dd/MM/yyyy">
                        </ajax:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td>
                        Cruise
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCruises" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"
                CssClass="button" />
            <asp:Button ID="btnExportAll" runat="server" Text="Export all" OnClick="btnExportAll_Click"
                CssClass="button" />
            <div>
                <ul style="list-style: none; padding: 0px; padding-top: 5px; margin: 0px; margin-top: 10px;"
                    class="tabbutton">
                    <asp:Repeater ID="rptGroups" runat="server" OnItemDataBound="rptGroups_ItemDataBound">
                        <ItemTemplate>
                            <li id="liMenu" runat="server">
                                <asp:HyperLink ID="hplGroup" runat="server"></asp:HyperLink>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
                <div style="clear: both;">
                </div>
            </div>
            <div>
                <table style="width: auto;" cellspacing="0" cellpadding="5">
                    <tr>
                        <th style="border-right: dotted 1px black;">
                            Tiêu chí
                        </th>
                        <asp:Repeater ID="rptAnswers" runat="server">
                            <ItemTemplate>
                                <th colspan="2" style="text-align: center; border-right: dotted 1px black;">
                                    <%# DataBinder.Eval(Container, "DataItem") %>
                                </th>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                    <asp:Repeater ID="rptQuestionsReport" runat="server" OnItemDataBound="rptQuestionsReport_ItemDataBound">
                        <ItemTemplate>
                            <tr>
                                <th style="border-right: dotted 1px black; border-top: dotted 1px black;">
                                    <asp:Literal ID="litQuestion" runat="server"></asp:Literal>
                                </th>
                                <asp:Repeater ID="rptAnswerData" runat="server" OnItemDataBound="rptAnswerData_ItemDataBound">
                                    <ItemTemplate>
                                        <td style="min-width: 50px; text-align: right; border-top: dotted 1px black;">
                                            <asp:Literal ID="litCount" runat="server"></asp:Literal>
                                        </td>
                                        <td style="min-width: 50px; text-align: right; border-right: dotted 1px black; border-top: dotted 1px black;">
                                            <asp:Literal ID="litPercentage" runat="server"></asp:Literal>
                                        </td>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </div>
        <div class="data_table">
            <div class="data_grid">
                <table cellspacing="1">
                    <tr>
                        <th style="width: 15px;">
                            No.
                        </th>
                        <th style="width: 80px;">
                            Date
                        </th>
                        <th style="width: 80px;">
                            Booking
                        </th>
                        <th>
                            Name
                        </th>
                        <th style="width: 110px;">
                            Cruise
                        </th>
                        <th>
                            Room
                        </th>
                        <th style="width: 80px;">
                            Trip
                        </th>
                        <th>
                            Guide
                        </th>
                        <th>
                            Driver
                        </th>
                        <th style="width: 120px;">
                            Address
                        </th>
                        <th>
                            Email
                        </th>
                        <asp:Repeater ID="rptQuestions" runat="server">
                            <ItemTemplate>
                                <th>
                                    <%# DataBinder.Eval(Container.DataItem, "Name") %>
                                </th>
                            </ItemTemplate>
                        </asp:Repeater>
                        <th>
                            Note
                        </th>
                        <th>
                        </th>
                    </tr>
                    <asp:Repeater ID="rptFeedback" runat="server" OnItemDataBound="rptFeedback_ItemDataBound">
                        <ItemTemplate>
                            <asp:Literal ID="trItem" runat="server" Text="<tr>"></asp:Literal>
                            <td>
                                <asp:Literal ID="litIndex" runat="server"></asp:Literal>
                            </td>
                            <td>
                                <asp:Literal ID="litDate" runat="server"></asp:Literal>
                            </td>
                            <td>
                                <asp:HyperLink ID="hplBooking" runat="server"></asp:HyperLink>
                            </td>
                            <td>
                                <asp:Literal ID="litName" runat="server"></asp:Literal>
                            </td>
                            <td>
                                <asp:HyperLink ID="hplCruise" runat="server"></asp:HyperLink>
                                <%--<asp:Literal ID="litCruise" runat="server"></asp:Literal>--%>
                            </td>
                            <td>
                                <asp:Literal ID="litRoom" runat="server"></asp:Literal>
                            </td>
                            <td>
                                <asp:Literal ID="litTrip" runat="server"></asp:Literal>
                            </td>
                            <td>
                                <asp:HyperLink ID="hplGuide" runat="server"></asp:HyperLink>
                            </td>
                            <td>
                                <asp:HyperLink ID="hplDriver" runat="server"></asp:HyperLink>
                            </td>
                            <td>
                                <asp:Literal ID="litAddress" runat="server"></asp:Literal>
                            </td>
                            <td>
                                <asp:Literal ID="litEmail" runat="server"></asp:Literal>
                            </td>
                            <asp:Repeater ID="rptOptions" runat="server" OnItemDataBound="rptOptions_ItemDataBound">
                                <ItemTemplate>
                                    <td>
                                        <asp:Literal ID="litOption" runat="server"></asp:Literal>
                                    </td>
                                </ItemTemplate>
                            </asp:Repeater>
                            <td>
                                <asp:Literal ID="litNote" runat="server"></asp:Literal>
                            </td>
                            <td>
                                <a id="anchorFeedback" runat="server" style="cursor: pointer;">Detail</a>&nbsp;<a
                                    id="anchorEmail" runat="server" style="cursor: pointer;">Email</a>
                                <asp:LinkButton ID="lbtDelete" runat="server" OnClick="lbtDelete_Click" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "Id") %>'>Delete</asp:LinkButton>
                            </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
            <div class="pager">
                <svc:Pager ID="pagerFeedback" runat="server" ControlToPage="rptFeedback" PagerLinkMode="HyperLinkQueryString"
                    PageSize="30"></svc:Pager>
            </div>
        </div>
    </fieldset>
</asp:Content>
