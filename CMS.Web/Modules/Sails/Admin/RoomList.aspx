<%@ Page Language="C#" MasterPageFile="SailsMaster.Master" AutoEventWireup="true" CodeBehind="RoomList.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.RoomList" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="System.Web.Extensions" Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <fieldset>
        <legend>
            <img alt="Room" src="../Images/sails.gif" align="absMiddle" />
            <asp:Label ID="titleRoomList" runat="server"></asp:Label>
        </legend>
        <div class="settinglist">        
            <div class="basicinfo">
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="labelStartDate" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="textBoxStartDate" runat="server"></asp:TextBox>
                            <asp:Image ID="imagecalenderStartDate" ImageUrl="/Images/calendar.gif" runat="server"
                            CssClass="image_button16" ImageAlign="AbsMiddle" />
                            <asp:RegularExpressionValidator ValidationGroup="date" ID="revStartDate" runat="server"
                            ControlToValidate="textBoxStartDate" EnableClientScript="false" ErrorMessage="Date is not valid"
                            ValidationExpression="(?n:^(?=\d)((?<day>31(?!(.0?[2469]|11))|30(?!.0?2)|29(?(.0?2)(?=.{3,4}(1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|(16|[2468][048]|[3579][26])00))|0?[1-9]|1\d|2[0-8])(?<sep>[/.-])(?<month>0?[1-9]|1[012])\2(?<year>(1[6-9]|[2-9]\d)\d{2})(?:(?=\x20\d)\x20|$))?(?<time>((0?[1-9]|1[012])(:[0-5]\d){0,2}(?i:\ [AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$)"></asp:RegularExpressionValidator>
                            <ajax:CalendarExtender ID="calenderStartDate" runat="server" Format="dd/MM/yyyy"
                            TargetControlID="textBoxStartDate" PopupButtonID="imagecalenderStartDate">
                            </ajax:CalendarExtender>
                        </td>
                        <td>
                            
                        </td>
                        <td>
                            
                        </td>
                    </tr>
                </table>
            </div>
            <asp:Button ID="buttonSearch" runat="server" OnClick="buttonSearch_Click"  ValidationGroup="date" CssClass="button" />
        </div>
        <div class="data_table">
            <div class="data_grid">
                <table>
                    <asp:Repeater ID="rptRoom" runat="server" OnItemDataBound="rptRoom_ItemDataBound" OnItemCommand="rptRoom_ItemCommand">
                    <HeaderTemplate>
                        <tr class="header">
                            <th>
                                No
                            </th>
                            <th style="width: 200px;">
                                <%#base.GetText("labelName") %>
                            </th>
                            <th style="width: 100px;">
                                <%#base.GetText("labelRoomTypex") %>
                            </th>
                            <th style="width: 100px;">
                                <%#base.GetText("labelRoomClass") %>
                            </th>
                            <th>
                                <%#base.GetText("textCruise") %>
                            </th>
                            <th style="width:170px;">
                            </th>
                            <th>
                                <%#base.GetText("labelAction") %>
                            </th>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr class="item">
                                <td><%#Container.ItemIndex + 1%></td>
                                <td>
                                    <asp:HyperLink ID="hyperLink_Name" runat="server"></asp:HyperLink>                                
                                </td>
                                <td>
                                    <asp:Label ID="label_RoomType" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="label_RoomClass" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="labelCruise" runat="server"></asp:Label>
                                </td>
                                <td id="tdAvailable" runat="server">
                                    
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
