<%@ Control Language="C#" AutoEventWireup="true" Codebehind="CustomersInfo.ascx.cs"
    Inherits="Portal.Modules.OrientalSails.Web.CustomersInfo" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<%@ Register TagPrefix="uc" TagName="customer" Src="Controls/CustomerInfoInput.ascx" %>
    <script type="text/javascript">
    function toggleVisible(id)
    {
        item = document.getElementById(id);
        if (item.style.display=="")
        {
            item.style.display="none";
        }
        else
        {
            item.style.display=""
        }
    }    
    </script>
<div class="customer_info">
    <ul>
        <li id="liAnonymous" runat="server">
            Your contact info:<br />
            <table>
                <tr>
                    <td>Your name</td>
                    <td><asp:TextBox ID="txtYourName" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Our notify email will be sent to</td>
                    <td><asp:TextBox ID="txtEmail" runat="server"></asp:TextBox></td>
                </tr>
            </table>
        </li>
        <li>
            <asp:Panel ID="panelCustomer" runat="server" Visible="false">
                You can paste your customer info here
                <FCKeditorV2:FCKeditor ID="fckCustomers" runat="server" Width="100%" Height="300"
                        BasePath="~/support/fckeditor/" ToolbarSet="Basic">
                </FCKeditorV2:FCKeditor>
                Or better, fill in our form below.
            </asp:Panel>
        </li>
        <li>
            Your pickup address:<br />
            <asp:TextBox ID="txtPickupAddress" runat="server" Width="100%" TextMode="MultiLine"></asp:TextBox>
        </li>
        <li>
            Special request:<br />
            <asp:TextBox ID="txtSpecialRequest" runat="server" Width="100%" TextMode="MultiLine"></asp:TextBox>
        </li>
        <li class="customer_info_data">
            <table style="width: 100%;">
                <asp:Repeater ID="rptRoomList" runat="server" OnItemDataBound="rptRoomList_itemDataBound">
                    <HeaderTemplate>
                        <tr>
                            <th style="width: 15%;">
                                <%#base.GetText("labelRoomName")%>
                            </th>
                            <th style="width: 85%;">
                                <%#base.GetText("stringCustomerInfo") %>
                            </th>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td rowspan="6">
                                <asp:Label ID="lblRoomName" runat="server"></asp:Label>
                                <asp:Label ID="label_RoomId" runat="server" style="display:none;"></asp:Label>
                                <asp:HiddenField ID="hiddenRoomClassId" runat="server" />
                                <asp:HiddenField ID="hiddenRoomTypeId" runat="server" />                                
                            </td>
                            <td></td>
                        </tr>
                        <tr id="trCustomer1" runat="server">
                            <td>
                                <uc:customer ID="customer1" runat="server"></uc:customer>
                            </td>
                        </tr>
                        <tr id="trCustomer2" runat="server">
                            <td>
                                <uc:customer ID="customer2" runat="server"></uc:customer>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div id="trChild" runat="server" style="display:none">
                                Child info<br />
                                <uc:customer ID="customerChild" runat="server"></uc:customer>
                                </div>
                            </td>                        
                        </tr>
                        <tr>
                            <td>
                                <div id="trBaby" runat="server" style="display:none">
                                Baby info<br />
                                <uc:customer ID="customerBaby" runat="server"></uc:customer>
                                </div>
                            </td>                        
                        </tr>
                        <tr id="trExtra" runat="server">
                            <td>
                                <asp:CheckBox ID="checkBoxAddChild" runat="server" Text='<%#base.GetText("stringAddChild") %>' />
                                <asp:CheckBox ID="checkBoxAddBaby" runat="server" Text='<%#base.GetText("stringAddBaby") %>' />
                                <asp:CheckBox ID="checkBoxSingle" runat="server" Text='Single' CssClass="checkbox"/>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </li>
        <li>
            <asp:Button ID="buttonSubmit" runat="server" OnClick="buttonSubmit_Click" />
        </li>
    </ul>
</div>