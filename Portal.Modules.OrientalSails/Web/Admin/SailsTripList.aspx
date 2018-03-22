<%@ Page Language="C#" MasterPageFile="SailsMaster.Master" AutoEventWireup="true" CodeBehind="SailsTripList.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.SailsTripList" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="System.Web.Extensions" Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <fieldset>
        <legend>
            <img alt="Room" src="../Images/sails.gif" align="absMiddle" />
            <asp:Label ID="titleSailsTripList" runat="server"></asp:Label>
        </legend>
        <div class="data_table">
            <div class="data_grid">
                <table>
                    <asp:Repeater ID="rptTripList" runat="server" OnItemDataBound="rptTripList_ItemDataBound" OnItemCommand="rptTripList_ItemCommand">
                        <HeaderTemplate>
                            <tr class="header">
                                <th style="width: 200px;">
                                    <%#base.GetText("labelName") %>
                                </th>
                                <th style="width: 100px;">
                                    <%#base.GetText("labelNumberOfDay") %>
                                </th>
                                <th style="width: 100px;">
                                    <%#base.GetText("labelNumberOfOptions")%>
                                </th>
                                <th>
                                    <%#base.GetText("textPriceConfig")%>
                                </th>
                                <th style="width: 100px;">
                                    
                                </th>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class="item">
                                <td>
                                    <asp:HyperLink ID="hyperLink_Name" runat="server"></asp:HyperLink>                                
                                </td>
                                <td>
                                    <asp:Label ID="label_NumberOfDays" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="label_NumberofOption" runat="server"></asp:Label>
                                </td>      
                                <td>
                                    <table style="width:auto;">                                    
                                    <asp:Repeater ID="rptOptions" runat="server" OnItemDataBound="rptOptions_ItemDataBound">
                                        <ItemTemplate>
                                            <tr>
                                                <td><asp:Literal ID="litOption" runat="server"></asp:Literal></td>
                                                <asp:Repeater ID="rptCruises" runat="server" OnItemDataBound="rptCruises_ItemDataBound">
                                                    <ItemTemplate>
                                                        <td><asp:HyperLink ID="hplCruise" runat="server"></asp:HyperLink></td>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    </table>
                                    <!--<asp:DropDownList ID="ddlOption" runat="server"></asp:DropDownList>
                                    <asp:ImageButton ID="imageButtonPrice" runat="server" ToolTip="Price" AlternateText="Price" ImageAlign="AbsMiddle" CssClass="image_button16" CommandName="Price" ImageUrl="../Images/price.gif" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"Id") %>'/>-->
                                </td>                          
                                <td>
                                    <asp:HyperLink ID="hyperLinkEdit" runat="server">
                                        <asp:Image ID="imageEdit" runat="server" ImageAlign="AbsMiddle" AlternateText="Edit" CssClass="image_button16" ImageUrl="../Images/edit.gif" />
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
        </div>
    </fieldset>
</asp:Content>
