<%@ Control Language="C#" AutoEventWireup="true" Codebehind="CustomerInfoInput.ascx.cs"
    Inherits="Portal.Modules.OrientalSails.Web.Controls.CustomerInfoInput" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<input id="hiddenId" type=hidden runat="server" class="hiddenId" />
<table>
    <tr>
        <td>
            Full name</td>
        <td>
            <asp:TextBox ID="txtName" runat="server"></asp:TextBox></td>
        <td>
            Gender</td>
        <td>
            <asp:DropDownList ID="ddlGender" runat="server">
                <asp:ListItem>-- Unknown --</asp:ListItem>
                <asp:ListItem>Male</asp:ListItem>
                <asp:ListItem>Female</asp:ListItem>
            </asp:DropDownList></td>
    </tr>
    <tr>
        <td>Birthday</td>
        <td><asp:TextBox ID="txtBirthDay" runat="server"></asp:TextBox></td>
        <td>Nationality</td>
        <td>
        <asp:DropDownList ID="ddlNationalities" runat="server"></asp:DropDownList>
        <asp:TextBox ID="txtNationality" runat="server" Visible="false"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Visa No.</td>
        <td><asp:TextBox ID="txtVisaNo" runat="server"></asp:TextBox></td>
        <td>Passport No.</td>
        <td><asp:TextBox ID="txtPassport" runat="server"></asp:TextBox></td>
    </tr>
    <tr>
        <td>Visa expired</td>
        <td><asp:TextBox ID="txtVisaExpired" runat="server"></asp:TextBox></td>
       <%-- <asp:PlaceHolder ID="plhChild" runat="server">
        <td>Child</td>
        <td><asp:CheckBox ID="chkChild" runat="server" CssClass="checkbox"/></td>
        </asp:PlaceHolder>--%>
    </tr>
    <tr>
        <td>Viet Kieu</td>
        <td><asp:CheckBox ID="chkVietKieu" runat="server" CssClass="checkbox"/></td>
        <td></td>
        <td><asp:TextBox ID="txtCode" runat="server" Visible="false" CssClass="txtCode"></asp:TextBox><asp:TextBox ID="txtTotal" runat="server" Visible="false"></asp:TextBox></td>
    </tr>
</table>
<ajax:CalendarExtender ID="calendarBirthday" runat="server" TargetControlID="txtBirthDay" Format="dd/MM/yyyy"></ajax:CalendarExtender>
<ajax:CalendarExtender ID="calendarVisa" runat="server" TargetControlID="txtVisaExpired" Format="dd/MM/yyyy"></ajax:CalendarExtender>
<asp:TextBox ID="txtNguyenQuan" runat="server" Width="120"></asp:TextBox>