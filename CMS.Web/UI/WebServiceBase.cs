using System.ComponentModel;
using System.Web;
using System.Web.Services;
using Castle.Windsor;
using CMS.Core.Domain;
using CMS.Core.Service;
using CMS.Web.Components;
using CMS.Web.Util;

namespace CMS.Web.UI
{
    /// <summary>
    /// Summary description for HotelService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class WebServiceBase
    {
        private readonly IWindsorContainer _container;
        private readonly ModuleBase _module;
        private readonly ModuleLoader _moduleLoader;

        public WebServiceBase(string className)
        {
            _container = ContainerAccessorUtil.GetContainer();
            _moduleLoader = Container.Resolve<ModuleLoader>();
            _module = _moduleLoader.GetModuleFromClassName(className);
        }

        public ModuleBase Module
        {
            get { return _module; }
        }

        /// <summary>
        /// A reference to the Windsor container that can be used as a Service Locator for service classes.
        /// </summary>
        protected IWindsorContainer Container
        {
            get { return _container; }
        }

        /// <summary>
        /// The core repository for persisting Cuyahoga objects.
        /// </summary>
        public CoreRepository CoreRepository
        {
            get { return HttpContext.Current.Items["CoreRepository"] as CoreRepository; }
        }
    }
}