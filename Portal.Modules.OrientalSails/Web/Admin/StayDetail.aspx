<%@ Page Language="C#" MasterPageFile="SailsMaster.Master" AutoEventWireup="true"
    Codebehind="StayDetail.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.StayDetail"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <fieldset>
        <legend></legend>
        <div class="advancedinfo">
            <table>
                <tr>
                    <td><asp:Button ID="btnBlank" runat="server" Text="Tải về tệp tin mẫu trắng" CssClass="button" OnClick="btnBlank_Click" /></td>
                    <td><asp:Button ID="btnIndex" runat="server" Text="Tải về tệp tin mẫu với dữ liệu hiện tại" CssClass="button" OnClick="btnIndex_Click" /></td>
                    <td><asp:FileUpload ID="fileImport" runat="server" /></td>
                    <td><asp:Button ID="btnImprot" runat="server" Text="Import dữ liệu" CssClass="button" OnClick="btnImport_Click"/></td>
                </tr>
            </table>
        </div>
        <div>
            <table>
                <asp:Repeater ID="rptCustomers" runat="server" OnItemDataBound="rptCustomers_ItemDataBound">
                    <HeaderTemplate>
                        <tr>
                            <th>
                                <%= base.GetText("textRoomNumber") %>
                            </th>
                            <th>
                                <%= base.GetText("textFullName") %>
                            </th>
                            <th>
                                <%= base.GetText("textGender") %>
                            </th>
                            <th>
                                <%= base.GetText("textBirthDate") %>
                            </th>
                            <th>
                                <%= base.GetText("textNationality") %>
                            </th>
                            <th>
                                <%= base.GetText("textPassport") %>
                            </th>
                            <th>
                                <%= base.GetText("textVietKieu") %>
                            </th>
                            <th>
                                <%= base.GetText("textPurpose") %>
                            </th>
                            <th>
                                <%= base.GetText("textStayTerm") %>
                            </th>
                            <th>
                            Note
                                <%--<%= base.GetText("textStayIn") %>--%>
                            </th>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:Literal ID="litRoom" runat="server"></asp:Literal>
                                <asp:HiddenField ID="hiddenId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "Id") %>' />
                                </td>
                            <td>
                                <asp:TextBox ID="txtFullName" runat="server"></asp:TextBox></td>
                            <td>
                                <asp:DropDownList ID="ddlGender" runat="server">
                                    <asp:ListItem>-- Gender --</asp:ListItem>
                                    <asp:ListItem>Male</asp:ListItem>
                                    <asp:ListItem>Female</asp:ListItem>
                                </asp:DropDownList></td>
                            <td>
                                <asp:TextBox ID="txtBirthDate" runat="server" Width="75"></asp:TextBox>
                                <ajax:CalendarExtender ID="calendarBirthdate" runat="server" TargetControlID="txtBirthDate" Format="dd/MM/yyyy"></ajax:CalendarExtender></td>
                            <td>
                                <asp:DropDownList ID="ddlNationalities" runat="server"></asp:DropDownList>
                                <asp:TextBox ID="txtNationality" runat="server" Visible="false"></asp:TextBox></td>
                            <td>
                                <asp:TextBox ID="txtPassport" runat="server"></asp:TextBox></td>
                            <td>
                                <asp:CheckBox ID="chkVietKieu" runat="server" /></td>
                            <td>
                                <asp:DropDownList ID="ddlPurposes" runat="server"></asp:DropDownList>
                                <asp:TextBox ID="txtPurpose" runat="server" Visible="false"></asp:TextBox></td>
                            <td>
                                <asp:TextBox ID="txtStayTerm" runat="server"></asp:TextBox></td>
                            <td>
                                <asp:TextBox ID="txtStayIn" runat="server" Width="300"></asp:TextBox></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
        <asp:Button ID="btnSave" runat="server" CssClass="button" OnClick="btnSave_Click" />
    </fieldset>
</asp:Content>
