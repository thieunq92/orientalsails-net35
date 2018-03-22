<%@ Page Language="C#" MasterPageFile="SailsMaster.Master" AutoEventWireup="true" CodeBehind="SailsTripEdit.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.SailsTripEdit" Title="Untitled Page" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<%@ Register Assembly="CMS.ServerControls" Namespace="CMS.ServerControls.FileUpload" TagPrefix="cc2" %>
<%@ Register Assembly="CMS.ServerControls" Namespace="CMS.ServerControls" TagPrefix="cc1" %> 
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
                        <td><%= base.GetText("textTripCode") %></td>
                        <td><asp:TextBox ID="txtTripCode" runat="server" MaxLength="5"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="labelNumberOfDay" runat="server"></asp:Label>
                        </td>
                        <td>
                           <asp:TextBox ID="textBoxNumberOfDay" runat="server"></asp:TextBox>
                           <asp:DropDownList ID="ddlHalfDay" runat="server">
                                <asp:ListItem Value="0">All day</asp:ListItem>
                                <asp:ListItem Value="1">Morning</asp:ListItem>
                                <asp:ListItem Value="2">Afternoon</asp:ListItem>
                           </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Label ID="labelNumberOfOptions" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlNumberOfOptions" runat="server">
                                <asp:ListItem Text="0" Value="0"></asp:ListItem> 
                                <asp:ListItem Text="2" Value="2"></asp:ListItem> 
                                <asp:ListItem Text="3" Value="3"></asp:ListItem> 
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="advancedinfo">
                <h4><asp:Label ID="labelDescription" runat="server"></asp:Label></h4>
                <FCKeditorV2:FCKeditor ID="fckDescription" runat="server" Width="100%" Height="300"
                        BasePath="~/support/fckeditor/" ToolbarSet="Basic">
                </FCKeditorV2:FCKeditor>
            </div>
            <div class="advancedinfo">
                <h4><asp:Label ID="labelItinerary" runat="server"></asp:Label></h4>
                <FCKeditorV2:FCKeditor ID="fckItinerary" runat="server" Width="100%" Height="300"
                        BasePath="~/support/fckeditor/" ToolbarSet="Basic">
                </FCKeditorV2:FCKeditor>
            </div>
            <div class="advancedinfo">
                <h4><asp:Label ID="labelInclusions" runat="server"></asp:Label></h4>
                <FCKeditorV2:FCKeditor ID="fckInclusions" runat="server" Width="100%" Height="300"
                        BasePath="~/support/fckeditor/" ToolbarSet="Basic">
                </FCKeditorV2:FCKeditor>
            </div>
            <div class="advancedinfo">
                <h4><asp:Label ID="labelExclusions" runat="server"></asp:Label></h4>
                <FCKeditorV2:FCKeditor ID="fckExclusions" runat="server" Width="100%" Height="300"
                        BasePath="~/support/fckeditor/" ToolbarSet="Basic">
                </FCKeditorV2:FCKeditor>
            </div>
            <div class="advancedinfo">
                <h4><asp:Label ID="labelWhatToTake" runat="server"></asp:Label></h4>
                <FCKeditorV2:FCKeditor ID="fckWhatToTake" runat="server" Width="100%" Height="300"
                        BasePath="~/support/fckeditor/" ToolbarSet="Basic">
                </FCKeditorV2:FCKeditor>
            </div>
            <div class="advancedinfo">
                <h4><asp:Label ID="labelTripImage" runat="server"></asp:Label></h4>
                <cc2:FileUploaderAJAX runat="server" ID="fileUploaderMap" />
                <asp:TextBox ID="textBoxHiddenMap" runat="server" Style="display: none;"></asp:TextBox>
                <div id="divMap" style="overflow: auto">
                </div>
            </div>
            
            <asp:Button ID="buttonSave" runat="server" OnClick="buttonSave_Click" CssClass="button" ValidationGroup="valid"/>
            <asp:Button ID="buttonCancel" runat="server" OnClick="buttonCancel_Clicl" CssClass="button" />
        </div>
    </fieldset>
</asp:Content>
