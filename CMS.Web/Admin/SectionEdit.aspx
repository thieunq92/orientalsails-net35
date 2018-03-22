<%@ Page language="c#" Codebehind="SectionEdit.aspx.cs" AutoEventWireup="false" Inherits="CMS.Web.Admin.SectionEdit" %>
<%@ Register Assembly="System.Web.Extensions" Namespace="System.Web.UI" TagPrefix="asp" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>SectionEdit</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</head>
	<body ms_positioning="FlowLayout">
		<form id="Form1" method="post" runat="server">
		<asp:ScriptManager runat="server" ID="scriptManager">
		</asp:ScriptManager>
			<div class="group">
				<h4>Cấu hình cơ bản</h4>
				<table>
					<tr>
						<td style="WIDTH:100px">Tiêu đề vùng</td>
						<td><asp:textbox id="txtTitle" runat="server" width="300px"></asp:textbox>
							<asp:requiredfieldvalidator id="rfvTitle" runat="server" display="Dynamic" cssclass="validator" controltovalidate="txtTitle"
								enableclientscript="False">Yêu cầu có tiêu đề</asp:requiredfieldvalidator></td>
					</tr>
					<tr>
						<td>Hiển thị tiêu đề trên vùng</td>
						<td><asp:checkbox id="chkShowTitle" runat="server"></asp:checkbox></td>
					</tr>
					<tr>
						<td>Phân hệ</td>
						<td>
							<asp:dropdownlist id="ddlModule" runat="server" autopostback="True" visible="False"></asp:dropdownlist>
							<asp:label id="lblModule" runat="server" visible="False"></asp:label>
						</td>
					</tr>
					<tr>
						<td>Vị trí</td>
						<td>
							<asp:dropdownlist id="ddlPlaceholder" runat="server"></asp:dropdownlist>
							<asp:hyperlink id="hplLookup" runat="server">Tìm</asp:hyperlink>
						</td>
					</tr>
					<tr>
						<td>Thời gian cache</td>
						<td><asp:textbox id="txtCacheDuration" runat="server" width="30px"></asp:textbox><asp:requiredfieldvalidator id="rfvCache" runat="server" display="Dynamic" cssclass="validator" controltovalidate="txtCacheDuration"
								enableclientscript="False">Phải cấp thông tin cache (để là 0 để bỏ chế độ cache)</asp:requiredfieldvalidator><asp:comparevalidator id="cpvCache" runat="server" display="Dynamic" cssclass="validator" controltovalidate="txtCacheDuration"
								enableclientscript="False" errormessage="Phải là số dương" operator="GreaterThanEqual" valuetocompare="0" type="Integer"></asp:comparevalidator></td>
					</tr>
				</table>
			</div>
			<asp:panel id="pnlCustomSettings" cssclass="group" runat="server" enableviewstate="false">
				<h4>Tùy chọn cho phân hệ</h4>
				<table>
					<asp:placeholder id="plcCustomSettings" runat="server" />
				</table>
			</asp:panel>
			<asp:panel id="pnlConnections" cssclass="group" runat="server" visible="False">
				<h4>Connections</h4>
				<table class="tbl">
					<asp:repeater id="rptConnections" runat="server">
						<headertemplate>
							<tr>
								<th>Với vùng</th>
								<th>Thao tác</th>
								<th></th>
							</tr>
						</headertemplate>
						<itemtemplate>
							<tr>
								<td><%# DataBinder.Eval(Container.DataItem, "Value.FullName") %></td>
								<td><%# DataBinder.Eval(Container.DataItem, "Key") %></td>
								<td><asp:linkbutton id="lbtDelete" runat="server" causesvalidation="False" commandname="DeleteConnection" commandargument='<%# DataBinder.Eval(Container.DataItem, "Key") %>'>Delete</asp:linkbutton></td>
							</tr>
						</itemtemplate>
					</asp:repeater>
				</table>
				<asp:hyperlink id="hplNewConnection" runat="server">Thêm liên hệ</asp:hyperlink>
			</asp:panel>
			<div class="group">
				<h4>Quản lý quyền</h4>
				<table class="tbl">
					<asp:repeater id="rptRoles" runat="server">
						<headertemplate>
							<tr>
								<th>
									Vai trò</th>
								<th>
									Quyền xem</th>
								<th>
									Quyền quản trị</th>
								<th>
									Thêm dữ liệu</th>
								<th>
									Sửa dữ liệu</th>
								<th>
									Xóa dữ liệu</th>																											
							</tr>
						</headertemplate>
						<itemtemplate>
							<tr>
								<td><%# DataBinder.Eval(Container.DataItem, "Name") %></td>
								<td style="text-align:center">
									<asp:checkbox id="chkViewAllowed" runat="server"></asp:checkbox></td>
								<td style="text-align:center">
									<asp:checkbox id="chkEditAllowed" runat="server"></asp:checkbox></td>
								<td style="text-align:center">
									<asp:checkbox id="chkInsertAllowed" runat="server"></asp:checkbox></td>
								<td style="text-align:center">
									<asp:checkbox id="chkModifyAllowed" runat="server"></asp:checkbox></td>
								<td style="text-align:center">
									<asp:checkbox id="chkDeleteAllowed" runat="server"></asp:checkbox></td>																											
							</tr>
						</itemtemplate>
					</asp:repeater></table>
			</div>
			<div>
				<asp:button id="btnSave" runat="server" text="Save"></asp:button>
				<asp:button id="btnBack" runat="server" text="Back" causesvalidation="False"></asp:button>
			</div>
			<script type="text/javascript"> <!--
			function setPlaceholderValue(ddlist, val)
			{
				var placeholdersList = document.getElementById(ddlist);
				if (placeholdersList != null)
				{
					for (i = 0; i < placeholdersList.options.length; i++)
					{
						if (placeholdersList.options[i].value == val)
						{
							placeholdersList.selectedIndex = i;
						}
					}				
				}
			}
			// -->
			</script>
		</form>
	</body>
</html>
