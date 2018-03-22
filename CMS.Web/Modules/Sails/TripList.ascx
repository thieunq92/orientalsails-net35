<%@ Control Language="C#" AutoEventWireup="true" Codebehind="TripList.ascx.cs" Inherits="Portal.Modules.OrientalSails.Web.TripList" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="CMS.ServerControls" Namespace="CMS.ServerControls" TagPrefix="cc1" %>
<div class="trip_list">
    <ul>
        <asp:Repeater ID="rptTripList" runat="server" OnItemDataBound="rptTripList_ItemDataBound"
            OnItemCommand="rptTripList_ItemCommand">
            <ItemTemplate>
                <li class="trip_list_item">
                	<h4><asp:HyperLink ID="hyperLink_Name" runat="server" CssClass="trip_list_name"></asp:HyperLink></h4>
                    <asp:Image ID="imageTrip" runat="server" CssClass="trip_list_image" />                    
                    <asp:Label ID="label_NumberofDays" runat="server" CssClass="trip_list_NoOfDay"></asp:Label>
                    <p runat="server" id="description" class="trip_list_description">
                    </p>
                    <asp:HyperLink ID="hyperLink_Detail" runat="server" CssClass="trip_list_detail"></asp:HyperLink>
                    <br />
                    <asp:DropDownList ID="ddlOption" runat="server">
                    </asp:DropDownList>
                    <asp:TextBox ID="textBoxStartDate" runat="server"></asp:TextBox>
                    <asp:Image ID="imagecalenderStartDate" ImageUrl="/Images/calendar.gif" runat="server"
                        CssClass="image_button16" ImageAlign="AbsMiddle" />
                    <ajax:calendarextender id="calenderStartDate" runat="server" format="dd/MM/yyyy"
                        targetcontrolid="textBoxStartDate" popupbuttonid="imagecalenderStartDate">
                    </ajax:calendarextender>
                    <asp:ImageButton ID="imageButtonBook" runat="server" ImageAlign="AbsMiddle" ImageUrl="/Modules/Sails/Images/book_now_icon.gif"
                        AlternateText="Book now" ToolTip="Book now" CommandName="Book" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"Id") %>' />
                    <asp:Label ID="labelError" runat="server"></asp:Label>
                    <div style="clear:both"></div>
                </li>
            </ItemTemplate>
        </asp:Repeater>
        <li class="trip_list_pager">
            <cc1:Pager ID="pagerTripList" runat="server" ControlToPage="rptTripList" HideWhenOnePage="true"
                OnCacheEmpty="pagerTripList_CacheEmpty" OnPageChanged="pagerTripList_PageChanged" />
        </li>
    </ul>
</div>
