using System;

using CMS.Core.Service;
using Castle.Windsor;
using CMS.Web.Util;

namespace CMS.Web.UI
{
	/// <summary>
	/// Base class for all aspx pages in Cuyahoga (public and admin).
	/// </summary>
	public class PortalPage : System.Web.UI.Page, ICuyahogaPage
	{
		private readonly IWindsorContainer _container;

		/// <summary>
		/// A reference to the Windsor container that can be used as a Service Locator for service classes.
		/// </summary>
		public IWindsorContainer Container
		{
			get { return this._container; }
		}

		/// <summary>
		/// Constructor.
		/// </summary>
		public PortalPage()
		{
			this._container = ContainerAccessorUtil.GetContainer();
		}
	}
}
