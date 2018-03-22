<%@ Page language="c#" Codebehind="TemplateEdit.aspx.cs" AutoEventWireup="false" Inherits="CMS.Web.Admin.TemplateEdit" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>TemplateEdit</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</head>
	<body ms_positioning="FlowLayout">
		<form id="Form1" method="post" runat="server">
			<p>
				<em>Hãy chắc chắn rằng bạn đã đặt một mẫu giao diện dạng control (.ascx) trong thư mục
				cơ sở (Base Path) và ít nhất một tệp css trong thư mục (Base path)/Css.</em>
			</p>
			<div class="group">
				<h4>Cấu hình cơ bản</h4>
				<table>
					<tr>
						<td style="WIDTH: 200px">Tên</td>
						<td><asp:textbox id="txtName" runat="server" width="200px"></asp:textbox>
							<asp:requiredfieldvalidator id="rfvName" runat="server" errormessage="Phải có tên" cssclass="validator"
								display="Dynamic" enableclientscript="False" controltovalidate="txtName"></asp:requiredfieldvalidator>
						</td>
					</tr>
					<tr>
						<td>Đường dẫn cơ sở (từ gốc ứng dụng, không bắt đầu bằng '/')</td>
						<td><asp:textbox id="txtBasePath" runat="server" width="200px"></asp:textbox>
							<asp:button id="btnVerifyBasePath" runat="server" text="Xác minh" causesvalidation="False"></asp:button>
							<asp:requiredfieldvalidator id="rfvBasePath" runat="server" errormessage="Phải có đường dẫn" cssclass="validator"
								display="Dynamic" enableclientscript="False" controltovalidate="txtBasePath"></asp:requiredfieldvalidator>
						</td>
					</tr>
					<tr>
						<td>Control mẫu giao diện</td>
						<td>
							<asp:dropdownlist id="ddlTemplateControls" runat="server"></asp:dropdownlist>
							<asp:label id="lblTemplateControlWarning" runat="server" cssclass="validator" visible="False"
								enableviewstate="False"></asp:label></td>
					</tr>
					<tr>
						<td>Css</td>
						<td><asp:dropdownlist id="ddlCss" runat="server"></asp:dropdownlist>
							<asp:label id="lblCssWarning" runat="server" cssclass="validator" visible="False" enableviewstate="False"></asp:label>
						</td>
					</tr>
				</table>
			</div>
			<br/>
			<asp:panel id="pnlPlaceholders" runat="server" visible="False" cssclass="group">
				<h4>Vị trí đặt vùng phân hệ</h4>
				<table class="tbl">
					<tr>
						<th>
							Vị trí</th>
						<th>
							Vùng phân hệ</th>
						<th>
						</th>
					</tr>
					<asp:repeater id="rptPlaceholders" runat="server">
						<itemtemplate>
							<tr>
								<td>
									<asp:label id="lblPlaceholder" runat="server"></asp:label></td>
								<td>
									<asp:hyperlink id="hplSection" runat="server" visible="False"></asp:hyperlink></td>
								<td>
									<asp:hyperlink id="hplAttachSection" runat="server" visible="false">Gắn vùng phân hệ</asp:hyperlink>
									<asp:linkbutton id="lbtDetachSection" runat="server" visible="false" commandname="detach">Gỡ vùng phân hệ</asp:linkbutton>
								</td>
							</tr>
						</itemtemplate>
					</asp:repeater></table>
			</asp:panel>
			<br/>
			<asp:button id="btnSave" runat="server" text="Lưu"></asp:button>
			<asp:button id="btnBack" runat="server" text="Quay lại" causesvalidation="false"></asp:button>
			<asp:button id="btnDelete" runat="server" text="Xóa" causesvalidation="false"></asp:button>
		</form>
	</body>
</html>
