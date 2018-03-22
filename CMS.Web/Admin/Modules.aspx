<%@ Page Language="c#" Codebehind="Modules.aspx.cs" AutoEventWireup="false" Inherits="CMS.Web.Admin.Modules" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Phân hệ</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1"/>
    <meta name="CODE_LANGUAGE" content="C#"/>
    <meta name="vs_defaultClientScript" content="JavaScript"/>
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5"/>
</head>
<body>
    <form id="Form1" method="post" runat="server">
       <p>
            <em>Lưu ý: Tùy chọn "nạp khi khởi động" ảnh hướng tới việc tự nạp khi khởi động ứng dụng. Tuy nhiên,
            đánh dấu chọn nó sẽ khiến chương trình thử nạp phân hệ ngay tức khắc</em>
        </p>
        <table class="tbl">
            <asp:Repeater ID="rptModules" runat="server">
                <HeaderTemplate>
                    <tr>
                        <th>
                            Tên phân hệ</th>
                        <th>
                            Assembly</th>
                        <th>
                            Nạp khi khởi động</th>
                            <th>
                            Trạng thái
                            </th>
                        <th>
                            Trạng thái cài đặt
                        </th>
                        <th>
                            Thực thi</th>
                    </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <%# DataBinder.Eval(Container.DataItem, "FullName") %>
                        </td>
                        <td>
                            <%# DataBinder.Eval(Container.DataItem, "AssemblyName") %>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkBoxActivation" runat="server" AutoPostBack="true" OnCheckedChanged="chkBoxActivation_CheckedChanged" /></td>
                       <td>
                          <asp:Literal ID="litActivationStatus" runat="server" />
                       </td>
                        <td>
                            <asp:Literal ID="litStatus" runat="server"></asp:Literal></td>
                        <td>
                            <asp:LinkButton ID="lbtInstall" runat="server" Visible="False" CommandName="Install"
                                CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Name") + ":" + DataBinder.Eval(Container.DataItem, "AssemblyName") %>'>Cài đặt</asp:LinkButton>
                            <asp:LinkButton ID="lbtUpgrade" runat="server" Visible="False" CommandName="Upgrade"
                                CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Name") + ":" + DataBinder.Eval(Container.DataItem, "AssemblyName") %>'>Nâng cấp</asp:LinkButton>
                            <asp:LinkButton ID="lbtUninstall" runat="server" Visible="False" CommandName="Uninstall"
                                CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Name") + ":" + DataBinder.Eval(Container.DataItem, "AssemblyName") %>'>Gỡ bỏ</asp:LinkButton>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </form>
</body>
</html>
