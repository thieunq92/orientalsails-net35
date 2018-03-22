<%@ Page Language="C#" MasterPageFile="SailsMaster.Master" AutoEventWireup="true" CodeBehind="Permissions.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.Permissions" Title="Permission Page - Oriental Sails Management Office"%>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <fieldset style="width:96%">
        <legend style="margin-top: 20px; margin-left: 45%; margin-bottom: -16px; font-size: 13px; font-variant:normal"><asp:Literal ID="litTitle" runat="server"></asp:Literal></legend>
        <div>
            <ul style="list-style:none; width: 100%;" class="permission_list">
            <asp:Repeater ID="rptPermissions" runat="server" OnItemDataBound="rptPermissions_ItemDataBound">
                <ItemTemplate>
                    <li id="liClear" class="p_header" runat="server" style="clear:both;" visible="false">
                    </li>
                    <li style="float:left; width: 300px;display:block;">
                        <asp:HiddenField ID="hiddenName" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "Name") %>'/>
                        <asp:CheckBox ID="chkPermission" runat="server" />
                    </li>
                </ItemTemplate>
            </asp:Repeater>
            </ul>
        </div>
        <br/>
        <br/>
        <asp:Button runat="server" ID="btnSave" CssClass="image_text_in_button image_text_in_button_buttoncontrol"
                    ToolTip="Save permission" Style="background-image: url('https://cdn1.iconfinder.com/data/icons/customicondesign-office-shadow/128/Save.png');
                    float: left; margin-left: 39px" Text="Save"
                    OnClick="btnSave_Click"></asp:Button>
    </fieldset>
</asp:Content>
