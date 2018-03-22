<%@ Page Language="C#" MasterPageFile="SailsMaster.Master" AutoEventWireup="true"
    CodeBehind="SetPermission.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.SetPermission"
    Title="Untitled Page" %>

<%@ Register Assembly="CMS.ServerControls" Namespace="CMS.ServerControls" TagPrefix="svc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <script>
        $(function () {
   $("#tabs").tabs({ 
       activate: function() {
          var selectedTab = $('#tabs').tabs('option', 'active');
          $("#<%= hdnSelectedTab.ClientID %>").val(selectedTab);
          },
       active: <%= hdnSelectedTab.Value %>
   });
 });
    </script>
    <style>
        .ajax__calendar_container
        {
            z-index : 1;
        }
    </style>
    <fieldset>
        <legend>Set permission</legend>
        <div class="data_table">
            <h4>
                Roles</h4>
            <div class="pager">
                <svc:Pager ID="pagerRoles" runat="server" ControlToPage="rptRoles" OnPageChanged="pagerRoles_PageChanged" />
            </div>
            <div class="data_grid">
                <table>
                    <asp:Repeater ID="rptRoles" runat="server" OnItemDataBound="rptRoles_ItemDataBound">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:Literal ID="litName" runat="server"></asp:Literal>
                                </td>
                                <td>
                                    <asp:HyperLink ID="hplSetPermission" runat="server">Set permission</asp:HyperLink>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </div>
        <div class="data_table">
            <h4>
                User</h4>
        </div>
        <div class="group">
            <h4>
                Filter
            </h4>
            <table border="0">
                <tr>
                    <td>
                        Tên đăng nhập
                    </td>
                    <td>
                        <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Đăng nhập lần cuối
                    </td>
                    <td>
                        from
                        <asp:TextBox ID="txtFrom" runat="server"></asp:TextBox>
                        <ajax:calendarextender id="calendarFrom" runat="server" targetcontrolid="txtFrom"
                            format="dd/MM/yyyy">
                        </ajax:calendarextender>
                    </td>
                    <td>
                        to
                        <asp:TextBox ID="txtTo" runat="server"></asp:TextBox>
                        <ajax:calendarextender id="calendarTo" runat="server" targetcontrolid="txtTo" format="dd/MM/yyyy">
                        </ajax:calendarextender>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnFind" runat="server" Text="Tìm kiếm" OnClick="btnFind_Click">
                        </asp:Button>
                    </td>
                </tr>
            </table>
        </div>
        <div id="tabs">
            <ul>
                <li><a href="#tabs-1">Active</a></li>
                <li><a href="#tabs-2">In Active</a></li>
            </ul>
            <div id="tabs-1">
                <div class="data_grid">
                    <table>
                        <asp:Repeater ID="rptUsersActive" runat="server" OnItemDataBound="rptUsersActive_ItemDataBound">
                            <HeaderTemplate>
                                <th>
                                    Tên đăng nhập
                                </th>
                                <th>
                                    Họ
                                </th>
                                <th>
                                    Tên
                                </th>
                                <th>
                                    Email
                                </th>
                                <th>
                                    Website
                                </th>
                                <th>
                                    Đăng nhập lần cuối
                                </th>
                                <th>
                                    IP lần cuối
                                </th>
                                <th>
                                </th>
                                <th>
                                </th>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <tr>
                                        <td>
                                            <%# DataBinder.Eval(Container.DataItem, "UserName") %>
                                        </td>
                                        <td>
                                            <%# DataBinder.Eval(Container.DataItem, "FirstName") %>
                                        </td>
                                        <td>
                                            <%# DataBinder.Eval(Container.DataItem, "LastName") %>
                                        </td>
                                        <td>
                                            <%# DataBinder.Eval(Container.DataItem, "Email") %>
                                        </td>
                                        <td>
                                            <%# DataBinder.Eval(Container.DataItem, "Website") %>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblLastLogin" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <%# DataBinder.Eval(Container.DataItem, "LastIp") %>
                                        </td>
                                        <td>
                                            <asp:HyperLink ID="hplEdit" runat="server">Set Role</asp:HyperLink>
                                        </td>
                                        <td>
                                            <asp:HyperLink ID="hplSetPermission" runat="server">Set Permission</asp:HyperLink>
                                        </td>
                                    </tr>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                    <div class="pager">
                        <svc:Pager ID="pagerUserActive" runat="server" ControlToPage="rptUsersActive" OnPageChanged="pagerUserActive_PageChanged" />
                    </div>
                </div>
            </div>
            <div id="tabs-2">
                <div class="data_grid">
                    <table>
                        <asp:Repeater ID="rptUsersInActive" runat="server" OnItemDataBound="rptUsersInActive_ItemDataBound">
                            <HeaderTemplate>
                                <th>
                                    Tên đăng nhập
                                </th>
                                <th>
                                    Họ
                                </th>
                                <th>
                                    Tên
                                </th>
                                <th>
                                    Email
                                </th>
                                <th>
                                    Website
                                </th>
                                <th>
                                    Đăng nhập lần cuối
                                </th>
                                <th>
                                    IP lần cuối
                                </th>
                                <th>
                                </th>
                                <th>
                                </th>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <tr>
                                        <td>
                                            <%# DataBinder.Eval(Container.DataItem, "UserName") %>
                                        </td>
                                        <td>
                                            <%# DataBinder.Eval(Container.DataItem, "FirstName") %>
                                        </td>
                                        <td>
                                            <%# DataBinder.Eval(Container.DataItem, "LastName") %>
                                        </td>
                                        <td>
                                            <%# DataBinder.Eval(Container.DataItem, "Email") %>
                                        </td>
                                        <td>
                                            <%# DataBinder.Eval(Container.DataItem, "Website") %>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblLastLogin" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <%# DataBinder.Eval(Container.DataItem, "LastIp") %>
                                        </td>
                                        <td>
                                            <asp:HyperLink ID="hplEdit" runat="server">Set Role</asp:HyperLink>
                                        </td>
                                        <td>
                                            <asp:HyperLink ID="hplSetPermission" runat="server">Set Permission</asp:HyperLink>
                                        </td>
                                    </tr>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                    <div class="pager">
                        <svc:Pager ID="pagerUserInActive" runat="server" ControlToPage="rptUsersInActive"
                            OnPageChanged="pagerUserInActive_PageChanged" />
                    </div>
                </div>
            </div>
        </div>
    </fieldset>
    <asp:HiddenField ID="hdnSelectedTab" runat="server" Value="0" />
</asp:Content>
