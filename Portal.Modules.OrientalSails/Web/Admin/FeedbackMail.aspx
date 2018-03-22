<%@ Page Language="C#" MasterPageFile="Popup.Master" AutoEventWireup="true" CodeBehind="FeedbackMail.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.FeedbackMail" Title="Untitled Page" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <div class="basicinfo">
        <table>
            <tr>
                <td>Send email to</td>
                <td><asp:TextBox ID="lblEmailTo" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Subject</td>
                <td><asp:TextBox ID="txtSubject" runat="server"/></td>
            </tr>
            <tr>
                <td></td>
                <td><asp:DropDownList ID="ddlTemplates" runat="server"></asp:DropDownList>
                <asp:Button runat="server" ID="btnLoad" OnClick="btnLoad_Click" CssClass="button" Text="Reload"/>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                <FCKeditorV2:FCKeditor ID="fckContent" runat="server" Width="100%" Height="300"
                        BasePath="~/support/fckeditor/" ToolbarSet="Basic">
                </FCKeditorV2:FCKeditor>
                </td>
            </tr>
        </table>
    </div>
    <asp:Button ID="btnSend" runat="server" OnClick="btnSend_Click" Text="Send" CssClass="button" />
</asp:Content>