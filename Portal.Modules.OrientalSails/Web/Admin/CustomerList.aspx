<%@ Page Language="C#" MasterPageFile="SailsMaster.Master" AutoEventWireup="true"
    Codebehind="CustomerList.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.CustomerList"
    Title="Untitled Page" %>

<%@ Register Assembly="CMS.ServerControls" Namespace="CMS.ServerControls" TagPrefix="svc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <fieldset>
        <legend>
            <img alt="Room" src="../Images/sails.gif" align="absMiddle" />
            <%= base.GetText("textCustomerList") %>
        </legend>
        <div class="search_panel">
            <table>
                <tr>
                    <td style="width: 13%">
                        <%= base.GetText("textFullName") %>
                    </td>
                    <td style="width: 20%">
                        <asp:TextBox ID="txtFullName" runat="server"></asp:TextBox></td>
                    <td style="width: 13%">
                        <%= base.GetText("textBirthDate")%>
                    </td>
                    <td style="width: 20%">
                        <asp:TextBox ID="txtBirthdate" runat="server"></asp:TextBox>
                        <ajax:CalendarExtender ID="calendarBirthdate" runat="server" TargetControlID="txtBirthDate"
                            Format="dd/MM/yyyy">
                        </ajax:CalendarExtender>
                    </td>
                    <td style="width: 14%">
                        <%= base.GetText("textPassport") %>
                    </td>
                    <td style="width: 20%">
                        <asp:TextBox ID="txtPassport" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <%= base.GetText("labelBookingId") %>
                    </td>
                    <td>
                        <asp:TextBox ID="txtBookingId" runat="server"></asp:TextBox></td>
                    <td>
                        <%= base.GetText("textGender")%>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlGender" runat="server">
                            <asp:ListItem>-- Gender --</asp:ListItem>
                            <asp:ListItem>Male</asp:ListItem>
                            <asp:ListItem>Female</asp:ListItem>
                        </asp:DropDownList></td>
                    <td>
                        <%= base.GetText("textNationality")%>
                    </td>
                    <td>
                        <asp:TextBox ID="txtNationality" runat="server"></asp:TextBox></td>
                </tr>
            </table>
        </div>
        <asp:Button ID="btnSearch" runat="server" CssClass="button" OnClick="btnSearch_Click" />
        <input class="button" type="button" id="btnProvisionalDetail" runat="server" visible="false"/>
        <div class="data_table">
            <div class="pager">
                <svc:Pager ID="pagerCustomers" runat="server" ControlToPage="rptCustomers" OnPageChanged="pagerCustomers_PageChanged"
                    PageSize="20" />
            </div>
            <div class="data_grid">
                <table>
                    <tr>
                        <th>
                            Name</th>
                        <th>
                            Birthdate</th>
                        <th>
                            Gender</th>
                        <th>
                            Nationality</th>
                        <th>
                            Passport</th>
                    </tr>
                    <asp:Repeater ID="rptCustomers" runat="server" OnItemDataBound="rptCustomers_ItemDataBound">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <%# DataBinder.Eval(Container.DataItem, "FullName") %>
                                </td>
                                <td>
                                    <asp:Literal ID="litBirthdate" runat="server"></asp:Literal>
                                </td>
                                <td>
                                    <asp:Literal ID="litGender" runat="server"></asp:Literal>
                                </td>
                                <td>
                                    <asp:Literal ID="litNationality" runat="server"></asp:Literal>
                                </td>
                                <td>
                                    <asp:Literal ID="litPassport" runat="server"></asp:Literal>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
            <div class="pager">
                <svc:Mirror ID="mirrorPager" runat="server" ControlIDToMirror="pagerCustomers" />
            </div>
        </div>
    </fieldset>
</asp:Content>
