<%@ Page Language="C#" MasterPageFile="SailsMaster.Master" AutoEventWireup="true"
    Codebehind="CruisesEdit.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.CruisesEdit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<%@ Register Assembly="CMS.ServerControls" Namespace="CMS.ServerControls.FileUpload"
    TagPrefix="svc" %>
<%@ Register Assembly="CMS.ServerControls" Namespace="CMS.ServerControls" TagPrefix="svc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <fieldset>
        <legend>
            <img alt="Room" src="../Images/sails.gif" align="absMiddle" />
            <asp:Label ID="titleSailsTripEdit" runat="server"></asp:Label>
        </legend>
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
                        <td>
                            <asp:Label ID="lblCruiseCode" runat="server" Text="Số hiệu tàu"></asp:Label>    
                        </td>
                        <td>
                            <asp:TextBox ID="txtCruiseCode" runat="server"></asp:TextBox>
                        </td>

                        <td><%= base.GetText("textTripCode") %></td>
                        <td><asp:TextBox ID="txtCode" runat="server" MaxLength="5"></asp:TextBox></td>
                        <td>
                            <asp:HyperLink ID="hplRoomPlan" runat="server" Text="Current room plan" Visible="false"></asp:HyperLink>
                            <asp:Literal ID="litRoomPlan" runat="server" Text="Upload room plan" Visible="true"></asp:Literal>
                        </td>
                        <td><asp:FileUpload ID="fileRoomPlan" runat="server" /></td>
                    </tr>
                </table>
            </div>
            <div class="advancedinfo">
                <ul>
                    <asp:Repeater ID="rptTrips" runat="server" OnItemDataBound="rptTrips_ItemDataBound">
                        <ItemTemplate>
                            <li>
                            <asp:CheckBox ID="chkTrip" runat="server" />
                            <asp:HiddenField ID="hiddenId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "Id") %>' />
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
            <div class="advancedinfo">
                <h4><asp:Label ID="labelDescription" runat="server"></asp:Label></h4>
                <FCKeditorV2:FCKeditor ID="fckDescription" runat="server" Width="100%" Height="300"
                        BasePath="~/support/fckeditor/" ToolbarSet="Basic">
                </FCKeditorV2:FCKeditor>
            </div>
            
            <%--<div class="advancedinfo">
                <h4><asp:Label ID="labelTripImage" runat="server"></asp:Label></h4>
                <svc:FileUploaderAJAX runat="server" ID="fileUploaderMap" />
                <asp:TextBox ID="textBoxHiddenMap" runat="server" Style="display: none;"></asp:TextBox>
                <div id="divMap" style="overflow: auto">
                </div>
            </div>--%>
            
            <asp:Button ID="buttonSave" runat="server" OnClick="buttonSave_Click" CssClass="button" ValidationGroup="valid"/>
            <%--<asp:Button ID="buttonCancel" runat="server" OnClick="buttonCancel_Clicl" CssClass="button" />--%>
        </div>
    </fieldset>
</asp:Content>