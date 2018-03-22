<%@ Control Language="c#" AutoEventWireup="false" Codebehind="Navigation.ascx.cs" Inherits="CMS.Web.Admin.Controls.Navigation" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<div class="navsection">
	<h3>
		<asp:image imageurl="../Images/home.gif" runat="server" imagealign="left" id="i1" alternatetext="Trang chủ"></asp:image>
		Các trang</h3>
	<asp:placeholder id="plhNodes" runat="server"></asp:placeholder>
	<br/>
	<asp:image imageurl="../Images/new.gif" runat="server" imagealign="left" id="inew" alternatetext="Trang mới"></asp:image><asp:hyperlink id="hplNew" navigateurl="../SiteEdit.aspx?SiteId=-1" cssclass="nodelink" runat="server">Thêm một trang mới</asp:hyperlink>
</div>
<br/>
<div class="navsection">
	<h3>
		<asp:image imageurl="../Images/modules.gif" runat="server" imagealign="left" id="i2" alternatetext="Các vùng"></asp:image>
		Vùng phân hệ</h3>
	<asp:hyperlink id="hplSections" navigateurl="../Sections.aspx" runat="server">Quản lý các vùng riêng lẻ</asp:hyperlink>
</div>
<br/>
<div class="navsection">
	<h3>
		<asp:image imageurl="../Images/modules.gif" runat="server" imagealign="left" id="i3" alternatetext="Phân hệ"></asp:image>
		Phân hệ</h3>
	<asp:hyperlink id="hplModules" navigateurl="../Modules.aspx" runat="server">Quản lý các phân hệ</asp:hyperlink>
</div>
<br/>
<div class="navsection">
	<h3>
		<asp:image imageurl="../Images/docs.gif" runat="server" imagealign="left" id="i4" alternatetext="Giao diện mẫu"></asp:image>
		Mẫu giao diện</h3>
	<asp:hyperlink id="hplTemplates" navigateurl="../Templates.aspx" runat="server">Quản lý các mẫu</asp:hyperlink>
</div>
<br/>
<div class="navsection">
	<h3>
		<asp:image imageurl="../Images/user.gif" runat="server" imagealign="left" id="i5" alternatetext="Người dùng"></asp:image>
		Người dùng
	</h3>
	<asp:hyperlink id="hplUsers" navigateurl="../Users.aspx" runat="server">Quản lý người dùng</asp:hyperlink>
</div>
<br/>
<div class="navsection">
	<h3>
		<asp:image imageurl="../Images/users.gif" runat="server" imagealign="left" id="i6" alternatetext="Vai trò"></asp:image>
		Vai trò
	</h3>
	<asp:hyperlink id="hplRoles" navigateurl="../Roles.aspx" runat="server">Quản lý các vai trò</asp:hyperlink>
</div>
<br/>
<div class="navsection">
	<h3>
		<asp:image imageurl="../Images/search.gif" runat="server" imagealign="left" id="i7" alternatetext="Tìm kiếm toàn văn"></asp:image>
		Tìm kiếm
	</h3>
	<asp:hyperlink id="hplRebuild" navigateurl="../RebuildIndex.aspx" runat="server">Dựng lại bảng tìm kiếm toàn văn</asp:hyperlink>
</div>
