<%@ Page language="c#" Codebehind="RebuildIndex.aspx.cs" AutoEventWireup="false" Inherits="CMS.Web.Admin.RebuildIndex" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>Dựng lại bảng tìm kiếm</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1"/>
		<meta name="CODE_LANGUAGE" content="C#"/>
		<meta name="vs_defaultClientScript" content="JavaScript"/>
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5"/>
	</head>
	<body ms_positioning="FlowLayout">
		<form id="Form1" method="post" runat="server">
			<p>
				<asp:label id="lblMessage" runat="server" visible="False">Dựng lại thành công.</asp:label>
			</p>
			<p>
				<asp:button id="btnRebuild" runat="server" text="Dựng lại bảng nội dung"></asp:button>
			</p>
			<div id="pleasewait" style="DISPLAY: none">
				Hãy đợi một chút trong khi bảng chỉ số đang được dựng...
			</div>
		</form>
	</body>
</html>
