<%@ Page Title="" Language="C#" MasterPageFile="SailsMaster.Master" AutoEventWireup="true" CodeBehind="DocumentView.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.DocumentView" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <fieldset>
        <legend>Documents</legend>
        
        <ajax:TabContainer runat="server" ID="tabDocuments">
          <Tabs>
              
          </Tabs>
        </ajax:TabContainer>
    </fieldset>
</asp:Content>
