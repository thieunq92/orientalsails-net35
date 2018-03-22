<%@ Page Language="C#" MasterPageFile="SailsMaster.Master" AutoEventWireup="true"
    CodeBehind="HaiPhongContracts.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.HaiPhongContracts" Title = "Hợp đồng Hải Phong"%>

<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <fieldset>
        <legend>
            <img alt="Room" src="../Images/sails.gif" align="absMiddle" />
            Hợp đồng Hải Phong </legend>
    </fieldset>
    <div class="advancedinfo">
        <table>
            <tr>
                <th>
                    Id
                </th>
                <th>
                    Tên hợp đồng
                </th>
                <th>
                    Áp dụng từ
                </th>
                <th>
                </th>
                <th>
                </th>
                <th>
                </th>
                <th>
                </th>
            </tr>
            <asp:Repeater ID="rptHaiPhongContract" runat="server" OnItemDataBound="rptHaiPhongContract_ItemDataBound">
                <ItemTemplate>
                    <tr>
                        <td>
                            <%# Eval("HaiPhongContractId")%>
                        </td>
                        <td>
                            <%# Eval("Name")%>
                        </td>
                        <td>
                            <%# ((DateTime)Eval("ApplyFrom")).ToString("dd/MM/yyyy")%>
                        </td>
                        <asp:Repeater ID="rptCruise" runat="server" OnItemDataBound="rptCruise_ItemDataBound">
                            <ItemTemplate>
                                <td>
                                    <asp:Literal ID="ltrExpenseLink" runat="server"></asp:Literal>
                                </td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </div>
</asp:Content>
