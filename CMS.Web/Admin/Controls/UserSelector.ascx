<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserSelector.ascx.cs" Inherits="CMS.Web.Admin.Controls.UserSelector" %>
<script language="javascript" type="text/javascript">
</script>
<asp:Label ID="labelUserName" runat="server"></asp:Label>
<asp:HiddenField ID="hiddenId" runat="server" />
<input type="button" id="btnSelect" runat="server" class="button" value="Select" onclick="" />
<input type="button" id="btnRemove" runat="server" value="Remove" class="button" />
