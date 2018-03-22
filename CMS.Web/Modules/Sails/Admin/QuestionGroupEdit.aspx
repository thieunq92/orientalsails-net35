<%@ Page Language="C#" MasterPageFile="SailsMaster.Master" AutoEventWireup="true"
    CodeBehind="QuestionGroupEdit.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.QuestionGroupEdit"
    Title="Untitled Page" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <fieldset>
        <legend>Survey question</legend>
        <div class="basicinfo">
            <table>
                <tr>
                    <td>
                        Priority
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtPriority" Width="40"></asp:TextBox>
                    </td>
                    <td>
                        Subject
                    </td>
                    <td>
                        <asp:TextBox ID="txtSubject" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        Selection allowed:
                    </td>
                    <td>
                        Select 1<asp:TextBox runat="server" ID="txtSelection1" Width="100"></asp:TextBox>
                    </td>
                    <td>
                        Select 2<asp:TextBox runat="server" ID="txtSelection2" Width="100"></asp:TextBox>
                    </td>
                    <td>
                        Select 3<asp:TextBox runat="server" ID="txtSelection3" Width="100"></asp:TextBox>
                    </td>
                    <td>
                        Select 4<asp:TextBox runat="server" ID="txtSelection4" Width="100"></asp:TextBox>
                    </td>
                    <td>
                        Select 5<asp:TextBox runat="server" ID="txtSelection5" Width="100"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="5">
                        Good choice
                        <asp:DropDownList runat="server" ID="ddlGoodChoice">
                            <asp:ListItem Value="1">Choice 1</asp:ListItem>
                            <asp:ListItem Value="2">Choice 2</asp:ListItem>
                            <asp:ListItem Value="3">Choice 3</asp:ListItem>
                            <asp:ListItem Value="4">Choice 4</asp:ListItem>
                            <asp:ListItem Value="5">Choice 5</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td colspan="3">
                        <span>
                            <input type="checkbox" runat="server" style="width: 30px" id="chkIsInDayboatForm" /><label
                                for="chkIsInDayboatForm">InDayboatForm</label></span>
                    </td>
                </tr>
                <tr>
                    <td colspan="8">
                        Sub-category
                    </td>
                </tr>
                <asp:Repeater ID="rptSubCategory" runat="server" OnItemDataBound="rptSubCategory_ItemDataBound">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:HiddenField ID="hiddenId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "Id") %>' />
                                Name
                            </td>
                            <td>
                                <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                Description
                            </td>
                            <td colspan="4">
                                <asp:TextBox ID="txtContent" runat="server" Width="500"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnRemove" runat="server" CssClass="button" Text="Remove" OnClick="btnRemove_Click" />
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
            <asp:Button ID="btnAdd" runat="server" CssClass="button" Text="Add" OnClick="btnAdd_Click" />
        </div>
        <asp:Button ID="btnSave" runat="server" CssClass="button" Text="Save" OnClick="btnSave_Click" />
    </fieldset>
</asp:Content>
