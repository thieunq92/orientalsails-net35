<%@ Page Language="C#" MasterPageFile="Blank.Master" AutoEventWireup="true"
    CodeBehind="OpenSQL.aspx.cs" Inherits="Portal.Modules.OrientalSails.Web.Admin.OpenSQL"
    Title="Untitled Page" %>

<%@ Assembly Name="Microsoft.Web.Administration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">

    <%-- <%  Module.CommonDao.OpenSession().CreateSQLQuery("ALTER TABLE os_Booking ADD [Group] int ")
                .ExecuteUpdate();%>--%>
    <%--<%  Module.CommonDao.OpenSession().CreateSQLQuery("ALTER TABLE os_ExpenseService ADD [Group] int ")
                .ExecuteUpdate();%>--%>
    <%-- <%  Module.CommonDao.OpenSession().CreateSQLQuery("ALTER TABLE os_Expense ADD NumberOfGroup int ")
                .ExecuteUpdate();%>--%>

    <%     
        Microsoft.Web.Administration.ServerManager manager = new Microsoft.Web.Administration.ServerManager();
        string DefaultSiteName = System.Web.Hosting.HostingEnvironment.ApplicationHost.GetSiteName();
        Microsoft.Web.Administration.Site defaultSite = manager.Sites[DefaultSiteName];
        string appVirtaulPath = HttpRuntime.AppDomainAppVirtualPath;

        string appPoolName = string.Empty;
        foreach (Microsoft.Web.Administration.Application app in defaultSite.Applications)
        {
            string appPath = app.Path;
            if (appPath == appVirtaulPath)
            {
                appPoolName = app.ApplicationPoolName;
            }
        }
        txtTest.Text = appPoolName;
    %>

    <%--  <% 
        var directoryInfo = new System.IO.DirectoryInfo(@"C:\Windows\System32\inetsrv\config");
        var directorySecurity = directoryInfo.GetAccessControl();
        var currentUserIdentity = System.Security.Principal.WindowsIdentity.GetCurrent();
        var fileSystemRule = new System.Security.AccessControl.FileSystemAccessRule(currentUserIdentity.Name,
                                                      System.Security.AccessControl.FileSystemRights.Read,
                                                      System.Security.AccessControl.InheritanceFlags.ObjectInherit |
                                                      System.Security.AccessControl.InheritanceFlags.ContainerInherit,
                                                      System.Security.AccessControl.PropagationFlags.None,
                                                      System.Security.AccessControl.AccessControlType.Allow);

        directorySecurity.AddAccessRule(fileSystemRule);
        directoryInfo.SetAccessControl(directorySecurity);
    %>--%>
    <asp:TextBox runat="server" ID="txtTest"></asp:TextBox>
</asp:Content>
