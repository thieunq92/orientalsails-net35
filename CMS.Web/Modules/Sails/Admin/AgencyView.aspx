<%@ Page Language="C#" MasterPageFile="SailsMaster.Master" AutoEventWireup="true"
    CodeBehind="AgencyView.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.AgencyView"
    Title="Agency View Page - Oriental Sails Management Office" %>

<%@ Register Assembly="CMS.ServerControls" Namespace="CMS.ServerControls" TagPrefix="svc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <fieldset>
        <svc:Popup ID="popupManager" runat="server">
        </svc:Popup>
        <div class="settinglist">
            <h3>
                <asp:Literal runat="server" ID="litName1"></asp:Literal></h3>
            <div class="basicinfo" style="width: 100%; border: none; margin-top: 0; margin-bottom: 0; padding-top: 0; padding-bottom: 0">
                <table class="striped">
                    <tr>
                        <td width="15%">
                            <b>Name</b>
                        </td>
                        <td>
                            <asp:Literal runat="server" ID="litName"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>Role</b>
                        </td>
                        <td>
                            <asp:Literal runat="server" ID="litRole"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>Address</b>
                        </td>
                        <td colspan="5">
                            <asp:Literal runat="server" ID="litAddress"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>Phone</b>
                        </td>
                        <td>
                            <asp:Literal runat="server" ID="litPhone"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>Fax</b>
                        </td>
                        <td>
                            <asp:Literal runat="server" ID="litFax"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>Email</b>
                        </td>
                        <td>
                            <asp:HyperLink runat="server" ID="hplEmail"></asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>Sale in charge</b>
                        </td>
                        <td>
                            <asp:Literal runat="server" ID="litSale"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>Sale phone</b>
                        </td>
                        <td>
                            <asp:Literal runat="server" ID="litSalePhone"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>Tax code</b>
                        </td>
                        <td>
                            <asp:Literal runat="server" ID="litTax"></asp:Literal>
                        </td>
                        <td colspan="2"></td>
                    </tr>
                    <tr>
                        <td>
                            <b>Location</b>
                        </td>
                        <td>
                            <asp:Literal runat="server" ID="litLocation"></asp:Literal>
                        </td>
                        <td colspan="4"></td>
                    </tr>
                    <tr>
                        <td>
                            <b>Accountant</b>
                        </td>
                        <td>
                            <asp:Literal runat="server" ID="litAccountant"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>Payment</b>
                        </td>
                        <td>
                            <asp:Literal runat="server" ID="litPayment"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>Other info</b>
                        </td>
                        <td colspan="5">
                            <asp:Literal runat="server" ID="litNote"></asp:Literal>
                        </td>
                    </tr>
                </table>
                <br />
                <asp:HyperLink runat="server" ID="hplEditAgency" ToolTip="Edit this agency" CssClass="image_text_in_button"
                    Style="background-image: url('https://cdn1.iconfinder.com/data/icons/CrystalClear/128x128/actions/edit.png')">Edit this agency</asp:HyperLink>
                <asp:HyperLink runat="server" ID="hplBookingList" ToolTip="Booking by this agency"
                    CssClass="image_text_in_button" Style="display:none; background-image:url(https://cdn0.iconfinder.com/data/icons/ie_Shine/128/shine_19.png)">Booking by this agency</asp:HyperLink>
                <a id="ctl00_AdminContent_hplBookingList" title="Booking by this agency" class="image_text_in_button" href="BookingList.aspx?NodeId=1&amp;SectionId=15&amp;ai=<%= Request.QueryString["AgencyId"] %>" style="background-image: url(https://cdn0.iconfinder.com/data/icons/ie_Shine/128/shine_19.png)">Booking by this agency</a>
                <div id="disableInform" style="display: none">
                    You don't have permission to use this function. If you want to use this function please contact administrator
                </div>
                <asp:HyperLink runat="server" ID="hplReceivable" ToolTip="Receivables(3 months)"
                    CssClass="image_text_in_button" Style="display: none; background-image: url('https://cdn0.iconfinder.com/data/icons/free-business-desktop-icons/128/Business.png')">Receivables (last 3 months)</asp:HyperLink>
                <a title="Receivables(3 months)" class="image_text_in_button" href="PaymentReport.aspx?NodeId=1&amp;SectionId=15&amp;ai=<%= Request.QueryString["AgencyId"] %>&amp;f=<%= DateTime.Today.AddMonths(-3).ToString("dd/MM/yyyy")%>&amp;t=<%=DateTime.Today.ToString("dd/MM/yyyy")%>" style="background-image: url('https://cdn0.iconfinder.com/data/icons/free-business-desktop-icons/128/Business.png')">Receivables (last 3 months)</a>
            </div>
            <div class="basicinfo" style="width: 100%; border: none;">
                <span style="font-weight: bold">CONTACTS</span>
                <br />
                <br />
                <asp:PlaceHolder runat="server" ID="plhContacts">
                    <table border="1" cellspacing="0" cellpadding="2" class="table_text_center">
                        <tr>
                            <th>Name
                            </th>
                            <th>Position
                            </th>
                            <th>Booker
                            </th>
                            <th>Phone
                            </th>
                            <th>Email
                            </th>
                            <th>Birthday
                            </th>
                            <th>Note
                            </th>
                            <th width="6%">Add meeting
                            </th>
                            <th width="3%">Edit
                            </th>
                            <th width="4%">Delete
                            </th>
                        </tr>
                        <asp:Repeater runat="server" ID="rptContacts" OnItemDataBound="rptContacts_ItemDataBound">
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <asp:Literal runat="server" ID="ltrName"></asp:Literal>
                                    </td>
                                    <td>
                                        <asp:Literal runat="server" ID="litPosition"></asp:Literal>
                                    </td>
                                    <td>
                                        <asp:Literal runat="server" ID="litBooker"></asp:Literal>
                                    </td>
                                    <td>
                                        <asp:Literal runat="server" ID="litPhone"></asp:Literal>
                                    </td>
                                    <td>
                                        <asp:HyperLink runat="server" ID="hplEmail"></asp:HyperLink>
                                    </td>
                                    <td>
                                        <%# ((DateTime?)Eval("Birthday"))==null?"" : ((DateTime?)Eval("Birthday")).Value.ToString("dd/MM/yyyy")%>
                                    </td>
                                    <td>
                                        <%# DataBinder.Eval(Container.DataItem, "Note") %>
                                    </td>
                                    <td>
                                        <asp:HyperLink runat="server" ID="hplCreateMeeting" ToolTip="Add a meeting with this contact"><img src="https://cdn1.iconfinder.com/data/icons/IconsLandVistaPeopleIconsDemo/128/Group_Meeting_Light.png" width="17px" height="17px" /></asp:HyperLink>
                                    </td>
                                    <td>
                                        <asp:HyperLink runat="server" ID="hplName" ToolTip="Edit this contact"><img src="https://cdn1.iconfinder.com/data/icons/CrystalClear/128x128/actions/edit.png" width="17px" height="17px" /></asp:HyperLink>
                                    </td>
                                    <td>
                                        <asp:LinkButton runat="server" ID="lbtDelete" OnClick="lbtDelete_Click" CommandArgument='<%#Eval("Id")%>'
                                            OnClientClick="return confirm('Are you sure?')" ToolTip="Delete this contact"><img src="https://cdn3.iconfinder.com/data/icons/softwaredemo/PNG/128x128/DeleteRed.png" width="17px" height="17px" /></asp:LinkButton>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                    <br />
                    <br />
                    <asp:HyperLink runat="server" ID="hplAddContact" CssClass="image_text_in_button"
                        ToolTip="Add a new contact" Style="background-image: url('https://cdn2.iconfinder.com/data/icons/humano2/128x128/actions/user-group-new.png')">New contact</asp:HyperLink>
                </asp:PlaceHolder>
                <asp:Label runat="server" ID="lblContacts" Text="You don't have permission to use this function. If you want to use this function please contact administrator"
                    Visible="False" />
            </div>
            <div class="basicinfo" style="width: 100%; border: none">
                <span style="font-weight: bold">RECENT ACTIVITIES</span>
                <br />
                <br />
                <asp:PlaceHolder runat="server" ID="plhActivities">
                    <table border="1" cellspacing="0" cellpadding="2" class="table_text_center">
                        <asp:Repeater runat="server" ID="rptActivities" OnItemDataBound="rptActivities_ItemDataBound">
                            <HeaderTemplate>
                                <tr>
                                    <th width="7%">Date meeting
                                    </th>
                                    <th width="10%">Sale
                                    </th>
                                    <th width="13%">Meeting with
                                    </th>
                                    <th width="13%">Position
                                    </th>
                                    <th>Note
                                    </th>
                                    <th width="3%">Edit
                                    </th>
                                    <th width="4%">Delete
                                    </th>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <asp:Literal runat="server" ID="ltrDateMeeting" />
                                    </td>
                                    <td>
                                        <asp:Literal runat="server" ID="ltrSale" />
                                    </td>
                                    <td>
                                        <asp:Literal runat="server" ID="ltrName" />
                                    </td>
                                    <td>
                                        <asp:Literal runat="server" ID="ltrPosition" />
                                    </td>
                                    <td>
                                        <asp:Literal runat="server" ID="ltrNote" />
                                    </td>
                                    <td>
                                        <asp:LinkButton runat="server" ID="lbtEditActivity" ToolTip="Edit this meeting"><img src="https://cdn1.iconfinder.com/data/icons/CrystalClear/128x128/actions/edit.png" width="17px" height="17px" /></asp:LinkButton>
                                    </td>
                                    <td>
                                        <asp:LinkButton runat="server" ID="lbtDeleteActivity" OnClick="lbtDeleteActivity_Click"
                                            CommandArgument='<%#Eval("Id")%>' OnClientClick="return confirm('Are you sure?')"
                                            ToolTip="Delete this meeting"><img src="https://cdn3.iconfinder.com/data/icons/softwaredemo/PNG/128x128/DeleteRed.png" width="17px" height="17px" /></asp:LinkButton>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </asp:PlaceHolder>
                <asp:Label runat="server" ID="lblActivities" Text="You don't have permission to use this function. If you want to use this function please contact administrator"
                    Visible="False" />
            </div>
            <div class="basicinfo" style="width: 100%; border: none">
                <span style="font-weight: bold">CONTRACTS</span>
                <br />
                <br />
                <asp:PlaceHolder runat="server" ID="plhContracts">
                    <table border="1" cellspacing="0" cellpadding="2" class="table_text_center">
                        <tr>
                            <th width="15%">Created Date</th>
                            <th>Name
                            </th>
                            <th width="15%">Expired on
                            </th>
                            <th width="5%">Received</th>
                            <th width="20%">Download
                            </th>
                            <th width="3%">Edit
                            </th>
                        </tr>
                        <asp:Repeater runat="server" ID="rptContracts" OnItemDataBound="rptContracts_ItemDataBound">
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <asp:Literal runat="server" ID="litCreatedDate" />
                                    </td>
                                    <td>
                                        <asp:Literal runat="server" ID="litName"></asp:Literal>
                                    </td>
                                    <td>
                                        <asp:Literal runat="server" ID="litExpired"></asp:Literal>
                                    </td>
                                    <td>
                                        <asp:Literal runat="server" ID="litReceived"></asp:Literal>
                                    </td>
                                    <td>
                                        <asp:HyperLink runat="server" ID="hplDownload" ToolTip="Download this contract file"><img src="https://cdn2.iconfinder.com/data/icons/freecns-cumulus/16/519672-178_Download-128.png" width="17px" height="17px" /></asp:HyperLink>
                                    </td>
                                    <td>
                                        <asp:HyperLink runat="server" ID="hplEdit" ToolTip="Edit this contract"><img src="https://cdn1.iconfinder.com/data/icons/CrystalClear/128x128/actions/edit.png" width="17px" height="17px" /></asp:HyperLink>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                    <br />
                    <br />
                    <asp:HyperLink runat="server" ID="hplAddContract" CssClass="image_text_in_button"
                        ToolTip="Add a new contract" Style="background-image: url('https://cdn1.iconfinder.com/data/icons/prettyoffice9/128/new-file.png')">New contract</asp:HyperLink>
                </asp:PlaceHolder>
                <asp:Label runat="server" ID="lblContracts" Text="You don't have permission to use this function. If you want to use this function please contact administrator"
                    Visible="False" />
            </div>
        </div>
    </fieldset>
</asp:Content>
