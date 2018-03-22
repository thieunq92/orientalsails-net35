<%@ Page Language="C#" MasterPageFile="SailsMaster.Master" AutoEventWireup="true"
    Codebehind="Costing.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.Costing"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <fieldset>
        <legend>
            <img alt="Room" src="../Images/sails.gif" align="absMiddle" />
            <%= base.GetText("textCostingConfiguration") %> </legend>
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
    
    function ddltype_changed(id, optionid, vids)
    {
        ddltype = document.getElementById(id);
        if (vids.indexOf('#' + ddltype.options[ddltype.selectedIndex].value + '#') >= 0)
        {
            setVisible(optionid, true);
        }
        else
        {
            setVisible(optionid, false);
        }
//        switch (ddltype.selectedIndex)
//        {
//            case 0:
//                setVisible(optionid, false);
//            break;
//            case 1:
//                setVisible(optionid, true);
//            break;
//        }
    }
    </script>
        <div class="advancedinfo">
            <table>
                <tr>
                    <th>
                        <%= base.GetText("textValidFrom") %>
                    </th>
                    <th>
                        <%= base.GetText("textTrip") %>
                    </th>
                    <td>
                        <%= base.GetText("textOption") %>
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
                                <asp:Literal ID="litTrip" runat="server"></asp:Literal>
                            </td>
                            <td>
                                <asp:Literal ID="litOption" runat="server"></asp:Literal>
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
                        <%= base.GetText("textValidFrom") %></td>
                    <td>
                        <asp:TextBox ID="txtValidFrom" runat="server">
                        </asp:TextBox></td>
                    <td>
                        <%= base.GetText("textTrip") %></td>
                    <td>
                        <asp:DropDownList ID="ddlTrips" runat="server">
                        </asp:DropDownList></td>
                    <td>
                        <asp:DropDownList ID="ddlOptions" runat="server" Width="80" Style="display: none;">
                            <asp:ListItem>Option 1</asp:ListItem>
                            <asp:ListItem>Option 2</asp:ListItem>
                        </asp:DropDownList></td>
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
                                <%= base.GetText("textCostType") %></td>
                            <td>
                                <%= base.GetText("textAdultCost") %></td>
                            <td>
                                <%= base.GetText("textChildCost") %></td>
                            <td>
                                <%= base.GetText("textBabyCost") %></td>
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
                            <td>
                                <asp:TextBox ID="txtChild" runat="server"></asp:TextBox></td>
                            <td>
                                <asp:TextBox ID="txtBaby" runat="server"></asp:TextBox></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button" OnClick="btnSave_Click" />
    </fieldset>
</asp:Content>
