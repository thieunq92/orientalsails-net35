<%@ Page Title="" Language="C#" MasterPageFile="Popup.Master" AutoEventWireup="true"
    CodeBehind="RoomSelector.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.RoomSelector" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <script type="text/javascript" src="/scripts/jquery/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="/scripts/jqueryui/jquery-ui-1.10.3.min.js"></script>
    <script type="text/javascript" src="/scripts/mo/script.2.js"></script>
    <link href="/css/jqueryui/jquery-ui-1.10.3.css" media="screen" rel="stylesheet" type="text/css" />
    <link href="/css/mo/style.2.css" media="screen" rel="stylesheet" type="text/css" />
    <script>
        $(function () {
            $(document).tooltip();
        });
    </script>
    <fieldset>
        <div class="form-body">            
            <input type="text" id="postBack" value="1" style="display: none;" />
            <legend>Room selector</legend>
            <div>
                <asp:Panel runat="server" ID="panelCruise" style="width: 660px; float:left;">
                <div class="cruise-plan">
                    <div class="room-list">
                        <asp:Repeater runat="server" ID="rptRooms" OnItemDataBound="rptRoom_ItemDataBound">
                            <ItemTemplate>
                                <div id="divRoom" runat="server">
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
                <div style="clear: both;">
                </div>
            </asp:Panel>
            <div class="basicinfo" style="width: 270px; padding-left: 10px; float: left;">
                <asp:Button runat="server" ID="btnSave" OnClick="btnSave_Click" CssClass="button" />                
                <input type="button" class="button" value="Close" name="close" onclick="window.opener.location = window.opener.location; close();">
                <asp:Button runat="server" ID="buttonExport" OnClick="btnExport_Click" CssClass="button" Text="Room list"/>
                <asp:Repeater runat="server" ID="rptBookingRooms" OnItemDataBound="rptBookingRooms_ItemDataBound">
                    <ItemTemplate>
                        <div id="<%# DataBinder.Eval(Container.DataItem, "Id") %>" class="booking-room <%# DataBinder.Eval(Container.DataItem, "RoomTypeKey") %>" >
                            <asp:HiddenField runat="server" ID="hiddenId" Value='<%# DataBinder.Eval(Container.DataItem, "Id") %>' />
                            <input type="radio" name="bkroom" style="width: auto;">
                            <div class="div-type">
                                <span class="bookingid">
                                    <asp:HyperLink runat="server" ID="hplBookingId"></asp:HyperLink>
                                </span>
                                <span class="type" >
                                    <asp:Literal runat="server" ID="litType"></asp:Literal></span>
                            </div>
                            <div class="div-room">
                                <span class="room-name" rel="<%# DataBinder.Eval(Container.DataItem, "RoomTypeName") %>">
                                    <asp:Literal runat="server" ID="litRoomName"></asp:Literal></span></div>
                            <div class="div-lock">
                                <asp:CheckBox runat="server" ID="chkLockRoom" ToolTip="Lock this room"/>
                            </div>
                            <div class="div-name">
                                <span class="name">
                                    <asp:Literal runat="server" ID="litCustomer"></asp:Literal></span></div>
                            <asp:TextBox runat="server" ID="txtRoomNumber" CssClass="field hidden"></asp:TextBox>
                            <div style="clear: both;">
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <div style="clear: both;">
                </div>
            </div>
            <div style="clear:both;"></div>
            </div>
        </div>
    </fieldset>
</asp:Content>
