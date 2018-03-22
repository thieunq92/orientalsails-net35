<%@ Page Language="C#" MasterPageFile="Popup.Master" AutoEventWireup="true" CodeBehind="AgencyContactEdit.aspx.cs"
    Inherits="Portal.Modules.OrientalSails.Web.Admin.AgencyContactEdit" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="CMS.ServerControls" Namespace="CMS.ServerControls" TagPrefix="svc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <div class="basicinfo">
        <table>
            <tr>
                <td>
                    Name
                </td>
                <td>
                    <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Is Booker
                </td>
                <td>
                    <asp:CheckBox runat="server" ID="chkBooker" />
                </td>
            </tr>
            <tr>
                <td>
                    Position
                </td>
                <td>
                    <asp:TextBox ID="txtPosition" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Phone
                </td>
                <td>
                    <asp:TextBox ID="txtPhone" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Email
                </td>
                <td>
                    <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Birthday
                </td>
                <td>
                    <asp:TextBox ID="txtBirthday" runat="server"></asp:TextBox>
                    <ajax:CalendarExtender ID="cldBirthday" runat="server" TargetControlID="txtBirthday"
                        Format="dd/MM/yyyy">
                    </ajax:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td>
                    Note
                </td>
                <td>
                    <asp:TextBox ID="txtNote" runat="server" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button" OnClick="btnSave_Click" />
</asp:Content>
