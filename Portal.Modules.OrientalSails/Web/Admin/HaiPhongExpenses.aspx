<%@ Page Language="C#" MasterPageFile="SailsMaster.Master" AutoEventWireup="true"
    CodeBehind="HaiPhongExpenses.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.HaiPhongExpenses"
    Title="Chi phí Hải Phong" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <script>
        $(function () {
            $("#tabs").tabs();
        });
    </script>
    <fieldset>
        <legend>
            <img alt="Room" src="../Images/sails.gif" align="absMiddle" />
            Chi phí Hải phòng - Tàu
            <asp:Literal ID="ltrCruiseName" runat="server" />
            - Hợp đồng
            <asp:Literal ID="ltrContractName" runat="server" />
            - Áp dụng từ
            <asp:Literal ID="ltrContractApplyFrom" runat="server" />
        </legend>
    </fieldset>
    <div id="tabs">
        <ul>
            <asp:Repeater runat="server" ID="rptExpenseTypeTitle">
                <ItemTemplate>
                    <li><a href="#<%# Eval("HaiPhongExpenseType.HaiPhongExpenseTypeId")%>">
                        <%# Eval("HaiPhongExpenseType.Name")%></a></li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
        <asp:Repeater runat="server" ID="rptExpenseType" OnItemDataBound="rptExpenseType_ItemDataBound">
            <ItemTemplate>
                <div id="<%# Eval("HaiPhongExpenseType.HaiPhongExpenseTypeId") %>">
                    <table>
                        <asp:Repeater ID="rptExpenseCharter" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td><%# Eval("Name") %></td>
                                    <td><input></input></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                        <asp:Repeater ID="rptExpenseCustomerType" runat="server" OnItemDataBound="rptExpenseCustomerType_ItemDataBound">
                            <ItemTemplate>
                                <tr>
                                    <th style="font-size: 14px">
                                        <%# Eval("HaiPhongExpenseCustomerType.Name") %>
                                    </th>
                                </tr>
                                <asp:Repeater ID="rptExpense" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <%# Eval("Name")%>
                                            </td>
                                            <td>
                                                <input></input>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <asp:Repeater ID="rptReduceExpense" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <%# Eval("Name")%>
                                            </td>
                                            <td>
                                                <input></input>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
