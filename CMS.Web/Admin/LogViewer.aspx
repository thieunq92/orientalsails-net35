<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogViewer.aspx.cs" Inherits="CMS.Web.Admin.LogViewer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
	<head>
		<title>Log Viewer</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR"/>
		<meta content="C#" name="CODE_LANGUAGE"/>
		<meta content="JavaScript" name="vs_defaultClientScript"/>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
	</head>
<body>
    <form id="form1" runat="server">
            <div class="group">
				<h4>Log viewer</h4>
				<table>
				    <asp:Repeater ID="rptLogs" runat="server">
				        <ItemTemplate>
				            <tr>
				            </tr>
				        </ItemTemplate>
				    </asp:Repeater>					
				</table>
			</div>
    </form>
</body>
</html>
