<%@ Page Language="C#" MasterPageFile="BookingOnline.Master" AutoEventWireup="true"
    CodeBehind="BookingTours1.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.BookingTours1" %>

<%@ Register Assembly="System.Web.Extensions" Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="CMS.ServerControls" Namespace="CMS.ServerControls" TagPrefix="svc" %>
<%@ Register Assembly="Portal.Modules.OrientalSails" Namespace="Portal.Modules.OrientalSails.Web.Controls"
    TagPrefix="orc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .cruisename {
            color: #333;
        }
    </style>
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
            $("#<%=txtDate.ClientID%>").datetimepicker({
                timepicker: false,
                format: "m/d/Y",
                minDate: new Date(),
                onChangeDateTime: function () {
                    $("#<%=txtDate.ClientID%>").blur();
                    __doPostBack('<%=txtDate.UniqueID%>', 'TextChanged');
                }
            });

            $("#<%=btnSave.ClientID%>").attr('value', 'Book Your Trip')
        });
    </script>
    <article id="booking" class="col-md-7 col-lg-8 col-md-push-2 content post-1847 page type-page status-publish hentry">
        <header class="header">
            <h1 class="title"><span class="line"></span>Booking</h1>
        </header>
        <section class="post-content clearfix">
            <h4>Trip Infomation</h4>
            <div class="row">
                <div class="col-xs-12 col-md-3">
                    <label>Departure Date<span style="color: red;">*</span></label>
                    <asp:TextBox ID="txtDate" runat="server" class="form-control hasDatepicker" placeholder="Depature" data-required="">
                    </asp:TextBox>
                    <span class="message"></span>
                </div>
                <div class="col-xs-12 col-md-9">
                    <label>Itinerary</label>
                    <asp:DropDownList ID="ddlTrips" runat="server" AutoPostBack="true" class="form-control" data-required="">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlOptions" runat="server" Width="80" Style="display: none;"
                        AutoPostBack="True">
                        <asp:ListItem>Option 1</asp:ListItem>
                        <asp:ListItem>Option 2</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <asp:UpdatePanel runat="server" ID="updatePanel1">
                        <contenttemplate>
                <em>Click vào tên tàu để bắt đầu nhập thông tin phòng</em>
                   <table id="TableCruises" class="table table-bordered">
                    <tr>
                        <th>Cruise</th>
                        <th>Avaiable Rooms</th>
                        <th></th>
                    </tr>
                        <asp:Repeater ID="rptCruises" runat="server" OnItemDataBound="rptCruises_ItemDataBound">
                                <ItemTemplate>
                                    <tr id="trCruise" runat="server">
                                        <td>
                                            <asp:LinkButton ID="lbtCruiseName" runat="server" OnClick="lbtCruiseName_Click" class="cruisename"></asp:LinkButton>
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
                </div>

                <asp:PlaceHolder runat="server" ID="plhPending" Visible="False"><span style="display: block; font-weight: bold; margin-bottom: 10px; margin-left: 0.5%; margin-top: 10px">Booking
                            pending</span>
                    <table>
                        <tr>
                            <th>Booking code
                            </th>
                            <th>Rooms
                            </th>
                            <th>Trip
                            </th>
                            <th>Partner
                            </th>
                            <th>Created by
                            </th>
                            <th>Sale in charge
                            </th>
                            <th>Pending until
                            </th>
                        </tr>
                        <asp:Repeater runat="server" ID="rptPendings" OnItemDataBound="rptPendings_ItemDataBound">
                            <itemtemplate>
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
                                    </itemtemplate>
                        </asp:Repeater>
                </asp:PlaceHolder>
                </table>
                        <asp:PlaceHolder ID="plhCruiseName" runat="server"><span style="display: inline-block; font-weight: bold; margin-left: 0.5%; margin-right: 35px; margin-top: 10px">You've
                            selected
                            <asp:Literal ID="litCurrentCruise" runat="server"></asp:Literal></span>
                            <asp:CheckBox runat="server" ID="chkCharter" Text=" Charter Booking"></asp:CheckBox>
                            <table style="margin-top: 10px">
                                <asp:Repeater ID="rptClass" runat="server" OnItemDataBound="rptClass_ItemDataBound">
                                    <itemtemplate>
                                        <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"Id") %>' />
                                        <asp:Repeater ID="rptTypes" runat="server" OnItemDataBound="rptTypes_ItemDataBound">
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="labelName" runat="server"></asp:Label><asp:HiddenField ID="HiddenField2"
                                                            runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"Id") %>' />
                                                    </td>
                                                    <td>
                                                        <div class="col-md-4">
                                                        <asp:DropDownList ID="ddlAdults" runat="server" class="form-control">
                                                        </asp:DropDownList>
                                                        </div>
                                                        <div class="col-md-4">
                                                        <asp:DropDownList ID="ddlChild" runat="server"  class="form-control">
                                                        </asp:DropDownList>
                                                        </div>
                                                        <div class="col-md-4">
                                                        <asp:DropDownList ID="ddlBaby" runat="server"  class="form-control">
                                                        </asp:DropDownList>
                                                        </div>
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
                                    </itemtemplate>
                                </asp:Repeater>
                            </table>
                        </asp:PlaceHolder>
                </contenttemplate>
                        <triggers>
                    <asp:AsyncPostBackTrigger ControlID="txtDate" EventName="TextChanged" />
                    <asp:AsyncPostBackTrigger ControlID="ddlTrips" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="ddlOptions" EventName="SelectedIndexChanged" />
                </triggers>
                </asp:UpdatePanel>
            </div>    <h4>YOUR INFOMATION</h4>
            <div class="row">
                <div class="col-xs-12 col-md-6">
                    <label>Your name<span style="color: red;">*</span></label>
                    <input class="name form-control" name="fullname" data-required="" type="text" />
                    <span class="message"></span>
                </div>
                <div class="col-xs-12 col-md-6">
                    <label>Your Email<span style="color: red;">*</span></label>
                    <input class="email form-control" name="email" data-required="" type="email" />
                    <span class="message"></span>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-12 col-md-12 col-lg-12">
                    <label>Pickup<span style="color: red;">*</span></label>
                    <input name="pickup" class="form-control" data-required="" type="text">
                    <span class="message"></span>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-12 col-md-12 col-lg-12">
                    <label>Your Request<span style="color: red;">*</span></label>
                    <textarea class="request form-control" rows="3" name="request" data-required=""></textarea>
                    <span class="message"></span>
                </div>
            </div>
            <div class="text-center">
                <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" class="btn view-detail text-center text-uppercase" Text="Book Your Trip" />
            </div>
        </section>
    </article>
    <div>
        <asp:LinkButton ID="lbtCheckAvaiable" OnClick="lbtCheckAvaiable_Click" runat="server"
            Visible="false">
        </asp:LinkButton>
        <orc:AgencySelector ID="agencySelector" runat="server" Visible="false" />
        <asp:TextBox ID="txtAgencyCode" runat="server" Width="75" Visible="false"></asp:TextBox>
        <ajax:TextBoxWatermarkExtender ID="waterAgency" runat="server" TargetControlID="txtAgencyCode"
            WatermarkText="TA Code">
        </ajax:TextBoxWatermarkExtender>

        <asp:Repeater ID="rptExtraServices" runat="server" OnItemDataBound="rptExtraServices_ItemDataBound" Visible="false">
            <itemtemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "Name") %>
                                    <asp:CheckBox ID="chkService" runat="server" CssClass="checkbox" />
                                    <asp:HiddenField ID="hiddenId" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "Id") %>' />
                                    <asp:HiddenField ID="hiddenValue" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "Price") %>' />
                                </itemtemplate>
        </asp:Repeater>
    </div>


</asp:Content>
