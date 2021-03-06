using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.UI.WebControls;
using CMS.Core.Domain;
using CMS.Core.Service;
using CMS.Core.Service.SiteStructure;
using CMS.Web.Admin.UI;

namespace CMS.Web.Admin
{
    /// <summary>
    /// Summary description for Modules.
    /// </summary>
    public class Modules : AdminBasePage
    {
        private IModuleTypeService _moduleTypeService;
        protected Repeater rptModules;

        private void Page_Load(object sender, EventArgs e)
        {
            base.Title = "Phân hệ";
            _moduleTypeService = Container.Resolve<IModuleTypeService>();
            if (! IsPostBack)
            {
                BindModules();
            }
        }

        private void BindModules()
        {
            // Retrieve the available modules that are installed.
            IList availableModules = _moduleTypeService.GetAllModuleTypes();
            // Retrieve the available modules from the filesystem.
            string moduleRootDir = HttpContext.Current.Server.MapPath("~/Modules");
            DirectoryInfo[] moduleDirectories = new DirectoryInfo(moduleRootDir).GetDirectories();
            // Go through the directories and check if there are missing ones. Those have to be added
            // as new ModuleType candidates.
            foreach (DirectoryInfo di in moduleDirectories)
            {
                // Skip hidden directories.
                bool shouldAdd = (di.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden;
                foreach (ModuleType moduleType in availableModules)
                {
                    if (moduleType.Name == di.Name)
                    {
                        shouldAdd = false;
                        break;
                    }
                }
                if (shouldAdd)
                {
                    ModuleType newModuleType = new ModuleType();
                    newModuleType.Name = di.Name;
                    availableModules.Add(newModuleType);
                }
            }
            rptModules.DataSource = availableModules;
            rptModules.DataBind();
        }

        private void rptModules_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                ModuleType moduleType = e.Item.DataItem as ModuleType;
                string physicalModuleInstallDirectory =
                    Path.Combine(Server.MapPath("~/Modules/" + moduleType.Name), "Install");
                Assembly moduleAssembly = null;
                if (moduleType.AssemblyName != null)
                {
                    moduleAssembly = Assembly.Load(moduleType.AssemblyName);
                }
                DatabaseInstaller dbInstaller = new DatabaseInstaller(physicalModuleInstallDirectory, moduleAssembly);
                bool canInstall = dbInstaller.CanInstall;
                bool canUpgrade = dbInstaller.CanUpgrade;
                bool canUninstall = dbInstaller.CanUninstall;
                LinkButton lbtInstall = e.Item.FindControl("lbtInstall") as LinkButton;
                lbtInstall.Visible = canInstall;
                lbtInstall.Attributes.Add("onclick", "return confirm('Cài đặt phân hệ?')");
                LinkButton lbtUpgrade = e.Item.FindControl("lbtUpgrade") as LinkButton;
                lbtUpgrade.Visible = canUpgrade;
                lbtUpgrade.Attributes.Add("onclick", "return confirm('Nâng cấp phân hệ?')");
                LinkButton lbtUninstall = e.Item.FindControl("lbtUninstall") as LinkButton;
                lbtUninstall.Visible = canUninstall;
                lbtUninstall.Attributes.Add("onclick", "return confirm('Gỡ bỏ phân hệ?')");

                CheckBox chkBox = e.Item.FindControl("chkBoxActivation") as CheckBox;
                if (canInstall)
                {
                    chkBox.Enabled = false;
                    chkBox.Checked = moduleType.AutoActivate;
                }
                else
                {
                    chkBox.Enabled = true;
                    chkBox.Checked = moduleType.AutoActivate;
                    if (moduleType.Name != null)
                        chkBox.InputAttributes.Add("moduleTypeId", moduleType.ModuleTypeId.ToString());
                }
                Literal litActivationStatus = e.Item.FindControl("litActivationStatus") as Literal;
                if (ModuleLoader.IsModuleActive(moduleType))
                {
                    litActivationStatus.Text = "<span style=\"color:green;\">Hoạt động</span>";
                }
                else
                {
                    litActivationStatus.Text = "<span style=\"color:red;\">Chưa nạp</span>";
                }

                Literal litStatus = e.Item.FindControl("litStatus") as Literal;
                if (dbInstaller.CurrentVersionInDatabase != null)
                {
                    litStatus.Text = String.Format("Đã cài ({0}.{1}.{2})"
                                                   , dbInstaller.CurrentVersionInDatabase.Major
                                                   , dbInstaller.CurrentVersionInDatabase.Minor
                                                   , dbInstaller.CurrentVersionInDatabase.Build);
                    if (dbInstaller.CanUpgrade)
                    {
                        litStatus.Text += " (tồn tại nâng cấp) ";
                    }
                }
                else
                {
                    litStatus.Text = "Đã gỡ bỏ";
                }
            }
        }

        private void rptModules_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string[] commandArguments = e.CommandArgument.ToString().Split(':');
            string moduleName = commandArguments[0];
            string assemblyName = commandArguments[1];
            Assembly assembly = null;
            if (assemblyName.Length > 0)
            {
                assembly = Assembly.Load(assemblyName);
            }

            string moduleInstallDirectory = Path.Combine(Server.MapPath("~/Modules/" + moduleName), "Install");
            DatabaseInstaller dbInstaller = new DatabaseInstaller(moduleInstallDirectory, assembly);

            try
            {
                switch (e.CommandName.ToLower())
                {
                    case "install":
                        dbInstaller.Install();
                        break;
                    case "upgrade":
                        dbInstaller.Upgrade();
                        break;
                    case "uninstall":
                        dbInstaller.Uninstall();
                        break;
                }

                // Rebind modules
                BindModules();

                ShowMessage(e.CommandName + ": thao tác thành công với " + moduleName + ".");
            }
            catch (Exception ex)
            {
                ShowError(e.CommandName + ": thao tác thất bại với " + moduleName + ".<br/>" + ex.Message);
            }
        }

        protected void chkBoxActivation_CheckedChanged(object sender, EventArgs e)
        {
            ModuleType mt = null;
            try
            {
                CheckBox box = (CheckBox) sender;
                if (box.InputAttributes["moduleTypeId"] != null)
                {
                    mt = _moduleTypeService.GetModuleById(int.Parse(box.InputAttributes["moduleTypeId"]));
                    if (box.Checked)
                    {
                        //set activation status
                        mt.AutoActivate = true;
                        _moduleTypeService.SaveOrUpdateModuleType(mt);
                        //activate now
                        ModuleLoader.ActivateModule(mt);
                    }
                    else
                    {
                        //set activation status
                        mt.AutoActivate = false;
                        _moduleTypeService.SaveOrUpdateModuleType(mt);
                    }
                }
                BindModules();
            }
            catch (Exception ex)
            {
                if (mt != null) ShowError("Nạp không thành công cho " + mt.Name + ".<br/>" + ex.Message);
                else ShowError("Không nạp được phân hệ.<br/>" + ex.Message);
            }
        }

        #region Web Form Designer generated code

        protected override void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.rptModules.ItemDataBound +=
                new System.Web.UI.WebControls.RepeaterItemEventHandler(this.rptModules_ItemDataBound);
            this.rptModules.ItemCommand +=
                new System.Web.UI.WebControls.RepeaterCommandEventHandler(this.rptModules_ItemCommand);
            this.Load += new System.EventHandler(this.Page_Load);
        }

        #endregion
    }
}