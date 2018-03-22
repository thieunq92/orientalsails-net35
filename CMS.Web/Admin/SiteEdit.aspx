<%@ Page language="c#" Codebehind="SiteEdit.aspx.cs" AutoEventWireup="false" Inherits="CMS.Web.Admin.SiteEdit" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>SiteEdit</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</head>
	<body>
		<form id="Form1" method="post" runat="server">
			<div class="group">
				<h4>Cấu hình cơ bản</h4>
				<table>
					<tr>
						<td style="WIDTH: 100px">Tên</td>
						<td><asp:textbox id="txtName" runat="server" width="300px" maxlength="100"></asp:textbox><asp:requiredfieldvalidator id="rfvName" runat="server" enableclientscript="False" controltovalidate="txtName"
								cssclass="validator" display="Dynamic">Phải có tên trang</asp:requiredfieldvalidator></td>
					</tr>
					<tr>
						<td>Đường dẫn tới trang (gồm cả http://)</td>
						<td><asp:textbox id="txtSiteUrl" runat="server" width="300px" maxlength="100"></asp:textbox><asp:requiredfieldvalidator id="rfvSiteUrl" runat="server" enableclientscript="False" controltovalidate="txtSiteUrl"
								cssclass="validator" display="Dynamic">Phải có đường dẫn</asp:requiredfieldvalidator></td>
					</tr>
					<tr>
						<td>Email chủ website</td>
						<td><asp:textbox id="txtWebmasterEmail" runat="server" maxlength="100" width="300px"></asp:textbox><asp:requiredfieldvalidator id="rfvWebmasterEmail" runat="server" errormessage="Phải có địa chỉ email"
								cssclass="validator" display="Dynamic" controltovalidate="txtWebmasterEmail" enableclientscript="False"></asp:requiredfieldvalidator></td>
					</tr>
					<tr>
						<td>Cho phép sử dụng đường dẫn thân thiện (Friendly URL)</td>
						<td><asp:checkbox id="chkUseFriendlyUrls" runat="server"></asp:checkbox></td>
					</tr>
				</table>
			</div>
			<div class="group">
				<h4>Mặc định</h4>
				<table>
					<tr>
						<td style="WIDTH: 100px">Mẫu giao diện</td>
						<td><asp:dropdownlist id="ddlTemplates" runat="server" autopostback="True"></asp:dropdownlist></td>
					</tr>
					<tr>
						<td>Vị trí</td>
						<td><asp:dropdownlist id="ddlPlaceholders" runat="server"></asp:dropdownlist><em>(đây là vị trí
						mà nội dung của &nbsp;các trang cơ bản&nbsp;sẽ được chèn vào)</em></td>
					</tr>
					<tr>
						<td>Vùng ngôn ngữ</td>
						<td><asp:dropdownlist id="ddlCultures" runat="server"></asp:dropdownlist></td>
					</tr>
					<tr>
						<td>Vai trò của người mới đăng ký</td>
						<td><asp:dropdownlist id="ddlRoles" runat="server"></asp:dropdownlist></td>
					</tr>
					<tr>
						<td>
							Thông tin Meta</td>
						<td>
							<asp:TextBox ID="txtMetaDescription" runat="server" MaxLength="500" TextMode="MultiLine"
								Width="400px" height="70px"></asp:TextBox></td>
					</tr>
					<tr>
						<td>
							Từ khóa</td>
						<td>
							<asp:TextBox ID="txtMetaKeywords" runat="server" MaxLength="500" TextMode="MultiLine"
								Width="400px" height="70px"></asp:TextBox></td>
					</tr>
				</table>
			</div>
			<asp:panel id="pnlAliases" runat="server" cssclass="group">
				<h4>Tên hiệu</h4>
				<table class="tbl">
					<asp:repeater id="rptAliases" runat="server">
						<headertemplate>
							<tr>
								<th>
									Đường dẫn hiệu</th>
								<th>
									Nút</th>
								<th>
									&nbsp;</th>
							</tr>
						</headertemplate>
						<itemtemplate>
							<tr>
								<td><%# DataBinder.Eval(Container.DataItem, "Url") %></td>
								<td>
									<asp:label id="lblEntryNode" runat="server"></asp:label></td>
								<td>
									<asp:hyperlink id="hplEdit" runat="server">Sửa</asp:hyperlink></td>
							</tr>
						</itemtemplate>
					</asp:repeater></table>
				<asp:hyperlink id="hplNewAlias" runat="server">Thêm tên hiệu</asp:hyperlink>
			</asp:panel>
			<div>
				<asp:button id="btnSave" runat="server" text="Lưu"></asp:button>
				<asp:button id="btnCancel" runat="server" text="Hoàn tác" causesvalidation="False"></asp:button>
				<asp:button id="btnDelete" runat="server" text="Xóa" causesvalidation="False"></asp:button>
			</div>
		</form>
	</body>
</html>
