<%@ Page Language="C#" MasterPageFile="SailsMaster.Master" AutoEventWireup="true"
    Codebehind="Sales.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.Sales"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Src="/Admin/Controls/UserSelector.ascx" TagPrefix="asp" TagName="UserSelector" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <div style="display: none;">
        <div class="basicinfo">
            <table>
                <tr>
                    <td>
                        Select an account to add as sale</td>
                    <td>
                        <asp:UserSelector ID="userSelector" runat="server">
                        </asp:UserSelector></td>
                    <td>
                        <asp:Button ID="btnAdd" runat="server" CssClass="button" Text="Add" OnClick="btnAdd_Click" /></td>
                </tr>
            </table>
        </div>
        <div class="data_table">
            <div class="data_grid">
                <table>
                    <tr>
                        <th>
                            Username</th>
                        <th>
                            Name</th>
                        <th>
                        </th>
                    </tr>
                    <asp:Repeater ID="rptSales" runat="server" OnItemDataBound="rptSales_ItemDataBound"
                        OnItemCommand="rptSales_ItemCommand">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:Literal ID="litUsername" runat="server"></asp:Literal></td>
                                <td>
                                    <asp:Literal ID="litName" runat="server"></asp:Literal></td>
                                <td>
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
        </div>
    </div>
    <div class="basicinfo">
        <table>
            <tr>
                <td>
                    Sale role
                </td>
                <td>
                    <asp:DropDownList ID="ddlSaleRoles" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Supplier role
                </td>
                <td>
                    <asp:DropDownList ID="ddlSupplierRoles" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Guide role
                </td>
                <td>
                    <asp:DropDownList ID="ddlGuideRoles" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <%--            <tr>
                <td>
                    Supplier role
                </td>
                <td>
                    <asp:DropDownList ID="DropDownList2" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>--%>
        </table>
    </div>
    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" CssClass="button" />
</asp:Content>
