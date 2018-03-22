using System;
using System.IO;
using System.Web;
using System.Web.UI;
using CMS.Core;
using CMS.Core.Domain;
using CMS.Web.Util;

namespace CMS.Web.UI
{
    /// <summary>
    /// Base class for all module user controls.
    /// Credits to the DotNetNuke team (http://www.dotnetnuke.com) for the output caching idea!
    /// </summary>
    public class BaseModuleControl : LocalizedUserControl
    {
        private string _cachedOutput;
        private bool _displaySyndicationIcon;
        private ModuleBase _module;
        protected PageEngine _pageEngine;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public BaseModuleControl()
        {
            // Show the syndication icon by default. It can be set by subclasses.
            _displaySyndicationIcon = (this is ISyndicatable);
        }

        /// <summary>
        /// Indicator if there is cached content. The derived ModuleControls should determine whether to
        /// load content or not.
        /// </summary>
        protected bool HasCachedOutput
        {
            get { return _cachedOutput != null; }
        }

        /// <summary>
        /// Indicate if the module should display the syndication icon at its current state.
        /// </summary>
        protected bool DisplaySyndicationIcon
        {
            get { return _displaySyndicationIcon; }
            set { _displaySyndicationIcon = value; }
        }

        /// <summary>
        /// The PageEngine that serves as the context for the module controls.
        /// </summary>
        public PageEngine PageEngine
        {
            get { return _pageEngine; }
        }

        /// <summary>
        /// The accompanying ModuleBase business object. Use this property  to access
        /// module properties, sections and nodes from the code-behind of the module user controls.
        /// </summary>
        public ModuleBase Module
        {
            get { return _module; }
            set { _module = value; }
        }

        protected override void OnInit(EventArgs e)
        {
            if (Module.Section.CacheDuration > 0
                && Module.CacheKey != null
                && ! Page.User.Identity.IsAuthenticated
                && ! Page.IsPostBack)
            {
                // Get the cached content. Don't use cached output after a postback.
                if (HttpContext.Current.Cache[Module.CacheKey] != null && ! IsPostBack)
                {
                    // Found cached content.
                    _cachedOutput = HttpContext.Current.Cache[Module.CacheKey].ToString();
                }
            }
            if (Page is PageEngine)
            {
                _pageEngine = (PageEngine) Page;
            }
            base.OnInit(e);
        }


        /// <summary>
        /// Wrap the section content in a visual block.
        /// </summary>
        /// <param name="writer"></param>
        protected override void Render(HtmlTextWriter writer)
        {
            // Rss feed
            writer.Write("<div class=\"moduletools\">");
            if (_displaySyndicationIcon)
            {
                writer.Write(String.Format("<a href=\"{0}\"><img src=\"{1}\" alt=\"RSS-2.0\"/></a>",
                                           UrlHelper.GetRssUrlFromSection(_module.Section) + _module.ModulePathInfo,
                                           UrlHelper.GetApplicationPath() + "Images/feed-icon.png"));
            }
            // Edit button
            User cuyahogaUser = Page.User.Identity as User;
            if (cuyahogaUser != null)
            {
                if (_module.Section.ModuleType.EditPath != null
                    && cuyahogaUser.CanEdit(_module.Section))
                {
                    if (_module.Section.Node != null)
                    {
                        writer.Write(String.Format("&nbsp;[<a href=\"{0}?NodeId={1}&amp;SectionId={2}\">Edit</a>]"
                                                   ,
                                                   UrlHelper.GetApplicationPath() + _module.Section.ModuleType.EditPath
                                                   , _module.Section.Node.Id
                                                   , _module.Section.Id));
                    }
                    else
                    {
                        writer.Write(String.Format("&nbsp;[<a href=\"{0}?NodeId={1}&amp;SectionId={2}\">Edit</a>]"
                                                   ,
                                                   UrlHelper.GetApplicationPath() + _module.Section.ModuleType.EditPath
                                                   , PageEngine.ActiveNode.Id
                                                   , _module.Section.Id));
                    }
                }
                if (cuyahogaUser.HasPermission(AccessLevel.Administrator))
                {
                    if (_module.Section.Node != null)
                    {
                        writer.Write(
                            String.Format(
                                "&nbsp;[<a href=\"{0}Admin/SectionEdit.aspx?NodeId={1}&amp;SectionId={2}\">Section Properties</a>]"
                                , UrlHelper.GetApplicationPath()
                                , _module.Section.Node.Id
                                , _module.Section.Id));
                    }
                    else
                    {
                        writer.Write(
                            String.Format(
                                "&nbsp;[<a href=\"{0}Admin/SectionEdit.aspx?SectionId={1}\">Section Properties</a>]"
                                , UrlHelper.GetApplicationPath()
                                , _module.Section.Id));
                    }
                }
            }
            writer.Write("</div>");
            // TODO: get rid of this html hacking. Need a more declarative approach.
            writer.Write("<div class=\"section\">");
            // Section title
            if (_module.Section != null && _module.Section.ShowTitle)
            {
                writer.Write("<h3>" + _module.DisplayTitle + "</h3>");
            }

            // Write module content and handle caching when neccesary.
            // Don't cache when the user is logged in or after a postback.
            if (_module.Section.CacheDuration > 0
                && Module.CacheKey != null
                && ! Page.User.Identity.IsAuthenticated
                && ! Page.IsPostBack)
            {
                if (_cachedOutput == null)
                {
                    StringWriter tempWriter = new StringWriter();
                    base.Render(new HtmlTextWriter(tempWriter));
                    _cachedOutput = tempWriter.ToString();
                    HttpContext.Current.Cache.Insert(Module.CacheKey, _cachedOutput, null
                                                     , DateTime.Now.AddSeconds(_module.Section.CacheDuration),
                                                     TimeSpan.Zero);
                }
                // Output the user control's content.
                writer.Write(_cachedOutput);
            }
            else
            {
                base.Render(writer);
            }
            writer.Write("</div>");
        }

        /// <summary>
        /// Empty the output cache for the current module state.
        /// </summary>
        protected void InvalidateCache()
        {
            if (Module.CacheKey != null)
            {
                HttpContext.Current.Cache.Remove(Module.CacheKey);
            }
        }

        /// <summary>
        /// Register module-specific stylesheets.
        /// </summary>
        /// <param name="key">The unique key for the stylesheet. Note that Cuyahoga already uses 'maincss' as key.</param>
        /// <param name="absoluteCssPath">The path to the css file from the application root (starting with /).</param>
        protected void RegisterStylesheet(string key, string absoluteCssPath)
        {
            if (_pageEngine != null)
            {
                _pageEngine.RegisterStylesheet(key, absoluteCssPath);
            }
        }

        #region -- Set Focus --
        //protected override void OnLoad(EventArgs e)
        //{
        //    base.OnLoad(e);
        //    AddEnterPressedScriptBlock();            
        //}

        //private void AddEnterPressedScriptBlock()
        //{
        //    const string jsString = "<script language=javascript><!--function EnterPressed(){" +
        //                            "if((event.which && event.which==13)||(event.keyCode && event.keyCode==13))" +
        //                            "return true;" +
        //                            "return false;}" +
        //                            "--></script>";
        //    //Page.RegisterClientScriptBlock("EnterPressed", jsString);
        //    Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "EnterPressed", jsString);
        //}

        //protected void SetFocus(Control controlToFocus)
        //{
        //    string formName = GetFormName(controlToFocus);
        //    string jsString = "<script language=javascript>document." + formName + ".elements['" + controlToFocus.UniqueID + "'].focus();</script>";
        //    //if (Page.IsStartupScriptRegistered("SetFocusToSearch") == false)
        //    //    Page.RegisterStartupScript("SetFocusToSearch", jsString);
        //    if (!Page.ClientScript.IsStartupScriptRegistered("SetFocusToSearch"))
        //    {
        //        Page.ClientScript.RegisterStartupScript(Page.GetType(),"SetFocusToSearch", jsString);
        //    }
        //}

        //protected static void SetOnEnter(Control TextBoxToSet)
        //{
        //    //string formName = GetFormName(TextBoxToSet);
        //    string jsString = "if(EnterPressed()){" +
        //          "__doPostBack('" + TextBoxToSet.UniqueID + "','');}";
        //    if (TextBoxToSet is System.Web.UI.HtmlControls.HtmlControl)
        //        ((System.Web.UI.HtmlControls.HtmlControl)TextBoxToSet).Attributes.Add("onkeydown", jsString);
        //    else if (TextBoxToSet is System.Web.UI.WebControls.WebControl)
        //        ((System.Web.UI.WebControls.WebControl)TextBoxToSet).Attributes.Add("onkeydown", jsString);
        //    else
        //    { // We throw an exception if TextBoxToSet is not of type HtmlControl or WebControl.
        //        throw new ArgumentException("Control TextBoxToSet should be derived from either System.Web.UI.HtmlControls.HtmlControl or System.Web.UI.WebControls.WebControl", "TextBoxToSet");
        //    }
        //}

        //protected static void TieButton(Control TextBoxToTie, Control ButtonToTie)
        //{
        //    string formName = GetFormName(ButtonToTie);
        //    string jsString = "if(EnterPressed()){" +
        //          "document." + formName + ".elements['" + ButtonToTie.UniqueID + "'].click();}";
        //    if (TextBoxToTie is System.Web.UI.HtmlControls.HtmlControl)
        //        ((System.Web.UI.HtmlControls.HtmlControl)TextBoxToTie).Attributes.Add("onkeydown", jsString);
        //    else if (TextBoxToTie is System.Web.UI.WebControls.WebControl)
        //        ((System.Web.UI.WebControls.WebControl)TextBoxToTie).Attributes.Add("onkeydown", jsString);
        //    else
        //    { // We throw an exception if TextBoxToTie is not of type HtmlControl or WebControl.
        //        throw new ArgumentException("Control TextBoxToTie should be derived from either System.Web.UI.HtmlControls.HtmlControl or System.Web.UI.WebControls.WebControl", "TextBoxToTie");
        //    }
        //}

        //protected static string GetFormName(Control sourceControl)
        //{
        //    string formName;
        //    try
        //    {
        //        int i = 0;
        //        Control c = sourceControl.Parent;
        //        while (!(c is System.Web.UI.HtmlControls.HtmlForm) & !(c is Page) && i < 500)
        //        {
        //            c = c.Parent;
        //            i++;
        //        }
        //        if (c is System.Web.UI.HtmlControls.HtmlForm)
        //            formName = c.ClientID;
        //        else
        //            formName = "forms(0)";
        //    }
        //    catch { formName = "forms(0)"; }
        //    return formName;
        //}
        #endregion
    }
}