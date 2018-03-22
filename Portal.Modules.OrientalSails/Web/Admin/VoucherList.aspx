<%@ Page Title="" Language="C#" MasterPageFile="SailsMaster.Master" AutoEventWireup="true"
    CodeBehind="VoucherList.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.VoucherList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="CMS.ServerControls" Namespace="CMS.ServerControls" TagPrefix="svc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <fieldset>
        <legend>
            <img alt="Room" src="../Images/sails.gif" align="absMiddle" />
            Voucher list </legend>
    </fieldset>
    <div class="settinglist">
        <div class="basicinfo">
            <table>
            </table>
        </div>
        <div class="data_table">
            <div class="data_grid">
                <table cellspacing="1">
                    <tr>
                        <th>
                            Name
                        </th>
                        <th>
                            Agency
                        </th>
                        <th>
                            Cruise
                        </th>
                        <th>
                            Apply for
                        </th>
                        <th>
                            Trip
                        </th>
                        <th>
                            Total
                        </th>
                        <th>
                            Remain
                        </th>
                        <th>
                            Valid until
                        </th>
                        <th>
                            Note
                        </th>
                        <th>
                            Contract
                        </th>
                        <th>
                        </th>
                    </tr>
                    <asp:Repeater ID="rptVoucherList" runat="server" OnItemDataBound="rptVoucherList_ItemDataBound">
                        <ItemTemplate>
                            <tr <%#DateTime.Parse(Eval("ValidUntil").ToString()) < DateTime.Now ? "style='background-color:red'":""%>>
                                <td>
                                    <asp:HyperLink runat="server" ID="hplName"></asp:HyperLink>
                                </td>
                                <td>
                                    <asp:Literal runat="server" ID="litAgency"></asp:Literal>
                                </td>
                                <td>
                                    <%#Eval("Cruise.Name") %>
                                </td>
                                <td>
                                    <%#Eval("NumberOfPerson")%>
                                </td>
                                <td>
                                    <%#Eval("Trip.TripCode") %>
                                </td>
                                <td>
                                    <asp:Literal runat="server" ID="litTotal"></asp:Literal>
                                </td>
                                <td>
                                    <asp:Literal runat="server" ID="litRemain"></asp:Literal>
                                    <asp:HyperLink runat="server" ID="hplBookings" Visible="False">(list bookings)</asp:HyperLink>
                                </td>
                                <td>
                                    <asp:Literal runat="server" ID="litValidUntil"></asp:Literal>
                                </td>
                                <td>
                                    <%#Eval("Note")%>
                                </td>
                                <td>
                                    <asp:HyperLink runat="server" ID="hplContract"></asp:HyperLink>
                                </td>
                                <td>
                                    <asp:HyperLink ID="hplEdit" runat="server">
                                        <asp:Image ID="imageView" runat="server" ImageAlign="AbsMiddle" AlternateText="View"
                                            CssClass="image_button16" ImageUrl="../Images/edit.gif" />
                                    </asp:HyperLink>
                                    <asp:ImageButton runat="server" ID="imageButtonDelete" ToolTip='Delete' ImageUrl="../Images/delete_file.gif"
                                        AlternateText='Delete' ImageAlign="AbsMiddle" CssClass="image_button16" CommandName="Delete"
                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem,"Id") %>' Visible="true"
                                        OnClick="btnDelete_Click" /><ajax:ConfirmButtonExtender ID="ConfirmButtonExtenderDelete"
                                            runat="server" TargetControlID="imageButtonDelete" ConfirmText='<%# base.GetText("messageConfirmDelete") %>'>
                                        </ajax:ConfirmButtonExtender>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </div>
        <div class="pager">
            <svc:Pager ID="pagerVoucher" runat="server" HideWhenOnePage="true" ControlToPage="rptVoucherList"
                PagerLinkMode="HyperLinkQueryString" PageSize="20" />
        </div>
    </div>
</asp:Content>
