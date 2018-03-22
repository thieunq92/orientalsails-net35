<%@ Page Language="C#" MasterPageFile="SailsMaster.Master" AutoEventWireup="true" CodeBehind="IncomeDate.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.IncomeDate" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <fieldset>
        <legend></legend>
        <div class="search_panel">
            <table>
                <tr>
                    <td><%= base.GetText("textDateToView") %></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
            </table>
        </div>
    </fieldset>
</asp:Content>
