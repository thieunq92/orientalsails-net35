<%@ Page Language="C#" MasterPageFile="SailsMaster.Master" AutoEventWireup="true" CodeBehind="Forum.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.Forum" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
<script type=”text/javascript”>
// Firefox worked fine. Internet Explorer shows scrollbar because of frameborder
function resizeFrame(f) {
f.style.height = f.contentWindow.document.body.scrollHeight + 'px';
}
</script>
    <iframe id="childframe" src="/home/forum.aspx" width="100%" onload="resizeFrame(document.getElementById('childframe'))"></iframe>
</asp:Content>
