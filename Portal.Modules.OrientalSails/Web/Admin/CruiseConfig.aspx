<%@ Page Language="C#" MasterPageFile="SailsMaster.Master" AutoEventWireup="true"
    Codebehind="CruiseConfig.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.CruiseConfig"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <fieldset>
        <legend>
            <img alt="Room" src="../Images/sails.gif" align="absMiddle" />
            Hai phong cruise price config </legend>
        <div class="advancedinfo">
            <table>
                <tr>
                    <th>
                        Valid from
                    </th>
                    <th>
                        Valid to
                    </th>
                    <th>
                        Cruise
                    </th>
                    <th>
                    </th>
                </tr>
                <asp:Repeater ID="rptCruiseTables" runat="server" OnItemDataBound="rptCruiseTables_ItemDataBound"
                    OnItemCommand="rptCruiseTables_ItemCommand">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:Literal ID="litValidFrom" runat="server"></asp:Literal></td>
                            <td>
                                <asp:Literal ID="litValidTo" runat="server"></asp:Literal></td>
                            <td>
                                <asp:Literal ID="litCruise" runat="server"></asp:Literal>
                            </td>
                            <td>
                                <asp:HyperLink ID="hyperLinkEdit" runat="server">
                                    <asp:Image ID="imageEdit" runat="server" ImageAlign="AbsMiddle" AlternateText="Edit"
                                        CssClass="image_button16" ImageUrl="../Images/edit.gif" />
                                </asp:HyperLink>
                                <asp:ImageButton runat="server" ID="imageButtonDelete" ToolTip='Delete' ImageUrl="../Images/delete_file.gif"
                                    AlternateText='Delete' ImageAlign="AbsMiddle" CssClass="image_button16" CommandName="Delete"
                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem,"Id") %>' />
                                <ajax:ConfirmButtonExtender ID="ConfirmButtonExtenderDelete" runat="server" TargetControlID="imageButtonDelete"
                                    ConfirmText='<%# base.GetText("messageConfirmDelete") %>'>
                                </ajax:ConfirmButtonExtender>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
        <input type="button" value="New table" id="inputNew" runat="server" class="button" />
        <div class="data_table">
            <table>
                <tr>
                    <td>
                        Valid from</td>
                    <td>
                        <asp:TextBox ID="txtValidFrom" runat="server"></asp:TextBox>
                        <ajax:CalendarExtender ID="calendarValidFrom" runat="server" Format="dd/MM/yyyy"
                            TargetControlID="txtValidFrom">
                        </ajax:CalendarExtender>
                    </td>
                    <td>
                        Valid to</td>
                    <td>
                        <asp:TextBox ID="txtValidTo" runat="server"></asp:TextBox>
                        <ajax:CalendarExtender ID="calendarValidTo" runat="server" Format="dd/MM/yyyy" TargetControlID="txtValidTo">
                        </ajax:CalendarExtender>
                    </td>
                    <td>
                        Cruise
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCruises" runat="server"></asp:DropDownList>
                    </td>
                </tr>
            </table>
            <div class="data_grid">
                <table>
                    <tr>
                        <td colspan="3">
                        </td>
                    </tr>
                    <asp:Repeater ID="rptCruiseExpenses" runat="server" OnItemDataBound="rptCruiseExpenses_ItemDataBound">
                        <HeaderTemplate>
                            <tr>
                                <th>
                                    Customer from</th>
                                <th>
                                    Customer to</th>
                                <th>
                                    Cost</th>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:HiddenField ID="hiddenId" runat="server" />
                                    <asp:TextBox ID="txtFrom" runat="server"></asp:TextBox></td>
                                <td>
                                    <asp:TextBox ID="txtTo" runat="server"></asp:TextBox></td>
                                <td>
                                    <asp:TextBox ID="txtCost" runat="server"></asp:TextBox></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
                <asp:Button ID="btnAddRow" runat="server" OnClick="btnAddRow_Click" CssClass="button"
                    Text="Add row" />
            </div>
        </div>
        <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" CssClass="button" />
    </fieldset>
</asp:Content>
