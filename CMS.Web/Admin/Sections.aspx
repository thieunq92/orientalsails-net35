<%@ Page language="c#" Codebehind="Sections.aspx.cs" AutoEventWireup="false" Inherits="CMS.Web.Admin.Sections" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 TRANSITIONAL//EN" >
<html>
	<head>
		<title>Sections</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR"/>
		<meta content="C#" name="CODE_LANGUAGE"/>
		<meta content="JavaScript" name="vs_defaultClientScript"/>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
	</head>
	<body ms_positioning="FlowLayout">
		<form id="Form1" method="post" runat="server">
			<p>Qủan lý các vùng phân hệ không thuộc về nút nào. Đây có thể là các vùng phân hệ đã bị gỡ ra từ nút hoặc 
			là các vùng phân hệ liên kết với &nbsp;một hay nhiều&nbsp; mẫu giao diện.</p>
			<table class="tbl">
				<asp:repeater id="rptSections" runat="server">
					<headertemplate>
						<tr>
							<th>
								Tên vùng</th>
							<th>
								Phân hệ</th>
							<th>
								Gắn vào (các) mẫu</th>
							<th>
								Thao tác</th>
						</tr>
					</headertemplate>
					<itemtemplate>
						<tr>
							<td><%# DataBinder.Eval(Container.DataItem, "Title") %></td>
							<td><%# DataBinder.Eval(Container.DataItem, "ModuleType.FullName") %></td>
							<td>
								<asp:literal id="litTemplates" runat="server" />
							</td>
							<td style="white-space:nowrap">
								<asp:hyperlink id="hplEdit" runat="server">Sửa</asp:hyperlink>
								<asp:linkbutton id="lbtDelete" runat="server" causesvalidation="False" commandname="Delete" commandargument='<%# DataBinder.Eval(Container.DataItem, "Id") %>'>Delete</asp:linkbutton>
								<asp:hyperlink id="hplAttachTemplate" runat="server">Gắn vào mẫu</asp:hyperlink>
								<asp:hyperlink id="hplAttachNode" runat="server">Gắn vào nút</asp:hyperlink>
							</td>
						</tr>
					</itemtemplate>
				</asp:repeater></table>
			<br/>
			<div><asp:button id="btnNew" runat="server" text="Thêm vùng phân hệ mới"></asp:button></div>
		</form>
	</body>
</html>
