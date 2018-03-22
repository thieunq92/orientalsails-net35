<%@ Page Language="C#" MasterPageFile="SailsMaster.Master" AutoEventWireup="true"
    CodeBehind="DocumentManage.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.DocumentManage"
    Title="Untitled Page" %>

<%@ Register Assembly="CMS.ServerControls" Namespace="CMS.ServerControls" TagPrefix="svc" %>
<%@ Register Assembly="Portal.Modules.OrientalSails" Namespace="Portal.Modules.OrientalSails.Web.Controls"
    TagPrefix="orc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <script type="text/javascript">
        function toggleVisible(id) {
            item = document.getElementById(id);
            if (item.style.display == "") {
                item.style.display = "none";
            }
            else {
                item.style.display = "";
            }
        }

        function setVisible(id, visible) {
            control = document.getElementById(id);
            if (visible)
            { control.style.display = ""; }
            else {
                control.style.display = "none";
            }

        }

        function ddltype_changed(id, optionid, vids) {
            ddltype = document.getElementById(id);
            if (vids.indexOf('#' + ddltype.options[ddltype.selectedIndex].value + '#') >= 0) {
                setVisible(optionid, true);
            }
            else {
                setVisible(optionid, false);
            }
        }

        function ddlagency_changed(id, codeid) {
            ddltype = document.getElementById(id);
            switch (ddltype.selectedIndex) {
                case 0:
                    setVisible(codeid, false);
                    break;
                default:
                    setVisible(codeid, true);
                    break;
            }
        }
    </script>
    <fieldset>
        <div class="listbox">
            <ul>
                <asp:Repeater ID="rptCategories" runat="server" OnItemDataBound="rptCategories_ItemDataBound">
                    <ItemTemplate>
                        <li>
                            <asp:HyperLink ID='hplEdit' runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Name")%>'
                                CommandName="edit">
                            </asp:HyperLink>
                            <ul>
                                <asp:Repeater ID="rptDocuments" runat="server" OnItemDataBound="rptDocuments_ItemDataBound">
                                    <ItemTemplate>
                                        <li>
                                            <asp:HyperLink ID='hplEdit' runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Name")%>'
                                                CommandName="edit">
                                            </asp:HyperLink></li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
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
                                Category
                            </td>
                            <td colspan="5">
                                <asp:DropDownList ID="ddlSuppliers" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td rowspan="2">
                                Document file
                            </td>
                            <td colspan="5">
                                <asp:HyperLink runat="server" ID="hplCurrentFile" Visible="False"></asp:HyperLink>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5">
                                <asp:FileUpload runat="server" ID="fileUpload"></asp:FileUpload>
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
    </fieldset>
</asp:Content>
