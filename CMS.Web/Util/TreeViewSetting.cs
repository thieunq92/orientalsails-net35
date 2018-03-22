using System.Web.UI;
using System.Web.UI.WebControls;
using CMS.Core.Domain;
using CMS.Web.Admin.UI;

namespace CMS.Web.Util
{
    public abstract class TreeViewSetting
    {
        protected ModuleBase _module;
        protected Page _page;

        public ModuleBase Module
        {
            get { return _module; }
            set { _module = value; }
        }

        public Page Page
        {
            set { _page = value; }
        }

        public abstract CustomTypeSettingControl GetTreeView(string Id, string value);

        public abstract bool ValidateSetting(string setting);
    }
}