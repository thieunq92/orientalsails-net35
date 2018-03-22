<%@ Page Language="C#" MasterPageFile="SailsMaster.Master" AutoEventWireup="true"
    Codebehind="User.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.UserPanel"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <div class="group">
        <h4>
            Thông tin người dùng</h4>
        <table>
            <tr>
                <td style="width: 200px">
                    Tên đăng nhập</td>
                <td>
                    <asp:TextBox ID="txtUsername" runat="server" Width="200px"></asp:TextBox><asp:Label
                        ID="lblUsername" runat="server" Visible="False"></asp:Label><asp:RequiredFieldValidator
                            ID="rfvUsername" runat="server" ErrorMessage="Phải có tên đăng nhập" CssClass="validator"
                            Display="Dynamic" EnableClientScript="False" ControlToValidate="txtUsername"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td>
                    Họ</td>
                <td>
                    <asp:TextBox ID="txtFirstname" runat="server" Width="200px"></asp:TextBox></td>
            </tr>
            <tr>
                <td>
                    Tên</td>
                <td>
                    <asp:TextBox ID="txtLastname" runat="server" Width="200px"></asp:TextBox></td>
            </tr>
            <tr>
                <td>
                    Email</td>
                <td>
                    <asp:TextBox ID="txtEmail" runat="server" Width="200px"></asp:TextBox><asp:RequiredFieldValidator
                        ID="rfvEmail" runat="server" ControlToValidate="txtEmail" EnableClientScript="False"
                        Display="Dynamic" CssClass="validator" ErrorMessage="Phải có email"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                            ID="revEmail" runat="server" ControlToValidate="txtEmail" EnableClientScript="False"
                            Display="Dynamic" CssClass="validator" ErrorMessage="Email không hợp lệ" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator></td>
            </tr>
            <tr>
                <td>
                    Phone</td>
                <td>
                    <asp:TextBox ID="txtWebsite" runat="server" Width="200px"></asp:TextBox></td>
            </tr>
            <tr>
                <td>
                    Hoạt động</td>
                <td>
                    <asp:CheckBox ID="chkActive" runat="server"></asp:CheckBox></td>
            </tr>
            <tr>
                <td>
                    Múi giờ</td>
                <td>
                    <asp:DropDownList ID="ddlTimeZone" runat="server">
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td>
                    Mật khẩu</td>
                <td>
                    <asp:TextBox ID="txtPassword" runat="server" Width="200px" TextMode="Password"></asp:TextBox></td>
            </tr>
            <tr>
                <td>
                    Nhập lại mật khẩu</td>
                <td>
                    <asp:TextBox ID="txtPassword2" runat="server" Width="200px" TextMode="Password"></asp:TextBox><asp:CompareValidator
                        ID="covPassword" runat="server" ControlToValidate="txtPassword" EnableClientScript="False"
                        Display="Dynamic" CssClass="validator" ErrorMessage="Hai ô mật khẩu không trùng nhau"
                        ControlToCompare="txtPassword2"></asp:CompareValidator></td>
            </tr>
        </table>
        <asp:button id="btnSave" runat="server" text="Lưu" OnClick="btnSave_Click" CssClass="button">
			</asp:button>
    </div>
</asp:Content>
