<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="SailsMaster.Master"
    CodeBehind="ViewMeetings.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.ViewMeetings" Title="View Meetings Page - Oriental Sails Management Office" %>

<%@ Register Assembly="CMS.ServerControls" Namespace="CMS.ServerControls" TagPrefix="svc" %>
<%@ Register TagPrefix="ajax" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=1.0.20229.20821, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
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
        <span style="font-size: 12px">View meetings have date meeting from</span>
        <asp:TextBox ID="txtFrom" runat="server" Width="200px">
        </asp:TextBox>
        <span style="font-size: 12px">&nbsp; &nbsp; to </span>
        <asp:TextBox ID="txtTo" runat="server" Width="200px">
        </asp:TextBox>
        <asp:PlaceHolder runat="server" ID="plhSales"><span style="font-size: 12px">&nbsp &nbsp;
            of sales</span>
            <asp:DropDownList runat="server" ID="ddlSales" />
        </asp:PlaceHolder>
        <div>
            <asp:Button runat="server" ID="btnView" CssClass="image_text_in_button image_text_in_button_buttoncontrol"
                ToolTip="View meetings" Style="background-image: url('https://cdn0.iconfinder.com/data/icons/faticons-2/29/view6-128.png');
                margin-top: 10px" Text="View meetings" OnClick="btnView_OnClick">
            </asp:Button>&nbsp;&nbsp;
            <asp:TextBox ID="txtPageSize" runat="server" Width="30px">
            </asp:TextBox><span style="font-size: 12px"> meetings/page</span>
        </div>
        <div class="basicinfo" style="width: 100%; border: none">
            <table border="1" cellspacing="0" cellpadding="2" class="table_text_center">
                <asp:Repeater ID="rptMeetings" runat="server" OnItemDataBound="rptMeetings_OnItemDataBound">
                    <HeaderTemplate>
                        <tr class="header">
                            <th width="7%">
                                <asp:LinkButton runat="server" ID="lbtUpdateTime" OnClick="lbtUpdateTime_OnClick"
                                    ToolTip="Click to sort descending or ascending">Update time</asp:LinkButton>
                                <asp:Image runat="server" ID="imgSortUtStatus" Width="8px" Visible="False" />
                            </th>
                            <th width="7%">
                                <asp:LinkButton runat="server" ID="lbtDateMeeting" OnClick="lbtDateMeeting_OnClick"
                                    ToolTip="Click to sort descending or ascending">Date meeting</asp:LinkButton>
                                <asp:Image runat="server" ID="imgSortDmStatus" Width="8px" Visible="False" />
                            </th>
                            <th width="10%" runat="server" id="thSales">
                                Sales
                            </th>
                            <th width="10%">
                                Meeting with
                            </th>
                            <th width="7%">
                                Position
                            </th>
                            <th width="10%">
                                Belong to agency
                            </th>
                            <th>
                                Note
                            </th>
                            <th width="7%">
                                View Full Note
                            </th>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:Literal runat="server" ID="ltrUpdateTime" />
                            </td>
                            <td>
                                <asp:Literal runat="server" ID="ltrDateMeeting" />
                            </td>
                            <td runat="server" id="tdSales">
                                <asp:Literal runat="server" ID="ltrSale" />
                            </td>
                            <td>
                                <asp:Literal runat="server" ID="ltrName" />
                            </td>
                            <td>
                                <asp:Literal runat="server" ID="ltrPosition" />
                            </td>
                            <td>
                                <asp:Literal runat="server" ID="ltrAgency" />
                            </td>
                            <td style="text-align: left">
                                <asp:Literal runat="server" ID="ltrNote" />
                            </td>
                            <td>
                                <img class="imgViewNote" src="https://cdn0.iconfinder.com/data/icons/faticons-2/29/view6-128.png"
                                    width="17px" height="17px" style="cursor: pointer" noteid='<%#Eval("Id") %>' />
                                <div class="divViewNote" style="display: none; font-size: 12px" noteid='<%#Eval("Id") %>'>
                                    <%#((String)Eval("Note")).Replace("\r\n","<br/>")%></div>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
            <div class="pager">
                <asp:Button runat="server" ID="btnExportMeetings" CssClass="image_text_in_button image_text_in_button_buttoncontrol"
                    ToolTip="Export meetings" Style="background-image: url('https://cdn1.iconfinder.com/data/icons/Momentum_MatteEntireSet/32/word.png');
                    float: left;" Text="Export meetings"
                    OnClick="btnExportMeetings_OnClick"></asp:Button>
                <svc:Pager ID="pagerMeetings" runat="server" HideWhenOnePage="True" ShowTotalPages="True"
                    ControlToPage="rptMeetings" OnPageChanged="pagerMeetings_OnPageChanged" PageSize="20" />
            </div>
        </div>
    </fieldset>
</asp:Content>
