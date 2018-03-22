<%@ Page Language="C#" MasterPageFile="SailsMaster.Master" AutoEventWireup="true"
    Codebehind="BalanceReport.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.BalanceReport"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <fieldset>
        <legend>
            <img alt="Room" src="../Images/sails.gif" align="absMiddle" />
            Balance report </legend>
        <div>
            <ul style="list-style: none; padding: 0px; padding-top: 5px; margin: 0px; margin-top: 10px;"
                class="tabbutton">
                <asp:Repeater ID="rptCruises" runat="server" OnItemDataBound="rptCruises_ItemDataBound">
                    <HeaderTemplate>
                        <li id="liMenu" runat="server">
                            <asp:HyperLink ID="hplCruises" runat="server" Text="All"></asp:HyperLink>
                        </li>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <li id="liMenu" runat="server">
                            <asp:HyperLink ID="hplCruises" runat="server"></asp:HyperLink>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>
        <div class="search_panel">
            <table>
                <tr>
                    <td>
                        Month</td>
                    <td>
                        <asp:DropDownList ID="ddlMonths" runat="server">
                            <asp:ListItem>-- All months --</asp:ListItem>
                            <asp:ListItem>1</asp:ListItem>
                            <asp:ListItem>2</asp:ListItem>
                            <asp:ListItem>3</asp:ListItem>
                            <asp:ListItem>4</asp:ListItem>
                            <asp:ListItem>5</asp:ListItem>
                            <asp:ListItem>6</asp:ListItem>
                            <asp:ListItem>7</asp:ListItem>
                            <asp:ListItem>8</asp:ListItem>
                            <asp:ListItem>9</asp:ListItem>
                            <asp:ListItem>10</asp:ListItem>
                            <asp:ListItem>11</asp:ListItem>
                            <asp:ListItem>12</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        Year</td>
                    <td>
                        <asp:TextBox ID="txtYear" runat="server"></asp:TextBox></td>
                </tr>
            </table>
        </div>
        <asp:Button ID="btnDisplay" runat="server" OnClick="btnDisplay_Click" Text="Display"
            CssClass="button" />
        <div class="data_table">
            <div class="data_grid">
                <table>
                    <asp:Repeater ID="rptBalance" runat="server" OnItemDataBound="rptBalance_ItemDataBound">
                        <HeaderTemplate>
                            <tr>
                                <th>
                                    Date</th>
                                <th>
                                    Income</th>
                                <!--<th>Receivable</th>-->
                                <th>
                                    Expense</th>
                                <!--<th>Payable</th>-->
                                <th>
                                    Balance</th>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:Literal ID="litDate" runat="server"></asp:Literal></td>
                                <td>
                                    <asp:Literal ID="litIncome" runat="server"></asp:Literal></td>
                                <!--<td><asp:Literal ID="litReceivable" runat="server"></asp:Literal></td>-->
                                <td>
                                    <asp:Literal ID="litExpense" runat="server"></asp:Literal></td>
                                <!--<td><asp:Literal ID="litPayable" runat="server"></asp:Literal></td>-->
                                <td>
                                    <asp:Literal ID="litBalance" runat="server"></asp:Literal></td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            <tr>
                                <td>
                                    GRAND TOTAL</td>
                                <td>
                                    <asp:Literal ID="litIncome" runat="server"></asp:Literal></td>
                                <!--<td><asp:Literal ID="litReceivable" runat="server"></asp:Literal></td>-->
                                <td>
                                    <asp:Literal ID="litExpense" runat="server"></asp:Literal></td>
                                <!--<td><asp:Literal ID="litPayable" runat="server"></asp:Literal></td>-->
                                <td>
                                    <asp:Literal ID="litBalance" runat="server"></asp:Literal></td>
                            </tr>
                        </FooterTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </div>
    </fieldset>
</asp:Content>
