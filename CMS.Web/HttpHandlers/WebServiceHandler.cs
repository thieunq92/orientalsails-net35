using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services.Protocols;
using System.Web.SessionState;
using Castle.Windsor;
using CMS.Core.Domain;
using CMS.Web.Util;
using log4net;

namespace CMS.Web.HttpHandlers
{
    /// <summary>
    /// This class handles all aspx page requests for CMS.
    /// </summary>
    public class WebServiceHandler : IHttpHandler, IRequiresSessionState
    {
        private static readonly ILog log = LogManager.GetLogger(typeof (PageHandler));
        //private IWindsorContainer _container;

        #region IHttpHandler Members

        /// <summary>
        /// Process the aspx request. This means (eventually) rewriting the url and registering the page 
        /// in the container.
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        {
            string rawUrl = context.Request.RawUrl;
            log.Info("Starting request for " + rawUrl);
            DateTime startTime = DateTime.Now;

            // Rewrite url

            #region //Ali Ozgur: This is an ajax request, so we have to realign the rewritten url.

            if (context.Request["HTTP_X_MICROSOFTAJAX"] != null)
            {
                int idx = rawUrl.ToLowerInvariant().IndexOf("/default.aspx");
                if (idx >= 0)
                {
                    rawUrl = rawUrl.Substring(idx, rawUrl.Length - idx);
                }
            }

            #endregion

            WebServiceHandlerFactory factory = new WebServiceHandlerFactory();
            IHttpHandler handler = factory.GetHandler(context, context.Request.RequestType, context.Request.FilePath,
                                                      context.Request.FilePath);

            // Process the page just like any other aspx page
            handler.ProcessRequest(context);

            // Release loaded modules. These modules are added to the HttpContext.Items collection by the ModuleLoader.
            ReleaseModules();

            // Log duration
            TimeSpan duration = DateTime.Now - startTime;
            log.Info(String.Format("Request finshed. Total duration: {0} ms.", duration.Milliseconds));
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsReusable
        {
            get { return true; }
        }

        #endregion

        private void ReleaseModules()
        {
            List<ModuleBase> loadedModules = HttpContext.Current.Items["LoadedModules"] as List<ModuleBase>;
            if (loadedModules != null)
            {
                IWindsorContainer container = ContainerAccessorUtil.GetContainer();

                foreach (ModuleBase module in loadedModules)
                {
                    container.Release(module);
                }
            }
        }
    }

    /// <summary>
    /// This class handles all aspx page requests for CMS.
    /// </summary>
    public class WebScriptHandler : IHttpHandler, IRequiresSessionState
    {
        private static readonly ILog log = LogManager.GetLogger(typeof (PageHandler));
        //private IWindsorContainer _container;

        #region IHttpHandler Members

        /// <summary>
        /// Process the aspx request. This means (eventually) rewriting the url and registering the page 
        /// in the container.
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        {
            string rawUrl = context.Request.RawUrl;
            log.Info("Starting request for " + rawUrl);
            DateTime startTime = DateTime.Now;

            // Rewrite url

            #region //Ali Ozgur: This is an ajax request, so we have to realign the rewritten url.

            if (context.Request["HTTP_X_MICROSOFTAJAX"] != null)
            {
                int idx = rawUrl.ToLowerInvariant().IndexOf("/default.aspx");
                if (idx >= 0)
                {
                    rawUrl = rawUrl.Substring(idx, rawUrl.Length - idx);
                }
            }

            #endregion

            WebServiceHandlerFactory factory = new WebServiceHandlerFactory();
            IHttpHandler handler = factory.GetHandler(context, context.Request.RequestType, context.Request.FilePath,
                                                      context.Request.FilePath);

            // Process the page just like any other asmx page
            handler.ProcessRequest(context);

            // Release loaded modules. These modules are added to the HttpContext.Items collection by the ModuleLoader.
            ReleaseModules();

            // Log duration
            TimeSpan duration = DateTime.Now - startTime;
            log.Info(String.Format("Request finshed. Total duration: {0} ms.", duration.Milliseconds));
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsReusable
        {
            get { return true; }
        }

        #endregion

        private void ReleaseModules()
        {
            List<ModuleBase> loadedModules = HttpContext.Current.Items["LoadedModules"] as List<ModuleBase>;
            if (loadedModules != null)
            {
                IWindsorContainer container = ContainerAccessorUtil.GetContainer();

                foreach (ModuleBase module in loadedModules)
                {
                    container.Release(module);
                }
            }
        }
    }
}