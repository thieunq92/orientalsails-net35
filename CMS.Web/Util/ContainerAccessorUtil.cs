using System;
using System.Web;

using Castle.Windsor;

using CMS.Web.Components;

namespace CMS.Web.Util
{
	/// <summary>
	/// Provides a facilty to obtain the container instance.
	/// </summary>
	public class ContainerAccessorUtil
	{
		private ContainerAccessorUtil()
		{
		}

		/// <summary>
		/// Obtain the Cuyahoga container.
		/// </summary>
		/// <returns></returns>
		public static PortalContainer GetContainer()
		{
			IContainerAccessor containerAccessor = HttpContext.Current.ApplicationInstance as IContainerAccessor;
	
			if (containerAccessor == null)
			{
				throw new Exception("You must extend the HttpApplication in your web project " + 
					"and implement the IContainerAccessor to properly expose your container instance");
			}
	
			PortalContainer container = containerAccessor.Container as PortalContainer;
	
			if (container == null)
			{
				throw new Exception("The container seems to be unavailable in " + 
					"your HttpApplication subclass");
			}

			return container;
		}
	}
}
