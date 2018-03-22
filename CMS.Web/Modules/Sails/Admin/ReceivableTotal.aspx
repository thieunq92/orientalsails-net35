<%@ Page Language="C#" MasterPageFile="SailsMaster.Master" AutoEventWireup="true"
    Codebehind="ReceivableTotal.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.ReceivableTotal"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">    
    <ajax:TabContainer ID="tabContainerAgencies" runat="server">
        <ajax:TabPanel ID="tabReceivable" runat="server" HeaderText="Receivable">
            <ContentTemplate>
                <div class="data_grid">
                    <table>
                        <tr>
                            <th>
                                No.</th>
                            <th>
                                Agency name</th>
                            <th>
                                Total receivable</th>
                        </tr>
                        <asp:Repeater ID="rptAgencies" runat="server" OnItemDataBound="rptAgencies_ItemDataBound">
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <asp:Literal ID="litNo" runat="server"></asp:Literal></td>
                                    <td>
                                        <asp:HyperLink ID="hplName" runat="server"></asp:HyperLink></td>
                                    <td>
                                        <asp:Literal ID="litTotal" runat="server"></asp:Literal></td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                <tr>
                                    <td colspan="2">
                                        GRAND TOTAL
                                    </td>
                                    <td>
                                        <asp:Literal ID="litTotal" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                            </FooterTemplate>
                        </asp:Repeater>
                    </table>
                </div>
            </ContentTemplate>
        </ajax:TabPanel>
        <ajax:TabPanel ID="tabPayable" runat="server" HeaderText="Payable">
            <ContentTemplate>
                <div class="data_grid">
                    <table>
                        <tr>
                            <th>
                                No.</th>
                            <th>
                                Agency name</th>
                            <th>
                                Total payable</th>
                        </tr>
                        <asp:Repeater ID="rptPayables" runat="server" OnItemDataBound="rptPayables_ItemDataBound">
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <asp:Literal ID="litNo" runat="server"></asp:Literal></td>
                                    <td>
                                        <asp:HyperLink ID="hplName" runat="server"></asp:HyperLink></td>
                                    <td>
                                        <asp:Literal ID="litTotal" runat="server"></asp:Literal></td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                <tr>
                                    <td colspan="2">
                                        GRAND TOTAL
                                    </td>
                                    <td>
                                        <asp:Literal ID="litTotal" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                            </FooterTemplate>
                        </asp:Repeater>
                    </table>
                </div>
            </ContentTemplate>
        </ajax:TabPanel>
    </ajax:TabContainer>
</asp:Content>
