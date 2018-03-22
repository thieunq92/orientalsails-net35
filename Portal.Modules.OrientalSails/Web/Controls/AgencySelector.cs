using System;
using System.Collections.Specialized;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMS.Core.Domain;
using CMS.ServerControls;
using CMS.Web.UI;
using Portal.Modules.OrientalSails.Domain;

namespace Portal.Modules.OrientalSails.Web.Controls
{
    public class AgencySelector : WebControl, IPostBackDataHandler, IModuleControl, ISelectorControl
    {
        public const string PID = "id";
        public const string PNAMEID = "nameid";
        protected Agency _agency;
        protected int _agencyId;
        protected SailsModule _module;

        protected string _nodeId;
        protected string _sectionId;

        public string NodeId
        {
            get { return _nodeId; }
            set { _nodeId = value; }
        }

        public string SectionId
        {
            get { return _sectionId; }
            set { _sectionId = value; }
        }

        public Agency SelectedAgency
        {
            get
            {
                if (_agencyId > 0)
                {
                    if (_agency == null)
                    {
                        _agency = _module.AgencyGetById(_agencyId);
                    }
                }
                return _agency;
            }
            set
            {
                if (value != null)
                {
                    _agency = value;
                    _agencyId = _agency.Id;
                }
                else
                {
                    _agencyId = 0;
                }
            }
        }

        #region Implementation of IPostBackDataHandler

        public virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
        {
            string postedId = postCollection[postDataKey];

            int presentValue = _agencyId;
            if (!presentValue.Equals(postedId))
            {
                _agencyId = Convert.ToInt32(postedId);
                return true;
            }

            return false;
        }

        public void RaisePostDataChangedEvent()
        {
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (Page is KitModuleAdminBasePage)
            {
                KitModuleAdminBasePage page = (KitModuleAdminBasePage) Page;
                _nodeId = page.Node.Id.ToString();
                _sectionId = page.Section.Id.ToString();
                Module = page.Module;
                Popup.RegisterStartupScript(page);
            }
        }

        protected override void Render(HtmlTextWriter output)
        {
            #region -- ID --

            output.AddAttribute(HtmlTextWriterAttribute.Type, "hidden");
            output.AddAttribute(HtmlTextWriterAttribute.Value, _agencyId.ToString());
            output.AddAttribute(HtmlTextWriterAttribute.Name, UniqueID);
            output.AddAttribute(HtmlTextWriterAttribute.Id, ClientID);
            output.RenderBeginTag(HtmlTextWriterTag.Input);
            output.RenderEndTag();

            #endregion

            string url =
                string.Format("/Modules/Sails/Admin/AgencySelectorPage.aspx?NodeId={0}&SectionId={1}&clientid={2}",
                              _nodeId,
                              _sectionId, ClientID);
            #region -- Button Select --

            if (Enabled)
            {
                output.AddAttribute(HtmlTextWriterAttribute.Type, "button");
                output.AddAttribute(HtmlTextWriterAttribute.Class, "button");
                output.AddAttribute(HtmlTextWriterAttribute.Value, "Select");
                output.AddAttribute(HtmlTextWriterAttribute.Onclick,
                                    Popup.OpenPopupScript(url, "Agency select", 600, 800));
                output.AddAttribute(HtmlTextWriterAttribute.Style, "margin-bottom:2px;margin-right:3px");
                output.RenderBeginTag(HtmlTextWriterTag.Input);
                output.RenderEndTag();
            }

            #endregion

            #region -- Name --

            output.AddAttribute(HtmlTextWriterAttribute.Id, ClientID + PNAMEID);
            output.AddAttribute(HtmlTextWriterAttribute.Type, "text");
            output.AddAttribute(HtmlTextWriterAttribute.Name, UniqueID + PNAMEID);
            output.AddAttribute(HtmlTextWriterAttribute.ReadOnly, "true");
            output.AddAttribute(HtmlTextWriterAttribute.Style, "width:206px");
            if (SelectedAgency != null)
            {
                output.AddAttribute(HtmlTextWriterAttribute.Value, SelectedAgency.Name);
                //output.Write(SelectedAgency.Name);
            }

            if (Enabled)
            {
                output.AddAttribute(HtmlTextWriterAttribute.Onclick,
                                    Popup.OpenPopupScript(url, "Agency select", 600, 1000));
            }
            output.RenderBeginTag(HtmlTextWriterTag.Input);
            output.RenderEndTag();

            #endregion

            
        }

        #endregion

        #region Implementation of IModuleControl

        public ModuleBase Module
        {
            set { _module = value as SailsModule; }
        }

        #endregion

        #region Implementation of ISelectorControl

        public object Selected
        {
            get
            {
                if (SelectedAgency != null)
                {
                    return SelectedAgency.Id;
                }
                return -1;
            }
            set
            {
                try
                {
                    SelectedAgency = _module.AgencyGetById((int) value);
                }
                catch
                {
                    SelectedAgency = null;
                }
            }
        }

        #endregion
    }
}