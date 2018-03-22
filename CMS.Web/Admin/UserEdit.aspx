<%@ Page language="c#" Codebehind="UserEdit.aspx.cs" AutoEventWireup="false" Inherits="CMS.Web.Admin.UserEdit" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>UserEdit</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</head>
	<body ms_positioning="FlowLayout">
		<form id="Form1" method="post" runat="server">
			<script type="text/javascript"> <!--
				function confirmDeleteUser(userId)
				{
					if (confirm("Bạn có chắc chắn muốn xóa người dùng này?"))
						document.location.href = "UserEdit.aspx?UserId=" + userId + "&Action=Delete";
				}
				// -->
			</script>
			<div class="group">
				<h4>Cấu hình cơ bản</h4>
				<table>
					<tr>
						<td style="WIDTH: 200px">Tên đăng nhập</td>
						<td><asp:textbox id="txtUsername" runat="server" width="200px"></asp:textbox><asp:label id="lblUsername" runat="server" visible="False"></asp:label><asp:requiredfieldvalidator id="rfvUsername" runat="server" errormessage="Phải có tên đăng nhập" cssclass="validator"
								display="Dynamic" enableclientscript="False" controltovalidate="txtUsername"></asp:requiredfieldvalidator></td>
					</tr>
					<tr>
						<td>Họ</td>
						<td><asp:textbox id="txtFirstname" runat="server" width="200px"></asp:textbox></td>
					</tr>
					<tr>
						<td>Tên</td>
						<td><asp:textbox id="txtLastname" runat="server" width="200px"></asp:textbox></td>
					</tr>
					<tr>
						<td>Email</td>
						<td><asp:textbox id="txtEmail" runat="server" width="200px"></asp:textbox><asp:requiredfieldvalidator id="rfvEmail" runat="server" controltovalidate="txtEmail" enableclientscript="False"
								display="Dynamic" cssclass="validator" errormessage="Phải có email"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revEmail" runat="server" controltovalidate="txtEmail" enableclientscript="False"
								display="Dynamic" cssclass="validator" errormessage="Email không hợp lệ" validationexpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:regularexpressionvalidator></td>
					</tr>
					<tr>
						<td>Website</td>
						<td>
							<asp:textbox id="txtWebsite" runat="server" width="200px"></asp:textbox></td>
					</tr>
					<tr>
						<td>Hoạt động</td>
						<td><asp:checkbox id="chkActive" runat="server"></asp:checkbox></td>
					</tr>
					<tr>
						<td>Múi giờ</td>
						<td>
							<asp:dropdownlist id="ddlTimeZone" runat="server"></asp:dropdownlist></td>
					</tr>
					<tr>
						<td>Mật khẩu</td>
						<td><asp:textbox id="txtPassword1" runat="server" width="200px" textmode="Password"></asp:textbox></td>
					</tr>
					<tr>
						<td>Nhập lại mật khẩu</td>
						<td><asp:textbox id="txtPassword2" runat="server" width="200px" textmode="Password"></asp:textbox><asp:comparevalidator id="covPassword" runat="server" controltovalidate="txtPassword1" enableclientscript="False"
								display="Dynamic" cssclass="validator" errormessage="Hai ô mật khẩu không trùng nhau" controltocompare="txtPassword2"></asp:comparevalidator></td>
					</tr>
				</table>
			</div>
			<div class="group">
				<h4>Vai trò</h4>
				<table class="tbl">
					<asp:repeater id="rptRoles" runat="server">
						<headertemplate>
							<tr>
								<th>
									Vai trò</th>
								<th>
								</th>
							</tr>
						</headertemplate>
						<itemtemplate>
							<tr>
								<td><%# DataBinder.Eval(Container.DataItem, "Name") %></td>
								<td style="text-align:center">
									<asp:checkbox id="chkRole" runat="server"></asp:checkbox></td>
							</tr>
						</itemtemplate>
					</asp:repeater></table>
			</div>
			<div><asp:button id="btnSave" runat="server" text="Lưu">
			</asp:button><asp:button id="btnCancel" runat="server" text="Hoàn tác" causesvalidation="False">
			</asp:button><asp:button id="btnDelete" runat="server" text="Xóa"></asp:button></div>
		</form>
	</body>
</html>
