<%@ Page language="c#" Codebehind="Login.aspx.cs" AutoEventWireup="false" Inherits="CMS.Web.Admin.Login" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>Login</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link rel="stylesheet" type="text/css" href="Admin.css"/>
		<link href="login.css" rel="stylesheet" type="text/css" />
	</head>
	<body ms_positioning="FlowLayout">
    <div id="logo">
	    <img src="/Images/logo.png" alt="logopng" width="116" height="34" /> <!--//  Logo on upper corner -->
    </div>

		<%--<form id="Form1" method="post" runat="server">
			<div id="login">
				<div class="box">
	                <div class="welcome" id="welcometitle">Welcome to ProAdmin, Please Login: <!--//  Welcome message --></div>
				<table width="100%">
					<tr>
						<td></td>
						<td><asp:label id="lblError" runat="server" enableviewstate="False" visible="False" cssclass="validator"></asp:label></td></tr>
					<tr>
						<td style="width:90px">User</td>
						<td><asp:textbox id="txtUsername" runat="server" width="140px"></asp:textbox></td>
					</tr>
					<tr>
						<td>Password</td>
						<td><asp:textbox id="txtPassword" runat="server" textmode="Password" width="140px"></asp:textbox></td>
					</tr>
					<tr>
						<td></td>
						<td><asp:button id="btnLogin" runat="server" text="Login"></asp:button></td>
					</tr>
				</table>
			    </div>
		    </div>
		</form>--%>
         <div class="box">
	                <div class="welcome" id="welcometitle">Welcome to B.I.T Admin, Please Login: <!--//  Welcome message --></div>
		
        <form id="Form2" method="post" runat="server">
        <div id="fields"> 
            <table width="333">
                <tr>
			        <td></td>
			        <td><asp:label id="lblError" runat="server" enableviewstate="False" visible="False" cssclass="validator"></asp:label></td>
			        </tr>
              <tr>
                <td width="79" height="35"><span class="login">USERNAME</span></td>
                <td width="244" height="35"><label>
                  <asp:textbox id="txtUsername" runat="server" width="140px"></asp:textbox>
                </label></td>
              </tr>
              
              
              <tr>
                <td height="35"><span class="login">PASSWORD</span></td>
                <td height="35"><asp:textbox id="txtPassword" runat="server" textmode="Password" width="140px"></asp:textbox></td> <!--//  Password field -->
              </tr>
              
              
              <tr>
                <td height="65">&nbsp;</td>
                <td height="65" valign="middle"><label>
                  <asp:button id="btnLogin" runat="server" text="Login" class="button" ></asp:button>
                  <!--//  login button -->
                </label></td>
              </tr>
            </table>
          </div>
          <div class="login" id="lostpassword"><a href="#">Lost Password?</a></div> <!--//  lost password part -->
          <div class="copyright" id="copyright">Product developed by <a href="http://www.bitcorp.vn"> B.I.T Corp. </a><br /> <!--//  copyright / footer -->
            Copyright &copy; B.I.T Corp 2009.
          <a href="#">Back to index.</a></div>
        </form>
        </div> <!--//  end div "box" -->
	</body>
</html>

