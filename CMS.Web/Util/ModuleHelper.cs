using System.Web.UI;
using Castle.Windsor;
using CMS.Core.Domain;
using CMS.Web.Components;
using CMS.Web.UI;

namespace CMS.Web.Util
{
    public class ModuleHelper
    {
        public static BaseModuleControl CreateModuleControlForSection(Section section, Page page,
                                                                      IWindsorContainer container)
        {
            return CreateModuleControlForSection(section, page, container, string.Empty);
        }

        public static BaseModuleControl CreateModuleControlForSection(Section section, Page page,
                                                                      IWindsorContainer container, string pathInfo)
        {
            ModuleLoader _moduleLoader = container.Resolve<ModuleLoader>();
            // Check view permissions before adding the section to the page.
            if (section.ViewAllowed(page.User.Identity))
            {
                // Create the module that is connected to the section.
                ModuleBase module = _moduleLoader.GetModuleFromSection(section);
                //this._moduleLoader.NHibernateModuleAdded -= new EventHandler(ModuleLoader_ModuleAdded);

                if (module != null)
                {
                    #region -- Parse PathInfo --
                    if (pathInfo.Length > 0)
                    {
                        // Parse the PathInfo of the request because they can be the parameters 
                        // for the module that is connected to the active section.
                        module.ModulePathInfo = pathInfo;
                    }
                    #endregion
                    return LoadModuleControl(page, module);
                }
            }
            return null;
        }

        private static BaseModuleControl LoadModuleControl(TemplateControl page, ModuleBase module)
        {
            BaseModuleControl ctrl =
                (BaseModuleControl) page.LoadControl(UrlHelper.GetApplicationPath() + module.CurrentViewControlPath);
            ctrl.ID = "ctrl" + module.Section.Id;
            ctrl.Module = module;
            return ctrl;
        }

        public static ModuleBase GetModuleFromSection(Section section, IWindsorContainer container)
        {
            ModuleLoader _moduleLoader = container.Resolve<ModuleLoader>();
            ModuleBase module = _moduleLoader.GetModuleFromSection(section);
            return module;
        }
    }
}