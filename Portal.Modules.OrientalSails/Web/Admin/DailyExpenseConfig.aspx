<%@ Page Language="C#" MasterPageFile="SailsMaster.Master" AutoEventWireup="true"
    Codebehind="DailyExpenseConfig.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.DailyExpenseConfig"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <fieldset>
        <legend>
            <img alt="Room" src="../Images/sails.gif" align="absMiddle" />
            <%= base.GetText("textCostingConfiguration") %>
        </legend>

        <script type="text/javascript">
    function setVisible(id, visible)
    {
        control = document.getElementById(id);
        if (visible)
        {control.style.display = "";}
        else
        {
        control.style.display = "none";
        }
        
    }
    
    function ddltype_changed(id, optionid)
    {
        ddltype = document.getElementById(id);
        switch (ddltype.selectedIndex)
        {
            case 0:
                setVisible(optionid, false);
            break;
            case 1:
                setVisible(optionid, true);
            break;
        }
    }
        </script>

        <div class="advancedinfo">
            <table>
                <tr>
                    <th>
                        <%= base.GetText("textValidFrom") %>
                    </th>
                    <th>
                    </th>
                    <td>
                    </td>
                    <th>
                    </th>
                </tr>
                <asp:Repeater ID="rptCostingTable" runat="server" OnItemDataBound="rptCostingTable_ItemDataBound"
                    OnItemCommand="rptCostingTable_ItemCommand">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:Literal ID="litValidFrom" runat="server"></asp:Literal></td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:HyperLink ID="hyperLinkEdit" runat="server">
                                    <asp:Image ID="imageEdit" runat="server" ImageAlign="AbsMiddle" AlternateText="Edit"
                                        CssClass="image_button16" ImageUrl="../Images/edit.gif" />
                                </asp:HyperLink>
                                <asp:ImageButton runat="server" ID="imageButtonDelete" ToolTip='Delete' ImageUrl="../Images/delete_file.gif"
                                    AlternateText='Delete' ImageAlign="AbsMiddle" CssClass="image_button16" CommandName="Delete"
                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem,"Id") %>' />
                                <ajax:ConfirmButtonExtender ID="ConfirmButtonExtenderDelete" runat="server" TargetControlID="imageButtonDelete"
                                    ConfirmText='<%# base.GetText("messageConfirmDelete") %>'>
                                </ajax:ConfirmButtonExtender>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
        <asp:Button ID="btnAdd" runat="server" Text="Add new table" CssClass="button" OnClick="btnAdd_Click"
            Visible="false" />
        <div class="basicinfo">
            <table>
                <tr>
                    <td>
                        <%= base.GetText("textValidFrom") %>
                    </td>
                    <td>
                        <asp:TextBox ID="txtValidFrom" runat="server">
                        </asp:TextBox></td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
            <ajax:CalendarExtender ID="calendarValidFrom" runat="server" TargetControlID="txtValidFrom"
                Format="dd/MM/yyyy">
            </ajax:CalendarExtender>
            <table>
                <asp:Repeater ID="rptServices" runat="server" OnItemDataBound="rptServices_ItemDataBound">
                    <HeaderTemplate>
                        <tr>
                            <td>
                                <%= base.GetText("textCostType") %>
                            </td>
                            <td>
                                <%= base.GetText("textAdultCost") %>
                            </td>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:Literal ID="litName" runat="server"></asp:Literal>
                                <asp:HiddenField ID="hiddenId" runat="server" />
                                <asp:HiddenField ID="hiddenType" runat="server" />
                            </td>
                            <td>
                                <asp:TextBox ID="txtAdult" runat="server"></asp:TextBox></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button" OnClick="btnSave_Click" />
    </fieldset>
</asp:Content>
