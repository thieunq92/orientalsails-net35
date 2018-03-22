<%@ Page Title="" Language="C#" MasterPageFile="SailsMaster.Master" AutoEventWireup="true" CodeBehind="VoucherTemplates.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.VoucherTemplates" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <fieldset> 
        <legend>Voucher template manage</legend>
        <div class="basicinfo">
            <table>
                <tr>
                    <th>File name</th>
                    <th></th>
                </tr>
                <asp:Repeater runat="server" ID="rptFiles" OnItemDataBound="rptFiles_ItemDataBound">
                    <ItemTemplate>
                        <tr>
                            <td><asp:Literal runat="server" ID="litFileName"></asp:Literal></td>
                            <td>
                                <asp:HyperLink runat="server" ID="hplDownload">[Download]</asp:HyperLink>
                                <asp:LinkButton runat="server" ID="lbtDelete" OnClick="lbtDelete_Click">[Delete]</asp:LinkButton>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
            <asp:FileUpload runat="server" ID="fileUploadTemplate"/>
            <asp:Button runat="server" ID="btnUpload" OnClick="btnUpload_Click" Text="Upload" CssClass="button"/>
        </div>
    </fieldset>
</asp:Content>
