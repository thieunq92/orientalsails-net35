<%@ Page Title="Inspection Report Page" Language="C#" MasterPageFile="SailsMaster.Master"
    AutoEventWireup="true" CodeBehind="InspectionReport.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.InspectionReport" %>

<%@ Register TagPrefix="ajax" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=1.0.20229.20821, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<%@ Register TagPrefix="svc" Namespace="CMS.ServerControls" Assembly="CMS.ServerControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=txtFrom.ClientID%>").datepicker({
                dateFormat: "dd/mm/yy",
                showOn: "both",
                buttonImageOnly: true,
                buttonImage: "/images/calendar.gif",
                changeMonth: true,
                changeYear: true,
            });

            $("#<%=txtTo.ClientID%>").datepicker({
                dateFormat: "dd/mm/yy",
                showOn: "both",
                buttonImageOnly: true,
                buttonImage: "/images/calendar.gif",
                changeMonth: true,
                changeYear: true,
            });

            $(".imgViewNote").click(function() {
                var noteid = $(this).attr("noteid");
                $(".divViewNote").each(function(index, element) {
                        if ($(element).attr("noteid") == noteid) {
                            $(element).dialog({
                                autoOpen: false,
                                modal: true,
                                title: "Full note",
                                width: 500
                            });
                            $(element).dialog("open");
                        }
                    }
                );
            });
        });
    </script>
    <style>
        #ui-datepicker-div
        {
            width: 197px;
            left: 50.3%;
            font-size: 10px;
        }
        .ui-datepicker-next:hover
        {
            left: 87%;
        }
        .ui-datepicker-next
        {
            cursor: pointer;
        }
        .ui-datepicker-prev
        {
            cursor: pointer;
        }
        .ui-datepicker-trigger
        {
            position: relative;
            top: 5px;
            width: 19px;
        }
    </style>
    <fieldset>
        <span style="font-size: 12px">Start date from</span>
        <asp:TextBox ID="txtFrom" runat="server" Width="200px">
        </asp:TextBox>
        <span style="font-size: 12px">&nbsp; &nbsp; to </span>
        <asp:TextBox ID="txtTo" runat="server" Width="200px">
        </asp:TextBox>
        <div>
            <asp:Button runat="server" ID="btnView" CssClass="image_text_in_button image_text_in_button_buttoncontrol"
                ToolTip="Search" Style="background-image: url('https://cdn0.iconfinder.com/data/icons/faticons-2/29/view6-128.png');
                margin-top: 10px" Text="Search"></asp:Button>&nbsp;&nbsp;
        </div>
        <div class="data_table">
            <div class="data_grid">
                <table cellspacing="1">
                    <asp:Repeater ID="rptBookingList" runat="server" OnItemDataBound="rptBookingList_ItemDataBound">
                        <HeaderTemplate>
                            <tr class="header">
                                <th>
                                    <%#base.GetText("textBookingCode")%>
                                </th>
                                <th>
                                    <%#base.GetText("labelTripName")%>
                                </th>
                                <th>
                                    <%#base.GetText("textCruise") %>
                                </th>
                                <th>
                                    <%#base.GetText("textNumberOfPax")%>
                                </th>
                                <th id="columnCustomerName" runat="server">
                                    <%#base.GetText("textCustomerName")%>
                                </th>
                                <th>
                                    <%#base.GetText("labelPartner") %>
                                </th>
                                <th>
                                    <%#base.GetText("textAgencyCode") %>
                                </th>
                                <th>
                                    <%#base.GetText("labelStatus") %>
                                </th>
                                <th>
                                </th>
                                <th>
                                    Last edit
                                </th>
                                <th>
                                    <%#base.GetText("labelStartDate") %>
                                </th>
                                <asp:PlaceHolder ID="plhAccounting" runat="server">
                                    <th>
                                        <%#base.GetText("textAccounting") %>
                                    </th>
                                </asp:PlaceHolder>
                                <th>
                                    <%#base.GetText("labelAction") %>
                                </th>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr id="trItem" runat="server" class="item">
                                <td>
                                    <asp:Panel CssClass="hover_content" ID="PopupMenu" runat="server">
                                        <asp:Literal ID="litNote" runat="server"></asp:Literal>
                                    </asp:Panel>
                                    <asp:HyperLink ID="hplCode" runat="server"></asp:HyperLink>
                                </td>
                                <td>
                                    <asp:HyperLink ID="hyperLink_Trip" runat="server"></asp:HyperLink>
                                </td>
                                <td>
                                    <asp:HyperLink ID="hplCruise" runat="server"></asp:HyperLink>
                                </td>
                                <td>
                                    <asp:Literal ID="litPax" runat="server"></asp:Literal>
                                </td>
                                <td id="columnCustomerName" runat="server">
                                    <asp:Label ID="labelName" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:HyperLink ID="hplAgency" runat="server"></asp:HyperLink>
                                </td>
                                <td>
                                    <asp:Literal ID="litAgencyCode" runat="server"></asp:Literal>
                                </td>
                                <td>
                                    <asp:Label ID="label_Status" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Literal ID="litAmend" runat="server" Text="Amended"></asp:Literal>
                                </td>
                                <td>
                                    <asp:Label ID="labelConfirmBy" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="label_startDate" runat="server"></asp:Label>
                                </td>
                                <td id="plhAccounting">
                                    <asp:Literal ID="litAccounting" runat="server"></asp:Literal>
                                </td>
                                <td>
                                    <asp:HyperLink ID="hyperLinkView" runat="server">
                                        <asp:Image ID="imageView" runat="server" ImageAlign="AbsMiddle" AlternateText="View"
                                            CssClass="image_button16" ImageUrl="../Images/edit.gif" />
                                    </asp:HyperLink>
                                    <asp:ImageButton runat="server" ID="imageButtonDelete" ToolTip='Delete' ImageUrl="../Images/delete_file.gif"
                                        AlternateText='Delete' ImageAlign="AbsMiddle" CssClass="image_button16" CommandName="Delete"
                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem,"Id") %>' Visible="false" />
                                    <ajax:ConfirmButtonExtender ID="ConfirmButtonExtenderDelete" runat="server" TargetControlID="imageButtonDelete"
                                        ConfirmText='<%# base.GetText("messageConfirmDelete") %>'>
                                    </ajax:ConfirmButtonExtender>
                                    <asp:Image ImageUrl="../Images/info.png" ID="imgNote" runat="server" CssClass="image_button16" />
                                    <ajax:HoverMenuExtender ID="hmeNote" runat="Server" HoverCssClass="popupHover" PopupControlID="PopupMenu"
                                        PopupPosition="Left" TargetControlID="imgNote" PopDelay="25" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
            <div class="pager">
                <svc:Pager ID="pagerBookings" runat="server" HideWhenOnePage="true" ControlToPage="rptBookingList"
                    PagerLinkMode="HyperLinkQueryString" PageSize="20" />
            </div>
        </div>
    </fieldset>  <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=txtFrom.ClientID%>").datepicker({
                dateFormat: "dd/mm/yy",
                showOn: "both",
                buttonImageOnly: true,
                buttonImage: "/images/calendar.gif",
                changeMonth: true,
                changeYear: true,
            });

            $("#<%=txtTo.ClientID%>").datepicker({
                dateFormat: "dd/mm/yy",
                showOn: "both",
                buttonImageOnly: true,
                buttonImage: "/images/calendar.gif",
                changeMonth: true,
                changeYear: true,
            });

            $(".imgViewNote").click(function() {
                var noteid = $(this).attr("noteid");
                $(".divViewNote").each(function(index, element) {
                        if ($(element).attr("noteid") == noteid) {
                            $(element).dialog({
                                autoOpen: false,
                                modal: true,
                                title: "Full note",
                                width: 500
                            });
                            $(element).dialog("open");
                        }
                    }
                );
            });
        });
    </script>
    <style>
        #ui-datepicker-div
        {
            width: 197px;
            left: 50.3%;
            font-size: 10px;
        }
        .ui-datepicker-next:hover
        {
            left: 87%;
        }
        .ui-datepicker-next
        {
            cursor: pointer;
        }
        .ui-datepicker-prev
        {
            cursor: pointer;
        }
        .ui-datepicker-trigger
        {
            position: relative;
            top: 5px;
            width: 19px;
        }
    </style>
</asp:Content>
