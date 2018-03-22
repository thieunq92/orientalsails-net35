<%@ Control Language="c#" AutoEventWireup="false" Inherits="CMS.Web.UI.BaseTemplate" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="uc1" TagName="navigation" Src="~/Controls/Navigation/NavigationPath.ascx" %>
<%@ Register TagPrefix="uc1" TagName="HierarchicalMenu" Src="~/Controls/Navigation/HierarchicalMenu.ascx" %>
<%@ Register Assembly="CMS.Web" Namespace="CMS.Web.Controls.UserOnlineCounter" TagPrefix="control" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
	<title><asp:literal id="PageTitle" runat="server"></asp:literal></title>
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
	<asp:literal id="MetaTags" runat="server" />
	<asp:literal id="Stylesheets" runat="server" />
	<asp:literal id="JavaScripts" runat="server" />
	<link rel="shortcut icon" href="/favicon.ico" />	
<script src="/js/equalcolumns.js" type="text/javascript"></script> 	
<script src="/js/menuTab.js" type="text/javascript"></script>

<script type="text/javascript" src="/js/mootools.js"></script>
<script type="text/javascript" src="/js/calendar.rc4.js"></script>

<script type="text/javascript" src="/js/menueffect.js">

			/***********************************************
			* Blm Multi-level Effect menu- By Brady Mulhollem at http://www.bradyontheweb.com/
			* Script featured on DynamicDrive.com
			* Visit Dynamic Drive at http://www.dynamicdrive.com/ for this script and more
			***********************************************/

</script>
	</head>
<body>
<form id="t" method="post" runat="server">
<ajaxToolkit:ToolkitScriptManager runat="server" Id="ScriptManager1" />
        <ajaxToolkit:AlwaysVisibleControlExtender ID="ace" runat="server"
    TargetControlID="updateProcessHotel"         
    VerticalSide="Middle"
    HorizontalSide="Center"
    ScrollEffectDuration=".1"/>
        <asp:UpdateProgress ID="updateProcessHotel" runat="server" >
        <ProgressTemplate>
        <img src="/Images/loading.gif" alt="loading" />
        </ProgressTemplate>
        </asp:UpdateProgress>
<center>
<!--Oriental Sails: Halong Bay | Oriental- Sails | Itineraries | Photos | Promotions | Download | Reservation-->
<div id="header">
	<div class="h_logo"></div>
    <div class="h_right">
   	  <table width="100%" border="0" align="right" cellpadding="0" cellspacing="0">          
          <tr>
            <td align="right" valign="top">
            	<div class="search">
                	<div class="searchbar">
                    	<img src="/templates/os/images/search-left.png" /></div>
                    <div class="searchcontent">
                        <img src="/templates/os/images/titlesearch.png" style="padding-top:3px" /><br />
                        <table width="100%" border="0" cellspacing="0" cellpadding="0" style="margin-top:10px">
                          <tr>
                            <td width="38%" align="left" valign="top"><span>Arrival Date</span></td>

                            <td width="25%" align="left" valign="top"><span>Durations</span></td>
                              <td colspan="2" align="left" valign="top"><span>Persons</span></td>
                          </tr>
                            <tr>
                              <td align="left" valign="top"><input id="date3" name="date3" type="text" style="width:65px; height:13px; color:#B1956D; font-size:10px" /></td>
                              <td align="left" valign="top">
                                    <select name="duration" id="duration" style="width:40px; height:15px; color:#B1956D; font-size:10px">
                                      <option value="2">2</option>

                                      <option value="3">3</option>
                                    </select>                   	    </td>
                              <td width="21%" align="left" valign="top">
                              <select name="duration" id="Select1" style="width:40px; height:15px; color:#B1956D; font-size:10px">
                                <option value="1" selected="selected">1</option>
                                      <option value="2">2</option>
                                      <option value="3">3</option>

                                      <option value="4">4</option>
                                      <option value="5">5</option>
                                      <option value="6">6</option>
                                      <option value="7">7</option>
                                      <option value="8">8</option>
                                      <option value="9">9</option>

                                      <option value="10">10</option>
                                    </select></td>
                              <td width="16%" align="left" valign="top"> 
                                <a href="reservation.html"><img src="/templates/os/images/search-bt.png" border="0" /></a></td>
                            </tr>
                        </table>
				  </div>
            </div>            
            </td>

          </tr>
          <tr>
            <td align="left" valign="top">
            	<div id="menu">
                	<div class="mnhome"><a href="feedback.html">Feedback</a> | <a href="contact.html">Contact us</a></div>
		    		<div class="mntab">
                   	  <ul id="tabmenu" class="nav">

      					<li class="active">
                           <a href="/default.aspx" ><span>Home</span></a></li>
           				<li>
                        	<a href="halong-bay.html"><span>Halong Bay</span></a></li>
                      	<li >
                            <a href="oriental-sails.html"><span>Oriental-Sails</span></a></li>
                        <li class="">

                            <a href="itineraries.html" class="MenuBarItemSubmenu"><span>Itineraries</span></a>
               			    <ul>
                                <li><a href="itineraries.html"><span>2 days 1 nights</span></a></li>
                                <li><a href="3_days_escape_to_legendary_halong_bay_with_oriental_sails.html"><span>3 days 2 nights</span></a></li>                                
                            </ul>
                        </li> 
                        <li class="">
                        	<a href="photos.html"><span>Photos</span></a></li>

                        <li >
                            <a href="promotion.html"><span>Promotions</span></a></li>
                        <li >
                            <a href="download.html"><span>Download</span></a></li>
                        <li >
                            <a href="Login.aspx?ReturnUrl=/Modules/Sails/Admin/BookingList.aspx?NodeId=1%26SectionId=15"><span>Agent Login</span></a></li>  
                        </ul>
                        <script type="text/javascript">
                        <!--
                        var MenuBar1 = new navigation.Widget.MenuBar("tabmenu", {imgDown:"images/SpryMenuBarDownHover.gif", imgRight:"images/SpryMenuBarRightHover.gif"});
                        //-->
                        </script>        
              </div>                   </div>

            </td>
          </tr>
        </table>
    </div>
    <div style="clear:both"></div>
</div>
<div class="frflash">
<object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=9,0,28,0" width="989" height="225">
  <param name="movie" value="http://orientalsails.com/images/oriental-sails.swf" />
  <param name="quality" value="high" />
  <param name="wmode" value="transparent" />

  <embed src="http://orientalsails.com/images/oriental-sails.swf" width="989" height="225" quality="high" pluginspage="http://www.adobe.com/shockwave/download/download.cgi?P1_Prod_Version=ShockwaveFlash" type="application/x-shockwave-flash" wmode="transparent"></embed>
</object>
</div>
<!--Oriental Sails: Halong Bay pictures-->

<!--Oriental Sails Content-->
<div id="container">
	<table width="100%" border="0" cellpadding="0" cellspacing="0" height="auto">
      <tr>
        <td width="249" align="left" valign="top">
         <div class="box-left">
         	<div class="content-left">
         	    
	        </div> 
          </div>
        </td>
        <td width="689" align="left" valign="top" >
      <div class="box-right">
              <div id="content">
              	<asp:placeholder id="maincontent" runat="server"></asp:placeholder>
              </div>
      </div>        </td>
      </tr>
      <tr>
      	<td align="center" valign="bottom" ><img src="/templates/os/images/logo-footer.jpg" width="126" height="96" /> 		</td>
        <td align="right" valign="bottom" >
        	<div class="footer">
            	<div class="footer-bar"><img src="/templates/os/images/footer-line.jpg" /></div>

                <div class="footer-text">
                	<div class="contact">2009 <a href="http://www.oriental-sails.com"> www.orientalsails.com</a>. All Rights Reserved <br />
21 Luong Ngoc Quyen Str., Hoan Kiem Dist, Hanoi, Vietnam - Tel: 84-4-39264009 - Fax: 84-4-39264010<br />
Email: <a href="mailto:sales@oriental-sails.com">sales@orientalsails.com </a></div>
                </div>
            </div>

        </td>
      </tr>
    </table>

</div>

</center>
</form>
</body>
</html>
