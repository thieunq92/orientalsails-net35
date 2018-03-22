<%@ Page Title="" Language="C#" MasterPageFile="Popup.Master" AutoEventWireup="true"
    CodeBehind="EditMeeting.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.EditMeeting" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <fieldset>
        <legend>Meeting with</legend>
        <div class="basicinfo">
            <table>
                <tr>
                    <td>
                        Name :
                    </td>
                    <td>
                        <asp:Literal runat="server" ID="litContact"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td>
                        Position :
                    </td>
                    <td>
                        <asp:Literal runat="server" ID="litPosition"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td>
                        Date Meeting :
                    </td>
                    <td>
                        <asp:TextBox ID="txtDateMeeting" runat="server" Width="178px" ReadOnly="true"></asp:TextBox>
                    </td>
                    <ajax:CalendarExtender ID="cldDateMeeting" runat="server" TargetControlID="txtDateMeeting"
                        Format="dd/MM/yyyy">
                    </ajax:CalendarExtender>
                </tr>
                <tr>
                    <td>
                        Note :
                    </td>
                    <td>
                        <asp:TextBox runat="server" TextMode="MultiLine" ID="txtNote" Wrap="True" Style="width: 100%;
                            height: 220px;"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
        <asp:Button runat="server" ID="btnSave" OnClick="btnSave_Click" />
    </fieldset>
</asp:Content>
