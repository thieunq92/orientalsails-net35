<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ModuleAdminHeader.ascx.cs" Inherits="CMS.Web.Controls.ModuleAdminHeader" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<div id="header">
	<div id="headertitle">Quản lý nội dung phân hệ</div>
</div>
<div id="subheader">
	[Node: <asp:label id="lblNode" runat="server"></asp:label>]
	[Section: <asp:label id="lblSection" runat="server"></asp:label>&nbsp;&nbsp;]
	[<asp:hyperlink id="hplBack" runat="server">Return to site</asp:hyperlink>]
</div>
