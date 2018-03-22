<%@ Page Language="C#" MasterPageFile="SailsMaster.Master" AutoEventWireup="true"
    CodeBehind="AgencyEdit.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.AgencyEdit"
    Title="Agency Edit Page - Oriental Sails Managemnet Office" %>

<%@ Register Assembly="CMS.ServerControls" Namespace="CMS.ServerControls.FileUpload"
    TagPrefix="svc" %>
<%@ Register Assembly="CMS.ServerControls" Namespace="CMS.ServerControls" TagPrefix="svc" %>
<%@ Register Src="/Admin/Controls/UserSelector.ascx" TagPrefix="asp" TagName="UserSelector" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <script>
        $(document).ready(function () {
            var listToHide = ['Captain', 'Sales', 'Anonymous user', 'Authenticated user', 'Editor', 'Administrator', 'Agent $71', 'Agent $68', 'Agent $69', 'Agent $70', 'Agent $72', 'Agent $73', 'Agent $74', 'Agent $75'];
            for (var i = 0; i < listToHide.length; i++) {
                $('#<%=ddlAgencyRoles.ClientID%> option').each(function (k, v) {
                    if ($(v).text() == listToHide[i]) {
                        $(v).css('display', 'none');
                    }
                });
            }
        })
    </script>
    <fieldset>
        <div class="settinglist">
            <svc:Popup ID="popupManager" runat="server">
            </svc:Popup>
            <div class="basicinfo" style="border: 1px dashed; margin: 20px auto; width: 55%; padding: 18px 145px; position: relative;">
                <span style="background: none repeat scroll 0% 0% white; font-weight: bold; display: inline-block; text-align: center; font-size: 13px; position: absolute; top: -10px; left: 30px; width: 140px;">AGENCY EDIT FORM</span>
                <table style="line-height:20px">
                    <tr>
                        <td style="width:35%">
                            <asp:Label ID="labelName" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="textBoxName" runat="server" width="100%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <%= base.GetText("textPhone")%>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPhone" runat="server" width="100%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <%= base.GetText("textFax")%>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFax" runat="server" width="100%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Locations
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlLocations" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <%= base.GetText("textAddress")%>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" width="100%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <%= base.GetText("textEmail")%>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEmail" runat="server" width="100%"></asp:TextBox>
                        </td>
                        <%--<td rowspan="6">
                            Price policy
                        </td>
                        <td rowspan="6">
                            <table>
                                <asp:Repeater runat="server" ID="rptCruises" OnItemDataBound="rptCruises_ItemDataBound">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:HiddenField ID="hiddenCruiseId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "Id") %>' />
                                                <%# DataBinder.Eval(Container.DataItem, "Name") %>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlRoles" runat="server">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                        </td>--%>
                    </tr>
                    <tr>
                        <td>
                            <%= base.GetText("textTaxCode")%>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTaxCode" runat="server" width="100%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Role
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlAgencyRoles">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <%= base.GetText("textSaleInCharge")%>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlSales" runat="server">
                            </asp:DropDownList>
                            Apply from
                            <asp:TextBox ID="txtSaleStart" runat="server" Width="90"></asp:TextBox><ajax:CalendarExtender
                                ID="CalendarExtender1" runat="server" TargetControlID="txtSaleStart" Format="dd/MM/yyyy">
                            </ajax:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <b>Sale in charge history :</b><br/>
                            <table style="width: 100%;">
                                <asp:Repeater ID="rptHistory" runat="server" OnItemDataBound="rptHistory_ItemDataBound">
                                    <ItemTemplate>
                                        <tr id="trLine" runat="server">
                                            <td>
                                                <asp:Literal ID="litSale" runat="server"></asp:Literal> apply from
                                                <asp:Literal ID="litSaleStart" runat="server"></asp:Literal>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <%= base.GetText("textAccountant")%>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAccountant" runat="server" width="100%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <%= base.GetText("textPaymentPeriod")%>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlPaymentPeriod" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <%= base.GetText("textOthers")%>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" width="100%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan ="2">
                            <asp:Literal ID="litCreated" runat="server"></asp:Literal>
                            <asp:Literal ID="litModified" runat="server"></asp:Literal>
                        </td>
                    </tr>
                </table>
                <br/>
                <asp:Button runat="server" ID="buttonSave" CssClass="image_text_in_button image_text_in_button_buttoncontrol"
                    ToolTip="Save agency information" Style="background-image: url('https://cdn1.iconfinder.com/data/icons/customicondesign-office-shadow/128/Save.png');" Text="Save" OnClick="buttonSave_Click"></asp:Button>
            </div>
            <asp:PlaceHolder runat="server" Visible="false">
                <asp:PlaceHolder ID="plhContracts" runat="server" Visible="false">
                    <div class="basicinfo">
                        <table>
                            <asp:Repeater ID="rptContracts" runat="server" OnItemDataBound="rptContracts_ItemDataBound"
                                OnItemCommand="rptContracts_ItemCommand">
                                <HeaderTemplate>
                                    <tr>
                                        <td>
                                            <%= base.GetText("textContractName")%>
                                        </td>
                                        <td>
                                            <%= base.GetText("textExpiredDate")%>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <%# DataBinder.Eval(Container.DataItem, "ContractName") %>
                                        </td>
                                        <td>
                                            <asp:Literal ID="litExpiredDate" runat="server"></asp:Literal>
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="imageView" runat="server" ImageAlign="AbsMiddle" AlternateText="View"
                                                CssClass="image_button16" ImageUrl="../Images/edit.gif" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>'
                                                CommandName="edit" Width="16" />
                                            <asp:LinkButton ID="lbtDownload" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>'
                                                CommandName='download' Text='<%# DataBinder.Eval(Container.DataItem, "FileName") %>'></asp:LinkButton>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
                    <asp:HiddenField ID="hiddenContractId" runat="server" />
                    <div class="basicinfo">
                        <table>
                            <tr>
                                <td>
                                    <%= base.GetText("textContractName")%>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtContractName" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <%= base.GetText("textExpiredDate")%>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtExpiredDate" runat="server"></asp:TextBox><ajax:CalendarExtender
                                        ID="calendarDate" runat="server" TargetControlID="txtExpiredDate" Format="dd/MM/yyyy">
                                    </ajax:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <%= base.GetText("textContractUpload")%>
                                </td>
                                <td>
                                    <asp:FileUpload ID="fileUploadContract" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" CssClass="button" />
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="plhHidden" runat="server" Visible="false">
                    <div class="basicinfo">
                        <h4>
                            Contract file</h4>
                        <asp:HyperLink ID="hplOldContract" runat="server">View current contract</asp:HyperLink><br />
                        <svc:FileUploaderAJAX ID="uploadContract" runat="server"></svc:FileUploaderAJAX>
                        <asp:TextBox ID="txtPath" runat="server" Style="display: none;"></asp:TextBox>
                    </div>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="plhAssignedUser" runat="server" Visible="false">
                    <div class="data_table">
                        <h4>
                            <%= base.GetText("textAssignedUser")%></h4>
                        <div class="data_grid">
                            <table cellpadding="2'" cellspacing="0">
                                <asp:Repeater ID="rptUsers" runat="server" OnItemCommand="rptUsers_ItemCommand">
                                    <HeaderTemplate>
                                        <tr>
                                            <th>
                                                Username
                                            </th>
                                            <th>
                                                Full name
                                            </th>
                                            <th>
                                                Email
                                            </th>
                                            <th>
                                            </th>
                                        </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <%# DataBinder.Eval(Container.DataItem, "Username") %>
                                            </td>
                                            <td>
                                                <%# DataBinder.Eval(Container.DataItem, "Fullname") %>
                                            </td>
                                            <td>
                                                <%# DataBinder.Eval(Container.DataItem, "Email") %>
                                            </td>
                                            <td>
                                                <asp:ImageButton runat="server" ID="imageButtonDelete" ToolTip='Delete' ImageUrl="../Images/delete_file.gif"
                                                    AlternateText='Delete' ImageAlign="AbsMiddle" CssClass="image_button16" CommandName="Delete"
                                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem,"Id") %>' />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                        </div>
                    </div>
                    <asp:UserSelector ID="userSelector" runat="server">
                    </asp:UserSelector>
                    <asp:Button ID="btnAdd" runat="server" CssClass="button" Text="Assign" OnClick="btnAdd_Click" />
                </asp:PlaceHolder>
                <div class="data_table">
                    <h4>
                        Booker (contacts) list</h4>
                    <div class="data_grid">
                        <table>
                            <tr>
                                <th>
                                    Name
                                </th>
                                <th>
                                    Position
                                </th>
                                <th>
                                    Phone
                                </th>
                                <th>
                                    Email
                                </th>
                                <th>
                                    Birdthday
                                </th>
                                <th>
                                    Note
                                </th>
                                <th>
                                    Enabled
                                </th>
                                <th>
                                    Action
                                </th>
                            </tr>
                            <asp:Repeater ID="rptContacts" runat="server" OnItemDataBound="rptContacts_ItemDataBound"
                                OnItemCommand="rptContacts_ItemCommand">
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <%# DataBinder.Eval(Container.DataItem, "Name") %>
                                        </td>
                                        <td>
                                            <%# DataBinder.Eval(Container.DataItem, "Position") %>
                                        </td>
                                        <td>
                                            <%# DataBinder.Eval(Container.DataItem, "Phone") %>
                                        </td>
                                        <td>
                                            <%# DataBinder.Eval(Container.DataItem, "Email") %>
                                        </td>
                                        <td>
                                            <%# ((DateTime?)Eval("Birthday"))==null?"" : ((DateTime?)Eval("Birthday")).Value.ToString("dd/MM/yyyy")%>
                                        </td>
                                        <td>
                                            <%# DataBinder.Eval(Container.DataItem, "Note") %>
                                        </td>
                                        <td>
                                            <asp:Literal ID="litEnabled" runat="server"></asp:Literal>
                                        </td>
                                        <td>
                                            <input id="btnEdit" runat="server" type="button" value="Edit" class="button" />
                                            <asp:Button ID="btnEnabled" runat="server" CssClass="button" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
                    <input id="btnAddContact" runat="server" type="button" value="Add contact" class="button" />
                </div>
            </asp:PlaceHolder>
        </div>
    </fieldset>
</asp:Content>
