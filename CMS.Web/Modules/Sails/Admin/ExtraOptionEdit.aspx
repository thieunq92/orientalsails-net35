<%@ Page Language="C#" MasterPageFile="SailsMaster.Master" AutoEventWireup="true" CodeBehind="ExtraOptionEdit.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.ExtraOptionEdit" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="System.Web.Extensions" Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
<fieldset>
        <legend>
            <img alt="Option" src="../Images/sails.gif" align="absMiddle" />
            <asp:Label ID="titleExtraOptionEdit" runat="server"></asp:Label>
        </legend>
        <div style="width: 600px; float: right;">
            <div class="settinglist">
                <asp:UpdatePanel ID="updatePanelEdit" runat="server">
                    <ContentTemplate>
                        <div class="basicinfo">
                            <asp:Label ID="labelStatus" runat="server"></asp:Label>
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="labelName" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="textBoxName" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="labelPrice" runat="server">Price</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="textBoxPrice" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td><%= base.GetText("textIncludedInRoomPrice") %></td>
                                    <td><asp:CheckBox ID="chkIncluded" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td><%= base.GetText("textTargetApply") %></td>
                                    <td>
                                    <asp:DropDownList ID="ddlTargets" runat="server">
                                        <asp:ListItem>All</asp:ListItem>
                                        <asp:ListItem>Customer</asp:ListItem>
                                        <asp:ListItem>Booking</asp:ListItem>
                                    </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="labelDescription" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="textBoxDescription" runat="server" TextMode="MultiLine" Width="150"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <asp:Label ID="labelNote" runat="server"></asp:Label>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <asp:Button ID="buttonSubmit" runat="server" ValidationGroup="date" CssClass="button"
                OnClick="buttonSubmit_Click" />
            <asp:Button ID="buttonAdd" runat="server" CssClass="button" OnClick="buttonAdd_Click" />
        </div>
        <div style="width: 320px; float: left;">
            <div class="data_table">
                <asp:UpdatePanel ID="updatePanelList" runat="server">
                    <ContentTemplate>
                        <div class="data_grid">
                            <table cellspacing="0" cellpadding="0">
                                <asp:Repeater ID="rptExtraOption" runat="server" OnItemDataBound="rptExtraOption_ItemDataBound"
                                    OnItemCommand="rptExtraOption_ItemCommand">
                                    <HeaderTemplate>
                                        <tr class="header">
                                            <th style="width: 150px;">
                                                <%#base.GetText("labelName") %>
                                            </th>
                                            <th style="width: 80px;">
                                                <%#base.GetText("labelPrice") %>
                                            </th>
                                            <th>
                                            </th>
                                        </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr class="item">
                                            <td>
                                                <asp:Label ID="label_Name" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="label_Price" runat="server"></asp:Label>
                                            </td>
                                            
                                            <td>
                                                <asp:ImageButton runat="server" ID="imageButtonEdit" ToolTip='Edit' ImageUrl="../Images/edit.gif"
                                                    AlternateText='Edit' ImageAlign="AbsMiddle" CssClass="image_button16" CommandName="Edit"
                                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem,"Id") %>' />
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
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </fieldset>
</asp:Content>

