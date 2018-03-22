<%@ Page Language="C#" MasterPageFile="SailsMaster.Master" AutoEventWireup="true"
    Codebehind="AgentPriceList.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.AgentPriceList"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.Extensions" Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <asp:Panel ID="panelContent" runat="server">
        <fieldset>
            <legend>
                <asp:Label ID="labelAgencyPricing" runat="server" Text="Agency Pricing"></asp:Label></legend>
            <div class="settinglist">
                <div class="basicinfo">
                    <%= base.GetText("labelRole") %>
                    <asp:Label runat="server" ID="lblRoleName"></asp:Label><br />
                    <%= base.GetText("textInclude") %>
                    <asp:Label runat="server" ID="lblUserCount"></asp:Label><%= base.GetText("textUser") %>.
                    <div class="errorbox">
                        <asp:Label runat="server" ID="lblError"></asp:Label>
                    </div>
                </div>
                <div class="data_table">
                    <div class="data_grid">
                        <table cellspacing="0" cellpadding="0">
                            <asp:Repeater ID="rptPrices" runat="server" OnItemCommand="rptPrices_ItemCommand"
                                OnItemDataBound="rptPrices_DataBound">
                                <ItemTemplate>
                                    <tr style="text-align: right;">
                                        <td>
                                            <%# DataBinder.Eval(Container.DataItem,"CostFrom") %>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblCostTo"></asp:Label></td>
                                        <td>
                                            <asp:Label runat="server" ID="lblApply"></asp:Label></td>
                                        <td>
                                            <asp:LinkButton runat="server" ID="hplEdit" CommandName="edit" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"Id") %>'
                                                Text="Edit"></asp:LinkButton>
                                        </td>
                                        <td>
                                            <asp:LinkButton runat="server" ID="linkButtonDelete" CommandName="delete" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"Id") %>'
                                                Text="Delete"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <HeaderTemplate>
                                    <tr>
                                        <th style="width: 30%;">
                                            Cost from
                                        </th>
                                        <th style="width: 30%;">
                                            Cost to
                                        </th>
                                        <th style="width: 30%;">
                                            Apply
                                        </th>
                                        <th colspan="2">
                                            Action
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
                </div>
                <asp:Button runat="server" ID="btnAddPrice" Text="Add rule" OnClick="btnAddPrice_Click"
                    CssClass="button" />
            </div>
        </fieldset>
    </asp:Panel>
    <asp:Panel runat="server" CssClass="modalPopup" ID="programmaticPopup" Style="display: none;
        width: 350px; padding: 10px">
        <asp:Panel runat="Server" ID="programmaticPopupDragHandle" Style="cursor: move; background-color: #DDDDDD;
            border: solid 1px Gray; color: Black; text-align: center;">
            Thêm mức giá
        </asp:Panel>
        <asp:TextBox runat="server" ID="txtHiddenId" Style="display: none;"></asp:TextBox>
        Từ
        <asp:TextBox runat="server" ID="txtFrom"></asp:TextBox><br />
        Đến
        <asp:TextBox runat="server" ID="txtTo"></asp:TextBox><br />
        Áp dụng mức
        <asp:TextBox runat="server" ID="txtPercentage"></asp:TextBox><br />
        <asp:DropDownList ID="ddlUnit" runat="server">
            <asp:ListItem>%
            </asp:ListItem>
            <asp:ListItem>$</asp:ListItem>
        </asp:DropDownList>
        <asp:Button runat="server" ID="btnOK" Text="OK" OnClick="btnOK_Click" />
        <input type="button" id="hideModalPopupViaClientButtonCancel" value="Cancel" />
        <br />
    </asp:Panel>
    <asp:Button runat="server" ID="hiddenTargetControlForModalPopup" Style="display: none;" />

    <script type="text/javascript">       
        // Add click handlers for buttons to show and hide modal popup on pageLoad
        function pageLoad() {
            $addHandler($get("hideModalPopupViaClientButtonCancel"), 'click', hideModalPopupViaClient);                          
        }
        
        function showModalPopupViaClient(ev) {
            ev.preventDefault();
            var modalPopupBehavior = $find('programmaticModalPopupBehavior');
            modalPopupBehavior.show();
        }
        
        function hideModalPopupViaClient(ev) {
            ev.preventDefault();        
            var modalPopupBehavior = $find('programmaticModalPopupBehavior');
            modalPopupBehavior.hide();
        }
    </script>

    <cc1:ModalPopupExtender runat="server" ID="programmaticModalPopup" BehaviorID="programmaticModalPopupBehavior"
        TargetControlID="hiddenTargetControlForModalPopup" PopupControlID="programmaticPopup"
        BackgroundCssClass="modalBackground" DropShadow="True" PopupDragHandleControlID="programmaticPopupDragHandle"
        RepositionMode="RepositionOnWindowScroll">
    </cc1:ModalPopupExtender>
</asp:Content>
