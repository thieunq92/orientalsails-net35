<%@ Page Title="" Language="C#" MasterPageFile="Popup.Master" AutoEventWireup="true" CodeBehind="AgencyContractEdit.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.AgencyContractEdit" %>
<%@ Register Assembly="CMS.ServerControls" Namespace="CMS.ServerControls" TagPrefix="svc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
        <div class="basicinfo">
        <table>
            <tr>
                <td>Name</td>
                <td><asp:TextBox ID="txtName" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Expired date</td>
                <td><asp:TextBox ID="txtExpiredDate" runat="server"></asp:TextBox><ajax:CalendarExtender
                                ID="CalendarExtender1" runat="server" TargetControlID="txtExpiredDate" Format="dd/MM/yyyy">
                            </ajax:CalendarExtender></td>
            </tr>
            <tr>
                <td>Attach file</td>
                <td>
                    <asp:HyperLink runat="server" ID="hplOldFile"></asp:HyperLink>
                    <asp:FileUpload runat="server" ID="fileUpload"></asp:FileUpload>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <input runat="server" type="checkbox" id="chkReceived" style="width:0px;float:left"/>
                    <label>Received</label>
                </td>
            </tr>
            <tr>
                <td colspan="2"><asp:Literal runat="server" ID="litCreateDate"/></td>
            </tr>
        </table>
    </div>
    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button" OnClick="btnSave_Click" />
</asp:Content>
