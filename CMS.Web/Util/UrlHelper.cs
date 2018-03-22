using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI.WebControls;
using CMS.Core.Domain;
using CMS.Core.Util;

namespace CMS.Web.Util
{
    /// <summary>
    /// The UrlHelper class contains methods for url creation and manipulation.
    /// </summary>
    public class UrlHelper
    {
        private UrlHelper()
        {
        }

        public static string EXTENSION
        {
            get
            {
                if (string.IsNullOrEmpty(Config.GetConfiguration()["FriendlyExtension"]))
                {
                    return ".aspx";
                }
                return Config.GetConfiguration()["FriendlyExtension"];
            }
        }// = ".aspx";

        /// <summary>
        /// GetApplicationPath returns the base application path and ensures that it allways ends with a "/".
        /// </summary>
        /// <returns></returns>
        public static string GetApplicationPath()
        {
            return Text.EnsureTrailingSlash(HttpContext.Current.Request.ApplicationPath);
        }

        /// <summary>
        /// Get the (lowercase) url of the site without any trailing slashes.
        /// </summary>
        /// <returns></returns>
        public static string GetSiteUrl()
        {
            string path = HttpContext.Current.Request.ApplicationPath;
            if (path.EndsWith("/") && path.Length == 1)
            {
                return GetHostUrl();
            }
            return GetHostUrl() + path.ToLower();
        }

        /// <summary>
        /// Returns a formatted url for a given node (/{ApplicationPath}/{Node.ShortDescription}.aspx.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static string GetFriendlyUrlFromNode(Node node)
        {
            return GetApplicationPath() + node.ShortDescription + EXTENSION;
        }

        /// <summary>
        /// Returns a formatted url for a given node (/{ApplicationPath}/{Node.Id}/view.aspx
        /// or /{ApplicationPath}/{Node.ShortDescription}.aspx if the site has friendly urls
        /// turned on).
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static string GetUrlFromNode(Node node)
        {
            if (node == null)
            {
                return "";
            }

            if (node.IsExternalLink)
            {
                return node.LinkUrl;
            }
            if (node.Site.UseFriendlyUrls)
            {
                return GetFriendlyUrlFromNode(node);
            }
            return GetApplicationPath() + node.Id + "/view.aspx";
        }

        /// <summary>
        /// Get the full url of a Node with the host url resolved via the Site property.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static string GetFullUrlFromNodeViaSite(Node node)
        {
            if (! node.IsExternalLink)
            {
                return Text.EnsureTrailingSlash(node.Site.SiteUrl) + node.Id + "/view.aspx";
            }
            return null;
        }

        /// <summary>
        /// Returns a formatted url for a given section (/{ApplicationPath}/{Section.Id}/section.aspx).
        /// </summary>
        /// <param name="section"></param>
        /// <returns></returns>
        public static string GetUrlFromSection(Section section)
        {
            return GetApplicationPath() + section.Id + "/section.aspx";
        }

        /// <summary>
        /// Returns a formatted url for a given section (/{ApplicationPath}/{Section.Id}/section.aspx).
        /// </summary>
        /// <param name="section"></param>
        /// <returns></returns>
        public static string GetUrlNoAspxFromSection(Section section)
        {
            return GetApplicationPath() + section.Id + "/section";
        }

        /// <summary>
        /// Returns a formatted url for a given section (http://{hostname}/{ApplicationPath}/{Section.Id}/section.aspx).
        /// </summary>
        /// <param name="section"></param>
        /// <returns></returns>
        public static string GetFullUrlFromSection(Section section)
        {
            return GetHostUrl() + GetUrlFromSection(section);
        }

        /// <summary>
        /// Get the full url of a Section with the host url resolved via the Site property.
        /// </summary>
        /// <param name="section"></param>
        /// <returns></returns>
        public static string GetFullUrlFromSectionViaSite(Section section)
        {
            if (section.Node != null)
            {
                return Text.EnsureTrailingSlash(section.Node.Site.SiteUrl) + section.Id + "/section.aspx";
            }
            return null;
        }

        /// <summary>
        /// Returns a formatted url for a rss feed for a given section 
        /// (http://{hostname}/{ApplicationPath}/{Section.Id}/rss.aspx).
        /// </summary>
        /// <param name="section"></param>
        /// <returns></returns>
        public static string GetRssUrlFromSection(Section section)
        {
            return GetHostUrl() + GetApplicationPath() + section.Id + "/feed.aspx";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pathInfo"></param>
        /// <returns></returns>
        public static string[] GetParamsFromPathInfo(string pathInfo)
        {
            if (pathInfo.Length > 0)
            {
                if (pathInfo.EndsWith("/"))
                {
                    pathInfo = pathInfo.Substring(0, pathInfo.Length - 1);
                }
                pathInfo = pathInfo.Substring(1, pathInfo.Length - 1);
                return pathInfo.Split(new char[] {'/'});
            }
            return null;
        }

        /// <summary>
        /// Set the target window for the node.
        /// </summary>
        /// <param name="hpl"></param>
        /// <param name="node"></param>
        public static void SetHyperLinkTarget(HyperLink hpl, Node node)
        {
            if (node.IsExternalLink)
            {
                switch (node.LinkTarget)
                {
                    case LinkTarget.Self:
                        hpl.Target = "_self";
                        break;
                    case LinkTarget.New:
                        hpl.Target = "_blank";
                        break;
                }
            }
        }

        private static string GetHostUrl()
        {
            string securePort = HttpContext.Current.Request.ServerVariables["SERVER_PORT_SECURE"];
            string protocol = securePort == null || securePort == "0" ? "http" : "https";
            string serverPort = HttpContext.Current.Request.ServerVariables["SERVER_PORT"];
            string port = serverPort == "80" ? string.Empty : ":" + serverPort;
            string serverName = HttpContext.Current.Request.ServerVariables["SERVER_NAME"];
            return string.Format("{0}://{1}{2}", protocol, serverName, port);
        }

        public static string ConvertToUrl(string title)
        {
            // Substitute spaces
            string tempTitle = Regex.Replace(title.Trim(), "\\s", "-");
            // Remove illegal characters
            tempTitle = Regex.Replace(tempTitle, "[^A-Za-z0-9+-.]", "");
            return tempTitle.ToLower();
        }

    }

    public static class HyperLinkTarget
    {
        public const string Blank = "_blank";
        public const string Parent = "_parent";
        public const string Search = "_search";
        public const string Self = "_self";
        public const string Top = "_top";
    }
}