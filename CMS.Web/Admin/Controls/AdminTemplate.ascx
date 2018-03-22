<%@ Control Language="c#" AutoEventWireup="false" Codebehind="AdminTemplate.ascx.cs" Inherits="CMS.Web.Admin.Controls.AdminTemplate" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="Header" Src="Header.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Navigation" Src="Navigation.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="Footer.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<title><asp:literal id="PageTitle" runat="server"></asp:literal></title>
		<link id="CssStyleSheet" rel="stylesheet" type="text/css" runat="server" />		
		
    <!--<link rel="stylesheet" type="text/css" href="/Admin/Css/theme.css" />
    <link rel="stylesheet" type="text/css" href="/Admin/Css/style.css" />-->
     <!--[if IE]>
    <link rel="stylesheet" type="text/css" href="css/ie-sucks.css" />
    <![endif]-->
    <link rel="shortcut icon" href="/favicon.gif" />
<%--    <script>
        var StyleFile = "theme" + document.cookie.charAt(6) + ".css";
        document.writeln('<link rel="stylesheet" type="text/css" href="/Modules/TourManagement/Admin/Css/asiana/' + StyleFile + '">');
    </script>--%>
    <script type="text/javascript">
      function getElementsByClassName(classname, node) {
      if(!node) node = document.getElementsByTagName("body")[0];
      var a = [];
      var re = new RegExp('\\b' + classname + '\\b');
      var els = node.getElementsByTagName("*");
      for(var i=0,j=els.length; i<j; i++)
      if(re.test(els[i].className))a.push(els[i]);
      return a;
      }
      
      function box1hover(element)
      {      
      	this.className = "box1_hover";
      }
      
      function box1out(element)
      {
      	this.className = "box1";
      }
      
      function box2hover(element)
      {
      	this.className = "box2_hover";
      }
      
      function box2out(element)
      {
      	this.className = "box2";
      }      
      
      function setEvent()
      {
      	a =getElementsByClassName("box1",null);
      	for (var i=0,j=a.length; i<j; i++)
      	{
      		a[i].onmouseover = box1hover;      		 
      		a[i].onmouseout = box1out;
      	}
      	a =getElementsByClassName("box2",null);
      	for (var i=0,j=a.length; i<j; i++)
      	{
      		a[i].onmouseover = box2hover;      		 
      		a[i].onmouseout = box2out;
      	}      	
      }
</script>   
	</head>
	<body>
	
		<form id="Frm" method="post" runat="server" enctype="multipart/form-data">
		
			<uc1:header id="Header" runat="server"></uc1:header>
			<div id="menupane">
				<uc1:navigation id="Nav" runat="server"></uc1:navigation>
			</div>
			<div id="contentpane">
				<h1><asp:literal id="PageTitleLabel" runat="server" /></h1>
				<div id="MessageBox" class="messagebox" runat="server" visible="false" enableviewstate="false"></div>
				<asp:placeholder id="PageContent" runat="server"></asp:placeholder>
			</div>
			<uc1:Footer id="Footer" runat="server" />
		</form>

	</body>
</html>