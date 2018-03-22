<%@ Page Language="C#" MasterPageFile="SailsMaster.Master" AutoEventWireup="true"
    Codebehind="CostTypes.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.CostTypes"
    Title="Untitled Page" %>

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
                    <asp:Repeater ID="rptServices" runat="server" OnItemDataBound="rptServices_ItemDataBound"
                        OnItemCommand="rptServices_ItemCommand">
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
                                <td>
                                    Group name
                                </td>
                                <td colspan="5">
                                    <asp:TextBox ID="txtGroupName" runat="server"></asp:TextBox>                                    
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkIsDailyInput" runat="server" Text="Daily input basis" /></td>
                                <td>
                                    <asp:CheckBox ID="chkIsCustomType" runat="server" Text="Have contact name and phone" /></td>
                                <td>
                                    <asp:CheckBox ID="chkIsSupplier" runat="server" Text="Is supplier" />
                                </td>
                                <!--<td>
                                    <asp:CheckBox ID="chkIsDaily" runat="server" Text="Is daily" />
                                </td>-->
                                <td>
                                    <asp:CheckBox ID="chkIsMonthly" runat="server" Text="Is monthly" />
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkIsYearly" runat="server" Text="Is yearly" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Default supplier</td>
                                <td colspan="5">
                                    <asp:DropDownList ID="ddlSuppliers" runat="server">
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td>
                                    Associated services</td>
                                <td colspan="5">
                                    <asp:DropDownList ID="ddlServices" runat="server">
                                    </asp:DropDownList></td>
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
