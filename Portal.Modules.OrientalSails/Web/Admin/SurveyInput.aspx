<%@ Page Language="C#" MasterPageFile="Popup.Master" AutoEventWireup="true" CodeBehind="SurveyInput.aspx.cs"
    Inherits="Portal.Modules.OrientalSails.Web.Admin.SurveyInput" Title="Untitled Page" %>

<%@ Register Assembly="System.Web.Extensions" Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
    <fieldset>
        <script type="text/javascript">
            function SetUniqueRadioButton(nameregex, current) {
                re = new RegExp(nameregex);
                for (i = 0; i < document.forms[0].elements.length; i++) {
                    elm = document.forms[0].elements[i];
                    if (elm.type == 'radio') {
                        if (elm.name.indexOf(nameregex) != -1)
                            elm.checked = false;
                    }
                }
                current.checked = true;
            }

            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function (sender, args) {
                if (args.get_error()) {
                    alert('Save error, please try again!');
                } else {
                    alert('Save succeeded!');
                    window.close();
                }
            }); 
        </script>
        <div class="basicinfo">
            <table>
                <tr>
                    <td>
                        Booking
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlBookings" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td>
                        Date
                    </td>
                    <td>
                        <asp:TextBox ID="txtStartDate" runat="server"></asp:TextBox><ajax:CalendarExtender
                            ID="calendarDate" runat="server" TargetControlID="txtStartDate" Format="dd/MM/yyyy">
                        </ajax:CalendarExtender>
                    </td>
                    <td>
                        Cruise
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCruises" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        Room
                    </td>
                    <td>
                        <asp:TextBox ID="txtRoomNumber" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        Guide
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlGuide" runat="server">
                        </asp:DropDownList>
                        <asp:TextBox ID="txtGuide" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        Driver
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlDrivers" runat="server">
                        </asp:DropDownList>
                        <asp:TextBox ID="txtDriver" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Name
                    </td>
                    <td>
                        <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        Address
                    </td>
                    <td>
                        <asp:TextBox ID="txtAddress" runat="server"></asp:TextBox>
                    </td>
                    <td>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlNationalities" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        Email
                    </td>
                    <td>
                        <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </div>
        <div class="basicinfo">
            <asp:Repeater ID="rptGroups" runat="server" OnItemDataBound="rptGroups_ItemDataBound">
                <ItemTemplate>
                    <asp:HiddenField ID="hiddenId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "Id") %>' />
                    <div class="group">
                        <h4>
                            <asp:Literal ID="litGroupName" runat="server"></asp:Literal></h4>
                        <table style="width: auto;">
                            <tr>
                                <td>
                                </td>
                                <asp:Repeater ID="rptOptions" runat="server">
                                    <ItemTemplate>
                                        <td>
                                            <%#DataBinder.Eval(Container, "DataItem") %>
                                        </td>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tr>
                            <asp:Repeater ID="rptQuestions" runat="server" OnItemDataBound="rptQuestion_ItemDataBound">
                                <ItemTemplate>
                                    <tr>
                                        <td style="padding-right: 20px;">
                                            <asp:HiddenField ID="hiddenId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "Id") %>' />
                                            <asp:Literal ID="litQuestion" runat="server"></asp:Literal>
                                        </td>
                                        <asp:RadioButtonList runat="server" ID="radOptions" />
                                        <asp:Repeater ID="rptOptions" runat="server" OnItemDataBound="rptOptions_ItemDataBound">
                                            <ItemTemplate>
                                                <td style="width: 100px;">
                                                    <asp:RadioButton runat="server" ID="radOption" CssClass="checkbox" />
                                                </td>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            <tr>
                                <td colspan="100">
                                    <asp:TextBox ID="txtComment" runat="server" Width="600" Height="70" TextMode="MultiLine"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <asp:Panel runat="server" ID="pnDayboat">
            <div class="basicinfo">
                <h2>
                    Dayboat Form</h2>
                <asp:Repeater ID="rptDayboat" runat="server" OnItemDataBound="rptGroups_ItemDataBound">
                    <ItemTemplate>
                        <asp:HiddenField ID="hiddenId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "Id") %>' />
                        <div class="group">
                            <h4>
                                <asp:Literal ID="litGroupName" runat="server"></asp:Literal></h4>
                            <table style="width: auto;">
                                <tr>
                                    <td>
                                    </td>
                                    <asp:Repeater ID="rptOptions" runat="server">
                                        <ItemTemplate>
                                            <td>
                                                <%#DataBinder.Eval(Container, "DataItem") %>
                                            </td>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tr>
                                <asp:Repeater ID="rptQuestions" runat="server" OnItemDataBound="rptQuestion_ItemDataBound">
                                    <ItemTemplate>
                                        <tr>
                                            <td style="padding-right: 20px;">
                                                <asp:HiddenField ID="hiddenId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "Id") %>' />
                                                <asp:Literal ID="litQuestion" runat="server"></asp:Literal>
                                            </td>
                                            <asp:RadioButtonList runat="server" ID="radOptions" />
                                            <asp:Repeater ID="rptOptions" runat="server" OnItemDataBound="rptOptions_ItemDataBound">
                                                <ItemTemplate>
                                                    <td style="width: 100px;">
                                                        <asp:RadioButton runat="server" ID="radOption" CssClass="checkbox" />
                                                    </td>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <tr>
                                    <td colspan="100">
                                        <asp:TextBox ID="txtComment" runat="server" Width="600" Height="70" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </asp:Panel>
        <%--        <div style="position: fixed; top: 150px; right: 10px; border: solid 1px #000; padding: 20px;">
            <asp:Button runat="server" ID="btnSaveDraft" OnClick="btnSaveDraft_Click" Text="Save draft" CssClass="button"/>
        </div>--%>
        <asp:UpdatePanel runat="server" ID="updSave">
            <ContentTemplate>
                <asp:Button runat="server" ID="btnSave" OnClick="btnSave_Click" CssClass="button" />
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:Button runat="server" ID="btnExportFeedBack" OnClick="btnExportFeedBack_Click"
            Text="Export" CssClass="button" />
    </fieldset>
    <style>
        #ctl00_AdminContent_updSave
        {
            display: inline-block;
        }
    </style>
</asp:Content>
