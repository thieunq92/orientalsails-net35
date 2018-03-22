<%@ Page Language="C#" MasterPageFile="SailsMaster.Master" AutoEventWireup="true" CodeBehind="RoomEdit.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.RoomEdit" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <fieldset>
        <legend>
            <img alt="Room" src="../Images/sails.gif" align="absMiddle" />
            <asp:Label ID="titleRoomEdit" runat="server"></asp:Label>
        </legend>
        <em>WARNING: Any changes apply on room type may cause error to the system (because report calculation engine has been designed to fit Oriental Sails)</em><br />
        <em>WARNING: New added room will cause error to the booking system (because of the specific room-map (oriental sail map) has been configurated)</em>
        <div class="settinglist">
            <div class="basicinfo">
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="labelName" runat="server"></asp:Label>    
                        </td>
                        <td>
                            <asp:TextBox ID="textBoxName" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvName" runat="server" ValidationGroup="valid" ControlToValidate="textBoxName" ErrorMessage="Requied Field"></asp:RequiredFieldValidator>
                        </td>
                        <td><%# base.GetText("textCruise") %></td>
                        <td><asp:DropDownList ID="ddlCruises" runat="server"></asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="labelRoomTypex" runat="server"></asp:Label>
                        </td>
                        <td>
                           <asp:DropDownList ID="ddlRoomTypex" runat="server"></asp:DropDownList>
                        </td>
                        <td>
                            <asp:Label ID="labelRoomClass" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlRoomClass" runat="server"></asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </div>
            <asp:Button ID="buttonSave" runat="server" OnClick="buttonSave_Click" CssClass="button" ValidationGroup="valid"/>
            <asp:Button ID="buttonCancel" runat="server" OnClick="buttonCancel_Clicl" CssClass="button" />
        </div>
    </fieldset>
</asp:Content>
