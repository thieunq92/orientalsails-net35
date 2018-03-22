<%@ Page Language="C#" MasterPageFile="SailsMaster.Master" AutoEventWireup="true"
    Codebehind="RoomClassEdit.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.RoomClassEdit"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="System.Web.Extensions" Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <fieldset>
        <legend>
            <img alt="Room type" src="../Images/sails.gif" align="absMiddle" />
            <%= base.GetText("titleRoomClassEdit")%>
        </legend>
        <div class="settinglist">
            <div class="listbox">
                <asp:UpdatePanel ID="updatePanelList" runat="server">
                    <ContentTemplate>
                        <table cellspacing="0" cellpadding="0" style="width: 100%;">
                            <asp:Repeater ID="rptRoomClass" runat="server" OnItemDataBound="rptRoomClass_ItemDataBound"
                                OnItemCommand="rptRoomClass_ItemCommand">
                                <HeaderTemplate>
                                    <tr class="header">
                                        <th>
                                            <%#base.GetText("labelName") %>
                                        </th>
                                        <th>
                                            <%#base.GetText("labelDescription") %>
                                        </th>
                                        <th style="width: 40px;">
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr class="item">
                                        <td>
                                            <asp:Label ID="label_Name" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="label_Description" runat="server"></asp:Label>
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
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="wbox">
                <asp:UpdatePanel ID="updatePanelEdit" runat="server">
                    <ContentTemplate>
                        <div class="wbox_title">
                            <asp:Label ID="labelFormTitle" runat="server"><%= base.GetText("textNewRoomClass") %></asp:Label>
                        </div>
                        <div class="wbox_content">
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
                                        <asp:Label ID="labelDescription" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="textBoxDescription" runat="server" TextMode="MultiLine" Width="150"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td><%= base.GetText("textCruise") %></td>
                                    <td><asp:DropDownList ID="ddlCruises" runat="server"></asp:DropDownList></td>
                                </tr>
                            </table>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:Button ID="buttonSubmit" runat="server" ValidationGroup="date" CssClass="button"
                    OnClick="buttonSubmit_Click" />
                <asp:Button ID="buttonAdd" runat="server" CssClass="button" OnClick="buttonAdd_Click" />
            </div>
        </div>
    </fieldset>
</asp:Content>
