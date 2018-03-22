<%@ Page Language="C#" MasterPageFile="SailsMaster.Master" AutoEventWireup="true"
    Codebehind="ExpensePeriod.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.ExpensePeriod"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <fieldset>
        <legend></legend>
        <!--<div>
            <ul style="list-style: none; padding: 0px; padding-top: 5px; margin: 0px; margin-top: 10px;"
                class="tabbutton">
                <asp:Repeater ID="rptCruises" runat="server" OnItemDataBound="rptCruises_ItemDataBound">
                    <ItemTemplate>
                        <li id="liMenu" runat="server">
                            <asp:HyperLink ID="hplCruises" runat="server"></asp:HyperLink>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>-->
        <div style="width: 600px;">
            <div class="basicinfo">
                <table>
                    <tr>
                        <td>
                            <asp:HyperLink ID="hplPrevious" runat="server" Text="Previous month"></asp:HyperLink></td>
                        <td>
                            <asp:Literal ID="litCurrent" runat="server"></asp:Literal></td>
                        <td>
                            <asp:HyperLink ID="hplNext" runat="server" Text="Next month"></asp:HyperLink></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DropDownList ID="ddlMonths" runat="server">
                            </asp:DropDownList></td>
                        <td>
                            <asp:TextBox ID="txtYear" runat="server"></asp:TextBox></td>
                        <td>
                            <asp:Button ID="btnGo" runat="server" Text="Go" CssClass="button" OnClick="btnGo_Click" /></td>
                    </tr>
                </table>
            </div>
            <div class="data_table">
                <h4>
                    Monthly</h4>
                <div class="data_grid">
                    <table>
                        <asp:Repeater ID="rptMonthlyExpenses" runat="server" OnItemDataBound="rptMonthlyExpenses_ItemDataBound">
                            <ItemTemplate>
                                <asp:PlaceHolder ID="plhGroupFooter" runat="server" Visible="false">
                                    <tr>
                                        <td colspan="2">
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtFooter" runat="server" ReadOnly="true" style="text-align:right; font-weight:bold;width:100px;"></asp:TextBox>
                                        </td>
                                    </tr>
                                </asp:PlaceHolder>
                                <asp:PlaceHolder ID="plhGroupHeader" runat="server" Visible="false">
                                    <tr>
                                        <td colspan="3">
                                            <strong>
                                                <asp:Literal ID="litHeader" runat="server"></asp:Literal></strong>
                                        </td>
                                    </tr>
                                </asp:PlaceHolder>
                                <tr>
                                    <td>
                                        <asp:HiddenField ID="hiddenTypeId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "Id") %>' />
                                        <asp:Literal ID="litName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Name") %>'></asp:Literal>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDetail" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:HiddenField ID="hiddenId" runat="server" />
                                        <asp:TextBox ID="txtCost" runat="server" Style="text-align: right; width:100px;"></asp:TextBox></td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                <tr>
                                    <td colspan="2">
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFooter" runat="server" ReadOnly="true" style="text-align:right; font-weight:bold;width:100px;"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <strong>TOTAL</strong>
                                    </td>
                                    <td>
                                            <asp:TextBox ID="txtTotal" runat="server" ReadOnly="true" style="text-align:right; font-weight:bold;width:100px;"></asp:TextBox>
                                    </td>
                                </tr>
                            </FooterTemplate>
                        </asp:Repeater>
                    </table>
                </div>
            </div>
            <div class="advancedinfo">
                <h4>
                    Yearly</h4>
                <table>
                    <asp:Repeater ID="rptYearlyExpenses" runat="server" OnItemDataBound="rptYearlyExpenses_ItemDataBound">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:HiddenField ID="hiddenTypeId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "Id") %>' />
                                    <%# DataBinder.Eval(Container.DataItem, "Name") %>
                                </td>
                                <td>
                                    <asp:HiddenField ID="hiddenId" runat="server" />
                                    <asp:TextBox ID="txtCost" runat="server" Style="text-align: right;"></asp:TextBox></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" CssClass="button" />
        </div>
    </fieldset>
</asp:Content>
