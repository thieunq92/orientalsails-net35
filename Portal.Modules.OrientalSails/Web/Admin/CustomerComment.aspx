<%@ Page Language="C#" MasterPageFile="SailsMaster.Master" AutoEventWireup="true"
    Codebehind="CustomerComment.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.CustomerComment"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <div class="basicinfo">
        <table>
            <tr>
                <td>
                    Tàu</td>
                <td>
                    <asp:DropDownList ID="ddlCruises" runat="server">
                    </asp:DropDownList></td>
                <td>
                    Ngày</td>
                <td>
                    <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
                    <ajax:CalendarExtender ID="calendarDate" runat="server" TargetControlID="txtDate"
                        Format="dd/MM/yyyy">
                    </ajax:CalendarExtender>
                </td>
            </tr>
        </table>
    </div>
    <asp:Button ID="btnComment" runat="server" Text="Display" CssClass="button" OnClick="btnComment_Click" />
    <asp:Button ID="btnReport" runat="server" Text="Report" CssClass="button" OnClick="btnReport_Click" />
    <asp:PlaceHolder ID="plhComment" runat="server">
        <div class="basicinfo">
            <table>
                <asp:Repeater ID="rptRooms" runat="server" OnItemDataBound="rptRooms_ItemDataBound">
                    <HeaderTemplate>
                        <tr>
                            <th>
                            </th>
                            <th>
                                Đoàn khách/phòng</th>
                            <th>
                                Tour(E,G,A,P)</th>
                            <th>
                                Phòng ngủ(E,G,A,P)</th>
                            <th>
                                Đồ ăn(E,G,A,P)</th>
                            <th>
                                Nhân viên(E,G,A,P)</th>
                            <th>
                                Hướng dẫn(E,G,A,P)</th>
                            <th>
                                Ý kiến KH</th>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:HiddenField ID="hiddenId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "Id") %>' />
                            </td>
                            <td>
                                <asp:Literal ID="litRoom" runat="server"></asp:Literal></td>
                            <td>
                                <asp:TextBox ID="txtTourComment" runat="server" Width="150"></asp:TextBox></td>
                            <td>
                                <asp:TextBox ID="txtRoomComment" runat="server" Width="150"></asp:TextBox></td>
                            <td>
                                <asp:TextBox ID="txtFoodComment" runat="server" Width="150"></asp:TextBox></td>
                            <td>
                                <asp:TextBox ID="txtStaffComment" runat="server" Width="150"></asp:TextBox></td>
                            <td>
                                <asp:TextBox ID="txtGuideComment" runat="server" Width="150"></asp:TextBox></td>
                            <td>
                                <asp:TextBox ID="txtCustomerIdea" runat="server" Width="150" TextMode="MultiLine"></asp:TextBox></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="button" />
    </asp:PlaceHolder>
</asp:Content>
