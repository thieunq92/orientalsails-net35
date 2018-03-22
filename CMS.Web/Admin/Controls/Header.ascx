<%@ Control Language="c#" AutoEventWireup="false" Codebehind="Header.ascx.cs" Inherits="CMS.Web.Admin.Controls.Header" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<div id="header">
	<div id="headertitle">
		System Administrator
	</div>
	<div id="headeruser">
	</div>
<%--	<div id="styleswitcher">
            <ul>
                <li><a href="javascript: document.cookie='theme='; window.location.reload();" title="Default" id="defswitch">d</a></li>
                <li><a href="javascript: document.cookie='theme=1'; window.location.reload();" title="Blue" id="blueswitch">b</a></li>
                <li><a href="javascript: document.cookie='theme=2'; window.location.reload();" title="Green" id="greenswitch">g</a></li>
                <li><a href="javascript: document.cookie='theme=3'; window.location.reload();" title="Brown" id="brownswitch">b</a></li>
                <li><a href="javascript: document.cookie='theme=4'; window.location.reload();" title="Mix" id="mixswitch">m</a></li>
                <li><a href="javascript: document.cookie='theme=5'; window.location.reload();" title="Mix" id="A1">m</a></li>
            </ul>
     </div>--%>
</div>

<div id="subheader">
	[<asp:hyperlink id="hplSite" runat="server">Return Site</asp:hyperlink>] 
	[<asp:linkbutton id="lbtLogout" runat="server">Logout</asp:linkbutton>]
</div>
