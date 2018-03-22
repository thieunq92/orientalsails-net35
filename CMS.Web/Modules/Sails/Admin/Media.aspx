<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="SailsMaster.Master" CodeBehind="Media.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.Media" Title= "Media"%>

<%@ Register Assembly="CMS.ServerControls" Namespace="CMS.ServerControls.FileUpload"
    TagPrefix="svc" %>
<%@ Register Assembly="CMS.ServerControls" Namespace="CMS.ServerControls" TagPrefix="svc" %>
<%@ Register Src="/Admin/Controls/UserSelector.ascx" TagPrefix="asp" TagName="UserSelector" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <fieldset>
        <legend>
            <img alt="Room" src="../Images/sails.gif" align="absMiddle" />
            <%= base.GetText("textMedia")%></legend>
        <iframe src="http://media.atravelmate.com" width="100%" height="1000px"></iframe> 
    </fieldset>
</asp:Content>

