using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;
using CMS.Core.Service;

namespace CMS.Web.Components
{
	/// <summary>
	/// The PortalContainer serves as the IoC container for CMS.
	/// </summary>
	public class PortalContainer : WindsorContainer
	{
		/// <summary>
		/// Constructor. The configuration is read from the web.config.
		/// </summary>
		public PortalContainer() : base(new XmlInterpreter())
		{
			RegisterFacilities();
			RegisterServices();
			ConfigureLegacySessionFactory();
		}

		private void RegisterFacilities()
		{
		}

		private void RegisterServices()
		{
			// The core services are registrated via services.config
			
			// Utility services
			AddComponent("web.moduleloader", typeof(ModuleLoader));
			AddComponent("core.sessionfactoryhelper", typeof(SessionFactoryHelper));

			// Legacy
			AddComponent("corerepositoryadapter", typeof(CoreRepositoryAdapter));
		}

		private void ConfigureLegacySessionFactory()
		{
			SessionFactoryHelper sessionFactoryHelper = this.Resolve<SessionFactoryHelper>();
			sessionFactoryHelper.ConfigureLegacySessionFactory();
		}
	}
}
