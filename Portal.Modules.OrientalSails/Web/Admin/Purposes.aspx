<%@ Page Language="C#" MasterPageFile="SailsMaster.Master" AutoEventWireup="true" CodeBehind="Purposes.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.Purposes" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="System.Web.Extensions" Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="CMS.ServerControls" Namespace="CMS.ServerControls" TagPrefix="svc" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <fieldset>
        <legend>
            <%= base.GetText("titleCostType") %>
        </legend>
        <div class="settinglist">
            <div class="listbox">
                <ul>
                    <asp:Repeater ID="rptPurposes" runat="server" OnItemDataBound="rptPurposes_ItemDataBound"
                        OnItemCommand="rptPurposes_ItemCommand">
                        <ItemTemplate>
                            <li>
                                <asp:LinkButton ID='lbtEdit' runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Name")%>'
                                    CommandName="edit">
                                </asp:LinkButton>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
                <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" CssClass="button" Text="New service" />
            </div>
            <div class="wbox">
                <div class="wbox_title">
                    <asp:Label ID="labelFormTitle" runat="server" Text="Title"></asp:Label>
                </div>
                <div class="wbox_content">
                    <div class="group">
                        <h4>
                            <%= base.GetText("textGeneralInformation") %>
                        </h4>
                        <table>
                            <tr>
                                <td style="width: 100px;">
                                    <%= base.GetText("textName") %>
                                </td>
                                <td colspan="5">
                                    <asp:TextBox ID="txtServiceName" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvTypeOfService" runat="server" ControlToValidate="txtServiceName"
                                        Text="Type is required!" ValidationGroup="service"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>Code
                                </td>
                                <td><asp:TextBox ID="txtCode" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" ValidationGroup="service"
                            CssClass="button" />
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click"
                            CssClass="button" />
                        <ajax:ConfirmButtonExtender ID="ConfirmButtonExtenderDelete" runat="server" TargetControlID="btnDelete"
                            ConfirmText='Are you sure want to delete?'>
                        </ajax:ConfirmButtonExtender>
                    </div>
                </div>
            </div>
        </div>
    </fieldset>
</asp:Content>
