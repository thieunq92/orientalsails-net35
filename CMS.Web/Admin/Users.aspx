<%@ Register TagPrefix="csc" Namespace="CMS.ServerControls" Assembly="CMS.ServerControls" %>
<%@ Page language="c#" Codebehind="Users.aspx.cs" AutoEventWireup="false" Inherits="CMS.Web.Admin.Users" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
  <head>
		<title>Users</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
  </head>
	<body>
		<form id="Form1" method="post" runat="server">
			<div class="group">
				<h4>Tìm người dùng</h4>
				Tên đăng nhập <asp:textbox id="txtUsername" runat="server"></asp:textbox><asp:button id="btnFind" runat="server" text="Tìm kiếm"></asp:button>
			</div>
			<asp:panel id="pnlResults" runat="server" cssclass="group">
				<h4>Kết quả tìm kiếm</h4>
				<p>
					<table class="tbl">
						<asp:repeater id=rptUsers runat="server">
							<headertemplate>
								<tr>
									<th>Tên đăng nhập</th>
									<th>Họ</th>
									<th>Tên</th>
									<th>Email</th>
									<th>Website</th>
									<th>Đăng nhập lần cuối</th>
									<th>IP lần cuối</th>
									<th></th>
								</tr>
							</headertemplate>
							<itemtemplate>
								<tr>
									<td><%# DataBinder.Eval(Container.DataItem, "UserName") %></td>
									<td><%# DataBinder.Eval(Container.DataItem, "FirstName") %></td>
									<td><%# DataBinder.Eval(Container.DataItem, "LastName") %></td>
									<td><%# DataBinder.Eval(Container.DataItem, "Email") %></td>
									<td><%# DataBinder.Eval(Container.DataItem, "Website") %></td>
									<td><asp:label id="lblLastLogin" runat="server"></asp:label></td>
									<td style="text-align:right"><%# DataBinder.Eval(Container.DataItem, "LastIp") %></td>
									<td>
										<asp:hyperlink id="hplEdit" runat="server">Sửa</asp:hyperlink>
									</td>
								</tr>
							</itemtemplate>
						</asp:repeater>
					</table>
				</p>
				<div class=pager>
					<csc:pager id="pgrUsers" runat="server" controltopage="rptUsers" cachedatasource="True" pagesize="10" CacheDuration="10"></csc:pager>
				</div>
			</asp:panel>
			<div>
				<asp:button id="btnNew" runat="server" text="Thêm người dùng"></asp:button>
			</div>
		</form>
	</body>
</html>
