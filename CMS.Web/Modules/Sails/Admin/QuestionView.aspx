<%@ Page Language="C#" MasterPageFile="SailsMaster.Master" AutoEventWireup="true"
    Codebehind="QuestionView.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.QuestionView"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <fieldset>
        <asp:Repeater ID="rptGroups" runat="server" OnItemDataBound="rptGroups_ItemDataBound">
            <ItemTemplate>
                <div class="group">
                    <h4>
                        <asp:HiddenField ID="hiddenId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "Id") %>'/>
                        <asp:Literal ID="litGroupName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Name") %>'></asp:Literal>
                        <asp:HyperLink ID="hplEdit" runat="server">[Edit]</asp:HyperLink>
                        <asp:LinkButton ID="lbtDelete" runat="server" OnClick="lbtDelete_Click">[Delete]</asp:LinkButton>
                    </h4>
                    <ul>
                        <asp:Repeater ID="rptQuestions" runat="server">
                            <ItemTemplate>
                                <li><%# DataBinder.Eval(Container.DataItem, "Name") %></li><br/>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
            </ItemTemplate>
        </asp:Repeater>
        <h2>Dayboat Form</h2>
        <asp:Repeater ID="rptDayboatGroup" runat="server" OnItemDataBound="rptGroups_ItemDataBound">
            <ItemTemplate>
                <div class="group">
                    <h4>
                        <asp:HiddenField ID="hiddenId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "Id") %>'/>
                        <asp:Literal ID="litGroupName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Name") %>'></asp:Literal>
                        <asp:HyperLink ID="hplEdit" runat="server">[Edit]</asp:HyperLink>
                        <asp:LinkButton ID="lbtDelete" runat="server" OnClick="lbtDelete_Click">[Delete]</asp:LinkButton>
                    </h4>
                    <ul>
                        <asp:Repeater ID="rptQuestions" runat="server">
                            <ItemTemplate>
                                <li><%# DataBinder.Eval(Container.DataItem, "Name") %></li><br/>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </fieldset>
</asp:Content>
