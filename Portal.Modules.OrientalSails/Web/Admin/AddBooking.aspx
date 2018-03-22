<%@ Page Language="C#" MasterPageFile="SailsMaster.Master" AutoEventWireup="true"
    CodeBehind="AddBooking.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.AddBooking"%>

<%@ Register Assembly="System.Web.Extensions" Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="CMS.ServerControls" Namespace="CMS.ServerControls" TagPrefix="svc" %>
<%@ Register Assembly="Portal.Modules.OrientalSails" Namespace="Portal.Modules.OrientalSails.Web.Controls"
    TagPrefix="orc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <script type="text/javascript">
        function toggleVisible(id) {
            item = document.getElementById(id);
            if (item.style.display == "") {
                item.style.display = "none";
            }
            else {
                item.style.display = "";
            }
        }

        function setVisible(id, visible) {
            control = document.getElementById(id);
            if (visible)
            { control.style.display = ""; }
            else {
                control.style.display = "none";
            }

        }

        function ddltype_changed(id, optionid, vids) {
            ddltype = document.getElementById(id);
            if (vids.indexOf('#' + ddltype.options[ddltype.selectedIndex].value + '#') >= 0) {
                setVisible(optionid, true);
            }
            else {
                setVisible(optionid, false);
            }
            //        switch (ddltype.selectedIndex)
            //        {
            //            case 0:
            //                setVisible(optionid, false);
            //            break;
            //            case 1:
            //                setVisible(optionid, true);
            //            break;
            //        }
        }

        function ddlagency_changed(id, codeid) {
            ddltype = document.getElementById(id);
            switch (ddltype.selectedIndex) {
                case 0:
                    setVisible(codeid, false);
                    break;
                default:
                    setVisible(codeid, true);
                    break;
            }
        }

        $(document).ready(function () {
            $("#<%=txtDate.ClientID%>").datepicker({
                dateFormat: "dd/mm/yy",
                showOn: "both",
                buttonImageOnly: true,
                buttonImage: "/images/calendar.gif",
                changeMonth: true,
                changeYear: true,
                onSelect: function () {
                    __doPostBack('<%=txtDate.UniqueID%>', 'TextChanged');
                }
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
        <legend>
            <img alt="Room" src="../Images/sails.gif" align="absMiddle" />
            <%= base.GetText("textAddBooking") %>
        </legend>
        <div>
            <div class="basicinfo">
                <table>
                    <tr>
                        <asp:PlaceHolder ID="plhTrip" runat="server">
                            <td>
                                <%= base.GetText("textTrip") %>
                            </td>
                            <td width="33%">
                                <asp:DropDownList ID="ddlTrips" runat="server" Width="329" AutoPostBack="true">
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlOptions" runat="server" Width="80" Style="display: none;"
                                    AutoPostBack="True">
                                    <asp:ListItem>Option 1</asp:ListItem>
                                    <asp:ListItem>Option 2</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </asp:PlaceHolder>
                        <td>
                            <%= base.GetText("textStartDate") %>
                            *
                        </td>
                        <td>
                            <asp:TextBox ID="txtDate" runat="server" ReadOnly="true">
                            </asp:TextBox>
                        </td>
                        <asp:PlaceHolder ID="plhEndDate" runat="server">
                            <td>
                                End date *
                            </td>
                            <td>
                                <asp:TextBox ID="txtEndDate" runat="server"></asp:TextBox><ajax:CalendarExtender
                                    ID="calenderEnd" runat="server" TargetControlID="txtEndDate" Format="dd/MM/yyyy">
                                </ajax:CalendarExtender>
                            </td>
                        </asp:PlaceHolder>
                        <td>
                            <asp:LinkButton ID="lbtCheckAvaiable" OnClick="lbtCheckAvaiable_Click" runat="server"
                                Visible="false"></asp:LinkButton>
                        </td>
                    </tr>
                    <tr style="display: none;">
                        <td>
                            <%= base.GetText("textCruise") %>
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
                    </tr>
                    <tr>
                        <td>
                            <%= base.GetText("textAgency") %>
                        </td>
                        <td>
                            <orc:AgencySelector ID="agencySelector" runat="server" />
                            <asp:TextBox ID="txtAgencyCode" runat="server" Width="75"></asp:TextBox>
                            <ajax:TextBoxWatermarkExtender ID="waterAgency" runat="server" TargetControlID="txtAgencyCode"
                                WatermarkText="TA Code">
                            </ajax:TextBoxWatermarkExtender>
                        </td>
                        <td colspan="2">
                            <asp:Repeater ID="rptExtraServices" runat="server" OnItemDataBound="rptExtraServices_ItemDataBound">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "Name") %>
                                    <asp:CheckBox ID="chkService" runat="server" CssClass="checkbox" />
                                    <asp:HiddenField ID="hiddenId" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "Id") %>' />
                                    <asp:HiddenField ID="hiddenValue" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "Price") %>' />
                                </ItemTemplate>
                            </asp:Repeater>
                        </td>
                    </tr>
                </table>
            </div>
            <asp:UpdatePanel runat="server" ID="updatePanel1">
                <ContentTemplate>
                    <div class="data_grid" style="border: 0px">
                        <em>Click vào tên tàu để bắt đầu nhập thông tin phòng</em>
                        <table>
                            <tr>
                                <th width="15%">
                                    Tên tàu
                                </th>
                                <th width="15%">
                                    Số phòng trống
                                </th>
                                <th width="28%">
                                    Trong đó
                                </th>
                            </tr>
                            <asp:Repeater ID="rptCruises" runat="server" OnItemDataBound="rptCruises_ItemDataBound">
                                <ItemTemplate>
                                    <tr id="trCruise" runat="server">
                                        <td>
                                            <asp:LinkButton ID="lbtCruiseName" runat="server" OnClick="lbtCruiseName_Click"></asp:LinkButton>
                                            <asp:Literal ID="litName" runat="server"></asp:Literal>
                                        </td>
                                        <td>
                                            <asp:Literal ID="litRoomCount" runat="server"></asp:Literal>
                                        </td>
                                        <td>
                                            <asp:Literal ID="litRoomDetail" runat="server"></asp:Literal>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                        <asp:PlaceHolder runat="server" ID="plhPending" Visible="False"><span style="display: block;
                            font-weight: bold; margin-bottom: 10px; margin-left: 0.5%; margin-top: 10px">Booking
                            pending</span>
                            <table>
                                <tr>
                                    <th>
                                        Booking code
                                    </th>
                                    <th>
                                        Rooms
                                    </th>
                                    <th>
                                        Trip
                                    </th>
                                    <th>
                                        Partner
                                    </th>
                                    <th>
                                        Created by
                                    </th>
                                    <th>
                                        Sale in charge
                                    </th>
                                    <th>
                                        Pending until
                                    </th>
                                </tr>
                                <asp:Repeater runat="server" ID="rptPendings" OnItemDataBound="rptPendings_ItemDataBound">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:HyperLink runat="server" ID="hplCode"></asp:HyperLink>
                                            </td>
                                            <td>
                                                <asp:Literal runat="server" ID="litRooms"></asp:Literal>
                                            </td>
                                            <td>
                                                <asp:Literal runat="server" ID="litTrip"></asp:Literal>
                                            </td>
                                            <td>
                                                <asp:HyperLink runat="server" ID="hplAgency"></asp:HyperLink>
                                            </td>
                                            <td>
                                                <asp:Label runat="server" ID="lblCreatedBy"></asp:Label>
                                                <ajax:HoverMenuExtender ID="hmeCreatedBy" runat="Server" HoverCssClass="popupHover"
                                                    PopupControlID="panelPending" PopupPosition="Left" TargetControlID="lblCreatedBy"
                                                    PopDelay="25" />
                                                <asp:Panel runat="server" ID="panelPending" CssClass="hover_content">
                                                    <asp:Literal runat="server" ID="litCreatedBy"></asp:Literal><br />
                                                    <asp:Literal runat="server" ID="litCreatorPhone"></asp:Literal><br />
                                                    <asp:Literal runat="server" ID="litCreatorEmail"></asp:Literal>
                                                </asp:Panel>
                                            </td>
                                            <td>
                                                <asp:Label runat="server" ID="lblSaleInCharge"></asp:Label>
                                                <ajax:HoverMenuExtender ID="hmeSale" runat="Server" HoverCssClass="popupHover" PopupControlID="panelSale"
                                                    PopupPosition="Left" TargetControlID="lblSaleInCharge" PopDelay="25" />
                                                <asp:Panel runat="server" ID="panelSale" CssClass="hover_content">
                                                    <asp:Literal runat="server" ID="litSale"></asp:Literal><br />
                                                    <asp:Literal runat="server" ID="litSalePhone"></asp:Literal><br />
                                                    <asp:Literal runat="server" ID="litSaleEmail"></asp:Literal>
                                                </asp:Panel>
                                            </td>
                                            <td>
                                                <asp:Literal runat="server" ID="litPending"></asp:Literal>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                        </asp:PlaceHolder>
                        </table>
                        <asp:PlaceHolder ID="plhCruiseName" runat="server"><span style="display: inline-block;
                            font-weight: bold; margin-left: 0.5%; margin-right: 35px; margin-top: 10px">You've
                            selected
                            <asp:Literal ID="litCurrentCruise" runat="server"></asp:Literal></span>
                            <asp:CheckBox runat="server" ID="chkCharter" Text=" Charter Booking"></asp:CheckBox>
                            <table style="margin-top: 10px">
                                <asp:Repeater ID="rptClass" runat="server" OnItemDataBound="rptClass_ItemDataBound">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hiddenId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"Id") %>' />
                                        <asp:Repeater ID="rptTypes" runat="server" OnItemDataBound="rptTypes_ItemDataBound">
                                            <ItemTemplate>
                                                <tr>
                                                    <td style="width: 200px;">
                                                        <asp:Label ID="labelName" runat="server"></asp:Label><asp:HiddenField ID="hiddenId"
                                                            runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"Id") %>' />
                                                    </td>
                                                    <td style="width: 300px;">
                                                        <asp:DropDownList ID="ddlAdults" runat="server" Width="70">
                                                        </asp:DropDownList>
                                                        <asp:DropDownList ID="ddlChild" runat="server" Width="70">
                                                        </asp:DropDownList>
                                                        <asp:DropDownList ID="ddlBaby" runat="server" Width="70">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <asp:PlaceHolder ID="plhCustomPrice" runat="server">
                                                        <td>
                                                            <asp:TextBox ID="txtPrice" runat="server"></asp:TextBox>
                                                        </td>
                                                    </asp:PlaceHolder>
                                                    <td>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                        </asp:PlaceHolder>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="txtDate" EventName="TextChanged" />
                    <asp:AsyncPostBackTrigger ControlID="ddlTrips" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="ddlOptions" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" CssClass="button"
            Style="margin-top: 10px" />
    </fieldset>
</asp:Content>
