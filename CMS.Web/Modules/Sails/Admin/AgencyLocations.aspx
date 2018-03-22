<%@ Page Title="" Language="C#" MasterPageFile="SailsMaster.Master" AutoEventWireup="true"
    CodeBehind="AgencyLocations.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.AgencyLocations" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <fieldset>
        <legend>Locations </legend>
        <div class="settinglist">
            <div class="listbox">
                <ul>
                    <asp:Repeater ID="rptServices" runat="server" OnItemDataBound="rptServices_ItemDataBound"
                        OnItemCommand="rptServices_ItemCommand">
                        <ItemTemplate>
                            <li>
                                <asp:LinkButton ID='lbtEdit' runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Name")%>'
                                    CommandName="edit">
                                </asp:LinkButton>
                                <ul>
                                    <asp:Repeater runat="server" ID="rptChild" OnItemDataBound="rptServices_ItemDataBound"
                                        OnItemCommand="rptServices_ItemCommand">
                                        <ItemTemplate>
                                            <li>
                                                <asp:LinkButton ID='lbtEdit' runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Name")%>'
                                                    CommandName="edit">
                                                </asp:LinkButton></li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
                <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" CssClass="button" Text="New service" />
            </div>
            <div class="wbox">
                <div class="wbox_title">
                    <asp:Label ID="labelFormTitle" runat="server" Text="Title"></asp:Label>
                </div>
                <div class="wbox_content">
                    <div class="group">
                        <h4>
                            Location info
                        </h4>
                        <table>
                            <tr>
                                <td style="width: 100px;">
                                    Name
                                </td>
                                <td colspan="5">
                                    <asp:TextBox ID="txtServiceName" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvTypeOfService" runat="server" ControlToValidate="txtServiceName"
                                        Text="Type is required!" ValidationGroup="service"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Parent
                                </td>
                                <td colspan="5">
                                    <asp:DropDownList ID="ddlSuppliers" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" ValidationGroup="service"
                            CssClass="button" />
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click"
                            CssClass="button" />
                        <ajax:ConfirmButtonExtender ID="ConfirmButtonExtenderDelete" runat="server" TargetControlID="btnDelete"
                            ConfirmText='Are you sure want to delete?'>
                        </ajax:ConfirmButtonExtender>
                    </div>
                </div>
            </div>
        </div>
    </fieldset>
</asp:Content>
